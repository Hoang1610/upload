


namespace Nhom10;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        tbPath = new TextBox();
        btFile = new Button();
        btFolder = new Button();
        btup = new Button();
        tbdes = new TextBox();
        tbMess = new TextBox();
        Delete = new Button();
        txt_id = new TextBox();
        label1 = new Label();
        label2 = new Label();
        button1 = new Button();
        label3 = new Label();
        SuspendLayout();
        // 
        // tbPath
        // 
        tbPath.AllowDrop = true;
        tbPath.ForeColor = SystemColors.ScrollBar;
        tbPath.Location = new Point(12, 30);
        tbPath.Name = "tbPath";
        tbPath.Size = new Size(414, 27);
        tbPath.TabIndex = 0;
        tbPath.Text = "Enter the path of the file or folder";
        tbPath.TextChanged += tbPath_TextChanged;
        tbPath.DragDrop += FormDragDrop;
        tbPath.DragEnter += FormDragEnter;
        tbPath.Enter += tbPath_Enter;
        tbPath.Leave += tbPath_Leave;
        // 
        // btFile
        // 
        btFile.Location = new Point(570, 26);
        btFile.Name = "btFile";
        btFile.Size = new Size(111, 35);
        btFile.TabIndex = 1;
        btFile.Text = "Choose Files";
        btFile.Click += ChooseFiles;
        // 
        // btFolder
        // 
        btFolder.Location = new Point(433, 26);
        btFolder.Name = "btFolder";
        btFolder.Size = new Size(130, 35);
        btFolder.TabIndex = 2;
        btFolder.Text = "Choose Folders";
        btFolder.Click += ChooseFolder;
        // 
        // btup
        // 
        btup.Location = new Point(650, 87);
        btup.Name = "btup";
        btup.Size = new Size(120, 35);
        btup.TabIndex = 3;
        btup.Text = "Upload";
        btup.Click += upLoadFileOrFolder;
        // 
        // tbdes
        // 
        tbdes.ForeColor = SystemColors.ScrollBar;
        tbdes.Location = new Point(116, 90);
        tbdes.Name = "tbdes";
        tbdes.Size = new Size(514, 27);
        tbdes.TabIndex = 4;
        tbdes.TextChanged += tbdes_TextChanged;
        tbdes.Enter += tbdes_Enter;
        tbdes.Leave += tbdes_Leave;
        // 
        // tbMess
        // 
        tbMess.Location = new Point(30, 192);
        tbMess.Multiline = true;
        tbMess.Name = "tbMess";
        tbMess.ReadOnly = true;
        tbMess.Size = new Size(740, 428);
        tbMess.TabIndex = 5;
        tbMess.TextChanged += tbMess_TextChanged;
        // 
        // Delete
        // 
        Delete.Location = new Point(689, 26);
        Delete.Name = "Delete";
        Delete.Size = new Size(102, 35);
        Delete.TabIndex = 6;
        Delete.Text = "Delete";
        Delete.UseVisualStyleBackColor = true;
        Delete.Click += btn__Delete_click;
        // 
        // txt_id
        // 
        txt_id.Location = new Point(116, 139);
        txt_id.Name = "txt_id";
        txt_id.Size = new Size(513, 27);
        txt_id.TabIndex = 7;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(706, 144);
        label1.Name = "label1";
        label1.Size = new Size(0, 20);
        label1.TabIndex = 8;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.BackColor = Color.Transparent;
        label2.Location = new Point(12, 141);
        label2.Name = "label2";
        label2.Size = new Size(65, 20);
        label2.TabIndex = 9;
        label2.Text = "File key :";
        label2.Click += label2_Click;
        // 
        // button1
        // 
        button1.Location = new Point(650, 133);
        button1.Name = "button1";
        button1.Size = new Size(120, 38);
        button1.TabIndex = 10;
        button1.Text = "Get List File";
        button1.UseVisualStyleBackColor = true;
        button1.Click += Get_list_key;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(12, 93);
        label3.Name = "label3";
        label3.Size = new Size(96, 20);
        label3.TabIndex = 11;
        label3.Text = "Enter Folder :";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 650);
        Controls.Add(label3);
        Controls.Add(button1);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(txt_id);
        Controls.Add(Delete);
        Controls.Add(tbPath);
        Controls.Add(btFile);
        Controls.Add(btFolder);
        Controls.Add(btup);
        Controls.Add(tbdes);
        Controls.Add(tbMess);
        Name = "Form1";
        Text = "Upload File To Drive";
        Load += Form1_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private Button btup, btFile, btFolder;
    private TextBox tbPath, tbMess, tbdes;
    private Button Delete;
    private TextBox txt_id;
    private Label label1;
    private Label label2;
    private Button button1;
    private Label label3;
}
