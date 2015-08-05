namespace FruPak.PF.Common.Code
{
    partial class SendDebugEmail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendDebugEmail));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.textBoxBugReport = new System.Windows.Forms.TextBox();
            this.pictureBoxBugReport = new System.Windows.Forms.PictureBox();
            this.labelBugReport = new System.Windows.Forms.Label();
            this.buttonTest = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBugReport)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(291, 166);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Enabled = false;
            this.buttonOk.Location = new System.Drawing.Point(372, 166);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "&Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // textBoxBugReport
            // 
            this.textBoxBugReport.Location = new System.Drawing.Point(15, 64);
            this.textBoxBugReport.Multiline = true;
            this.textBoxBugReport.Name = "textBoxBugReport";
            this.textBoxBugReport.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxBugReport.Size = new System.Drawing.Size(432, 96);
            this.textBoxBugReport.TabIndex = 1;
            this.textBoxBugReport.TextChanged += new System.EventHandler(this.textBoxBugReport_TextChanged);
            // 
            // pictureBoxBugReport
            // 
            this.pictureBoxBugReport.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxBugReport.Image")));
            this.pictureBoxBugReport.Location = new System.Drawing.Point(415, 9);
            this.pictureBoxBugReport.Name = "pictureBoxBugReport";
            this.pictureBoxBugReport.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxBugReport.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxBugReport.TabIndex = 5;
            this.pictureBoxBugReport.TabStop = false;
            // 
            // labelBugReport
            // 
            this.labelBugReport.Location = new System.Drawing.Point(12, 9);
            this.labelBugReport.Name = "labelBugReport";
            this.labelBugReport.Size = new System.Drawing.Size(397, 32);
            this.labelBugReport.TabIndex = 6;
            this.labelBugReport.Text = "You are about to send a bug report. Please enter a brief description of the probl" +
    "em. Thanks.";
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(96, 166);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(75, 23);
            this.buttonTest.TabIndex = 7;
            this.buttonTest.TabStop = false;
            this.buttonTest.Text = "Test";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Visible = false;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(15, 166);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 2;
            this.buttonClear.Text = "Clea&r";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // SendDebugEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 199);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.labelBugReport);
            this.Controls.Add(this.pictureBoxBugReport);
            this.Controls.Add(this.textBoxBugReport);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SendDebugEmail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Send Debug Email";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBugReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.TextBox textBoxBugReport;
        private System.Windows.Forms.PictureBox pictureBoxBugReport;
        private System.Windows.Forms.Label labelBugReport;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.Button buttonClear;
    }
}