namespace PF.Utils.Scanning
{
    partial class Wet_Dry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Wet_Dry));
            this.barcode1 = new PF.Utils.UserControls.Barcode();
            this.lbl_WO_Num = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_message = new System.Windows.Forms.Label();
            this.btn_Close = new System.Windows.Forms.Button();
            this.rbn_Wet = new System.Windows.Forms.RadioButton();
            this.rbn_Dry = new System.Windows.Forms.RadioButton();
            this.rbn_PF = new System.Windows.Forms.RadioButton();
            this.rbn_GDI = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // barcode1
            // 
            this.barcode1.BarcodeValue = "";
            this.barcode1.Location = new System.Drawing.Point(12, 77);
            this.barcode1.Name = "barcode1";
            this.barcode1.Set_Label = "Barcode:";
            this.barcode1.Size = new System.Drawing.Size(293, 33);
            this.barcode1.TabIndex = 1;
            // 
            // lbl_WO_Num
            // 
            this.lbl_WO_Num.AutoSize = true;
            this.lbl_WO_Num.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_WO_Num.Location = new System.Drawing.Point(109, 44);
            this.lbl_WO_Num.Name = "lbl_WO_Num";
            this.lbl_WO_Num.Size = new System.Drawing.Size(0, 20);
            this.lbl_WO_Num.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 20);
            this.label1.TabIndex = 36;
            this.label1.Text = "Work Order:";
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(14, 6);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(2, 22);
            this.lbl_message.TabIndex = 35;
            // 
            // btn_Close
            // 
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(201, 315);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(80, 46);
            this.btn_Close.TabIndex = 6;
            this.btn_Close.Tag = "";
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // rbn_Wet
            // 
            this.rbn_Wet.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbn_Wet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbn_Wet.Location = new System.Drawing.Point(18, 19);
            this.rbn_Wet.Name = "rbn_Wet";
            this.rbn_Wet.Size = new System.Drawing.Size(80, 58);
            this.rbn_Wet.TabIndex = 4;
            this.rbn_Wet.TabStop = true;
            this.rbn_Wet.Text = "&Wet Tip";
            this.rbn_Wet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbn_Wet.UseVisualStyleBackColor = true;
            this.rbn_Wet.CheckedChanged += new System.EventHandler(this.rbn_btn_Click);
            // 
            // rbn_Dry
            // 
            this.rbn_Dry.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbn_Dry.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbn_Dry.Location = new System.Drawing.Point(129, 19);
            this.rbn_Dry.Name = "rbn_Dry";
            this.rbn_Dry.Size = new System.Drawing.Size(80, 58);
            this.rbn_Dry.TabIndex = 5;
            this.rbn_Dry.TabStop = true;
            this.rbn_Dry.Text = "&Dry Tip";
            this.rbn_Dry.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbn_Dry.UseVisualStyleBackColor = true;
            this.rbn_Dry.CheckedChanged += new System.EventHandler(this.rbn_btn_Click);
            // 
            // rbn_PF
            // 
            this.rbn_PF.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbn_PF.Checked = true;
            this.rbn_PF.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbn_PF.Location = new System.Drawing.Point(129, 19);
            this.rbn_PF.Name = "rbn_PF";
            this.rbn_PF.Size = new System.Drawing.Size(80, 58);
            this.rbn_PF.TabIndex = 3;
            this.rbn_PF.TabStop = true;
            this.rbn_PF.Text = "&Process Factory";
            this.rbn_PF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbn_PF.UseVisualStyleBackColor = true;
            // 
            // rbn_GDI
            // 
            this.rbn_GDI.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbn_GDI.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbn_GDI.Location = new System.Drawing.Point(18, 19);
            this.rbn_GDI.Name = "rbn_GDI";
            this.rbn_GDI.Size = new System.Drawing.Size(80, 58);
            this.rbn_GDI.TabIndex = 2;
            this.rbn_GDI.TabStop = true;
            this.rbn_GDI.Text = "&GDI";
            this.rbn_GDI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbn_GDI.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbn_GDI);
            this.groupBox1.Controls.Add(this.rbn_PF);
            this.groupBox1.Location = new System.Drawing.Point(72, 116);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 87);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbn_Wet);
            this.groupBox2.Controls.Add(this.rbn_Dry);
            this.groupBox2.Location = new System.Drawing.Point(72, 209);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(233, 100);
            this.groupBox2.TabIndex = 45;
            this.groupBox2.TabStop = false;
            // 
            // Wet_Dry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 370);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.barcode1);
            this.Controls.Add(this.lbl_WO_Num);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_message);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Wet_Dry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PF.Utils.Scanning.WET_DRY";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Wet_Dry_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControls.Barcode barcode1;
        private System.Windows.Forms.Label lbl_WO_Num;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.RadioButton rbn_Wet;
        private System.Windows.Forms.RadioButton rbn_Dry;
        private System.Windows.Forms.RadioButton rbn_PF;
        private System.Windows.Forms.RadioButton rbn_GDI;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}