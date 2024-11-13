using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace Nhom6
{
    public partial class Form1 : Form
    {
        private static readonly string[] Scopes = { DriveService.Scope.Drive };
        private static readonly string ApplicationName = "btl-ltm";
        private UserCredential credential = null!;

        public Form1()
        {
            InitializeComponent();
            StartUpload();
        }

        private void StartUpload()
        {
            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                var clientSecrets = GoogleClientSecrets.FromStream(stream).Secrets;
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    clientSecrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                tbMess.Text = tbMess.Text + $"Credential file saved to: {credPath}" + "\r\n";
            }
        }

        private void ChooseFiles(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "All Files|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string text = null!;
                    // User selected file(s)
                    foreach (string filename in openFileDialog.FileNames)
                    {
                        text += filename + ";";
                    }
                    tbPath.Text = text.Remove(text.Length - 1);
                    tbPath.ForeColor = Color.Black;
                }
            }
        }

        private void ChooseFolder(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    // User selected a folder
                    tbPath.Text = folderBrowserDialog.SelectedPath;
                }
                tbPath.ForeColor = Color.Black;
            }
        }

        private string GetOrCreateFolderId(DriveService service, string folderName, string parentFolderId)
        {
            // Check if the folder already exists in the parent folder
            if (folderName == "")
                folderName = "root";
            var request = service.Files.List();
            request.Q = $"name='{folderName}' and '{parentFolderId}' in parents and mimeType='application/vnd.google-apps.folder'";
            var results = request.Execute().Files;

            if (results.Any())
            {
                // Folder already exists, return its ID
                return results.First().Id;
            }

            // Folder doesn't exist, create a new one
            var folderMetadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = folderName,
                MimeType = "application/vnd.google-apps.folder",
                // Parents = new List<string> { parentFolderId }
                Parents = parentFolderId != null ? new[] { parentFolderId } : null
            };

            try
            {
                var folderRequest = service.Files.Create(folderMetadata);
                folderRequest.Fields = "id";
                var folder = folderRequest.Execute();
                return folder.Id;
            }
            catch (System.Exception ex)
            {
                tbMess.Text = tbMess.Text + $"An error occurred: {ex.Message}" + "\r\n";
                return null!;
            }
        }

        private async void upLoadFileOrFolder(object sender, EventArgs e)
        {
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            string path = tbPath.Text;

            try
            {
                bool flag = false;
                if (Directory.Exists(path))
                {
                    flag = true;
                    // Upload all files in the selected folder
                    string parentFolderId = tbdes.Text == "" ? "root" : GetOrCreateFolderId(service, tbdes.Text, "root");
                    await Task.Run(() => UploadFolder(service, path, parentFolderId));
                }
                else
                {
                    string[] filepaths = path.Split(";");
                    var uploadTasks = new List<Task>();

                    foreach (var filepath in filepaths)
                    {
                        if (File.Exists(filepath))
                        {
                            // Upload a single file
                            flag = true;
                            string pathRoot = tbdes.Text == "" ? "root" : GetOrCreateFolderId(service, tbdes.Text, "root");
                            uploadTasks.Add(Task.Run(() => UploadFile(service, filepath, pathRoot, 0)));
                        }
                        else
                        {
                            tbMess.Text = tbMess.Text + "Invalid file path." + "\r\n";
                        }
                    }

                    // Wait for all file upload tasks to complete
                    await Task.WhenAll(uploadTasks);
                }

                if (!flag)
                {
                    tbMess.Text = tbMess.Text + "Invalid folder path." + "\r\n";
                }
            }
            catch (Exception ex)
            {
                tbMess.Text = tbMess.Text + $"An error occurred: {ex.Message}" + "\r\n";
            }
        }


        private async Task UploadFileAsync(DriveService service, string filePath, string folderId, int id)
        {
            await Task.Run(() =>
            {
                bool flag = false;
                string FileID;
                try
                {
                    string fileName = Path.GetFileName(filePath);
                    FileID = GetFileIdInFolder(service, fileName, folderId);

                    // Check if the file already exists in the target folder
                    if (FileID != null)
                    {
                        // Show a pop-up message with options
                        DialogResult result = MessageBox.Show(
                            $"File '{fileName}' already exists in the folder. Do you want to overwrite it?",
                            "File Exists",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);
                        // }
                        if (result == DialogResult.Yes)
                        {
                            flag = true;
                        }
                        else
                        {
                            id += 1;
                            if (id > 0)
                                fileName = fileName + " (" + id + ")";
                        }
                    }
                    if (flag)
                    {
                        service.Files.Delete(FileID).Execute();
                    }

                    var fileStream = new FileStream(filePath, FileMode.Open);
                    var fileMetadata = new Google.Apis.Drive.v3.Data.File
                    {
                        Name = fileName,
                        Parents = new List<string> { folderId }
                    };

                    var request = service.Files.Create(fileMetadata, fileStream, "application/octet-stream");
                    request.Upload();
                    fileStream.Close();
                    InvokeOnUiThread(() =>
                    {
                        tbMess.Text = tbMess.Text + $"{fileName} uploaded successfully." + "\r\n";
                        txt_id.Text = request.ResponseBody.Id.ToString();
                    });
                }
                catch (Exception ex)
                {
                    InvokeOnUiThread(() =>
                    {
                        tbMess.Text = tbMess.Text + $"An error occurred: {ex.Message}" + "\r\n";
                    });
                }
            });
        }

        // ...

        private void UploadFile(DriveService service, string filePath, string folderId, int id)
        {
            UploadFileAsync(service, filePath, folderId, id).Wait();
        }

        private string GetFileIdInFolder(DriveService service, string fileName, string folderId)
        {
            try
            {
                // Check if the file with the same name already exists in the target folder
                var request = service.Files.List();
                request.Q = $"name='{fileName}' and '{folderId}' in parents";
                var results = request.Execute().Files;

                if (results.Any())
                {
                    // File exists, return its ID
                    return results.First().Id;
                }
                else
                {
                    // File doesn't exist
                    return null!;
                }
            }
            catch (Exception ex)
            {
                tbMess.Text = tbMess.Text + $"Error checking file existence: {ex.Message}" + "\r\n";
                return null!;
            }
        }

        private async Task UploadFolderAsync(DriveService service, string localFolderPath, string parentFolderId)
        {
            await Task.Run(() =>
            {
                try
                {
                    string folderName = Path.GetFileName(localFolderPath);
                    string folderId = GetOrCreateFolderId(service, folderName, parentFolderId);

                    // Get all files in the selected folder
                    string[] filePaths = Directory.GetFiles(localFolderPath);
                    string[] folderPaths = Directory.GetDirectories(localFolderPath);

                    // Use Task.WhenAll to run the upload tasks concurrently
                    var uploadTasks = folderPaths.Select(folderPath => Task.Run(() => UploadFolder(service, folderPath, folderId)))
                        .Concat(filePaths.Select((filePath, index) => Task.Run(() => UploadFile(service, filePath, folderId, index))))
                        .ToArray();

                    Task.WaitAll(uploadTasks);

                    InvokeOnUiThread(() =>
                    {
                        tbMess.Text = tbMess.Text + $"Folder {localFolderPath} uploaded successfully." + "\r\n";
                    });
                }
                catch (Exception ex)
                {
                    InvokeOnUiThread(() =>
                    {
                        tbMess.Text = tbMess.Text + $"An error occurred: {ex.Message}" + "\r\n";
                    });
                }
            });
        }

        // ...

        private void UploadFolder(DriveService service, string localFolderPath, string parentFolderId)
        {
            UploadFolderAsync(service, localFolderPath, parentFolderId).Wait();
        }

        // ...

        private void InvokeOnUiThread(Action action)
        {
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }
        private void FormDragDrop(object sender, DragEventArgs e)
        {
            if (e.Data!.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void FormDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data!.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop)!;
                tbPath.Text = string.Join(";", files!);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tbdes_Enter(object sender, EventArgs e)
        {
            if (tbdes.Text == "Enter folder name destination")
            {
                tbdes.Text = "";
                tbdes.ForeColor = Color.Black;
            }
        }

        private void tbdes_Leave(object sender, EventArgs e)
        {
            if (tbdes.Text == "")
            {
                tbdes.Text = "Enter folder name destination";
                tbdes.ForeColor = Color.LightGray;
            }
        }

        private void tbPath_Enter(object sender, EventArgs e)
        {
            if (tbPath.Text == "Enter the path of the file or folder")
            {
                tbPath.Text = "";
                tbPath.ForeColor = Color.Black;
            }
        }

        private void tbPath_Leave(object sender, EventArgs e)
        {
            if (tbPath.Text == "")
            {
                tbPath.Text = "Enter the path of the file or folder";
                tbPath.ForeColor = Color.LightGray;
            }
        }

        private void tbPath_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn__Delete_click(object sender, EventArgs e)
        {
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            var req = service.Files.Delete(txt_id.Text);
            string res = req.Execute();
            if (res == null) MessageBox.Show(res);
            else
            {
                tbMess.Text = tbMess.Text + $"{tbPath.Text} Delete successfully." + "\r\n";
                txt_id.Text = "";
            }
        }

        private void tbdes_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txt_id_TextChanged(object sender, EventArgs e)
        {

        }

        private void Get_list_key(object sender, EventArgs e)
        {
            try
            {
                var service = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });
                string parentFolderId = tbdes.Text == "" ? "root" : GetOrCreateFolderId(service, tbdes.Text, "root");
                var fileList = service.Files.List();
                fileList.Q = $"mimeType!='application/vnd.google-apps.folder' and'{parentFolderId}' in parents";
                fileList.Fields = "nextPageToken,files(id,name,size,mimeType,createdTime)";
                var result = new List<Google.Apis.Drive.v3.Data.File>();
                string pageToken = null;
                do
                {
                    fileList.PageToken = pageToken;
                    var fileResult = fileList.Execute();
                    var files = fileResult.Files;
                    pageToken = fileResult.NextPageToken;
                    result.AddRange(files);
                } while (pageToken != null);

                foreach (var item in result)
                {
                    string fileDesc = item.Name + " " + item.Id + Environment.NewLine;
                    tbMess.Text += fileDesc;

                }
            }
            catch (Exception ex) { 
            }
            
        }

        private void tbMess_TextChanged(object sender, EventArgs e)
        {
 
        }
    }
}
