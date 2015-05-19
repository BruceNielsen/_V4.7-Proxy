namespace FruPak.PF.Utils.Scanning
{
    partial class Create_Small_Rejects
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Create_Small_Rejects));
            this.nud_required = new System.Windows.Forms.NumericUpDown();
            this.lbl_required = new System.Windows.Forms.Label();
            this.btn_Print = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_reprint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nud_required)).BeginInit();
            this.SuspendLayout();
            // 
            // nud_required
            // 
            this.nud_required.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nud_required.Location = new System.Drawing.Point(199, 50);
            this.nud_required.Name = "nud_required";
            this.nud_required.Size = new System.Drawing.Size(76, 26);
            this.nud_required.TabIndex = 1;
            // 
            // lbl_required
            // 
            this.lbl_required.AutoSize = true;
            this.lbl_required.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_required.Location = new System.Drawing.Point(12, 52);
            this.lbl_required.Name = "lbl_required";
            this.lbl_required.Size = new System.Drawing.Size(181, 20);
            this.lbl_required.TabIndex = 1;
            this.lbl_required.Text = "Enter Number Required:";
            // 
            // btn_Print
            // 
            this.btn_Print.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Print.Location = new System.Drawing.Point(16, 98);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(75, 52);
            this.btn_Print.TabIndex = 2;
            this.btn_Print.Text = "Print";
            this.btn_Print.UseVisualStyleBackColor = true;
            this.btn_Print.Click += new System.EventHandler(this.btn_Print_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(200, 98);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 52);
            this.btn_Close.TabIndex = 4;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_reprint
            // 
            this.btn_reprint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_reprint.Location = new System.Drawing.Point(107, 98);
            this.btn_reprint.Name = "btn_reprint";
            this.btn_reprint.Size = new System.Drawing.Size(75, 52);
            this.btn_reprint.TabIndex = 3;
            this.btn_reprint.Text = "RePrint";
            this.btn_reprint.UseVisualStyleBackColor = true;
            this.btn_reprint.Click += new System.EventHandler(this.btn_reprint_Click);
            // 
            // Create_Small_Rejects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 318);
            this.Controls.Add(this.btn_reprint);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_Print);
            this.Controls.Add(this.lbl_required);
            this.Controls.Add(this.nud_required);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Create_Small_Rejects";
            this.Text = "FruPak.PF.Utils.Scanning.Create Small Rejects";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Create_Small_Rejects_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.nud_required)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nud_required;
        private System.Windows.Forms.Label lbl_required;
        private System.Windows.Forms.Button btn_Print;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_reprint;
    }
}