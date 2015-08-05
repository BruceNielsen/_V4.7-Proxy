namespace FruPak.PF.Utils.Scanning
{
    partial class Pallet_Weight
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pallet_Weight));
            this.btn_Tipped = new System.Windows.Forms.Button();
            this.nud_Gross_Weight = new System.Windows.Forms.NumericUpDown();
            this.lbl_message = new System.Windows.Forms.Label();
            this.lbl_weight = new System.Windows.Forms.Label();
            this.nud_Tare_Weight = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Close = new System.Windows.Forms.Button();
            this.barcode1 = new FruPak.PF.Utils.UserControls.Barcode();
            this.rbn_WPC = new System.Windows.Forms.RadioButton();
            this.rbn_PalletCard = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Gross_Weight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Tare_Weight)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Tipped
            // 
            this.btn_Tipped.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Tipped.Location = new System.Drawing.Point(284, 197);
            this.btn_Tipped.Name = "btn_Tipped";
            this.btn_Tipped.Size = new System.Drawing.Size(82, 68);
            this.btn_Tipped.TabIndex = 6;
            this.btn_Tipped.Tag = "";
            this.btn_Tipped.Text = "&Update";
            this.btn_Tipped.UseVisualStyleBackColor = true;
            this.btn_Tipped.Click += new System.EventHandler(this.btn_Tipped_Click);
            // 
            // nud_Gross_Weight
            // 
            this.nud_Gross_Weight.DecimalPlaces = 3;
            this.nud_Gross_Weight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nud_Gross_Weight.Location = new System.Drawing.Point(142, 129);
            this.nud_Gross_Weight.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            196608});
            this.nud_Gross_Weight.Name = "nud_Gross_Weight";
            this.nud_Gross_Weight.Size = new System.Drawing.Size(96, 26);
            this.nud_Gross_Weight.TabIndex = 2;
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(18, 5);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(2, 22);
            this.lbl_message.TabIndex = 33;
            // 
            // lbl_weight
            // 
            this.lbl_weight.AutoSize = true;
            this.lbl_weight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_weight.Location = new System.Drawing.Point(26, 131);
            this.lbl_weight.Name = "lbl_weight";
            this.lbl_weight.Size = new System.Drawing.Size(110, 20);
            this.lbl_weight.TabIndex = 31;
            this.lbl_weight.Text = "Gross Weight:";
            // 
            // nud_Tare_Weight
            // 
            this.nud_Tare_Weight.DecimalPlaces = 3;
            this.nud_Tare_Weight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nud_Tare_Weight.Location = new System.Drawing.Point(142, 177);
            this.nud_Tare_Weight.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            196608});
            this.nud_Tare_Weight.Name = "nud_Tare_Weight";
            this.nud_Tare_Weight.Size = new System.Drawing.Size(96, 26);
            this.nud_Tare_Weight.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 20);
            this.label1.TabIndex = 36;
            this.label1.Text = "Tare Weight:";
            // 
            // btn_Close
            // 
            this.btn_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(406, 197);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(79, 68);
            this.btn_Close.TabIndex = 7;
            this.btn_Close.Tag = "";
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // barcode1
            // 
            this.barcode1.BarcodeValue = "";
            this.barcode1.Location = new System.Drawing.Point(30, 48);
            this.barcode1.Name = "barcode1";
            this.barcode1.Set_Label = "Barcode:";
            this.barcode1.Size = new System.Drawing.Size(293, 33);
            this.barcode1.TabIndex = 1;
            this.barcode1.txtChanged += new System.EventHandler(this.barcode1_txtChanged);
            // 
            // rbn_WPC
            // 
            this.rbn_WPC.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbn_WPC.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbn_WPC.Location = new System.Drawing.Point(406, 107);
            this.rbn_WPC.Name = "rbn_WPC";
            this.rbn_WPC.Size = new System.Drawing.Size(79, 68);
            this.rbn_WPC.TabIndex = 5;
            this.rbn_WPC.Text = "Bin &Number";
            this.rbn_WPC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbn_WPC.UseVisualStyleBackColor = true;
            // 
            // rbn_PalletCard
            // 
            this.rbn_PalletCard.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbn_PalletCard.Checked = true;
            this.rbn_PalletCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbn_PalletCard.Location = new System.Drawing.Point(284, 107);
            this.rbn_PalletCard.Name = "rbn_PalletCard";
            this.rbn_PalletCard.Size = new System.Drawing.Size(82, 68);
            this.rbn_PalletCard.TabIndex = 4;
            this.rbn_PalletCard.TabStop = true;
            this.rbn_PalletCard.Text = "&Barcode";
            this.rbn_PalletCard.UseVisualStyleBackColor = true;
            // 
            // Pallet_Weight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 281);
            this.Controls.Add(this.rbn_WPC);
            this.Controls.Add(this.rbn_PalletCard);
            this.Controls.Add(this.barcode1);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.nud_Tare_Weight);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Tipped);
            this.Controls.Add(this.nud_Gross_Weight);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.lbl_weight);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Pallet_Weight";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FruPak.PF.Utils.Scanning.Pallet Weight";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Pallet_Weight_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.nud_Gross_Weight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Tare_Weight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Tipped;
        private System.Windows.Forms.NumericUpDown nud_Gross_Weight;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.Label lbl_weight;
        private System.Windows.Forms.NumericUpDown nud_Tare_Weight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Close;
        private UserControls.Barcode barcode1;
        private System.Windows.Forms.RadioButton rbn_WPC;
        private System.Windows.Forms.RadioButton rbn_PalletCard;
    }
}