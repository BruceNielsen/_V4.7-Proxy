namespace FruPak.PF.Utils.Scanning
{
    partial class GDI_Scanning
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GDI_Scanning));
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Dry = new System.Windows.Forms.Button();
            this.nud_weight = new System.Windows.Forms.NumericUpDown();
            this.lbl_message = new System.Windows.Forms.Label();
            this.txt_barcode = new System.Windows.Forms.TextBox();
            this.lbl_weight = new System.Windows.Forms.Label();
            this.lbl_barcode = new System.Windows.Forms.Label();
            this.lbl_sub_Id = new System.Windows.Forms.Label();
            this.btn_Weight = new System.Windows.Forms.Button();
            this.cmb_sub_Id = new System.Windows.Forms.ComboBox();
            this.btn_Close = new System.Windows.Forms.Button();
            this.lbl_WO_Num = new System.Windows.Forms.Label();
            this.btn_Wet = new System.Windows.Forms.Button();
            this.rbn_Bin = new System.Windows.Forms.RadioButton();
            this.rbn_Overrun = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.nud_weight)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 20);
            this.label1.TabIndex = 30;
            this.label1.Text = "Work Order:";
            // 
            // btn_Dry
            // 
            this.btn_Dry.Location = new System.Drawing.Point(454, 77);
            this.btn_Dry.Name = "btn_Dry";
            this.btn_Dry.Size = new System.Drawing.Size(79, 55);
            this.btn_Dry.TabIndex = 7;
            this.btn_Dry.Tag = "";
            this.btn_Dry.Text = "Dry Tip";
            this.btn_Dry.UseVisualStyleBackColor = true;
            this.btn_Dry.Click += new System.EventHandler(this.btn_Tipped_Click);
            // 
            // nud_weight
            // 
            this.nud_weight.DecimalPlaces = 3;
            this.nud_weight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nud_weight.Location = new System.Drawing.Point(142, 234);
            this.nud_weight.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nud_weight.Name = "nud_weight";
            this.nud_weight.Size = new System.Drawing.Size(77, 26);
            this.nud_weight.TabIndex = 5;
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(15, 8);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(2, 22);
            this.lbl_message.TabIndex = 26;
            // 
            // txt_barcode
            // 
            this.txt_barcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_barcode.Location = new System.Drawing.Point(113, 168);
            this.txt_barcode.Name = "txt_barcode";
            this.txt_barcode.Size = new System.Drawing.Size(205, 26);
            this.txt_barcode.TabIndex = 3;
            this.txt_barcode.TextChanged += new System.EventHandler(this.txt_barcode_TextChanged);
            this.txt_barcode.Validating += new System.ComponentModel.CancelEventHandler(this.txt_barcode_Validating);
            // 
            // lbl_weight
            // 
            this.lbl_weight.AutoSize = true;
            this.lbl_weight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_weight.Location = new System.Drawing.Point(63, 236);
            this.lbl_weight.Name = "lbl_weight";
            this.lbl_weight.Size = new System.Drawing.Size(63, 20);
            this.lbl_weight.TabIndex = 24;
            this.lbl_weight.Text = "Weight:";
            // 
            // lbl_barcode
            // 
            this.lbl_barcode.AutoSize = true;
            this.lbl_barcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_barcode.Location = new System.Drawing.Point(18, 171);
            this.lbl_barcode.Name = "lbl_barcode";
            this.lbl_barcode.Size = new System.Drawing.Size(73, 20);
            this.lbl_barcode.TabIndex = 23;
            this.lbl_barcode.Text = "Barcode:";
            // 
            // lbl_sub_Id
            // 
            this.lbl_sub_Id.AutoSize = true;
            this.lbl_sub_Id.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_sub_Id.Location = new System.Drawing.Point(22, 203);
            this.lbl_sub_Id.Name = "lbl_sub_Id";
            this.lbl_sub_Id.Size = new System.Drawing.Size(114, 20);
            this.lbl_sub_Id.TabIndex = 32;
            this.lbl_sub_Id.Text = "Submission_Id";
            // 
            // btn_Weight
            // 
            this.btn_Weight.Location = new System.Drawing.Point(353, 152);
            this.btn_Weight.Name = "btn_Weight";
            this.btn_Weight.Size = new System.Drawing.Size(79, 55);
            this.btn_Weight.TabIndex = 8;
            this.btn_Weight.Tag = "                    lbl_message.Text = \"Invalid barcode\";";
            this.btn_Weight.Text = "Weight";
            this.btn_Weight.UseVisualStyleBackColor = true;
            this.btn_Weight.Click += new System.EventHandler(this.btn_Weight_Click);
            // 
            // cmb_sub_Id
            // 
            this.cmb_sub_Id.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_sub_Id.FormattingEnabled = true;
            this.cmb_sub_Id.ItemHeight = 20;
            this.cmb_sub_Id.Location = new System.Drawing.Point(142, 200);
            this.cmb_sub_Id.Name = "cmb_sub_Id";
            this.cmb_sub_Id.Size = new System.Drawing.Size(104, 28);
            this.cmb_sub_Id.TabIndex = 4;
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(454, 152);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(79, 55);
            this.btn_Close.TabIndex = 9;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // lbl_WO_Num
            // 
            this.lbl_WO_Num.AutoSize = true;
            this.lbl_WO_Num.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_WO_Num.Location = new System.Drawing.Point(109, 46);
            this.lbl_WO_Num.Name = "lbl_WO_Num";
            this.lbl_WO_Num.Size = new System.Drawing.Size(0, 20);
            this.lbl_WO_Num.TabIndex = 35;
            // 
            // btn_Wet
            // 
            this.btn_Wet.Location = new System.Drawing.Point(369, 77);
            this.btn_Wet.Name = "btn_Wet";
            this.btn_Wet.Size = new System.Drawing.Size(79, 55);
            this.btn_Wet.TabIndex = 6;
            this.btn_Wet.Tag = "";
            this.btn_Wet.Text = "Wet Tip";
            this.btn_Wet.UseVisualStyleBackColor = true;
            this.btn_Wet.Click += new System.EventHandler(this.btn_Tipped_Click);
            // 
            // rbn_Bin
            // 
            this.rbn_Bin.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbn_Bin.Checked = true;
            this.rbn_Bin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbn_Bin.Location = new System.Drawing.Point(83, 77);
            this.rbn_Bin.Name = "rbn_Bin";
            this.rbn_Bin.Size = new System.Drawing.Size(127, 56);
            this.rbn_Bin.TabIndex = 1;
            this.rbn_Bin.TabStop = true;
            this.rbn_Bin.Text = "Submitted Bin";
            this.rbn_Bin.UseVisualStyleBackColor = true;
            this.rbn_Bin.CheckedChanged += new System.EventHandler(this.rbn_CheckedChanged);
            // 
            // rbn_Overrun
            // 
            this.rbn_Overrun.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbn_Overrun.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbn_Overrun.Location = new System.Drawing.Point(216, 77);
            this.rbn_Overrun.Name = "rbn_Overrun";
            this.rbn_Overrun.Size = new System.Drawing.Size(110, 56);
            this.rbn_Overrun.TabIndex = 2;
            this.rbn_Overrun.Text = "Overrun Bin";
            this.rbn_Overrun.UseVisualStyleBackColor = true;
            // 
            // GDI_Scanning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 278);
            this.Controls.Add(this.rbn_Overrun);
            this.Controls.Add(this.rbn_Bin);
            this.Controls.Add(this.btn_Wet);
            this.Controls.Add(this.lbl_WO_Num);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.cmb_sub_Id);
            this.Controls.Add(this.lbl_sub_Id);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Dry);
            this.Controls.Add(this.nud_weight);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.btn_Weight);
            this.Controls.Add(this.txt_barcode);
            this.Controls.Add(this.lbl_weight);
            this.Controls.Add(this.lbl_barcode);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "GDI_Scanning";
            this.Text = "FruPak.PF.Utils.Scanning.GDI Scanning";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GDI_Scanning_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.nud_weight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Dry;
        private System.Windows.Forms.NumericUpDown nud_weight;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.TextBox txt_barcode;
        private System.Windows.Forms.Label lbl_weight;
        private System.Windows.Forms.Label lbl_barcode;
        private System.Windows.Forms.Label lbl_sub_Id;
        private System.Windows.Forms.Button btn_Weight;
        private System.Windows.Forms.ComboBox cmb_sub_Id;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Label lbl_WO_Num;
        private System.Windows.Forms.Button btn_Wet;
        private System.Windows.Forms.RadioButton rbn_Bin;
        private System.Windows.Forms.RadioButton rbn_Overrun;
    }
}