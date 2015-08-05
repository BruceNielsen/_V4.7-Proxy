namespace FruPak.PF.Accounts
{
    partial class Statement_Email
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Statement_Email));
            this.customer1 = new FruPak.PF.Utils.UserControls.Customer();
            this.btn_Continue = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // customer1
            // 
            this.customer1.CFocused = false;
            this.customer1.Customer_Id = 0;
            this.customer1.Location = new System.Drawing.Point(12, 37);
            this.customer1.Name = "customer1";
            this.customer1.Size = new System.Drawing.Size(296, 28);
            this.customer1.TabIndex = 1;
            // 
            // btn_Continue
            // 
            this.btn_Continue.Location = new System.Drawing.Point(72, 71);
            this.btn_Continue.Name = "btn_Continue";
            this.btn_Continue.Size = new System.Drawing.Size(75, 23);
            this.btn_Continue.TabIndex = 2;
            this.btn_Continue.Text = "&Continue";
            this.btn_Continue.UseVisualStyleBackColor = true;
            this.btn_Continue.Click += new System.EventHandler(this.btn_Continue_Click);
            // 
            // Statement_Email
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 361);
            this.Controls.Add(this.btn_Continue);
            this.Controls.Add(this.customer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Statement_Email";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FruPak.PF.Accounts.Statement Email";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Statement_Email_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private Utils.UserControls.Customer customer1;
        private System.Windows.Forms.Button btn_Continue;
    }
}