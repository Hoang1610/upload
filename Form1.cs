using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace Nhom10
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

        }

        private void ChooseFiles(object sender, EventArgs e)
        {

        }

        private void ChooseFolder(object sender, EventArgs e)
        {

        }



        private async void upLoadFileOrFolder(object sender, EventArgs e)
        {

        }


        private void FormDragDrop(object sender, DragEventArgs e)
        {

        }

        private void FormDragEnter(object sender, DragEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tbdes_Enter(object sender, EventArgs e)
        {

        }

        private void tbdes_Leave(object sender, EventArgs e)
        {

        }

        private void tbPath_Enter(object sender, EventArgs e)
        {

        }

        private void tbPath_Leave(object sender, EventArgs e)
        {

        }

        private void tbPath_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn__Delete_click(object sender, EventArgs e)
        {

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


        }

        private void tbMess_TextChanged(object sender, EventArgs e)
        {

        }
    }
}