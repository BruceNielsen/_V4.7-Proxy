namespace FruPak.PF.WorkOrder
{
    partial class WO_Labels
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WO_Labels));
            this.cmb_Material = new System.Windows.Forms.ComboBox();
            this.lbl_output = new System.Windows.Forms.Label();
            this.cmb_Batch1 = new System.Windows.Forms.ComboBox();
            this.lbl_batch = new System.Windows.Forms.Label();
            this.nud_Quantity1 = new System.Windows.Forms.NumericUpDown();
            this.lbl_qunatity = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.nud_Quantity2 = new System.Windows.Forms.NumericUpDown();
            this.cmb_Batch2 = new System.Windows.Forms.ComboBox();
            this.txt_Pallet_Total = new System.Windows.Forms.TextBox();
            this.lbl_total = new System.Windows.Forms.Label();
            this.nud_Quantity3 = new System.Windows.Forms.NumericUpDown();
            this.cmb_Batch3 = new System.Windows.Forms.ComboBox();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.cmb_Pallet_Type = new System.Windows.Forms.ComboBox();
            this.lbl_Pallet_Type = new System.Windows.Forms.Label();
            this.lbl_Location = new System.Windows.Forms.Label();
            this.cmb_Location = new System.Windows.Forms.ComboBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.ckb_AutoPrint = new System.Windows.Forms.CheckBox();
            this.ckb_Auto_Reset = new System.Windows.Forms.CheckBox();
            this.lbl_weight = new System.Windows.Forms.Label();
            this.rb_EOR1 = new System.Windows.Forms.RadioButton();
            this.rb_EOR2 = new System.Windows.Forms.RadioButton();
            this.rb_EOR3 = new System.Windows.Forms.RadioButton();
            this.nud_EOR_Weight = new System.Windows.Forms.NumericUpDown();
            this.ckb_plc_on_order = new System.Windows.Forms.CheckBox();
            this.cmb_Orders = new System.Windows.Forms.ComboBox();
            this.lbl_order = new System.Windows.Forms.Label();
            this.ckb_dup = new System.Windows.Forms.CheckBox();
            this.woDisplay1 = new FruPak.PF.Utils.UserControls.WODisplay();
            this.marqueeLabel1 = new FruPak.PF.WorkOrder.Marquee_Label();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Quantity1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Quantity2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Quantity3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_EOR_Weight)).BeginInit();
            this.SuspendLayout();
            // 
            // cmb_Material
            // 
            this.cmb_Material.FormattingEnabled = true;
            this.cmb_Material.Location = new System.Drawing.Point(98, 73);
            this.cmb_Material.Name = "cmb_Material";
            this.cmb_Material.Size = new System.Drawing.Size(515, 21);
            this.cmb_Material.TabIndex = 2;
            this.cmb_Material.SelectionChangeCommitted += new System.EventHandler(this.cmb_Material_SelectionChangeCommitted);
            // 
            // lbl_output
            // 
            this.lbl_output.AutoSize = true;
            this.lbl_output.Location = new System.Drawing.Point(11, 76);
            this.lbl_output.Name = "lbl_output";
            this.lbl_output.Size = new System.Drawing.Size(82, 13);
            this.lbl_output.TabIndex = 20;
            this.lbl_output.Text = "Output Product:";
            // 
            // cmb_Batch1
            // 
            this.cmb_Batch1.FormattingEnabled = true;
            this.cmb_Batch1.Location = new System.Drawing.Point(487, 117);
            this.cmb_Batch1.Name = "cmb_Batch1";
            this.cmb_Batch1.Size = new System.Drawing.Size(40, 21);
            this.cmb_Batch1.TabIndex = 6;
            this.cmb_Batch1.SelectedIndexChanged += new System.EventHandler(this.cmb_Batch_SelectedIndexChanged);
            this.cmb_Batch1.SelectionChangeCommitted += new System.EventHandler(this.cmb_Batch1_SelectionChangeCommitted);
            // 
            // lbl_batch
            // 
            this.lbl_batch.AutoSize = true;
            this.lbl_batch.Location = new System.Drawing.Point(484, 100);
            this.lbl_batch.Name = "lbl_batch";
            this.lbl_batch.Size = new System.Drawing.Size(35, 13);
            this.lbl_batch.TabIndex = 22;
            this.lbl_batch.Text = "Batch";
            // 
            // nud_Quantity1
            // 
            this.nud_Quantity1.Location = new System.Drawing.Point(545, 117);
            this.nud_Quantity1.Name = "nud_Quantity1";
            this.nud_Quantity1.Size = new System.Drawing.Size(60, 20);
            this.nud_Quantity1.TabIndex = 7;
            this.nud_Quantity1.ValueChanged += new System.EventHandler(this.nud_Quantity_ValueChanged);
            this.nud_Quantity1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Enter_KeyPress);
            this.nud_Quantity1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nud_Quantity_KeyUp);
            // 
            // lbl_qunatity
            // 
            this.lbl_qunatity.AutoSize = true;
            this.lbl_qunatity.Location = new System.Drawing.Point(542, 100);
            this.lbl_qunatity.Name = "lbl_qunatity";
            this.lbl_qunatity.Size = new System.Drawing.Size(67, 13);
            this.lbl_qunatity.TabIndex = 24;
            this.lbl_qunatity.Text = "Qty on Pallet";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(32, 204);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(868, 95);
            this.dataGridView1.TabIndex = 24;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // nud_Quantity2
            // 
            this.nud_Quantity2.Location = new System.Drawing.Point(545, 144);
            this.nud_Quantity2.Name = "nud_Quantity2";
            this.nud_Quantity2.Size = new System.Drawing.Size(60, 20);
            this.nud_Quantity2.TabIndex = 10;
            this.nud_Quantity2.ValueChanged += new System.EventHandler(this.nud_Quantity_ValueChanged);
            this.nud_Quantity2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nud_Quantity_KeyUp);
            // 
            // cmb_Batch2
            // 
            this.cmb_Batch2.FormattingEnabled = true;
            this.cmb_Batch2.Location = new System.Drawing.Point(487, 144);
            this.cmb_Batch2.Name = "cmb_Batch2";
            this.cmb_Batch2.Size = new System.Drawing.Size(40, 21);
            this.cmb_Batch2.TabIndex = 9;
            this.cmb_Batch2.SelectedIndexChanged += new System.EventHandler(this.cmb_Batch_SelectedIndexChanged);
            // 
            // txt_Pallet_Total
            // 
            this.txt_Pallet_Total.Location = new System.Drawing.Point(621, 118);
            this.txt_Pallet_Total.Name = "txt_Pallet_Total";
            this.txt_Pallet_Total.ReadOnly = true;
            this.txt_Pallet_Total.Size = new System.Drawing.Size(60, 20);
            this.txt_Pallet_Total.TabIndex = 14;
            // 
            // lbl_total
            // 
            this.lbl_total.AutoSize = true;
            this.lbl_total.Location = new System.Drawing.Point(629, 100);
            this.lbl_total.Name = "lbl_total";
            this.lbl_total.Size = new System.Drawing.Size(31, 13);
            this.lbl_total.TabIndex = 30;
            this.lbl_total.Text = "Total";
            // 
            // nud_Quantity3
            // 
            this.nud_Quantity3.Location = new System.Drawing.Point(545, 171);
            this.nud_Quantity3.Name = "nud_Quantity3";
            this.nud_Quantity3.Size = new System.Drawing.Size(60, 20);
            this.nud_Quantity3.TabIndex = 13;
            this.nud_Quantity3.ValueChanged += new System.EventHandler(this.nud_Quantity_ValueChanged);
            this.nud_Quantity3.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nud_Quantity_KeyUp);
            // 
            // cmb_Batch3
            // 
            this.cmb_Batch3.FormattingEnabled = true;
            this.cmb_Batch3.Location = new System.Drawing.Point(487, 171);
            this.cmb_Batch3.Name = "cmb_Batch3";
            this.cmb_Batch3.Size = new System.Drawing.Size(40, 21);
            this.cmb_Batch3.TabIndex = 12;
            this.cmb_Batch3.SelectedIndexChanged += new System.EventHandler(this.cmb_Batch_SelectedIndexChanged);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(905, 166);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 23;
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(813, 166);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_reset.TabIndex = 22;
            this.btn_reset.Text = "&Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(722, 166);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 21;
            this.btn_Add.Text = "&Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // cmb_Pallet_Type
            // 
            this.cmb_Pallet_Type.FormattingEnabled = true;
            this.cmb_Pallet_Type.Location = new System.Drawing.Point(98, 100);
            this.cmb_Pallet_Type.Name = "cmb_Pallet_Type";
            this.cmb_Pallet_Type.Size = new System.Drawing.Size(277, 21);
            this.cmb_Pallet_Type.TabIndex = 3;
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
            this.cmb_Location.FormattingEnabled = true;
            this.cmb_Location.Location = new System.Drawing.Point(98, 127);
            this.cmb_Location.Name = "cmb_Location";
            this.cmb_Location.Size = new System.Drawing.Size(277, 21);
            this.cmb_Location.TabIndex = 4;
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
            this.dataGridView2.Size = new System.Drawing.Size(868, 123);
            this.dataGridView2.TabIndex = 25;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            // 
            // ckb_AutoPrint
            // 
            this.ckb_AutoPrint.AutoSize = true;
            this.ckb_AutoPrint.Location = new System.Drawing.Point(722, 85);
            this.ckb_AutoPrint.Name = "ckb_AutoPrint";
            this.ckb_AutoPrint.Size = new System.Drawing.Size(84, 17);
            this.ckb_AutoPrint.TabIndex = 16;
            this.ckb_AutoPrint.Text = "Print on Add";
            this.ckb_AutoPrint.UseVisualStyleBackColor = true;
            // 
            // ckb_Auto_Reset
            // 
            this.ckb_Auto_Reset.AutoSize = true;
            this.ckb_Auto_Reset.Location = new System.Drawing.Point(813, 85);
            this.ckb_Auto_Reset.Name = "ckb_Auto_Reset";
            this.ckb_Auto_Reset.Size = new System.Drawing.Size(121, 17);
            this.ckb_Auto_Reset.TabIndex = 17;
            this.ckb_Auto_Reset.Text = "Turn Auto Reset Off";
            this.ckb_Auto_Reset.UseVisualStyleBackColor = true;
            // 
            // lbl_weight
            // 
            this.lbl_weight.AutoSize = true;
            this.lbl_weight.Location = new System.Drawing.Point(640, 151);
            this.lbl_weight.Name = "lbl_weight";
            this.lbl_weight.Size = new System.Drawing.Size(41, 13);
            this.lbl_weight.TabIndex = 73;
            this.lbl_weight.Text = "Weight";
            this.lbl_weight.Visible = false;
            // 
            // rb_EOR1
            // 
            this.rb_EOR1.AutoSize = true;
            this.rb_EOR1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rb_EOR1.Location = new System.Drawing.Point(392, 118);
            this.rb_EOR1.Name = "rb_EOR1";
            this.rb_EOR1.Size = new System.Drawing.Size(79, 17);
            this.rb_EOR1.TabIndex = 5;
            this.rb_EOR1.TabStop = true;
            this.rb_EOR1.Text = "End of Run";
            this.rb_EOR1.UseVisualStyleBackColor = true;
            this.rb_EOR1.Visible = false;
            this.rb_EOR1.CheckedChanged += new System.EventHandler(this.rb_EORCheckedChanged);
            // 
            // rb_EOR2
            // 
            this.rb_EOR2.AutoSize = true;
            this.rb_EOR2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rb_EOR2.Location = new System.Drawing.Point(392, 144);
            this.rb_EOR2.Name = "rb_EOR2";
            this.rb_EOR2.Size = new System.Drawing.Size(79, 17);
            this.rb_EOR2.TabIndex = 8;
            this.rb_EOR2.TabStop = true;
            this.rb_EOR2.Text = "End of Run";
            this.rb_EOR2.UseVisualStyleBackColor = true;
            this.rb_EOR2.Visible = false;
            this.rb_EOR2.CheckedChanged += new System.EventHandler(this.rb_EORCheckedChanged);
            // 
            // rb_EOR3
            // 
            this.rb_EOR3.AutoSize = true;
            this.rb_EOR3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rb_EOR3.Location = new System.Drawing.Point(392, 172);
            this.rb_EOR3.Name = "rb_EOR3";
            this.rb_EOR3.Size = new System.Drawing.Size(79, 17);
            this.rb_EOR3.TabIndex = 11;
            this.rb_EOR3.TabStop = true;
            this.rb_EOR3.Text = "End of Run";
            this.rb_EOR3.UseVisualStyleBackColor = true;
            this.rb_EOR3.Visible = false;
            this.rb_EOR3.CheckedChanged += new System.EventHandler(this.rb_EORCheckedChanged);
            // 
            // nud_EOR_Weight
            // 
            this.nud_EOR_Weight.DecimalPlaces = 3;
            this.nud_EOR_Weight.Location = new System.Drawing.Point(621, 171);
            this.nud_EOR_Weight.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nud_EOR_Weight.Name = "nud_EOR_Weight";
            this.nud_EOR_Weight.Size = new System.Drawing.Size(89, 20);
            this.nud_EOR_Weight.TabIndex = 15;
            this.nud_EOR_Weight.Visible = false;
            // 
            // ckb_plc_on_order
            // 
            this.ckb_plc_on_order.AutoSize = true;
            this.ckb_plc_on_order.Location = new System.Drawing.Point(813, 108);
            this.ckb_plc_on_order.Name = "ckb_plc_on_order";
            this.ckb_plc_on_order.Size = new System.Drawing.Size(97, 17);
            this.ckb_plc_on_order.TabIndex = 19;
            this.ckb_plc_on_order.Text = "Place on Order";
            this.ckb_plc_on_order.UseVisualStyleBackColor = true;
            this.ckb_plc_on_order.CheckedChanged += new System.EventHandler(this.ckb_plc_on_order_CheckedChanged);
            // 
            // cmb_Orders
            // 
            this.cmb_Orders.FormattingEnabled = true;
            this.cmb_Orders.Location = new System.Drawing.Point(798, 127);
            this.cmb_Orders.Name = "cmb_Orders";
            this.cmb_Orders.Size = new System.Drawing.Size(182, 21);
            this.cmb_Orders.TabIndex = 20;
            this.cmb_Orders.Visible = false;
            // 
            // lbl_order
            // 
            this.lbl_order.AutoSize = true;
            this.lbl_order.Location = new System.Drawing.Point(720, 130);
            this.lbl_order.Name = "lbl_order";
            this.lbl_order.Size = new System.Drawing.Size(72, 13);
            this.lbl_order.TabIndex = 80;
            this.lbl_order.Text = "Select Order :";
            this.lbl_order.Visible = false;
            // 
            // ckb_dup
            // 
            this.ckb_dup.AutoSize = true;
            this.ckb_dup.Location = new System.Drawing.Point(722, 108);
            this.ckb_dup.Name = "ckb_dup";
            this.ckb_dup.Size = new System.Drawing.Size(91, 17);
            this.ckb_dup.TabIndex = 18;
            this.ckb_dup.Text = "Print 2 Copies";
            this.ckb_dup.UseVisualStyleBackColor = true;
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
            // marqueeLabel1
            // 
            this.marqueeLabel1.AutoSize = true;
            this.marqueeLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.marqueeLabel1.Location = new System.Drawing.Point(14, 9);
            this.marqueeLabel1.Name = "marqueeLabel1";
            this.marqueeLabel1.Size = new System.Drawing.Size(0, 23);
            this.marqueeLabel1.TabIndex = 81;
            this.marqueeLabel1.UseCompatibleTextRendering = true;
            // 
            // WO_Labels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 500);
            this.Controls.Add(this.ckb_dup);
            this.Controls.Add(this.marqueeLabel1);
            this.Controls.Add(this.lbl_order);
            this.Controls.Add(this.cmb_Orders);
            this.Controls.Add(this.ckb_plc_on_order);
            this.Controls.Add(this.nud_EOR_Weight);
            this.Controls.Add(this.rb_EOR3);
            this.Controls.Add(this.rb_EOR2);
            this.Controls.Add(this.rb_EOR1);
            this.Controls.Add(this.lbl_weight);
            this.Controls.Add(this.ckb_Auto_Reset);
            this.Controls.Add(this.ckb_AutoPrint);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.lbl_Location);
            this.Controls.Add(this.cmb_Location);
            this.Controls.Add(this.lbl_Pallet_Type);
            this.Controls.Add(this.cmb_Pallet_Type);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.woDisplay1);
            this.Controls.Add(this.nud_Quantity3);
            this.Controls.Add(this.cmb_Batch3);
            this.Controls.Add(this.lbl_total);
            this.Controls.Add(this.txt_Pallet_Total);
            this.Controls.Add(this.nud_Quantity2);
            this.Controls.Add(this.cmb_Batch2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lbl_qunatity);
            this.Controls.Add(this.nud_Quantity1);
            this.Controls.Add(this.lbl_batch);
            this.Controls.Add(this.cmb_Batch1);
            this.Controls.Add(this.lbl_output);
            this.Controls.Add(this.cmb_Material);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "WO_Labels";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FruPak.PF.WorkOrder.Work Order Labels";
            this.Load += new System.EventHandler(this.SizeAllColumns);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WO_Labels_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.nud_Quantity1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Quantity2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Quantity3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_EOR_Weight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.ComboBox cmb_Material;
        protected System.Windows.Forms.Label lbl_output;
        protected System.Windows.Forms.ComboBox cmb_Batch1;
        protected System.Windows.Forms.Label lbl_batch;
        protected System.Windows.Forms.NumericUpDown nud_Quantity1;
        protected System.Windows.Forms.Label lbl_qunatity;
        protected System.Windows.Forms.DataGridView dataGridView1;
        protected System.Windows.Forms.NumericUpDown nud_Quantity2;
        protected System.Windows.Forms.ComboBox cmb_Batch2;
        protected System.Windows.Forms.TextBox txt_Pallet_Total;
        protected System.Windows.Forms.Label lbl_total;
        protected System.Windows.Forms.NumericUpDown nud_Quantity3;
        protected System.Windows.Forms.ComboBox cmb_Batch3;
        protected System.Windows.Forms.Button btn_Close;
        protected System.Windows.Forms.Button btn_reset;
        protected System.Windows.Forms.Button btn_Add;
        protected Utils.UserControls.WODisplay woDisplay1;
        protected System.Windows.Forms.ComboBox cmb_Pallet_Type;
        protected System.Windows.Forms.Label lbl_Pallet_Type;
        protected System.Windows.Forms.Label lbl_Location;
        protected System.Windows.Forms.ComboBox cmb_Location;
        protected System.Windows.Forms.DataGridView dataGridView2;
        protected System.Windows.Forms.CheckBox ckb_AutoPrint;
        protected System.Windows.Forms.CheckBox ckb_Auto_Reset;
        protected System.Windows.Forms.Label lbl_weight;
        protected System.Windows.Forms.RadioButton rb_EOR1;
        protected System.Windows.Forms.RadioButton rb_EOR2;
        protected System.Windows.Forms.RadioButton rb_EOR3;
        protected System.Windows.Forms.NumericUpDown nud_EOR_Weight;
        protected System.Windows.Forms.CheckBox ckb_plc_on_order;
        protected System.Windows.Forms.ComboBox cmb_Orders;
        protected System.Windows.Forms.Label lbl_order;
        private Marquee_Label marqueeLabel1;
        protected System.Windows.Forms.CheckBox ckb_dup;


    }
}