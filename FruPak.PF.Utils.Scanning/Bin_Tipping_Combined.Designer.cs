namespace FruPak.PF.Utils.Scanning
{
    partial class Bin_Tipping_Combined
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bin_Tipping_Combined));
            this.label_WorkOrder_BinTipping = new System.Windows.Forms.Label();
            this.nud_weight_BinTipping = new System.Windows.Forms.NumericUpDown();
            this.lbl_message_BinTipping = new System.Windows.Forms.Label();
            this.btn_Weight_BinTipping = new System.Windows.Forms.Button();
            this.lbl_weight_BinTipping = new System.Windows.Forms.Label();
            this.btn_Close_BinTipping = new System.Windows.Forms.Button();
            this.lbl_WO_Num = new System.Windows.Forms.Label();
            this.btn_Dry_BinTipping = new System.Windows.Forms.Button();
            this.btn_Wet_BinTipping = new System.Windows.Forms.Button();
            this.groupBoxBinTipping = new System.Windows.Forms.GroupBox();
            this.groupBoxBinTareWeights = new System.Windows.Forms.GroupBox();
            this.labelNotes = new System.Windows.Forms.Label();
            this.btn_Tipped_BinTareWeights = new System.Windows.Forms.Button();
            this.nud_weight_BinTareWeights = new System.Windows.Forms.NumericUpDown();
            this.lbl_message_BinTareWeights = new System.Windows.Forms.Label();
            this.lbl_weight_BinTareWeights = new System.Windows.Forms.Label();
            this.barcode_BinTareWeights = new FruPak.PF.Utils.UserControls.Barcode();
            this.barcode_BinTipping = new FruPak.PF.Utils.UserControls.Barcode();
            this.buttonCopyBinTareWeightsBarcode = new System.Windows.Forms.Button();
            this.buttonCopyBinTippingBarcode = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nud_weight_BinTipping)).BeginInit();
            this.groupBoxBinTipping.SuspendLayout();
            this.groupBoxBinTareWeights.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_weight_BinTareWeights)).BeginInit();
            this.SuspendLayout();
            // 
            // label_WorkOrder_BinTipping
            // 
            this.label_WorkOrder_BinTipping.AutoSize = true;
            this.label_WorkOrder_BinTipping.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_WorkOrder_BinTipping.Location = new System.Drawing.Point(12, 61);
            this.label_WorkOrder_BinTipping.Name = "label_WorkOrder_BinTipping";
            this.label_WorkOrder_BinTipping.Size = new System.Drawing.Size(94, 20);
            this.label_WorkOrder_BinTipping.TabIndex = 30;
            this.label_WorkOrder_BinTipping.Text = "Work Order:";
            // 
            // nud_weight_BinTipping
            // 
            this.nud_weight_BinTipping.DecimalPlaces = 3;
            this.nud_weight_BinTipping.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nud_weight_BinTipping.Location = new System.Drawing.Point(135, 148);
            this.nud_weight_BinTipping.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nud_weight_BinTipping.Name = "nud_weight_BinTipping";
            this.nud_weight_BinTipping.Size = new System.Drawing.Size(113, 26);
            this.nud_weight_BinTipping.TabIndex = 2;
            this.nud_weight_BinTipping.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_barcode_KeyDown);
            this.nud_weight_BinTipping.Leave += new System.EventHandler(this.nud_weight_Leave);
            // 
            // lbl_message_BinTipping
            // 
            this.lbl_message_BinTipping.AutoSize = true;
            this.lbl_message_BinTipping.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_message_BinTipping.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message_BinTipping.Location = new System.Drawing.Point(18, 23);
            this.lbl_message_BinTipping.Name = "lbl_message_BinTipping";
            this.lbl_message_BinTipping.Size = new System.Drawing.Size(2, 22);
            this.lbl_message_BinTipping.TabIndex = 26;
            // 
            // btn_Weight_BinTipping
            // 
            this.btn_Weight_BinTipping.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Weight_BinTipping.Location = new System.Drawing.Point(79, 260);
            this.btn_Weight_BinTipping.Name = "btn_Weight_BinTipping";
            this.btn_Weight_BinTipping.Size = new System.Drawing.Size(75, 46);
            this.btn_Weight_BinTipping.TabIndex = 5;
            this.btn_Weight_BinTipping.Tag = "                    lbl_message.Text = \"Invalid barcode\";";
            this.btn_Weight_BinTipping.Text = "Weight";
            this.btn_Weight_BinTipping.UseVisualStyleBackColor = true;
            this.btn_Weight_BinTipping.Click += new System.EventHandler(this.btn_Weight_Click);
            // 
            // lbl_weight_BinTipping
            // 
            this.lbl_weight_BinTipping.AutoSize = true;
            this.lbl_weight_BinTipping.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_weight_BinTipping.Location = new System.Drawing.Point(19, 150);
            this.lbl_weight_BinTipping.Name = "lbl_weight_BinTipping";
            this.lbl_weight_BinTipping.Size = new System.Drawing.Size(110, 20);
            this.lbl_weight_BinTipping.TabIndex = 24;
            this.lbl_weight_BinTipping.Text = "Gross Weight:";
            // 
            // btn_Close_BinTipping
            // 
            this.btn_Close_BinTipping.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close_BinTipping.Location = new System.Drawing.Point(173, 260);
            this.btn_Close_BinTipping.Name = "btn_Close_BinTipping";
            this.btn_Close_BinTipping.Size = new System.Drawing.Size(75, 46);
            this.btn_Close_BinTipping.TabIndex = 6;
            this.btn_Close_BinTipping.Tag = "";
            this.btn_Close_BinTipping.Text = "Close";
            this.btn_Close_BinTipping.UseVisualStyleBackColor = true;
            this.btn_Close_BinTipping.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // lbl_WO_Num
            // 
            this.lbl_WO_Num.AutoSize = true;
            this.lbl_WO_Num.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_WO_Num.Location = new System.Drawing.Point(113, 61);
            this.lbl_WO_Num.Name = "lbl_WO_Num";
            this.lbl_WO_Num.Size = new System.Drawing.Size(0, 20);
            this.lbl_WO_Num.TabIndex = 33;
            // 
            // btn_Dry_BinTipping
            // 
            this.btn_Dry_BinTipping.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Dry_BinTipping.Location = new System.Drawing.Point(173, 192);
            this.btn_Dry_BinTipping.Name = "btn_Dry_BinTipping";
            this.btn_Dry_BinTipping.Size = new System.Drawing.Size(75, 46);
            this.btn_Dry_BinTipping.TabIndex = 4;
            this.btn_Dry_BinTipping.Text = "Dry Tip";
            this.btn_Dry_BinTipping.UseVisualStyleBackColor = true;
            this.btn_Dry_BinTipping.Click += new System.EventHandler(this.btn_Wet_Dry_Click);
            // 
            // btn_Wet_BinTipping
            // 
            this.btn_Wet_BinTipping.FlatAppearance.BorderSize = 4;
            this.btn_Wet_BinTipping.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Wet_BinTipping.Location = new System.Drawing.Point(79, 192);
            this.btn_Wet_BinTipping.Name = "btn_Wet_BinTipping";
            this.btn_Wet_BinTipping.Size = new System.Drawing.Size(75, 46);
            this.btn_Wet_BinTipping.TabIndex = 3;
            this.btn_Wet_BinTipping.Text = "Wet Tip";
            this.btn_Wet_BinTipping.UseVisualStyleBackColor = true;
            this.btn_Wet_BinTipping.Click += new System.EventHandler(this.btn_Wet_Dry_Click);
            // 
            // groupBoxBinTipping
            // 
            this.groupBoxBinTipping.Controls.Add(this.buttonCopyBinTippingBarcode);
            this.groupBoxBinTipping.Controls.Add(this.barcode_BinTipping);
            this.groupBoxBinTipping.Controls.Add(this.lbl_weight_BinTipping);
            this.groupBoxBinTipping.Controls.Add(this.btn_Wet_BinTipping);
            this.groupBoxBinTipping.Controls.Add(this.btn_Weight_BinTipping);
            this.groupBoxBinTipping.Controls.Add(this.btn_Dry_BinTipping);
            this.groupBoxBinTipping.Controls.Add(this.lbl_message_BinTipping);
            this.groupBoxBinTipping.Controls.Add(this.lbl_WO_Num);
            this.groupBoxBinTipping.Controls.Add(this.nud_weight_BinTipping);
            this.groupBoxBinTipping.Controls.Add(this.btn_Close_BinTipping);
            this.groupBoxBinTipping.Controls.Add(this.label_WorkOrder_BinTipping);
            this.groupBoxBinTipping.Location = new System.Drawing.Point(12, 12);
            this.groupBoxBinTipping.Name = "groupBoxBinTipping";
            this.groupBoxBinTipping.Size = new System.Drawing.Size(358, 325);
            this.groupBoxBinTipping.TabIndex = 34;
            this.groupBoxBinTipping.TabStop = false;
            this.groupBoxBinTipping.Text = "Bin Tipping";
            // 
            // groupBoxBinTareWeights
            // 
            this.groupBoxBinTareWeights.Controls.Add(this.buttonCopyBinTareWeightsBarcode);
            this.groupBoxBinTareWeights.Controls.Add(this.labelNotes);
            this.groupBoxBinTareWeights.Controls.Add(this.barcode_BinTareWeights);
            this.groupBoxBinTareWeights.Controls.Add(this.btn_Tipped_BinTareWeights);
            this.groupBoxBinTareWeights.Controls.Add(this.nud_weight_BinTareWeights);
            this.groupBoxBinTareWeights.Controls.Add(this.lbl_message_BinTareWeights);
            this.groupBoxBinTareWeights.Controls.Add(this.lbl_weight_BinTareWeights);
            this.groupBoxBinTareWeights.Location = new System.Drawing.Point(376, 12);
            this.groupBoxBinTareWeights.Name = "groupBoxBinTareWeights";
            this.groupBoxBinTareWeights.Size = new System.Drawing.Size(385, 325);
            this.groupBoxBinTareWeights.TabIndex = 35;
            this.groupBoxBinTareWeights.TabStop = false;
            this.groupBoxBinTareWeights.Text = "Bin Tare Weights";
            // 
            // labelNotes
            // 
            this.labelNotes.Location = new System.Drawing.Point(57, 260);
            this.labelNotes.Name = "labelNotes";
            this.labelNotes.Size = new System.Drawing.Size(241, 46);
            this.labelNotes.TabIndex = 33;
            this.labelNotes.Text = "Note: The <- and -> buttons will copy the barcode from one field to another in th" +
    "e direction indicated, as long as the barcode is not empty.";
            // 
            // btn_Tipped_BinTareWeights
            // 
            this.btn_Tipped_BinTareWeights.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Tipped_BinTareWeights.Location = new System.Drawing.Point(60, 192);
            this.btn_Tipped_BinTareWeights.Name = "btn_Tipped_BinTareWeights";
            this.btn_Tipped_BinTareWeights.Size = new System.Drawing.Size(238, 46);
            this.btn_Tipped_BinTareWeights.TabIndex = 29;
            this.btn_Tipped_BinTareWeights.Tag = "";
            this.btn_Tipped_BinTareWeights.Text = "Update";
            this.btn_Tipped_BinTareWeights.UseVisualStyleBackColor = true;
            this.btn_Tipped_BinTareWeights.Click += new System.EventHandler(this.btn_Tipped_Click);
            // 
            // nud_weight_BinTareWeights
            // 
            this.nud_weight_BinTareWeights.DecimalPlaces = 3;
            this.nud_weight_BinTareWeights.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nud_weight_BinTareWeights.Location = new System.Drawing.Point(161, 150);
            this.nud_weight_BinTareWeights.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            196608});
            this.nud_weight_BinTareWeights.Name = "nud_weight_BinTareWeights";
            this.nud_weight_BinTareWeights.Size = new System.Drawing.Size(137, 26);
            this.nud_weight_BinTareWeights.TabIndex = 28;
            this.nud_weight_BinTareWeights.Leave += new System.EventHandler(this.nud_weight_Leave_BinTareWeights);
            // 
            // lbl_message_BinTareWeights
            // 
            this.lbl_message_BinTareWeights.AutoSize = true;
            this.lbl_message_BinTareWeights.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_message_BinTareWeights.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message_BinTareWeights.Location = new System.Drawing.Point(50, 23);
            this.lbl_message_BinTareWeights.Name = "lbl_message_BinTareWeights";
            this.lbl_message_BinTareWeights.Size = new System.Drawing.Size(2, 22);
            this.lbl_message_BinTareWeights.TabIndex = 32;
            // 
            // lbl_weight_BinTareWeights
            // 
            this.lbl_weight_BinTareWeights.AutoSize = true;
            this.lbl_weight_BinTareWeights.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_weight_BinTareWeights.Location = new System.Drawing.Point(56, 150);
            this.lbl_weight_BinTareWeights.Name = "lbl_weight_BinTareWeights";
            this.lbl_weight_BinTareWeights.Size = new System.Drawing.Size(99, 20);
            this.lbl_weight_BinTareWeights.TabIndex = 31;
            this.lbl_weight_BinTareWeights.Text = "Tare Weight:";
            // 
            // barcode_BinTareWeights
            // 
            this.barcode_BinTareWeights.BarcodeValue = "";
            this.barcode_BinTareWeights.Location = new System.Drawing.Point(50, 93);
            this.barcode_BinTareWeights.Name = "barcode_BinTareWeights";
            this.barcode_BinTareWeights.Set_Label = "Barcode:";
            this.barcode_BinTareWeights.Size = new System.Drawing.Size(293, 33);
            this.barcode_BinTareWeights.TabIndex = 27;
            this.barcode_BinTareWeights.txtChanged += new System.EventHandler(this.barcode_BinTareWeights_txtChanged);
            // 
            // barcode_BinTipping
            // 
            this.barcode_BinTipping.BarcodeValue = "";
            this.barcode_BinTipping.Location = new System.Drawing.Point(16, 93);
            this.barcode_BinTipping.Name = "barcode_BinTipping";
            this.barcode_BinTipping.Set_Label = "Barcode:";
            this.barcode_BinTipping.Size = new System.Drawing.Size(293, 33);
            this.barcode_BinTipping.TabIndex = 1;
            this.barcode_BinTipping.txtChanged += new System.EventHandler(this.barcode1_txtChanged);
            this.barcode_BinTipping.Validating += new System.ComponentModel.CancelEventHandler(this.txt_barcode_Validating);
            // 
            // buttonCopyBinTareWeightsBarcode
            // 
            this.buttonCopyBinTareWeightsBarcode.Location = new System.Drawing.Point(6, 93);
            this.buttonCopyBinTareWeightsBarcode.Name = "buttonCopyBinTareWeightsBarcode";
            this.buttonCopyBinTareWeightsBarcode.Size = new System.Drawing.Size(35, 32);
            this.buttonCopyBinTareWeightsBarcode.TabIndex = 34;
            this.buttonCopyBinTareWeightsBarcode.Text = "<-";
            this.buttonCopyBinTareWeightsBarcode.UseVisualStyleBackColor = true;
            this.buttonCopyBinTareWeightsBarcode.Click += new System.EventHandler(this.buttonCopyBinTareWeightsBarcode_Click);
            // 
            // buttonCopyBinTippingBarcode
            // 
            this.buttonCopyBinTippingBarcode.Location = new System.Drawing.Point(315, 93);
            this.buttonCopyBinTippingBarcode.Name = "buttonCopyBinTippingBarcode";
            this.buttonCopyBinTippingBarcode.Size = new System.Drawing.Size(35, 32);
            this.buttonCopyBinTippingBarcode.TabIndex = 35;
            this.buttonCopyBinTippingBarcode.Text = "->";
            this.buttonCopyBinTippingBarcode.UseVisualStyleBackColor = true;
            this.buttonCopyBinTippingBarcode.Click += new System.EventHandler(this.buttonCopyBinTippingBarcode_Click);
            // 
            // Bin_Tipping_Combined
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(771, 348);
            this.Controls.Add(this.groupBoxBinTareWeights);
            this.Controls.Add(this.groupBoxBinTipping);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Bin_Tipping_Combined";
            this.Text = "FruPak.PF.Utils.Scanning.Bin_Tipping_Combined";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Bin_Tipping_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.nud_weight_BinTipping)).EndInit();
            this.groupBoxBinTipping.ResumeLayout(false);
            this.groupBoxBinTipping.PerformLayout();
            this.groupBoxBinTareWeights.ResumeLayout(false);
            this.groupBoxBinTareWeights.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_weight_BinTareWeights)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_WorkOrder_BinTipping;
        private System.Windows.Forms.NumericUpDown nud_weight_BinTipping;
        private System.Windows.Forms.Label lbl_message_BinTipping;
        private System.Windows.Forms.Button btn_Weight_BinTipping;
        private System.Windows.Forms.Label lbl_weight_BinTipping;
        private System.Windows.Forms.Button btn_Close_BinTipping;
        private System.Windows.Forms.Label lbl_WO_Num;
        private System.Windows.Forms.Button btn_Dry_BinTipping;
        private System.Windows.Forms.Button btn_Wet_BinTipping;
        private UserControls.Barcode barcode_BinTipping;
        private System.Windows.Forms.GroupBox groupBoxBinTipping;
        private System.Windows.Forms.GroupBox groupBoxBinTareWeights;
        private UserControls.Barcode barcode_BinTareWeights;
        private System.Windows.Forms.Button btn_Tipped_BinTareWeights;
        private System.Windows.Forms.NumericUpDown nud_weight_BinTareWeights;
        private System.Windows.Forms.Label lbl_message_BinTareWeights;
        private System.Windows.Forms.Label lbl_weight_BinTareWeights;
        private System.Windows.Forms.Label labelNotes;
        private System.Windows.Forms.Button buttonCopyBinTippingBarcode;
        private System.Windows.Forms.Button buttonCopyBinTareWeightsBarcode;
    }
}