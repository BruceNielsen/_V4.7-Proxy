namespace FruPak.PF.Utils.Scanning
{
    partial class Reprint_Palletcards
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reprint_Palletcards));
            this.btn_Print = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.barcode1 = new FruPak.PF.Utils.UserControls.Barcode();
            this.rbn_PalletCard = new System.Windows.Forms.RadioButton();
            this.rbn_WPC = new System.Windows.Forms.RadioButton();
            this.lbl_message = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Print
            // 
            this.btn_Print.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Print.Location = new System.Drawing.Point(104, 202);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(75, 56);
            this.btn_Print.TabIndex = 4;
            this.btn_Print.Text = "&Print";
            this.btn_Print.UseVisualStyleBackColor = true;
            this.btn_Print.Click += new System.EventHandler(this.btn_Print_Click);
            // 
            // btn_close
            // 
            this.btn_close.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.Location = new System.Drawing.Point(235, 202);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 56);
            this.btn_close.TabIndex = 5;
            this.btn_close.Text = "&Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // barcode1
            // 
            this.barcode1.BarcodeValue = "";
            this.barcode1.Location = new System.Drawing.Point(22, 71);
            this.barcode1.Name = "barcode1";
            this.barcode1.Set_Label = "Barcode:";
            this.barcode1.Size = new System.Drawing.Size(293, 33);
            this.barcode1.TabIndex = 1;
            // 
            // rbn_PalletCard
            // 
            this.rbn_PalletCard.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbn_PalletCard.Checked = true;
            this.rbn_PalletCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbn_PalletCard.Location = new System.Drawing.Point(104, 123);
            this.rbn_PalletCard.Name = "rbn_PalletCard";
            this.rbn_PalletCard.Size = new System.Drawing.Size(86, 56);
            this.rbn_PalletCard.TabIndex = 2;
            this.rbn_PalletCard.TabStop = true;
            this.rbn_PalletCard.Text = "&Barcode";
            this.rbn_PalletCard.UseVisualStyleBackColor = true;
            // 
            // rbn_WPC
            // 
            this.rbn_WPC.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbn_WPC.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbn_WPC.Location = new System.Drawing.Point(235, 123);
            this.rbn_WPC.Name = "rbn_WPC";
            this.rbn_WPC.Size = new System.Drawing.Size(75, 56);
            this.rbn_WPC.TabIndex = 3;
            this.rbn_WPC.TabStop = true;
            this.rbn_WPC.Text = "Bin &Number";
            this.rbn_WPC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbn_WPC.UseVisualStyleBackColor = true;
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(12, 9);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(0, 20);
            this.lbl_message.TabIndex = 23;
            // 
            // Reprint_Palletcards
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 273);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.rbn_WPC);
            this.Controls.Add(this.rbn_PalletCard);
            this.Controls.Add(this.barcode1);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_Print);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Reprint_Palletcards";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FruPak.PF.Utils.Scanning.Reprint Palletcards";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Reprint_Palletcards_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Print;
        private System.Windows.Forms.Button btn_close;
        private UserControls.Barcode barcode1;
        private System.Windows.Forms.RadioButton rbn_PalletCard;
        private System.Windows.Forms.RadioButton rbn_WPC;
        private System.Windows.Forms.Label lbl_message;
    }
}