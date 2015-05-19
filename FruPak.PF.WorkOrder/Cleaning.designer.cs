namespace FruPak.PF.WorkOrder
{
    partial class Cleaning
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cleaning));
            this.tbc_Cleaning = new System.Windows.Forms.TabControl();
            this.tbp_Base = new System.Windows.Forms.TabPage();
            this.pic_Cleaner = new System.Windows.Forms.PictureBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.dtp_Completed = new System.Windows.Forms.DateTimePicker();
            this.btn_Close = new System.Windows.Forms.Button();
            this.tbc_Cleaning.SuspendLayout();
            this.tbp_Base.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Cleaner)).BeginInit();
            this.SuspendLayout();
            // 
            // tbc_Cleaning
            // 
            this.tbc_Cleaning.Controls.Add(this.tbp_Base);
            this.tbc_Cleaning.ImageList = this.imageList1;
            this.tbc_Cleaning.Location = new System.Drawing.Point(12, 64);
            this.tbc_Cleaning.Name = "tbc_Cleaning";
            this.tbc_Cleaning.SelectedIndex = 0;
            this.tbc_Cleaning.Size = new System.Drawing.Size(816, 269);
            this.tbc_Cleaning.TabIndex = 0;
            this.tbc_Cleaning.SelectedIndexChanged += new System.EventHandler(this.tbc_Cleaning_SelectedIndexChanged);
            // 
            // tbp_Base
            // 
            this.tbp_Base.Controls.Add(this.pic_Cleaner);
            this.tbp_Base.Location = new System.Drawing.Point(4, 23);
            this.tbp_Base.Name = "tbp_Base";
            this.tbp_Base.Padding = new System.Windows.Forms.Padding(3);
            this.tbp_Base.Size = new System.Drawing.Size(808, 242);
            this.tbp_Base.TabIndex = 0;
            this.tbp_Base.UseVisualStyleBackColor = true;
            // 
            // pic_Cleaner
            // 
            this.pic_Cleaner.Image = ((System.Drawing.Image)(resources.GetObject("pic_Cleaner.Image")));
            this.pic_Cleaner.Location = new System.Drawing.Point(238, 6);
            this.pic_Cleaner.Name = "pic_Cleaner";
            this.pic_Cleaner.Size = new System.Drawing.Size(206, 230);
            this.pic_Cleaner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_Cleaner.TabIndex = 0;
            this.pic_Cleaner.TabStop = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "valid.jpg");
            this.imageList1.Images.SetKeyName(1, "hold.jpg");
            this.imageList1.Images.SetKeyName(2, "not_valid.jpg");
            // 
            // dtp_Completed
            // 
            this.dtp_Completed.Location = new System.Drawing.Point(12, 38);
            this.dtp_Completed.Name = "dtp_Completed";
            this.dtp_Completed.Size = new System.Drawing.Size(200, 20);
            this.dtp_Completed.TabIndex = 1;
            this.dtp_Completed.ValueChanged += new System.EventHandler(this.dtp_Completed_ValueChanged);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(749, 339);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 2;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // Cleaning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 373);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.dtp_Completed);
            this.Controls.Add(this.tbc_Cleaning);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Cleaning";
            this.Text = "FruPak.PF.WorkOrder.Cleaning";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Cleaning_KeyDown);
            this.tbc_Cleaning.ResumeLayout(false);
            this.tbp_Base.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Cleaner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbc_Cleaning;
        private System.Windows.Forms.DateTimePicker dtp_Completed;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.TabPage tbp_Base;
        private System.Windows.Forms.PictureBox pic_Cleaner;
        private System.Windows.Forms.ImageList imageList1;
    }
}