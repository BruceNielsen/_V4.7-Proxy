namespace FruPak.PF.BaseLoad
{
    partial class BLWO_Labels
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BLWO_Labels));
            this.cmb_Material = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_Batch1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.nud_Quantity1 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.nud_Quantity2 = new System.Windows.Forms.NumericUpDown();
            this.cmb_Batch2 = new System.Windows.Forms.ComboBox();
            this.nud_Quantity3 = new System.Windows.Forms.NumericUpDown();
            this.cmb_Batch3 = new System.Windows.Forms.ComboBox();
            this.lbl_message = new System.Windows.Forms.Label();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.woDisplay1 = new FruPak.PF.Utils.UserControls.WODisplay();
            this.cmb_Pallet_Type = new System.Windows.Forms.ComboBox();
            this.lbl_Pallet_Type = new System.Windows.Forms.Label();
            this.lbl_Location = new System.Windows.Forms.Label();
            this.cmb_Location = new System.Windows.Forms.ComboBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.dtp_Batch_Date1 = new System.Windows.Forms.DateTimePicker();
            this.dtp_Batch_Date2 = new System.Windows.Forms.DateTimePicker();
            this.dtp_Batch_Date3 = new System.Windows.Forms.DateTimePicker();
            this.txt_Pallet_Total = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Quantity1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Quantity2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Quantity3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // cmb_Material
            // 
            this.cmb_Material.FormattingEnabled = true;
            this.cmb_Material.Location = new System.Drawing.Point(99, 73);
            this.cmb_Material.Name = "cmb_Material";
            this.cmb_Material.Size = new System.Drawing.Size(515, 21);
            this.cmb_Material.TabIndex = 2;
            this.cmb_Material.Tag = "5";
            this.cmb_Material.SelectionChangeCommitted += new System.EventHandler(this.cmb_Material_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Output Product:";
            // 
            // cmb_Batch1
            // 
            this.cmb_Batch1.FormattingEnabled = true;
            this.cmb_Batch1.Location = new System.Drawing.Point(573, 120);
            this.cmb_Batch1.Name = "cmb_Batch1";
            this.cmb_Batch1.Size = new System.Drawing.Size(40, 21);
            this.cmb_Batch1.TabIndex = 6;
            this.cmb_Batch1.SelectionChangeCommitted += new System.EventHandler(this.cmb_Batch1_SelectionChangeCommitted);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(570, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Batch";
            // 
            // nud_Quantity1
            // 
            this.nud_Quantity1.Location = new System.Drawing.Point(631, 120);
            this.nud_Quantity1.Name = "nud_Quantity1";
            this.nud_Quantity1.Size = new System.Drawing.Size(60, 20);
            this.nud_Quantity1.TabIndex = 7;
            this.nud_Quantity1.ValueChanged += new System.EventHandler(this.nud_Quantity_ValueChanged);
            this.nud_Quantity1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nud_Quantity_KeyUp);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(628, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Qty on Pallet";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(32, 240);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(764, 95);
            this.dataGridView1.TabIndex = 15;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // nud_Quantity2
            // 
            this.nud_Quantity2.Location = new System.Drawing.Point(631, 147);
            this.nud_Quantity2.Name = "nud_Quantity2";
            this.nud_Quantity2.Size = new System.Drawing.Size(60, 20);
            this.nud_Quantity2.TabIndex = 10;
            this.nud_Quantity2.ValueChanged += new System.EventHandler(this.nud_Quantity_ValueChanged);
            this.nud_Quantity2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nud_Quantity_KeyUp);
            // 
            // cmb_Batch2
            // 
            this.cmb_Batch2.FormattingEnabled = true;
            this.cmb_Batch2.Location = new System.Drawing.Point(573, 147);
            this.cmb_Batch2.Name = "cmb_Batch2";
            this.cmb_Batch2.Size = new System.Drawing.Size(40, 21);
            this.cmb_Batch2.TabIndex = 9;
            // 
            // nud_Quantity3
            // 
            this.nud_Quantity3.Location = new System.Drawing.Point(631, 174);
            this.nud_Quantity3.Name = "nud_Quantity3";
            this.nud_Quantity3.Size = new System.Drawing.Size(60, 20);
            this.nud_Quantity3.TabIndex = 13;
            this.nud_Quantity3.ValueChanged += new System.EventHandler(this.nud_Quantity_ValueChanged);
            this.nud_Quantity3.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nud_Quantity_KeyUp);
            // 
            // cmb_Batch3
            // 
            this.cmb_Batch3.FormattingEnabled = true;
            this.cmb_Batch3.Location = new System.Drawing.Point(573, 174);
            this.cmb_Batch3.Name = "cmb_Batch3";
            this.cmb_Batch3.Size = new System.Drawing.Size(40, 21);
            this.cmb_Batch3.TabIndex = 12;
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(10, 7);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(0, 17);
            this.lbl_message.TabIndex = 41;
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(901, 211);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 19;
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(809, 211);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_reset.TabIndex = 18;
            this.btn_reset.Text = "&Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(718, 211);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 17;
            this.btn_Add.Text = "&Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // woDisplay1
            // 
            this.woDisplay1.Enabled = false;
            this.woDisplay1.FruitType_Id = 0;
            this.woDisplay1.Location = new System.Drawing.Point(13, 38);
            this.woDisplay1.Name = "woDisplay1";
            this.woDisplay1.Process_Date = null;
            this.woDisplay1.Product_Id = 0;
            this.woDisplay1.Size = new System.Drawing.Size(846, 29);
            this.woDisplay1.TabIndex = 1;
            this.woDisplay1.Variety_Id = 0;
            this.woDisplay1.Work_Order_Id = 0;
            // 
            // cmb_Pallet_Type
            // 
            this.cmb_Pallet_Type.FormattingEnabled = true;
            this.cmb_Pallet_Type.Location = new System.Drawing.Point(98, 100);
            this.cmb_Pallet_Type.Name = "cmb_Pallet_Type";
            this.cmb_Pallet_Type.Size = new System.Drawing.Size(277, 21);
            this.cmb_Pallet_Type.TabIndex = 3;
            this.cmb_Pallet_Type.Tag = "10";
            // 
            // lbl_Pallet_Type
            // 
            this.lbl_Pallet_Type.AutoSize = true;
            this.lbl_Pallet_Type.Location = new System.Drawing.Point(29, 103);
            this.lbl_Pallet_Type.Name = "lbl_Pallet_Type";
            this.lbl_Pallet_Type.Size = new System.Drawing.Size(63, 13);
            this.lbl_Pallet_Type.TabIndex = 43;
            this.lbl_Pallet_Type.Text = "Pallet Type:";
            // 
            // lbl_Location
            // 
            this.lbl_Location.AutoSize = true;
            this.lbl_Location.Location = new System.Drawing.Point(29, 130);
            this.lbl_Location.Name = "lbl_Location";
            this.lbl_Location.Size = new System.Drawing.Size(51, 13);
            this.lbl_Location.TabIndex = 45;
            this.lbl_Location.Text = "Location:";
            // 
            // cmb_Location
            // 
            this.cmb_Location.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_Location.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_Location.FormattingEnabled = true;
            this.cmb_Location.Location = new System.Drawing.Point(98, 127);
            this.cmb_Location.Name = "cmb_Location";
            this.cmb_Location.Size = new System.Drawing.Size(277, 21);
            this.cmb_Location.TabIndex = 4;
            this.cmb_Location.Tag = "15";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(32, 341);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.Size = new System.Drawing.Size(764, 123);
            this.dataGridView2.TabIndex = 16;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(465, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 47;
            this.label2.Text = "Date";
            // 
            // dtp_Batch_Date1
            // 
            this.dtp_Batch_Date1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_Batch_Date1.Location = new System.Drawing.Point(450, 121);
            this.dtp_Batch_Date1.Name = "dtp_Batch_Date1";
            this.dtp_Batch_Date1.Size = new System.Drawing.Size(97, 20);
            this.dtp_Batch_Date1.TabIndex = 5;
            this.dtp_Batch_Date1.ValueChanged += new System.EventHandler(this.dtp_Batch_Date1_ValueChanged);
            // 
            // dtp_Batch_Date2
            // 
            this.dtp_Batch_Date2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_Batch_Date2.Location = new System.Drawing.Point(450, 148);
            this.dtp_Batch_Date2.Name = "dtp_Batch_Date2";
            this.dtp_Batch_Date2.Size = new System.Drawing.Size(97, 20);
            this.dtp_Batch_Date2.TabIndex = 8;
            // 
            // dtp_Batch_Date3
            // 
            this.dtp_Batch_Date3.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_Batch_Date3.Location = new System.Drawing.Point(450, 175);
            this.dtp_Batch_Date3.Name = "dtp_Batch_Date3";
            this.dtp_Batch_Date3.Size = new System.Drawing.Size(97, 20);
            this.dtp_Batch_Date3.TabIndex = 11;
            // 
            // txt_Pallet_Total
            // 
            this.txt_Pallet_Total.Location = new System.Drawing.Point(718, 119);
            this.txt_Pallet_Total.Name = "txt_Pallet_Total";
            this.txt_Pallet_Total.ReadOnly = true;
            this.txt_Pallet_Total.Size = new System.Drawing.Size(60, 20);
            this.txt_Pallet_Total.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(715, 104);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "Total";
            // 
            // BLWO_Labels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 500);
            this.Controls.Add(this.dtp_Batch_Date3);
            this.Controls.Add(this.dtp_Batch_Date2);
            this.Controls.Add(this.dtp_Batch_Date1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.lbl_Location);
            this.Controls.Add(this.cmb_Location);
            this.Controls.Add(this.lbl_Pallet_Type);
            this.Controls.Add(this.cmb_Pallet_Type);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.woDisplay1);
            this.Controls.Add(this.nud_Quantity3);
            this.Controls.Add(this.cmb_Batch3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt_Pallet_Total);
            this.Controls.Add(this.nud_Quantity2);
            this.Controls.Add(this.cmb_Batch2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.nud_Quantity1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmb_Batch1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_Material);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "BLWO_Labels";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FruPak.PF.BaseLoad.Work Order Labels";
            this.Load += new System.EventHandler(this.SizeAllColumns);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BLWO_Labels_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.nud_Quantity1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Quantity2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Quantity3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_Material;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_Batch1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nud_Quantity1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.NumericUpDown nud_Quantity2;
        private System.Windows.Forms.ComboBox cmb_Batch2;
        private System.Windows.Forms.NumericUpDown nud_Quantity3;
        private System.Windows.Forms.ComboBox cmb_Batch3;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_Add;
        private FruPak.PF.Utils.UserControls.WODisplay woDisplay1;
        private System.Windows.Forms.ComboBox cmb_Pallet_Type;
        private System.Windows.Forms.Label lbl_Pallet_Type;
        private System.Windows.Forms.Label lbl_Location;
        private System.Windows.Forms.ComboBox cmb_Location;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtp_Batch_Date1;
        private System.Windows.Forms.DateTimePicker dtp_Batch_Date2;
        private System.Windows.Forms.DateTimePicker dtp_Batch_Date3;
        private System.Windows.Forms.TextBox txt_Pallet_Total;
        private System.Windows.Forms.Label label7;
    }
}