namespace PF.Utils.Scanning
{
    partial class Reject_Bins_Weights
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
            this.barcode1 = new PF.Utils.UserControls.Barcode();
            this.btn_Peeler = new System.Windows.Forms.Button();
            this.btn_Small = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.lbl_message = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nud_Gross = new System.Windows.Forms.NumericUpDown();
            this.btn_Grower = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Gross)).BeginInit();
            this.SuspendLayout();
            // 
            // barcode1
            // 
            this.barcode1.BarcodeValue = "";
            this.barcode1.Location = new System.Drawing.Point(12, 58);
            this.barcode1.Name = "barcode1";
            this.barcode1.Set_Label = "Barcode:";
            this.barcode1.Size = new System.Drawing.Size(293, 33);
            this.barcode1.TabIndex = 1;
            // 
            // btn_Peeler
            // 
            this.btn_Peeler.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Peeler.Location = new System.Drawing.Point(122, 188);
            this.btn_Peeler.Name = "btn_Peeler";
            this.btn_Peeler.Size = new System.Drawing.Size(75, 50);
            this.btn_Peeler.TabIndex = 4;
            this.btn_Peeler.Text = "&Peeler";
            this.btn_Peeler.UseVisualStyleBackColor = true;
            this.btn_Peeler.Click += new System.EventHandler(this.btn_Peeler_Click);
            // 
            // btn_Small
            // 
            this.btn_Small.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Small.Location = new System.Drawing.Point(22, 188);
            this.btn_Small.Name = "btn_Small";
            this.btn_Small.Size = new System.Drawing.Size(75, 50);
            this.btn_Small.TabIndex = 3;
            this.btn_Small.Text = "&Smalls";
            this.btn_Small.UseVisualStyleBackColor = true;
            this.btn_Small.Click += new System.EventHandler(this.btn_Small_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(122, 244);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 50);
            this.btn_Close.TabIndex = 6;
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(9, 9);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(2, 22);
            this.lbl_message.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Gross Weight:";
            // 
            // nud_Gross
            // 
            this.nud_Gross.DecimalPlaces = 3;
            this.nud_Gross.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nud_Gross.Location = new System.Drawing.Point(134, 108);
            this.nud_Gross.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nud_Gross.Name = "nud_Gross";
            this.nud_Gross.Size = new System.Drawing.Size(120, 26);
            this.nud_Gross.TabIndex = 2;
            // 
            // btn_Grower
            // 
            this.btn_Grower.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Grower.Location = new System.Drawing.Point(22, 244);
            this.btn_Grower.Name = "btn_Grower";
            this.btn_Grower.Size = new System.Drawing.Size(75, 50);
            this.btn_Grower.TabIndex = 5;
            this.btn_Grower.Text = "&Grower";
            this.btn_Grower.UseVisualStyleBackColor = true;
            this.btn_Grower.Click += new System.EventHandler(this.btn_Grower_Click);
            // 
            // Reject_Bins_Weights
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 318);
            this.Controls.Add(this.btn_Grower);
            this.Controls.Add(this.nud_Gross);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_Small);
            this.Controls.Add(this.btn_Peeler);
            this.Controls.Add(this.barcode1);
            this.KeyPreview = true;
            this.Name = "Reject_Bins_Weights";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PF.Utils.Scanning.Reject_Bins_Weights";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Reject_Bins_Weights_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.nud_Gross)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControls.Barcode barcode1;
        private System.Windows.Forms.Button btn_Peeler;
        private System.Windows.Forms.Button btn_Small;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nud_Gross;
        private System.Windows.Forms.Button btn_Grower;
    }
}