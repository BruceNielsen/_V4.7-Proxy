namespace PF.Utils.Security
{
    partial class Password_Reset
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Password_Reset));
            this.txt_Password_1 = new System.Windows.Forms.TextBox();
            this.lbl_Password_1 = new System.Windows.Forms.Label();
            this.txt_Password_2 = new System.Windows.Forms.TextBox();
            this.lbl_Password_2 = new System.Windows.Forms.Label();
            this.lbl_message = new System.Windows.Forms.Label();
            this.txt_Password_Old = new System.Windows.Forms.TextBox();
            this.lbl_Password_Old = new System.Windows.Forms.Label();
            this.btn_Update = new System.Windows.Forms.Button();
            this.btn_Reset = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_Password_1
            // 
            this.txt_Password_1.Location = new System.Drawing.Point(170, 105);
            this.txt_Password_1.Name = "txt_Password_1";
            this.txt_Password_1.PasswordChar = '*';
            this.txt_Password_1.Size = new System.Drawing.Size(193, 20);
            this.txt_Password_1.TabIndex = 2;
            // 
            // lbl_Password_1
            // 
            this.lbl_Password_1.AutoSize = true;
            this.lbl_Password_1.Location = new System.Drawing.Point(35, 108);
            this.lbl_Password_1.Name = "lbl_Password_1";
            this.lbl_Password_1.Size = new System.Drawing.Size(81, 13);
            this.lbl_Password_1.TabIndex = 4;
            this.lbl_Password_1.Text = "New Password:";
            // 
            // txt_Password_2
            // 
            this.txt_Password_2.Location = new System.Drawing.Point(170, 143);
            this.txt_Password_2.Name = "txt_Password_2";
            this.txt_Password_2.PasswordChar = '*';
            this.txt_Password_2.Size = new System.Drawing.Size(193, 20);
            this.txt_Password_2.TabIndex = 3;
            // 
            // lbl_Password_2
            // 
            this.lbl_Password_2.AutoSize = true;
            this.lbl_Password_2.Location = new System.Drawing.Point(35, 146);
            this.lbl_Password_2.Name = "lbl_Password_2";
            this.lbl_Password_2.Size = new System.Drawing.Size(129, 13);
            this.lbl_Password_2.TabIndex = 6;
            this.lbl_Password_2.Text = "Re-Enter New  Password:";
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(12, 9);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(0, 17);
            this.lbl_message.TabIndex = 8;
            // 
            // txt_Password_Old
            // 
            this.txt_Password_Old.Location = new System.Drawing.Point(170, 67);
            this.txt_Password_Old.Name = "txt_Password_Old";
            this.txt_Password_Old.PasswordChar = '*';
            this.txt_Password_Old.Size = new System.Drawing.Size(193, 20);
            this.txt_Password_Old.TabIndex = 1;
            // 
            // lbl_Password_Old
            // 
            this.lbl_Password_Old.AutoSize = true;
            this.lbl_Password_Old.Location = new System.Drawing.Point(35, 70);
            this.lbl_Password_Old.Name = "lbl_Password_Old";
            this.lbl_Password_Old.Size = new System.Drawing.Size(75, 13);
            this.lbl_Password_Old.TabIndex = 9;
            this.lbl_Password_Old.Text = "Old Password:";
            // 
            // btn_Update
            // 
            this.btn_Update.Location = new System.Drawing.Point(38, 196);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(75, 23);
            this.btn_Update.TabIndex = 4;
            this.btn_Update.Text = "&Update";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // btn_Reset
            // 
            this.btn_Reset.Location = new System.Drawing.Point(129, 196);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(75, 23);
            this.btn_Reset.TabIndex = 5;
            this.btn_Reset.Text = "&Reset";
            this.btn_Reset.UseVisualStyleBackColor = true;
            this.btn_Reset.Click += new System.EventHandler(this.btn_Reset_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(219, 196);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 6;
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // Password_Reset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 265);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_Reset);
            this.Controls.Add(this.btn_Update);
            this.Controls.Add(this.txt_Password_Old);
            this.Controls.Add(this.lbl_Password_Old);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.txt_Password_2);
            this.Controls.Add(this.lbl_Password_2);
            this.Controls.Add(this.txt_Password_1);
            this.Controls.Add(this.lbl_Password_1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Password_Reset";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PF.Utils.Security.Password Reset";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Password_Reset_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Password_1;
        private System.Windows.Forms.Label lbl_Password_1;
        private System.Windows.Forms.TextBox txt_Password_2;
        private System.Windows.Forms.Label lbl_Password_2;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.TextBox txt_Password_Old;
        private System.Windows.Forms.Label lbl_Password_Old;
        private System.Windows.Forms.Button btn_Update;
        private System.Windows.Forms.Button btn_Reset;
        private System.Windows.Forms.Button btn_Close;
    }
}