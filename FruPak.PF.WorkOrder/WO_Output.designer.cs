namespace FruPak.PF.WorkOrder
{
    partial class WO_Output
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WO_Output));
            this.label10 = new System.Windows.Forms.Label();
            this.cmb_Material = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmb_Weight = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmb_Product_Group = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmb_Brand = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmb_PackType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_Trader = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.lbl_message = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_Grade = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_Treatment = new System.Windows.Forms.ComboBox();
            this.woDisplay1 = new FruPak.PF.Utils.UserControls.WODisplay();
            this.label4 = new System.Windows.Forms.Label();
            this.cmb_Size = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(728, 117);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 13);
            this.label10.TabIndex = 45;
            this.label10.Text = "Material";
            // 
            // cmb_Material
            // 
            this.cmb_Material.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_Material.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_Material.FormattingEnabled = true;
            this.cmb_Material.Location = new System.Drawing.Point(778, 114);
            this.cmb_Material.Name = "cmb_Material";
            this.cmb_Material.Size = new System.Drawing.Size(100, 21);
            this.cmb_Material.TabIndex = 10;
            this.cmb_Material.SelectionChangeCommitted += new System.EventHandler(this.cmb_Material_SelectionChangeCommitted);
            this.cmb_Material.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            this.cmb_Material.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Enter_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(558, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 13);
            this.label9.TabIndex = 43;
            this.label9.Text = "Weight";
            // 
            // cmb_Weight
            // 
            this.cmb_Weight.FormattingEnabled = true;
            this.cmb_Weight.Location = new System.Drawing.Point(605, 87);
            this.cmb_Weight.Name = "cmb_Weight";
            this.cmb_Weight.Size = new System.Drawing.Size(77, 21);
            this.cmb_Weight.TabIndex = 8;
            this.cmb_Weight.SelectionChangeCommitted += new System.EventHandler(this.cmb_Weight_SelectionChangeCommitted);
            this.cmb_Weight.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(293, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 41;
            this.label8.Text = "Pack Type";
            // 
            // cmb_Product_Group
            // 
            this.cmb_Product_Group.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_Product_Group.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_Product_Group.FormattingEnabled = true;
            this.cmb_Product_Group.Location = new System.Drawing.Point(90, 114);
            this.cmb_Product_Group.Name = "cmb_Product_Group";
            this.cmb_Product_Group.Size = new System.Drawing.Size(193, 21);
            this.cmb_Product_Group.TabIndex = 3;
            this.cmb_Product_Group.SelectionChangeCommitted += new System.EventHandler(this.cmb_Product_Group_SelectionChangeCommitted);
            this.cmb_Product_Group.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(317, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 39;
            this.label7.Text = "Brand";
            // 
            // cmb_Brand
            // 
            this.cmb_Brand.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_Brand.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_Brand.FormattingEnabled = true;
            this.cmb_Brand.Location = new System.Drawing.Point(358, 114);
            this.cmb_Brand.Name = "cmb_Brand";
            this.cmb_Brand.Size = new System.Drawing.Size(185, 21);
            this.cmb_Brand.TabIndex = 6;
            this.cmb_Brand.SelectionChangeCommitted += new System.EventHandler(this.cmb_Brand_SelectionChangeCommitted);
            this.cmb_Brand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 37;
            this.label5.Text = "Product Group";
            // 
            // cmb_PackType
            // 
            this.cmb_PackType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_PackType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_PackType.FormattingEnabled = true;
            this.cmb_PackType.Location = new System.Drawing.Point(358, 89);
            this.cmb_PackType.Name = "cmb_PackType";
            this.cmb_PackType.Size = new System.Drawing.Size(185, 21);
            this.cmb_PackType.TabIndex = 5;
            this.cmb_PackType.SelectionChangeCommitted += new System.EventHandler(this.cmb_PackType_SelectionChangeCommitted);
            this.cmb_PackType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Trader";
            // 
            // cmb_Trader
            // 
            this.cmb_Trader.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_Trader.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_Trader.FormattingEnabled = true;
            this.cmb_Trader.Location = new System.Drawing.Point(90, 87);
            this.cmb_Trader.Name = "cmb_Trader";
            this.cmb_Trader.Size = new System.Drawing.Size(193, 21);
            this.cmb_Trader.TabIndex = 2;
            this.cmb_Trader.SelectionChangeCommitted += new System.EventHandler(this.cmb_Trader_SelectionChangeCommitted);
            this.cmb_Trader.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(26, 197);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(852, 154);
            this.dataGridView1.TabIndex = 70;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(803, 159);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 13;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(711, 159);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_reset.TabIndex = 12;
            this.btn_reset.Text = "Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(620, 159);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 11;
            this.btn_Add.Text = "Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(8, 9);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(0, 17);
            this.lbl_message.TabIndex = 52;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(317, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 58;
            this.label2.Text = "Grade";
            // 
            // cmb_Grade
            // 
            this.cmb_Grade.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_Grade.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_Grade.FormattingEnabled = true;
            this.cmb_Grade.Location = new System.Drawing.Point(359, 140);
            this.cmb_Grade.Name = "cmb_Grade";
            this.cmb_Grade.Size = new System.Drawing.Size(184, 21);
            this.cmb_Grade.TabIndex = 7;
            this.cmb_Grade.SelectionChangeCommitted += new System.EventHandler(this.cmb_Grade_SelectionChangeCommitted);
            this.cmb_Grade.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 60;
            this.label3.Text = "Treatment";
            // 
            // cmb_Treatment
            // 
            this.cmb_Treatment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_Treatment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_Treatment.FormattingEnabled = true;
            this.cmb_Treatment.Location = new System.Drawing.Point(90, 140);
            this.cmb_Treatment.Name = "cmb_Treatment";
            this.cmb_Treatment.Size = new System.Drawing.Size(193, 21);
            this.cmb_Treatment.TabIndex = 4;
            this.cmb_Treatment.SelectionChangeCommitted += new System.EventHandler(this.cmb_Treatment_SelectionChangeCommitted);
            this.cmb_Treatment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // woDisplay1
            // 
            this.woDisplay1.FruitType_Id = 0;
            this.woDisplay1.Location = new System.Drawing.Point(11, 42);
            this.woDisplay1.Name = "woDisplay1";
            this.woDisplay1.Process_Date = null;
            this.woDisplay1.Product_Id = 0;
            this.woDisplay1.Size = new System.Drawing.Size(846, 29);
            this.woDisplay1.TabIndex = 1;
            this.woDisplay1.Variety_Id = 0;
            this.woDisplay1.Work_Order_Id = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(572, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 63;
            this.label4.Text = "Size";
            // 
            // cmb_Size
            // 
            this.cmb_Size.FormattingEnabled = true;
            this.cmb_Size.Location = new System.Drawing.Point(605, 114);
            this.cmb_Size.Name = "cmb_Size";
            this.cmb_Size.Size = new System.Drawing.Size(77, 21);
            this.cmb_Size.TabIndex = 9;
            this.cmb_Size.SelectionChangeCommitted += new System.EventHandler(this.cmb_Size_SelectionChangeCommitted);
            this.cmb_Size.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // WO_Output
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 365);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmb_Size);
            this.Controls.Add(this.woDisplay1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmb_Treatment);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmb_Grade);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cmb_Material);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmb_Weight);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmb_Product_Group);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmb_Brand);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmb_PackType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_Trader);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "WO_Output";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FruPak.PF.WorkOrder.Work Order Output";
            this.Load += new System.EventHandler(this.SizeAllColumns);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WO_Output_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmb_Material;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmb_Weight;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmb_Product_Group;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmb_Brand;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmb_PackType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_Trader;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_Grade;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_Treatment;
        private FruPak.PF.Utils.UserControls.WODisplay woDisplay1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmb_Size;
    }
}