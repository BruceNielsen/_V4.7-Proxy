namespace FruPak.PF.Utils.Scanning
{
    partial class Bin_Tare_Weights
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bin_Tare_Weights));
            this.btn_Tipped = new System.Windows.Forms.Button();
            this.nud_weight = new System.Windows.Forms.NumericUpDown();
            this.lbl_message = new System.Windows.Forms.Label();
            this.lbl_weight = new System.Windows.Forms.Label();
            this.btn_Close = new System.Windows.Forms.Button();
            this.barcode1 = new FruPak.PF.Utils.UserControls.Barcode();
            ((System.ComponentModel.ISupportInitialize)(this.nud_weight)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Tipped
            // 
            this.btn_Tipped.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Tipped.Location = new System.Drawing.Point(25, 157);
            this.btn_Tipped.Name = "btn_Tipped";
            this.btn_Tipped.Size = new System.Drawing.Size(258, 46);
            this.btn_Tipped.TabIndex = 3;
            this.btn_Tipped.Tag = "";
            this.btn_Tipped.Text = "Update";
            this.btn_Tipped.UseVisualStyleBackColor = true;
            this.btn_Tipped.Click += new System.EventHandler(this.btn_Tipped_Click);
            // 
            // nud_weight
            // 
            this.nud_weight.DecimalPlaces = 3;
            this.nud_weight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nud_weight.Location = new System.Drawing.Point(147, 113);
            this.nud_weight.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            196608});
            this.nud_weight.Name = "nud_weight";
            this.nud_weight.Size = new System.Drawing.Size(137, 26);
            this.nud_weight.TabIndex = 2;
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(14, 5);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(2, 22);
            this.lbl_message.TabIndex = 26;
            // 
            // lbl_weight
            // 
            this.lbl_weight.AutoSize = true;
            this.lbl_weight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_weight.Location = new System.Drawing.Point(31, 113);
            this.lbl_weight.Name = "lbl_weight";
            this.lbl_weight.Size = new System.Drawing.Size(99, 20);
            this.lbl_weight.TabIndex = 24;
            this.lbl_weight.Text = "Tare Weight:";
            // 
            // btn_Close
            // 
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(26, 231);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(258, 46);
            this.btn_Close.TabIndex = 4;
            this.btn_Close.Tag = "";
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // barcode1
            // 
            this.barcode1.BarcodeValue = "";
            this.barcode1.Location = new System.Drawing.Point(14, 47);
            this.barcode1.Name = "barcode1";
            this.barcode1.Set_Label = "Barcode:";
            this.barcode1.Size = new System.Drawing.Size(293, 33);
            this.barcode1.TabIndex = 1;
            // 
            // Bin_Tare_Weights
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 318);
            this.Controls.Add(this.barcode1);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_Tipped);
            this.Controls.Add(this.nud_weight);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.lbl_weight);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Bin_Tare_Weights";
            this.Text = "FruPak.PF.Utils.Scanning.Bin Tare Weights";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Bin_Tare_Weights_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.nud_weight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Tipped;
        private System.Windows.Forms.NumericUpDown nud_weight;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.Label lbl_weight;
        private System.Windows.Forms.Button btn_Close;
        private UserControls.Barcode barcode1;
    }
}