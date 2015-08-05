namespace FruPak.PF.Utils.Security
{
    partial class Logon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Logon));
            this.txt_User_Id = new System.Windows.Forms.TextBox();
            this.lbl_User_Id = new System.Windows.Forms.Label();
            this.lbl_Password = new System.Windows.Forms.Label();
            this.txt_Password = new System.Windows.Forms.TextBox();
            this.btn_login = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_Change_Password = new System.Windows.Forms.Button();
            this.rb_DB_Production = new System.Windows.Forms.RadioButton();
            this.rb_DB_Test = new System.Windows.Forms.RadioButton();
            this.panelHackRobinJ = new System.Windows.Forms.Panel();
            this.panelHackSelkuckG = new System.Windows.Forms.Panel();
            this.panelHackGlenysP = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // txt_User_Id
            // 
            this.txt_User_Id.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_User_Id.Location = new System.Drawing.Point(109, 35);
            this.txt_User_Id.Name = "txt_User_Id";
            this.txt_User_Id.Size = new System.Drawing.Size(193, 26);
            this.txt_User_Id.TabIndex = 1;
            this.txt_User_Id.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_User_Id_KeyDown);
            this.txt_User_Id.Leave += new System.EventHandler(this.txt_User_Id_Leave);
            // 
            // lbl_User_Id
            // 
            this.lbl_User_Id.AutoSize = true;
            this.lbl_User_Id.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_User_Id.Location = new System.Drawing.Point(21, 38);
            this.lbl_User_Id.Name = "lbl_User_Id";
            this.lbl_User_Id.Size = new System.Drawing.Size(65, 20);
            this.lbl_User_Id.TabIndex = 1;
            this.lbl_User_Id.Text = "User Id:";
            // 
            // lbl_Password
            // 
            this.lbl_Password.AutoSize = true;
            this.lbl_Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Password.Location = new System.Drawing.Point(21, 84);
            this.lbl_Password.Name = "lbl_Password";
            this.lbl_Password.Size = new System.Drawing.Size(82, 20);
            this.lbl_Password.TabIndex = 2;
            this.lbl_Password.Text = "Password:";
            // 
            // txt_Password
            // 
            this.txt_Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Password.Location = new System.Drawing.Point(109, 82);
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.PasswordChar = '*';
            this.txt_Password.Size = new System.Drawing.Size(193, 26);
            this.txt_Password.TabIndex = 2;
            this.txt_Password.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_User_Id_KeyDown);
            this.txt_Password.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Enter_KeyPress);
            // 
            // btn_login
            // 
            this.btn_login.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_login.Location = new System.Drawing.Point(109, 169);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(75, 50);
            this.btn_login.TabIndex = 5;
            this.btn_login.Text = "&Login";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(227, 169);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 50);
            this.btn_Close.TabIndex = 6;
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_Change_Password
            // 
            this.btn_Change_Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Change_Password.Location = new System.Drawing.Point(325, 34);
            this.btn_Change_Password.Name = "btn_Change_Password";
            this.btn_Change_Password.Size = new System.Drawing.Size(178, 74);
            this.btn_Change_Password.TabIndex = 7;
            this.btn_Change_Password.Text = "Change Pass&word";
            this.btn_Change_Password.UseVisualStyleBackColor = true;
            this.btn_Change_Password.Click += new System.EventHandler(this.btn_Change_Password_Click);
            // 
            // rb_DB_Production
            // 
            this.rb_DB_Production.AutoSize = true;
            this.rb_DB_Production.Checked = true;
            this.rb_DB_Production.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_DB_Production.Location = new System.Drawing.Point(109, 128);
            this.rb_DB_Production.Name = "rb_DB_Production";
            this.rb_DB_Production.Size = new System.Drawing.Size(103, 24);
            this.rb_DB_Production.TabIndex = 3;
            this.rb_DB_Production.TabStop = true;
            this.rb_DB_Production.Text = "&Production";
            this.rb_DB_Production.UseVisualStyleBackColor = true;
            this.rb_DB_Production.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // rb_DB_Test
            // 
            this.rb_DB_Test.AutoSize = true;
            this.rb_DB_Test.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_DB_Test.Location = new System.Drawing.Point(244, 128);
            this.rb_DB_Test.Name = "rb_DB_Test";
            this.rb_DB_Test.Size = new System.Drawing.Size(58, 24);
            this.rb_DB_Test.TabIndex = 4;
            this.rb_DB_Test.Tag = "Test";
            this.rb_DB_Test.Text = "&Test";
            this.rb_DB_Test.UseVisualStyleBackColor = true;
            this.rb_DB_Test.Visible = false;
            this.rb_DB_Test.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // panelHackRobinJ
            // 
            this.panelHackRobinJ.Location = new System.Drawing.Point(434, 169);
            this.panelHackRobinJ.Name = "panelHackRobinJ";
            this.panelHackRobinJ.Size = new System.Drawing.Size(85, 74);
            this.panelHackRobinJ.TabIndex = 9;
            this.panelHackRobinJ.Click += new System.EventHandler(this.panelHack_Click);
            // 
            // panelHackSelkuckG
            // 
            this.panelHackSelkuckG.Location = new System.Drawing.Point(454, 5);
            this.panelHackSelkuckG.Name = "panelHackSelkuckG";
            this.panelHackSelkuckG.Size = new System.Drawing.Size(65, 23);
            this.panelHackSelkuckG.TabIndex = 10;
            this.panelHackSelkuckG.Click += new System.EventHandler(this.panelHackSelkuckG_Click);
            // 
            // panelHackGlenysP
            // 
            this.panelHackGlenysP.Location = new System.Drawing.Point(1, 4);
            this.panelHackGlenysP.Name = "panelHackGlenysP";
            this.panelHackGlenysP.Size = new System.Drawing.Size(56, 24);
            this.panelHackGlenysP.TabIndex = 11;
            this.panelHackGlenysP.Click += new System.EventHandler(this.panelHackGlenysP_Click);
            // 
            // Logon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Close;
            this.ClientSize = new System.Drawing.Size(513, 236);
            this.ControlBox = false;
            this.Controls.Add(this.panelHackGlenysP);
            this.Controls.Add(this.panelHackSelkuckG);
            this.Controls.Add(this.panelHackRobinJ);
            this.Controls.Add(this.rb_DB_Test);
            this.Controls.Add(this.rb_DB_Production);
            this.Controls.Add(this.btn_Change_Password);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.txt_Password);
            this.Controls.Add(this.lbl_Password);
            this.Controls.Add(this.lbl_User_Id);
            this.Controls.Add(this.txt_User_Id);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Logon";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FruPak.PF.Utils.Security.Logon";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Logon_FormClosing);
            this.Load += new System.EventHandler(this.Logon_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Logon_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_User_Id;
        private System.Windows.Forms.Label lbl_User_Id;
        private System.Windows.Forms.Label lbl_Password;
        private System.Windows.Forms.TextBox txt_Password;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_Change_Password;
        private System.Windows.Forms.RadioButton rb_DB_Production;
        private System.Windows.Forms.RadioButton rb_DB_Test;
        private System.Windows.Forms.Panel panelHackRobinJ;
        private System.Windows.Forms.Panel panelHackSelkuckG;
        private System.Windows.Forms.Panel panelHackGlenysP;
    }
}

