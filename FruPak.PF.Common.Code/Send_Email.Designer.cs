namespace FruPak.PF.Common.Code
{
    partial class Send_Email
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Send_Email));
            this.label7 = new System.Windows.Forms.Label();
            this.txt_Attachments = new System.Windows.Forms.TextBox();
            this.txt_Email_To = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_Email_Message = new System.Windows.Forms.RichTextBox();
            this.chk_email_copy = new System.Windows.Forms.CheckBox();
            this.lbl_message = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_Network_Pswd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Send = new System.Windows.Forms.Button();
            this.txt_Network_Id = new System.Windows.Forms.TextBox();
            this.txt_Email_Subject = new System.Windows.Forms.TextBox();
            this.txt_Email_From = new System.Windows.Forms.TextBox();
            this.checkBoxCcTo = new System.Windows.Forms.CheckBox();
            this.textBoxCcTo = new System.Windows.Forms.TextBox();
            this.buttonUpdateEmailToAddress = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 59;
            this.label7.Text = "Attachments:";
            // 
            // txt_Attachments
            // 
            this.txt_Attachments.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_Attachments.Location = new System.Drawing.Point(122, 110);
            this.txt_Attachments.Multiline = true;
            this.txt_Attachments.Name = "txt_Attachments";
            this.txt_Attachments.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_Attachments.Size = new System.Drawing.Size(425, 93);
            this.txt_Attachments.TabIndex = 2;
            this.txt_Attachments.Text = "12";
            // 
            // txt_Email_To
            // 
            this.txt_Email_To.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_Email_To.Location = new System.Drawing.Point(122, 47);
            this.txt_Email_To.Multiline = true;
            this.txt_Email_To.Name = "txt_Email_To";
            this.txt_Email_To.Size = new System.Drawing.Size(425, 57);
            this.txt_Email_To.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 56;
            this.label6.Text = "Email To:";
            // 
            // txt_Email_Message
            // 
            this.txt_Email_Message.Location = new System.Drawing.Point(122, 235);
            this.txt_Email_Message.Name = "txt_Email_Message";
            this.txt_Email_Message.Size = new System.Drawing.Size(425, 138);
            this.txt_Email_Message.TabIndex = 4;
            this.txt_Email_Message.Text = "";
            // 
            // chk_email_copy
            // 
            this.chk_email_copy.AutoSize = true;
            this.chk_email_copy.Checked = true;
            this.chk_email_copy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_email_copy.Location = new System.Drawing.Point(122, 460);
            this.chk_email_copy.Name = "chk_email_copy";
            this.chk_email_copy.Size = new System.Drawing.Size(99, 17);
            this.chk_email_copy.TabIndex = 8;
            this.chk_email_copy.Text = "Copy &to Sender";
            this.chk_email_copy.UseVisualStyleBackColor = true;
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(16, 9);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(0, 17);
            this.lbl_message.TabIndex = 49;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 437);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 13);
            this.label5.TabIndex = 48;
            this.label5.Text = "Network Password:";
            // 
            // txt_Network_Pswd
            // 
            this.txt_Network_Pswd.Location = new System.Drawing.Point(122, 434);
            this.txt_Network_Pswd.Name = "txt_Network_Pswd";
            this.txt_Network_Pswd.PasswordChar = '*';
            this.txt_Network_Pswd.Size = new System.Drawing.Size(239, 20);
            this.txt_Network_Pswd.TabIndex = 7;
            this.txt_Network_Pswd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            this.txt_Network_Pswd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Enter_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 411);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 46;
            this.label4.Text = "Network UserId:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 238);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "Email Message:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 212);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "Email Subject:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 385);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "From Email Address:";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(201, 493);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 10;
            this.btn_Cancel.Text = "&Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Send
            // 
            this.btn_Send.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Send.Location = new System.Drawing.Point(120, 493);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(75, 23);
            this.btn_Send.TabIndex = 9;
            this.btn_Send.Text = "&Send";
            this.btn_Send.UseVisualStyleBackColor = true;
            this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // txt_Network_Id
            // 
            this.txt_Network_Id.Location = new System.Drawing.Point(122, 408);
            this.txt_Network_Id.Name = "txt_Network_Id";
            this.txt_Network_Id.Size = new System.Drawing.Size(239, 20);
            this.txt_Network_Id.TabIndex = 6;
            this.txt_Network_Id.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // txt_Email_Subject
            // 
            this.txt_Email_Subject.Location = new System.Drawing.Point(122, 209);
            this.txt_Email_Subject.Name = "txt_Email_Subject";
            this.txt_Email_Subject.Size = new System.Drawing.Size(425, 20);
            this.txt_Email_Subject.TabIndex = 3;
            this.txt_Email_Subject.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // txt_Email_From
            // 
            this.txt_Email_From.Location = new System.Drawing.Point(122, 382);
            this.txt_Email_From.Name = "txt_Email_From";
            this.txt_Email_From.Size = new System.Drawing.Size(239, 20);
            this.txt_Email_From.TabIndex = 5;
            this.txt_Email_From.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // checkBoxCcTo
            // 
            this.checkBoxCcTo.AutoSize = true;
            this.checkBoxCcTo.Location = new System.Drawing.Point(314, 460);
            this.checkBoxCcTo.Name = "checkBoxCcTo";
            this.checkBoxCcTo.Size = new System.Drawing.Size(40, 17);
            this.checkBoxCcTo.TabIndex = 60;
            this.checkBoxCcTo.Text = "CC";
            this.checkBoxCcTo.UseVisualStyleBackColor = true;
            this.checkBoxCcTo.CheckedChanged += new System.EventHandler(this.checkBoxCcTo_CheckedChanged);
            // 
            // textBoxCcTo
            // 
            this.textBoxCcTo.Location = new System.Drawing.Point(360, 460);
            this.textBoxCcTo.Name = "textBoxCcTo";
            this.textBoxCcTo.Size = new System.Drawing.Size(187, 20);
            this.textBoxCcTo.TabIndex = 61;
            this.textBoxCcTo.Text = "phantom-dev@outlook.com";
            // 
            // buttonUpdateEmailToAddress
            // 
            this.buttonUpdateEmailToAddress.Location = new System.Drawing.Point(19, 66);
            this.buttonUpdateEmailToAddress.Name = "buttonUpdateEmailToAddress";
            this.buttonUpdateEmailToAddress.Size = new System.Drawing.Size(94, 38);
            this.buttonUpdateEmailToAddress.TabIndex = 62;
            this.buttonUpdateEmailToAddress.Text = "Update Address";
            this.buttonUpdateEmailToAddress.UseVisualStyleBackColor = true;
            this.buttonUpdateEmailToAddress.Click += new System.EventHandler(this.buttonUpdateEmailToAddress_Click);
            // 
            // Send_Email
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 539);
            this.Controls.Add(this.buttonUpdateEmailToAddress);
            this.Controls.Add(this.textBoxCcTo);
            this.Controls.Add(this.checkBoxCcTo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt_Attachments);
            this.Controls.Add(this.txt_Email_To);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_Email_Message);
            this.Controls.Add(this.chk_email_copy);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_Network_Pswd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Send);
            this.Controls.Add(this.txt_Network_Id);
            this.Controls.Add(this.txt_Email_Subject);
            this.Controls.Add(this.txt_Email_From);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Send_Email";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FruPak.PF.Common.Code.Send Email";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Invoice_email_FormClosing);
            this.Load += new System.EventHandler(this.Send_Email_Load);
            this.Shown += new System.EventHandler(this.Send_Email_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Send_Email_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Cancel;
        internal System.Windows.Forms.TextBox txt_Attachments;
        internal System.Windows.Forms.TextBox txt_Email_From;
        internal System.Windows.Forms.RichTextBox txt_Email_Message;
        internal System.Windows.Forms.CheckBox chk_email_copy;
        internal System.Windows.Forms.TextBox txt_Network_Pswd;
        internal System.Windows.Forms.Button btn_Send;
        internal System.Windows.Forms.TextBox txt_Network_Id;
        internal System.Windows.Forms.TextBox txt_Email_Subject;
        internal System.Windows.Forms.CheckBox checkBoxCcTo;
        public System.Windows.Forms.TextBox textBoxCcTo;
        private System.Windows.Forms.TextBox txt_Email_To;
        private System.Windows.Forms.Button buttonUpdateEmailToAddress;
    }
}