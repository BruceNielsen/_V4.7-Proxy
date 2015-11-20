namespace PF.WorkOrder
{
    partial class Work_Order_Maintenance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Work_Order_Maintenance));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lbl_message = new System.Windows.Forms.Label();
            this.lbl_Code = new System.Windows.Forms.Label();
            this.txt_code = new System.Windows.Forms.TextBox();
            this.lbl_Description = new System.Windows.Forms.Label();
            this.txt_Description = new System.Windows.Forms.TextBox();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.btn_Update_Relationship = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.ckb_Active = new System.Windows.Forms.CheckBox();
            this.rb_PreOpCheck = new System.Windows.Forms.RadioButton();
            this.rb_batch = new System.Windows.Forms.RadioButton();
            this.rb_Other = new System.Windows.Forms.RadioButton();
            this.lbl_Value = new System.Windows.Forms.Label();
            this.txt_Value = new System.Windows.Forms.TextBox();
            this.lbl_Product = new System.Windows.Forms.Label();
            this.cmb_Product = new System.Windows.Forms.ComboBox();
            this.cmb_Code = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkedListBox2 = new System.Windows.Forms.CheckedListBox();
            this.fruit1 = new PF.Utils.UserControls.Fruit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 38);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(543, 531);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(9, 9);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(0, 17);
            this.lbl_message.TabIndex = 1;
            // 
            // lbl_Code
            // 
            this.lbl_Code.AutoSize = true;
            this.lbl_Code.Location = new System.Drawing.Point(587, 126);
            this.lbl_Code.Name = "lbl_Code";
            this.lbl_Code.Size = new System.Drawing.Size(35, 13);
            this.lbl_Code.TabIndex = 2;
            this.lbl_Code.Text = "Code:";
            // 
            // txt_code
            // 
            this.txt_code.Location = new System.Drawing.Point(652, 124);
            this.txt_code.MaxLength = 10;
            this.txt_code.Name = "txt_code";
            this.txt_code.Size = new System.Drawing.Size(123, 20);
            this.txt_code.TabIndex = 3;
            // 
            // lbl_Description
            // 
            this.lbl_Description.AutoSize = true;
            this.lbl_Description.Location = new System.Drawing.Point(578, 153);
            this.lbl_Description.Name = "lbl_Description";
            this.lbl_Description.Size = new System.Drawing.Size(63, 13);
            this.lbl_Description.TabIndex = 4;
            this.lbl_Description.Text = "Description:";
            // 
            // txt_Description
            // 
            this.txt_Description.Location = new System.Drawing.Point(647, 150);
            this.txt_Description.Multiline = true;
            this.txt_Description.Name = "txt_Description";
            this.txt_Description.Size = new System.Drawing.Size(340, 85);
            this.txt_Description.TabIndex = 5;
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(897, 265);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 13;
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(805, 265);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_reset.TabIndex = 12;
            this.btn_reset.Text = "&Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(714, 265);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 11;
            this.btn_Add.Text = "&Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // btn_Update_Relationship
            // 
            this.btn_Update_Relationship.Location = new System.Drawing.Point(749, 546);
            this.btn_Update_Relationship.Name = "btn_Update_Relationship";
            this.btn_Update_Relationship.Size = new System.Drawing.Size(131, 23);
            this.btn_Update_Relationship.TabIndex = 15;
            this.btn_Update_Relationship.Text = "&Update Relationship";
            this.btn_Update_Relationship.UseVisualStyleBackColor = true;
            this.btn_Update_Relationship.Visible = false;
            this.btn_Update_Relationship.Click += new System.EventHandler(this.btn_Update_Relationship_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(6, 6);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(320, 169);
            this.checkedListBox1.TabIndex = 11;
            this.checkedListBox1.Visible = false;
            // 
            // ckb_Active
            // 
            this.ckb_Active.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckb_Active.Checked = true;
            this.ckb_Active.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_Active.Location = new System.Drawing.Point(581, 264);
            this.ckb_Active.Name = "ckb_Active";
            this.ckb_Active.Size = new System.Drawing.Size(84, 24);
            this.ckb_Active.TabIndex = 10;
            this.ckb_Active.Text = "Active &Ind:";
            this.ckb_Active.UseVisualStyleBackColor = true;
            this.ckb_Active.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            this.ckb_Active.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Enter_KeyPress);
            // 
            // rb_PreOpCheck
            // 
            this.rb_PreOpCheck.AutoSize = true;
            this.rb_PreOpCheck.Location = new System.Drawing.Point(788, 241);
            this.rb_PreOpCheck.Name = "rb_PreOpCheck";
            this.rb_PreOpCheck.Size = new System.Drawing.Size(92, 17);
            this.rb_PreOpCheck.TabIndex = 7;
            this.rb_PreOpCheck.TabStop = true;
            this.rb_PreOpCheck.Text = "&Pre Op Check";
            this.rb_PreOpCheck.UseVisualStyleBackColor = true;
            this.rb_PreOpCheck.Visible = false;
            this.rb_PreOpCheck.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // rb_batch
            // 
            this.rb_batch.AutoSize = true;
            this.rb_batch.Location = new System.Drawing.Point(886, 241);
            this.rb_batch.Name = "rb_batch";
            this.rb_batch.Size = new System.Drawing.Size(53, 17);
            this.rb_batch.TabIndex = 8;
            this.rb_batch.TabStop = true;
            this.rb_batch.Text = "&Batch";
            this.rb_batch.UseVisualStyleBackColor = true;
            this.rb_batch.Visible = false;
            this.rb_batch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // rb_Other
            // 
            this.rb_Other.AutoSize = true;
            this.rb_Other.Location = new System.Drawing.Point(945, 241);
            this.rb_Other.Name = "rb_Other";
            this.rb_Other.Size = new System.Drawing.Size(51, 17);
            this.rb_Other.TabIndex = 9;
            this.rb_Other.TabStop = true;
            this.rb_Other.Text = "&Other";
            this.rb_Other.UseVisualStyleBackColor = true;
            this.rb_Other.Visible = false;
            this.rb_Other.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // lbl_Value
            // 
            this.lbl_Value.AutoSize = true;
            this.lbl_Value.Location = new System.Drawing.Point(578, 241);
            this.lbl_Value.Name = "lbl_Value";
            this.lbl_Value.Size = new System.Drawing.Size(37, 13);
            this.lbl_Value.TabIndex = 41;
            this.lbl_Value.Text = "Value:";
            this.lbl_Value.Visible = false;
            // 
            // txt_Value
            // 
            this.txt_Value.Location = new System.Drawing.Point(647, 238);
            this.txt_Value.MaxLength = 10;
            this.txt_Value.Name = "txt_Value";
            this.txt_Value.Size = new System.Drawing.Size(123, 20);
            this.txt_Value.TabIndex = 6;
            this.txt_Value.Visible = false;
            this.txt_Value.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // lbl_Product
            // 
            this.lbl_Product.AutoSize = true;
            this.lbl_Product.Location = new System.Drawing.Point(587, 38);
            this.lbl_Product.Name = "lbl_Product";
            this.lbl_Product.Size = new System.Drawing.Size(47, 13);
            this.lbl_Product.TabIndex = 43;
            this.lbl_Product.Text = "Product:";
            this.lbl_Product.Visible = false;
            // 
            // cmb_Product
            // 
            this.cmb_Product.FormattingEnabled = true;
            this.cmb_Product.Location = new System.Drawing.Point(647, 35);
            this.cmb_Product.Name = "cmb_Product";
            this.cmb_Product.Size = new System.Drawing.Size(121, 21);
            this.cmb_Product.TabIndex = 2;
            this.cmb_Product.Visible = false;
            this.cmb_Product.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // cmb_Code
            // 
            this.cmb_Code.FormattingEnabled = true;
            this.cmb_Code.Location = new System.Drawing.Point(647, 123);
            this.cmb_Code.Name = "cmb_Code";
            this.cmb_Code.Size = new System.Drawing.Size(121, 21);
            this.cmb_Code.TabIndex = 4;
            this.cmb_Code.Visible = false;
            this.cmb_Code.SelectionChangeCommitted += new System.EventHandler(this.cmb_Code_SelectionChangeCommitted);
            this.cmb_Code.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(647, 308);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(340, 214);
            this.tabControl1.TabIndex = 14;
            this.tabControl1.Visible = false;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.checkedListBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(332, 188);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.Controls.Add(this.checkedListBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(332, 188);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            // 
            // checkedListBox2
            // 
            this.checkedListBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBox2.FormattingEnabled = true;
            this.checkedListBox2.Location = new System.Drawing.Point(6, 10);
            this.checkedListBox2.Name = "checkedListBox2";
            this.checkedListBox2.Size = new System.Drawing.Size(320, 169);
            this.checkedListBox2.TabIndex = 24;
            this.checkedListBox2.Visible = false;
            // 
            // fruit1
            // 
            this.fruit1.Block_Id = 0;
            this.fruit1.FruitType_Id = 0;
            this.fruit1.FruitVariety_Id = 0;
            this.fruit1.Location = new System.Drawing.Point(590, 62);
            this.fruit1.Name = "fruit1";
            this.fruit1.Size = new System.Drawing.Size(265, 55);
            this.fruit1.TabIndex = 3;
            this.fruit1.Visible = false;
            // 
            // Work_Order_Maintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 581);
            this.Controls.Add(this.fruit1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.cmb_Code);
            this.Controls.Add(this.cmb_Product);
            this.Controls.Add(this.lbl_Product);
            this.Controls.Add(this.txt_Value);
            this.Controls.Add(this.lbl_Value);
            this.Controls.Add(this.rb_Other);
            this.Controls.Add(this.rb_batch);
            this.Controls.Add(this.rb_PreOpCheck);
            this.Controls.Add(this.ckb_Active);
            this.Controls.Add(this.btn_Update_Relationship);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.txt_Description);
            this.Controls.Add(this.lbl_Description);
            this.Controls.Add(this.txt_code);
            this.Controls.Add(this.lbl_Code);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Work_Order_Maintenance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PF.WorkOrder.Work Order Maintenance";
            this.Load += new System.EventHandler(this.SizeAllColumns);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Work_Order_Maintenance_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.Label lbl_Code;
        private System.Windows.Forms.TextBox txt_code;
        private System.Windows.Forms.Label lbl_Description;
        private System.Windows.Forms.TextBox txt_Description;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btn_Update_Relationship;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.CheckBox ckb_Active;
        private System.Windows.Forms.RadioButton rb_PreOpCheck;
        private System.Windows.Forms.RadioButton rb_batch;
        private System.Windows.Forms.RadioButton rb_Other;
        private System.Windows.Forms.Label lbl_Value;
        private System.Windows.Forms.TextBox txt_Value;
        private System.Windows.Forms.Label lbl_Product;
        private System.Windows.Forms.ComboBox cmb_Product;
        private System.Windows.Forms.ComboBox cmb_Code;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckedListBox checkedListBox2;
        private Utils.UserControls.Fruit fruit1;
    }
}

