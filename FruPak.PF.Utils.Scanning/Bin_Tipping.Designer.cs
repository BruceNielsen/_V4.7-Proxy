namespace FruPak.PF.Utils.Scanning
{
    partial class Bin_Tipping
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bin_Tipping));
            this.label1 = new System.Windows.Forms.Label();
            this.nud_weight = new System.Windows.Forms.NumericUpDown();
            this.lbl_message = new System.Windows.Forms.Label();
            this.btn_Weight = new System.Windows.Forms.Button();
            this.lbl_weight = new System.Windows.Forms.Label();
            this.btn_Close = new System.Windows.Forms.Button();
            this.lbl_WO_Num = new System.Windows.Forms.Label();
            this.btn_Dry = new System.Windows.Forms.Button();
            this.btn_Wet = new System.Windows.Forms.Button();
            this.barcode1 = new FruPak.PF.Utils.UserControls.Barcode();
            ((System.ComponentModel.ISupportInitialize)(this.nud_weight)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 20);
            this.label1.TabIndex = 30;
            this.label1.Text = "Work Order:";
            // 
            // nud_weight
            // 
            this.nud_weight.DecimalPlaces = 3;
            this.nud_weight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nud_weight.Location = new System.Drawing.Point(130, 133);
            this.nud_weight.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nud_weight.Name = "nud_weight";
            this.nud_weight.Size = new System.Drawing.Size(113, 26);
            this.nud_weight.TabIndex = 2;
            this.nud_weight.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_barcode_KeyDown);
            this.nud_weight.Leave += new System.EventHandler(this.nud_weight_Leave);
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(13, 8);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(2, 22);
            this.lbl_message.TabIndex = 26;
            // 
            // btn_Weight
            // 
            this.btn_Weight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Weight.Location = new System.Drawing.Point(74, 245);
            this.btn_Weight.Name = "btn_Weight";
            this.btn_Weight.Size = new System.Drawing.Size(75, 46);
            this.btn_Weight.TabIndex = 5;
            this.btn_Weight.Tag = "                    lbl_message.Text = \"Invalid barcode\";";
            this.btn_Weight.Text = "Weight";
            this.btn_Weight.UseVisualStyleBackColor = true;
            this.btn_Weight.Click += new System.EventHandler(this.btn_Weight_Click);
            // 
            // lbl_weight
            // 
            this.lbl_weight.AutoSize = true;
            this.lbl_weight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_weight.Location = new System.Drawing.Point(45, 135);
            this.lbl_weight.Name = "lbl_weight";
            this.lbl_weight.Size = new System.Drawing.Size(63, 20);
            this.lbl_weight.TabIndex = 24;
            this.lbl_weight.Text = "Weight:";
            // 
            // btn_Close
            // 
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(168, 245);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 46);
            this.btn_Close.TabIndex = 6;
            this.btn_Close.Tag = "";
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // lbl_WO_Num
            // 
            this.lbl_WO_Num.AutoSize = true;
            this.lbl_WO_Num.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_WO_Num.Location = new System.Drawing.Point(108, 46);
            this.lbl_WO_Num.Name = "lbl_WO_Num";
            this.lbl_WO_Num.Size = new System.Drawing.Size(0, 20);
            this.lbl_WO_Num.TabIndex = 33;
            // 
            // btn_Dry
            // 
            this.btn_Dry.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Dry.Location = new System.Drawing.Point(168, 177);
            this.btn_Dry.Name = "btn_Dry";
            this.btn_Dry.Size = new System.Drawing.Size(75, 46);
            this.btn_Dry.TabIndex = 4;
            this.btn_Dry.Text = "Dry Tip";
            this.btn_Dry.UseVisualStyleBackColor = true;
            this.btn_Dry.Click += new System.EventHandler(this.btn_Wet_Dry_Click);
            // 
            // btn_Wet
            // 
            this.btn_Wet.FlatAppearance.BorderSize = 4;
            this.btn_Wet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Wet.Location = new System.Drawing.Point(74, 177);
            this.btn_Wet.Name = "btn_Wet";
            this.btn_Wet.Size = new System.Drawing.Size(75, 46);
            this.btn_Wet.TabIndex = 3;
            this.btn_Wet.Text = "Wet Tip";
            this.btn_Wet.UseVisualStyleBackColor = true;
            this.btn_Wet.Click += new System.EventHandler(this.btn_Wet_Dry_Click);
            // 
            // barcode1
            // 
            this.barcode1.BarcodeValue = "";
            this.barcode1.Location = new System.Drawing.Point(11, 79);
            this.barcode1.Name = "barcode1";
            this.barcode1.Set_Label = "Barcode:";
            this.barcode1.Size = new System.Drawing.Size(293, 33);
            this.barcode1.TabIndex = 1;
            this.barcode1.txtChanged += new System.EventHandler(this.barcode1_txtChanged);
            this.barcode1.Validating += new System.ComponentModel.CancelEventHandler(this.txt_barcode_Validating);
            // 
            // Bin_Tipping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(397, 303);
            this.Controls.Add(this.barcode1);
            this.Controls.Add(this.btn_Wet);
            this.Controls.Add(this.btn_Dry);
            this.Controls.Add(this.lbl_WO_Num);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nud_weight);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.btn_Weight);
            this.Controls.Add(this.lbl_weight);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Bin_Tipping";
            this.Text = "FruPak.PF.Utils.Scanning.Bin Tipping";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Bin_Tipping_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.nud_weight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nud_weight;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.Button btn_Weight;
        private System.Windows.Forms.Label lbl_weight;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Label lbl_WO_Num;
        private System.Windows.Forms.Button btn_Dry;
        private System.Windows.Forms.Button btn_Wet;
        private UserControls.Barcode barcode1;
    }
}