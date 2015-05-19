namespace FruPak.PF.Utils.Common
{
    partial class Material
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Material));
            this.lbl_Material = new System.Windows.Forms.Label();
            this.txt_Material_No = new System.Windows.Forms.TextBox();
            this.lbl_Trader = new System.Windows.Forms.Label();
            this.cmb_Trader = new System.Windows.Forms.ComboBox();
            this.cmb_PackType = new System.Windows.Forms.ComboBox();
            this.lbl_PackType = new System.Windows.Forms.Label();
            this.cmb_Size = new System.Windows.Forms.ComboBox();
            this.lbl_Size = new System.Windows.Forms.Label();
            this.cmb_Count = new System.Windows.Forms.ComboBox();
            this.lbl_Count = new System.Windows.Forms.Label();
            this.cmb_Brand = new System.Windows.Forms.ComboBox();
            this.lbl_Brand = new System.Windows.Forms.Label();
            this.cmb_Grade = new System.Windows.Forms.ComboBox();
            this.lbl_Grade = new System.Windows.Forms.Label();
            this.cmb_Treatment = new System.Windows.Forms.ComboBox();
            this.lbl_Treatment = new System.Windows.Forms.Label();
            this.cmb_MarketAttrib = new System.Windows.Forms.ComboBox();
            this.lbl_MarketAttrib = new System.Windows.Forms.Label();
            this.cmb_GrowingMethod = new System.Windows.Forms.ComboBox();
            this.lbl_GrowingMethod = new System.Windows.Forms.Label();
            this.cmb_Product_Group = new System.Windows.Forms.ComboBox();
            this.lbl_ProductGroup = new System.Windows.Forms.Label();
            this.lbl_Weight = new System.Windows.Forms.Label();
            this.nud_weight = new System.Windows.Forms.NumericUpDown();
            this.txt_Description = new System.Windows.Forms.TextBox();
            this.lbl_description = new System.Windows.Forms.Label();
            this.lbl_message = new System.Windows.Forms.Label();
            this.ckb_Active = new System.Windows.Forms.CheckBox();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.cmb_Pallet_Type = new System.Windows.Forms.ComboBox();
            this.lbl_Pallet_Type = new System.Windows.Forms.Label();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.btn_reltn = new System.Windows.Forms.Button();
            this.fruit1 = new FruPak.PF.Utils.UserControls.Fruit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_weight)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Material
            // 
            this.lbl_Material.AutoSize = true;
            this.lbl_Material.Location = new System.Drawing.Point(18, 59);
            this.lbl_Material.Name = "lbl_Material";
            this.lbl_Material.Size = new System.Drawing.Size(72, 13);
            this.lbl_Material.TabIndex = 0;
            this.lbl_Material.Text = "Material Num:";
            // 
            // txt_Material_No
            // 
            this.txt_Material_No.Location = new System.Drawing.Point(96, 56);
            this.txt_Material_No.MaxLength = 8;
            this.txt_Material_No.Name = "txt_Material_No";
            this.txt_Material_No.Size = new System.Drawing.Size(100, 20);
            this.txt_Material_No.TabIndex = 1;
            this.txt_Material_No.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            this.txt_Material_No.Leave += new System.EventHandler(this.txt_Material_No_Leave);
            // 
            // lbl_Trader
            // 
            this.lbl_Trader.AutoSize = true;
            this.lbl_Trader.Location = new System.Drawing.Point(18, 85);
            this.lbl_Trader.Name = "lbl_Trader";
            this.lbl_Trader.Size = new System.Drawing.Size(41, 13);
            this.lbl_Trader.TabIndex = 2;
            this.lbl_Trader.Text = "Trader:";
            // 
            // cmb_Trader
            // 
            this.cmb_Trader.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_Trader.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_Trader.FormattingEnabled = true;
            this.cmb_Trader.Location = new System.Drawing.Point(96, 82);
            this.cmb_Trader.Name = "cmb_Trader";
            this.cmb_Trader.Size = new System.Drawing.Size(189, 21);
            this.cmb_Trader.TabIndex = 2;
            this.cmb_Trader.SelectionChangeCommitted += new System.EventHandler(this.combo_SelectionChangeCommitted);
            this.cmb_Trader.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // cmb_PackType
            // 
            this.cmb_PackType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_PackType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_PackType.FormattingEnabled = true;
            this.cmb_PackType.Location = new System.Drawing.Point(96, 109);
            this.cmb_PackType.Name = "cmb_PackType";
            this.cmb_PackType.Size = new System.Drawing.Size(189, 21);
            this.cmb_PackType.TabIndex = 3;
            this.cmb_PackType.SelectionChangeCommitted += new System.EventHandler(this.combo_SelectionChangeCommitted);
            this.cmb_PackType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // lbl_PackType
            // 
            this.lbl_PackType.AutoSize = true;
            this.lbl_PackType.Location = new System.Drawing.Point(18, 112);
            this.lbl_PackType.Name = "lbl_PackType";
            this.lbl_PackType.Size = new System.Drawing.Size(62, 13);
            this.lbl_PackType.TabIndex = 5;
            this.lbl_PackType.Text = "Pack Type:";
            // 
            // cmb_Size
            // 
            this.cmb_Size.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_Size.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_Size.FormattingEnabled = true;
            this.cmb_Size.Location = new System.Drawing.Point(96, 136);
            this.cmb_Size.Name = "cmb_Size";
            this.cmb_Size.Size = new System.Drawing.Size(189, 21);
            this.cmb_Size.TabIndex = 4;
            this.cmb_Size.SelectionChangeCommitted += new System.EventHandler(this.combo_SelectionChangeCommitted);
            this.cmb_Size.SelectedValueChanged += new System.EventHandler(this.combo_SelectionChangeCommitted);
            this.cmb_Size.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // lbl_Size
            // 
            this.lbl_Size.AutoSize = true;
            this.lbl_Size.Location = new System.Drawing.Point(18, 139);
            this.lbl_Size.Name = "lbl_Size";
            this.lbl_Size.Size = new System.Drawing.Size(30, 13);
            this.lbl_Size.TabIndex = 7;
            this.lbl_Size.Text = "Size:";
            // 
            // cmb_Count
            // 
            this.cmb_Count.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_Count.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_Count.FormattingEnabled = true;
            this.cmb_Count.Location = new System.Drawing.Point(96, 163);
            this.cmb_Count.Name = "cmb_Count";
            this.cmb_Count.Size = new System.Drawing.Size(189, 21);
            this.cmb_Count.TabIndex = 5;
            this.cmb_Count.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // lbl_Count
            // 
            this.lbl_Count.AutoSize = true;
            this.lbl_Count.Location = new System.Drawing.Point(18, 166);
            this.lbl_Count.Name = "lbl_Count";
            this.lbl_Count.Size = new System.Drawing.Size(38, 13);
            this.lbl_Count.TabIndex = 9;
            this.lbl_Count.Text = "Count:";
            // 
            // cmb_Brand
            // 
            this.cmb_Brand.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_Brand.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_Brand.FormattingEnabled = true;
            this.cmb_Brand.Location = new System.Drawing.Point(406, 109);
            this.cmb_Brand.Name = "cmb_Brand";
            this.cmb_Brand.Size = new System.Drawing.Size(189, 21);
            this.cmb_Brand.TabIndex = 9;
            this.cmb_Brand.SelectionChangeCommitted += new System.EventHandler(this.combo_SelectionChangeCommitted);
            this.cmb_Brand.SelectedValueChanged += new System.EventHandler(this.combo_SelectionChangeCommitted);
            this.cmb_Brand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // lbl_Brand
            // 
            this.lbl_Brand.AutoSize = true;
            this.lbl_Brand.Location = new System.Drawing.Point(328, 112);
            this.lbl_Brand.Name = "lbl_Brand";
            this.lbl_Brand.Size = new System.Drawing.Size(38, 13);
            this.lbl_Brand.TabIndex = 11;
            this.lbl_Brand.Text = "Brand:";
            // 
            // cmb_Grade
            // 
            this.cmb_Grade.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_Grade.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_Grade.FormattingEnabled = true;
            this.cmb_Grade.Location = new System.Drawing.Point(406, 136);
            this.cmb_Grade.Name = "cmb_Grade";
            this.cmb_Grade.Size = new System.Drawing.Size(189, 21);
            this.cmb_Grade.TabIndex = 10;
            this.cmb_Grade.SelectionChangeCommitted += new System.EventHandler(this.combo_SelectionChangeCommitted);
            this.cmb_Grade.SelectedValueChanged += new System.EventHandler(this.combo_SelectionChangeCommitted);
            this.cmb_Grade.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // lbl_Grade
            // 
            this.lbl_Grade.AutoSize = true;
            this.lbl_Grade.Location = new System.Drawing.Point(328, 139);
            this.lbl_Grade.Name = "lbl_Grade";
            this.lbl_Grade.Size = new System.Drawing.Size(39, 13);
            this.lbl_Grade.TabIndex = 13;
            this.lbl_Grade.Text = "Grade:";
            // 
            // cmb_Treatment
            // 
            this.cmb_Treatment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_Treatment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_Treatment.FormattingEnabled = true;
            this.cmb_Treatment.Location = new System.Drawing.Point(406, 163);
            this.cmb_Treatment.Name = "cmb_Treatment";
            this.cmb_Treatment.Size = new System.Drawing.Size(189, 21);
            this.cmb_Treatment.TabIndex = 11;
            this.cmb_Treatment.SelectionChangeCommitted += new System.EventHandler(this.combo_SelectionChangeCommitted);
            this.cmb_Treatment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // lbl_Treatment
            // 
            this.lbl_Treatment.AutoSize = true;
            this.lbl_Treatment.Location = new System.Drawing.Point(328, 166);
            this.lbl_Treatment.Name = "lbl_Treatment";
            this.lbl_Treatment.Size = new System.Drawing.Size(58, 13);
            this.lbl_Treatment.TabIndex = 15;
            this.lbl_Treatment.Text = "Treatment:";
            // 
            // cmb_MarketAttrib
            // 
            this.cmb_MarketAttrib.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_MarketAttrib.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_MarketAttrib.FormattingEnabled = true;
            this.cmb_MarketAttrib.Location = new System.Drawing.Point(406, 190);
            this.cmb_MarketAttrib.Name = "cmb_MarketAttrib";
            this.cmb_MarketAttrib.Size = new System.Drawing.Size(189, 21);
            this.cmb_MarketAttrib.TabIndex = 12;
            this.cmb_MarketAttrib.SelectionChangeCommitted += new System.EventHandler(this.combo_SelectionChangeCommitted);
            this.cmb_MarketAttrib.SelectedValueChanged += new System.EventHandler(this.combo_SelectionChangeCommitted);
            this.cmb_MarketAttrib.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // lbl_MarketAttrib
            // 
            this.lbl_MarketAttrib.AutoSize = true;
            this.lbl_MarketAttrib.Location = new System.Drawing.Point(315, 193);
            this.lbl_MarketAttrib.Name = "lbl_MarketAttrib";
            this.lbl_MarketAttrib.Size = new System.Drawing.Size(85, 13);
            this.lbl_MarketAttrib.TabIndex = 17;
            this.lbl_MarketAttrib.Text = "Market Attribute:";
            // 
            // cmb_GrowingMethod
            // 
            this.cmb_GrowingMethod.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_GrowingMethod.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_GrowingMethod.FormattingEnabled = true;
            this.cmb_GrowingMethod.Location = new System.Drawing.Point(96, 190);
            this.cmb_GrowingMethod.Name = "cmb_GrowingMethod";
            this.cmb_GrowingMethod.Size = new System.Drawing.Size(189, 21);
            this.cmb_GrowingMethod.TabIndex = 6;
            this.cmb_GrowingMethod.SelectionChangeCommitted += new System.EventHandler(this.combo_SelectionChangeCommitted);
            this.cmb_GrowingMethod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // lbl_GrowingMethod
            // 
            this.lbl_GrowingMethod.AutoSize = true;
            this.lbl_GrowingMethod.Location = new System.Drawing.Point(5, 193);
            this.lbl_GrowingMethod.Name = "lbl_GrowingMethod";
            this.lbl_GrowingMethod.Size = new System.Drawing.Size(88, 13);
            this.lbl_GrowingMethod.TabIndex = 19;
            this.lbl_GrowingMethod.Text = "Growing Method:";
            // 
            // cmb_Product_Group
            // 
            this.cmb_Product_Group.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_Product_Group.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_Product_Group.FormattingEnabled = true;
            this.cmb_Product_Group.Location = new System.Drawing.Point(96, 217);
            this.cmb_Product_Group.Name = "cmb_Product_Group";
            this.cmb_Product_Group.Size = new System.Drawing.Size(189, 21);
            this.cmb_Product_Group.TabIndex = 7;
            this.cmb_Product_Group.SelectionChangeCommitted += new System.EventHandler(this.combo_SelectionChangeCommitted);
            this.cmb_Product_Group.SelectedValueChanged += new System.EventHandler(this.combo_SelectionChangeCommitted);
            this.cmb_Product_Group.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // lbl_ProductGroup
            // 
            this.lbl_ProductGroup.AutoSize = true;
            this.lbl_ProductGroup.Location = new System.Drawing.Point(5, 220);
            this.lbl_ProductGroup.Name = "lbl_ProductGroup";
            this.lbl_ProductGroup.Size = new System.Drawing.Size(79, 13);
            this.lbl_ProductGroup.TabIndex = 21;
            this.lbl_ProductGroup.Text = "Product Group:";
            // 
            // lbl_Weight
            // 
            this.lbl_Weight.AutoSize = true;
            this.lbl_Weight.Location = new System.Drawing.Point(315, 249);
            this.lbl_Weight.Name = "lbl_Weight";
            this.lbl_Weight.Size = new System.Drawing.Size(44, 13);
            this.lbl_Weight.TabIndex = 23;
            this.lbl_Weight.Text = "Weight:";
            // 
            // nud_weight
            // 
            this.nud_weight.DecimalPlaces = 3;
            this.nud_weight.Location = new System.Drawing.Point(406, 247);
            this.nud_weight.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nud_weight.Name = "nud_weight";
            this.nud_weight.Size = new System.Drawing.Size(77, 20);
            this.nud_weight.TabIndex = 14;
            this.nud_weight.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // txt_Description
            // 
            this.txt_Description.Location = new System.Drawing.Point(96, 288);
            this.txt_Description.Multiline = true;
            this.txt_Description.Name = "txt_Description";
            this.txt_Description.Size = new System.Drawing.Size(499, 85);
            this.txt_Description.TabIndex = 15;
            // 
            // lbl_description
            // 
            this.lbl_description.AutoSize = true;
            this.lbl_description.Location = new System.Drawing.Point(11, 291);
            this.lbl_description.Name = "lbl_description";
            this.lbl_description.Size = new System.Drawing.Size(63, 13);
            this.lbl_description.TabIndex = 26;
            this.lbl_description.Text = "Description:";
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(12, 9);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(0, 17);
            this.lbl_message.TabIndex = 27;
            // 
            // ckb_Active
            // 
            this.ckb_Active.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckb_Active.Checked = true;
            this.ckb_Active.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_Active.Location = new System.Drawing.Point(21, 379);
            this.ckb_Active.Name = "ckb_Active";
            this.ckb_Active.Size = new System.Drawing.Size(84, 24);
            this.ckb_Active.TabIndex = 16;
            this.ckb_Active.Text = "Active Ind:";
            this.ckb_Active.UseVisualStyleBackColor = true;
            this.ckb_Active.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            this.ckb_Active.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Enter_KeyPress);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(444, 429);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 20;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(282, 429);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_reset.TabIndex = 18;
            this.btn_reset.Text = "Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(120, 429);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 16;
            this.btn_Add.Text = "Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // cmb_Pallet_Type
            // 
            this.cmb_Pallet_Type.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_Pallet_Type.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_Pallet_Type.FormattingEnabled = true;
            this.cmb_Pallet_Type.Location = new System.Drawing.Point(406, 217);
            this.cmb_Pallet_Type.Name = "cmb_Pallet_Type";
            this.cmb_Pallet_Type.Size = new System.Drawing.Size(189, 21);
            this.cmb_Pallet_Type.TabIndex = 13;
            this.cmb_Pallet_Type.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // lbl_Pallet_Type
            // 
            this.lbl_Pallet_Type.AutoSize = true;
            this.lbl_Pallet_Type.Location = new System.Drawing.Point(315, 220);
            this.lbl_Pallet_Type.Name = "lbl_Pallet_Type";
            this.lbl_Pallet_Type.Size = new System.Drawing.Size(63, 13);
            this.lbl_Pallet_Type.TabIndex = 54;
            this.lbl_Pallet_Type.Text = "Pallet Type:";
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Location = new System.Drawing.Point(363, 429);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(75, 23);
            this.btn_Refresh.TabIndex = 19;
            this.btn_Refresh.Text = "Refresh";
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // btn_reltn
            // 
            this.btn_reltn.Location = new System.Drawing.Point(201, 418);
            this.btn_reltn.Name = "btn_reltn";
            this.btn_reltn.Size = new System.Drawing.Size(75, 34);
            this.btn_reltn.TabIndex = 17;
            this.btn_reltn.Text = "Maintain Relationships";
            this.btn_reltn.UseVisualStyleBackColor = true;
            this.btn_reltn.Click += new System.EventHandler(this.btn_reltn_Click);
            // 
            // fruit1
            // 
            this.fruit1.Block_Id = 0;
            this.fruit1.FruitType_Id = 1;
            this.fruit1.FruitVariety_Id = 0;
            this.fruit1.Location = new System.Drawing.Point(315, 37);
            this.fruit1.Name = "fruit1";
            this.fruit1.Size = new System.Drawing.Size(298, 66);
            this.fruit1.TabIndex = 8;
            this.fruit1.FruitVarietyChanged += new System.EventHandler(this.fruit1_FruitVarietyChanged);
            this.fruit1.FruitTypeChanged += new System.EventHandler(this.fruit1_FruitTypeChanged);
            // 
            // Material
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 480);
            this.Controls.Add(this.btn_reltn);
            this.Controls.Add(this.btn_Refresh);
            this.Controls.Add(this.cmb_Pallet_Type);
            this.Controls.Add(this.lbl_Pallet_Type);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.ckb_Active);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.lbl_description);
            this.Controls.Add(this.txt_Description);
            this.Controls.Add(this.nud_weight);
            this.Controls.Add(this.lbl_Weight);
            this.Controls.Add(this.cmb_Product_Group);
            this.Controls.Add(this.lbl_ProductGroup);
            this.Controls.Add(this.cmb_GrowingMethod);
            this.Controls.Add(this.lbl_GrowingMethod);
            this.Controls.Add(this.cmb_MarketAttrib);
            this.Controls.Add(this.lbl_MarketAttrib);
            this.Controls.Add(this.cmb_Treatment);
            this.Controls.Add(this.lbl_Treatment);
            this.Controls.Add(this.cmb_Grade);
            this.Controls.Add(this.lbl_Grade);
            this.Controls.Add(this.cmb_Brand);
            this.Controls.Add(this.lbl_Brand);
            this.Controls.Add(this.cmb_Count);
            this.Controls.Add(this.lbl_Count);
            this.Controls.Add(this.cmb_Size);
            this.Controls.Add(this.lbl_Size);
            this.Controls.Add(this.cmb_PackType);
            this.Controls.Add(this.lbl_PackType);
            this.Controls.Add(this.fruit1);
            this.Controls.Add(this.cmb_Trader);
            this.Controls.Add(this.lbl_Trader);
            this.Controls.Add(this.txt_Material_No);
            this.Controls.Add(this.lbl_Material);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Material";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FruPak.PF.Utils.Common.Material";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Material_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.nud_weight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Material;
        private System.Windows.Forms.TextBox txt_Material_No;
        private System.Windows.Forms.Label lbl_Trader;
        private System.Windows.Forms.ComboBox cmb_Trader;
        private UserControls.Fruit fruit1;
        private System.Windows.Forms.ComboBox cmb_PackType;
        private System.Windows.Forms.Label lbl_PackType;
        private System.Windows.Forms.ComboBox cmb_Size;
        private System.Windows.Forms.Label lbl_Size;
        private System.Windows.Forms.ComboBox cmb_Count;
        private System.Windows.Forms.Label lbl_Count;
        private System.Windows.Forms.ComboBox cmb_Brand;
        private System.Windows.Forms.Label lbl_Brand;
        private System.Windows.Forms.ComboBox cmb_Grade;
        private System.Windows.Forms.Label lbl_Grade;
        private System.Windows.Forms.ComboBox cmb_Treatment;
        private System.Windows.Forms.Label lbl_Treatment;
        private System.Windows.Forms.ComboBox cmb_MarketAttrib;
        private System.Windows.Forms.Label lbl_MarketAttrib;
        private System.Windows.Forms.ComboBox cmb_GrowingMethod;
        private System.Windows.Forms.Label lbl_GrowingMethod;
        private System.Windows.Forms.ComboBox cmb_Product_Group;
        private System.Windows.Forms.Label lbl_ProductGroup;
        private System.Windows.Forms.Label lbl_Weight;
        private System.Windows.Forms.NumericUpDown nud_weight;
        private System.Windows.Forms.TextBox txt_Description;
        private System.Windows.Forms.Label lbl_description;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.CheckBox ckb_Active;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.ComboBox cmb_Pallet_Type;
        private System.Windows.Forms.Label lbl_Pallet_Type;
        private System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.Button btn_reltn;
    }
}