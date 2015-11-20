namespace PF.Utils.Security
{
    partial class User_Maintenance
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(User_Maintenance));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lbl_message = new System.Windows.Forms.Label();
            this.ckb_Active = new System.Windows.Forms.CheckBox();
            this.txt_Password = new System.Windows.Forms.TextBox();
            this.txt_Logon = new System.Windows.Forms.TextBox();
            this.txt_Last_Name = new System.Windows.Forms.TextBox();
            this.txt_First_Name = new System.Windows.Forms.TextBox();
            this.lbl_Password = new System.Windows.Forms.Label();
            this.lbl_Last_Name = new System.Windows.Forms.Label();
            this.lbl_First_Name = new System.Windows.Forms.Label();
            this.lbl_Logon = new System.Windows.Forms.Label();
            this.btn_Update = new System.Windows.Forms.Button();
            this.ckb_Show_Password = new System.Windows.Forms.CheckBox();
            this.btn_Reset = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.btn_Add_Members = new System.Windows.Forms.Button();
            this.ckb_allow_test = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(12, 47);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(678, 444);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(12, 9);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(0, 17);
            this.lbl_message.TabIndex = 1;
            // 
            // ckb_Active
            // 
            this.ckb_Active.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckb_Active.Checked = true;
            this.ckb_Active.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_Active.Location = new System.Drawing.Point(710, 168);
            this.ckb_Active.Name = "ckb_Active";
            this.ckb_Active.Size = new System.Drawing.Size(80, 16);
            this.ckb_Active.TabIndex = 7;
            this.ckb_Active.Text = "Active &Ind:";
            this.ckb_Active.UseVisualStyleBackColor = true;
            // 
            // txt_Password
            // 
            this.txt_Password.Location = new System.Drawing.Point(774, 132);
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.PasswordChar = '*';
            this.txt_Password.Size = new System.Drawing.Size(100, 20);
            this.txt_Password.TabIndex = 5;
            // 
            // txt_Logon
            // 
            this.txt_Logon.Location = new System.Drawing.Point(773, 105);
            this.txt_Logon.MaxLength = 20;
            this.txt_Logon.Name = "txt_Logon";
            this.txt_Logon.Size = new System.Drawing.Size(220, 20);
            this.txt_Logon.TabIndex = 4;
            // 
            // txt_Last_Name
            // 
            this.txt_Last_Name.Location = new System.Drawing.Point(774, 76);
            this.txt_Last_Name.MaxLength = 20;
            this.txt_Last_Name.Name = "txt_Last_Name";
            this.txt_Last_Name.Size = new System.Drawing.Size(219, 20);
            this.txt_Last_Name.TabIndex = 3;
            this.txt_Last_Name.TextChanged += new System.EventHandler(this.txt_Last_Name_TextChanged);
            // 
            // txt_First_Name
            // 
            this.txt_First_Name.Location = new System.Drawing.Point(774, 46);
            this.txt_First_Name.MaxLength = 20;
            this.txt_First_Name.Name = "txt_First_Name";
            this.txt_First_Name.Size = new System.Drawing.Size(219, 20);
            this.txt_First_Name.TabIndex = 2;
            this.txt_First_Name.TextChanged += new System.EventHandler(this.txt_First_Name_TextChanged);
            // 
            // lbl_Password
            // 
            this.lbl_Password.AutoSize = true;
            this.lbl_Password.Location = new System.Drawing.Point(707, 135);
            this.lbl_Password.Name = "lbl_Password";
            this.lbl_Password.Size = new System.Drawing.Size(56, 13);
            this.lbl_Password.TabIndex = 17;
            this.lbl_Password.Text = "Password:";
            // 
            // lbl_Last_Name
            // 
            this.lbl_Last_Name.AutoSize = true;
            this.lbl_Last_Name.Location = new System.Drawing.Point(707, 79);
            this.lbl_Last_Name.Name = "lbl_Last_Name";
            this.lbl_Last_Name.Size = new System.Drawing.Size(61, 13);
            this.lbl_Last_Name.TabIndex = 16;
            this.lbl_Last_Name.Text = "Last Name:";
            // 
            // lbl_First_Name
            // 
            this.lbl_First_Name.AutoSize = true;
            this.lbl_First_Name.Location = new System.Drawing.Point(707, 49);
            this.lbl_First_Name.Name = "lbl_First_Name";
            this.lbl_First_Name.Size = new System.Drawing.Size(60, 13);
            this.lbl_First_Name.TabIndex = 15;
            this.lbl_First_Name.Text = "First Name:";
            // 
            // lbl_Logon
            // 
            this.lbl_Logon.AutoSize = true;
            this.lbl_Logon.Location = new System.Drawing.Point(707, 108);
            this.lbl_Logon.Name = "lbl_Logon";
            this.lbl_Logon.Size = new System.Drawing.Size(43, 13);
            this.lbl_Logon.TabIndex = 14;
            this.lbl_Logon.Text = "UserID:";
            // 
            // btn_Update
            // 
            this.btn_Update.Location = new System.Drawing.Point(737, 203);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(75, 23);
            this.btn_Update.TabIndex = 9;
            this.btn_Update.Text = "&Add";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // ckb_Show_Password
            // 
            this.ckb_Show_Password.AutoSize = true;
            this.ckb_Show_Password.Location = new System.Drawing.Point(891, 135);
            this.ckb_Show_Password.Name = "ckb_Show_Password";
            this.ckb_Show_Password.Size = new System.Drawing.Size(102, 17);
            this.ckb_Show_Password.TabIndex = 6;
            this.ckb_Show_Password.Text = "&Show Password";
            this.ckb_Show_Password.UseVisualStyleBackColor = true;
            this.ckb_Show_Password.CheckedChanged += new System.EventHandler(this.ckb_Show_Password_Click);
            // 
            // btn_Reset
            // 
            this.btn_Reset.Location = new System.Drawing.Point(821, 203);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(75, 23);
            this.btn_Reset.TabIndex = 10;
            this.btn_Reset.Text = "&Reset";
            this.btn_Reset.UseVisualStyleBackColor = true;
            this.btn_Reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(902, 203);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 11;
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(715, 243);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(278, 184);
            this.checkedListBox1.TabIndex = 12;
            this.checkedListBox1.Visible = false;
            // 
            // btn_Add_Members
            // 
            this.btn_Add_Members.Location = new System.Drawing.Point(774, 447);
            this.btn_Add_Members.Name = "btn_Add_Members";
            this.btn_Add_Members.Size = new System.Drawing.Size(163, 23);
            this.btn_Add_Members.TabIndex = 13;
            this.btn_Add_Members.Text = "&Update Users in Group";
            this.btn_Add_Members.UseVisualStyleBackColor = true;
            this.btn_Add_Members.Visible = false;
            this.btn_Add_Members.Click += new System.EventHandler(this.btn_Add_Members_Click);
            // 
            // ckb_allow_test
            // 
            this.ckb_allow_test.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckb_allow_test.Location = new System.Drawing.Point(821, 168);
            this.ckb_allow_test.Name = "ckb_allow_test";
            this.ckb_allow_test.Size = new System.Drawing.Size(104, 16);
            this.ckb_allow_test.TabIndex = 8;
            this.ckb_allow_test.Text = "Access to &Test:";
            this.ckb_allow_test.UseVisualStyleBackColor = true;
            // 
            // User_Maintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 503);
            this.Controls.Add(this.ckb_allow_test);
            this.Controls.Add(this.btn_Add_Members);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_Reset);
            this.Controls.Add(this.ckb_Show_Password);
            this.Controls.Add(this.btn_Update);
            this.Controls.Add(this.ckb_Active);
            this.Controls.Add(this.txt_Password);
            this.Controls.Add(this.txt_Logon);
            this.Controls.Add(this.txt_Last_Name);
            this.Controls.Add(this.txt_First_Name);
            this.Controls.Add(this.lbl_Password);
            this.Controls.Add(this.lbl_Last_Name);
            this.Controls.Add(this.lbl_First_Name);
            this.Controls.Add(this.lbl_Logon);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "User_Maintenance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PF.Utils.Security.User Maintenance";
            this.Load += new System.EventHandler(this.SizeAllColumns);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.User_Maintenance_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.CheckBox ckb_Active;
        private System.Windows.Forms.TextBox txt_Password;
        private System.Windows.Forms.TextBox txt_Logon;
        private System.Windows.Forms.TextBox txt_Last_Name;
        private System.Windows.Forms.TextBox txt_First_Name;
        private System.Windows.Forms.Label lbl_Password;
        private System.Windows.Forms.Label lbl_Last_Name;
        private System.Windows.Forms.Label lbl_First_Name;
        private System.Windows.Forms.Label lbl_Logon;
        private System.Windows.Forms.Button btn_Update;
        private System.Windows.Forms.CheckBox ckb_Show_Password;
        private System.Windows.Forms.Button btn_Reset;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button btn_Add_Members;
        private System.Windows.Forms.CheckBox ckb_allow_test;
    }
}