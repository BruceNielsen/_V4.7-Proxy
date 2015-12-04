namespace PF.Accounts
{
    partial class Sales_Rates
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sales_Rates));
            this.lbl_message = new System.Windows.Forms.Label();
            this.btn_Add = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.cmb_Material = new System.Windows.Forms.ComboBox();
            this.lbl_product = new System.Windows.Forms.Label();
            this.lbl_price = new System.Windows.Forms.Label();
            this.txt_Price = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ckb_Active = new System.Windows.Forms.CheckBox();
            this.customer1 = new PF.Utils.UserControls.Customer();
            this.button1 = new System.Windows.Forms.Button();
            this.labelMaterialCopy = new System.Windows.Forms.Label();
            this.buttonPasteMissingRateInfo = new System.Windows.Forms.Button();
            this.labelMissingRatesMaterial = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(13, 11);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(14, 19);
            this.lbl_message.TabIndex = 30;
            this.lbl_message.Text = ".";
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(1101, 656);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 5;
            this.btn_Add.Text = "&Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(1263, 656);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 7;
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(1182, 656);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_reset.TabIndex = 6;
            this.btn_reset.Text = "&Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // cmb_Material
            // 
            this.cmb_Material.FormattingEnabled = true;
            this.cmb_Material.Location = new System.Drawing.Point(102, 61);
            this.cmb_Material.Name = "cmb_Material";
            this.cmb_Material.Size = new System.Drawing.Size(1236, 21);
            this.cmb_Material.TabIndex = 2;
            this.cmb_Material.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // lbl_product
            // 
            this.lbl_product.AutoSize = true;
            this.lbl_product.Location = new System.Drawing.Point(12, 64);
            this.lbl_product.Name = "lbl_product";
            this.lbl_product.Size = new System.Drawing.Size(84, 13);
            this.lbl_product.TabIndex = 46;
            this.lbl_product.Text = "Material Number";
            // 
            // lbl_price
            // 
            this.lbl_price.AutoSize = true;
            this.lbl_price.Location = new System.Drawing.Point(1223, 611);
            this.lbl_price.Name = "lbl_price";
            this.lbl_price.Size = new System.Drawing.Size(34, 13);
            this.lbl_price.TabIndex = 47;
            this.lbl_price.Text = "Price:";
            // 
            // txt_Price
            // 
            this.txt_Price.Location = new System.Drawing.Point(1263, 606);
            this.txt_Price.Name = "txt_Price";
            this.txt_Price.Size = new System.Drawing.Size(75, 20);
            this.txt_Price.TabIndex = 3;
            this.txt_Price.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 106);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(1326, 494);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // ckb_Active
            // 
            this.ckb_Active.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckb_Active.Checked = true;
            this.ckb_Active.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_Active.Location = new System.Drawing.Point(1101, 606);
            this.ckb_Active.Name = "ckb_Active";
            this.ckb_Active.Size = new System.Drawing.Size(84, 24);
            this.ckb_Active.TabIndex = 4;
            this.ckb_Active.Text = "Active &Ind:";
            this.ckb_Active.UseVisualStyleBackColor = true;
            this.ckb_Active.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            this.ckb_Active.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Enter_KeyPress);
            // 
            // customer1
            // 
            this.customer1.CFocused = false;
            this.customer1.Customer_Id = 0;
            this.customer1.Location = new System.Drawing.Point(12, 33);
            this.customer1.Name = "customer1";
            this.customer1.Size = new System.Drawing.Size(657, 28);
            this.customer1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(915, 656);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(152, 23);
            this.button1.TabIndex = 48;
            this.button1.Text = "ArgumentException";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelMaterialCopy
            // 
            this.labelMaterialCopy.AutoSize = true;
            this.labelMaterialCopy.Location = new System.Drawing.Point(432, 661);
            this.labelMaterialCopy.Name = "labelMaterialCopy";
            this.labelMaterialCopy.Size = new System.Drawing.Size(10, 13);
            this.labelMaterialCopy.TabIndex = 50;
            this.labelMaterialCopy.Text = ".";
            // 
            // buttonPasteMissingRateInfo
            // 
            this.buttonPasteMissingRateInfo.Location = new System.Drawing.Point(1124, 33);
            this.buttonPasteMissingRateInfo.Name = "buttonPasteMissingRateInfo";
            this.buttonPasteMissingRateInfo.Size = new System.Drawing.Size(214, 23);
            this.buttonPasteMissingRateInfo.TabIndex = 51;
            this.buttonPasteMissingRateInfo.Text = "Paste Missing Rate Info";
            this.buttonPasteMissingRateInfo.UseVisualStyleBackColor = true;
            this.buttonPasteMissingRateInfo.Click += new System.EventHandler(this.buttonPasteMissingRateInfo_Click);
            // 
            // labelMissingRatesMaterial
            // 
            this.labelMissingRatesMaterial.AutoSize = true;
            this.labelMissingRatesMaterial.Location = new System.Drawing.Point(99, 90);
            this.labelMissingRatesMaterial.Name = "labelMissingRatesMaterial";
            this.labelMissingRatesMaterial.Size = new System.Drawing.Size(10, 13);
            this.labelMissingRatesMaterial.TabIndex = 52;
            this.labelMissingRatesMaterial.Text = ".";
            // 
            // Sales_Rates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 691);
            this.Controls.Add(this.labelMissingRatesMaterial);
            this.Controls.Add(this.buttonPasteMissingRateInfo);
            this.Controls.Add(this.labelMaterialCopy);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.customer1);
            this.Controls.Add(this.ckb_Active);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txt_Price);
            this.Controls.Add(this.lbl_price);
            this.Controls.Add(this.lbl_product);
            this.Controls.Add(this.cmb_Material);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.lbl_message);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Sales_Rates";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PF.Accounts.Sales Rates";
            this.Load += new System.EventHandler(this.SizeAllColumns);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Sales_Rates_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.ComboBox cmb_Material;
        private System.Windows.Forms.Label lbl_product;
        private System.Windows.Forms.Label lbl_price;
        private System.Windows.Forms.TextBox txt_Price;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox ckb_Active;
        private Utils.UserControls.Customer customer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelMaterialCopy;
        private System.Windows.Forms.Button buttonPasteMissingRateInfo;
        public System.Windows.Forms.Label labelMissingRatesMaterial;
    }
}