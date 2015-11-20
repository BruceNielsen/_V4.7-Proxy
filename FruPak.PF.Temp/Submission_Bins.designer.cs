namespace PF.Temp
{
    partial class Submission_Bins
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Submission_Bins));
            this.materialNumber1 = new PF.Utils.UserControls.MaterialNumber();
            this.fruit1 = new PF.Utils.UserControls.Fruit();
            this.cmb_ESP = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmb_Storage = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmb_Location = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Submission = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_message = new System.Windows.Forms.Label();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.nud_total_bins = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nud_total_bins)).BeginInit();
            this.SuspendLayout();
            // 
            // materialNumber1
            // 
            this.materialNumber1.Location = new System.Drawing.Point(12, 185);
            this.materialNumber1.Material_Number = 0;
            this.materialNumber1.Name = "materialNumber1";
            this.materialNumber1.selectedBrand = 0;
            this.materialNumber1.selectedGrade = 0;
            this.materialNumber1.selectedGrowingMethod = 0;
            this.materialNumber1.selectedMaterial = 0;
            this.materialNumber1.selectedPackType = 0;
            this.materialNumber1.selectedProductGroup = 0;
            this.materialNumber1.selectedSize = 0;
            this.materialNumber1.selectedTrader = 0;
            this.materialNumber1.selectedTreatment = 0;
            this.materialNumber1.selectedVariety = 0;
            this.materialNumber1.selectedWeight = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.materialNumber1.Size = new System.Drawing.Size(882, 82);
            this.materialNumber1.TabIndex = 4;
            // 
            // fruit1
            // 
            this.fruit1.Block_Id = 0;
            this.fruit1.FruitType_Id = 0;
            this.fruit1.FruitVariety_Id = 0;
            this.fruit1.Location = new System.Drawing.Point(21, 124);
            this.fruit1.Name = "fruit1";
            this.fruit1.Size = new System.Drawing.Size(265, 55);
            this.fruit1.TabIndex = 3;
            this.fruit1.FruitVarietyChanged += new System.EventHandler(this.fruit1_FruitVarietyChanged);
            // 
            // cmb_ESP
            // 
            this.cmb_ESP.FormattingEnabled = true;
            this.cmb_ESP.Location = new System.Drawing.Point(367, 273);
            this.cmb_ESP.Name = "cmb_ESP";
            this.cmb_ESP.Size = new System.Drawing.Size(184, 21);
            this.cmb_ESP.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(320, 276);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 33;
            this.label6.Text = "ESP:";
            // 
            // cmb_Storage
            // 
            this.cmb_Storage.FormattingEnabled = true;
            this.cmb_Storage.Location = new System.Drawing.Point(102, 273);
            this.cmb_Storage.Name = "cmb_Storage";
            this.cmb_Storage.Size = new System.Drawing.Size(184, 21);
            this.cmb_Storage.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 276);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "Storage:";
            // 
            // cmb_Location
            // 
            this.cmb_Location.FormattingEnabled = true;
            this.cmb_Location.Location = new System.Drawing.Point(75, 97);
            this.cmb_Location.Name = "cmb_Location";
            this.cmb_Location.Size = new System.Drawing.Size(238, 21);
            this.cmb_Location.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Location:";
            // 
            // txt_Submission
            // 
            this.txt_Submission.Enabled = false;
            this.txt_Submission.Location = new System.Drawing.Point(102, 56);
            this.txt_Submission.Name = "txt_Submission";
            this.txt_Submission.ReadOnly = true;
            this.txt_Submission.Size = new System.Drawing.Size(100, 20);
            this.txt_Submission.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Submission #";
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(5, 11);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(0, 17);
            this.lbl_message.TabIndex = 22;
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Location = new System.Drawing.Point(366, 353);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(75, 23);
            this.btn_Refresh.TabIndex = 10;
            this.btn_Refresh.Text = "Re&fresh";
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(447, 353);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 11;
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(285, 353);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_reset.TabIndex = 9;
            this.btn_reset.Text = "&Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(204, 353);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 8;
            this.btn_Add.Text = "&Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 313);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(207, 13);
            this.label3.TabIndex = 74;
            this.label3.Text = "Number of Bins to be added to Submission";
            // 
            // nud_total_bins
            // 
            this.nud_total_bins.Location = new System.Drawing.Point(239, 311);
            this.nud_total_bins.Name = "nud_total_bins";
            this.nud_total_bins.Size = new System.Drawing.Size(47, 20);
            this.nud_total_bins.TabIndex = 7;
            // 
            // Submission_Bins
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 454);
            this.Controls.Add(this.nud_total_bins);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_Refresh);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.materialNumber1);
            this.Controls.Add(this.fruit1);
            this.Controls.Add(this.cmb_ESP);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmb_Storage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmb_Location);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_Submission);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_message);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Submission_Bins";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PF.Temp.Submission Bins";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Submission_Bins_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.nud_total_bins)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Utils.UserControls.MaterialNumber materialNumber1;
        private Utils.UserControls.Fruit fruit1;
        private System.Windows.Forms.ComboBox cmb_ESP;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmb_Storage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmb_Location;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Submission;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nud_total_bins;
    }
}