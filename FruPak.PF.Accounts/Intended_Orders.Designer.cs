namespace FruPak.PF.Accounts
{
    partial class Intended_Orders
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Intended_Orders));
            this.btn_Add = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lbl_message = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_Material_Num = new System.Windows.Forms.ComboBox();
            this.nud_Quantity = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nud_Unit_Price = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Comments = new System.Windows.Forms.RichTextBox();
            this.ckb_Active = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_Customer_Order = new System.Windows.Forms.TextBox();
            this.cmb_Fruit_Type = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.customer1 = new FruPak.PF.Utils.UserControls.Customer();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Quantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Unit_Price)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(570, 534);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 10;
            this.btn_Add.Text = "Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(732, 534);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 12;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(651, 534);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_reset.TabIndex = 11;
            this.btn_reset.Text = "Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(16, 49);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(790, 324);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(12, 9);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(2, 19);
            this.lbl_message.TabIndex = 45;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 447);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "Material Number:";
            // 
            // cmb_Material_Num
            // 
            this.cmb_Material_Num.FormattingEnabled = true;
            this.cmb_Material_Num.Location = new System.Drawing.Point(101, 444);
            this.cmb_Material_Num.Name = "cmb_Material_Num";
            this.cmb_Material_Num.Size = new System.Drawing.Size(289, 21);
            this.cmb_Material_Num.TabIndex = 4;
            // 
            // nud_Quantity
            // 
            this.nud_Quantity.DecimalPlaces = 3;
            this.nud_Quantity.Location = new System.Drawing.Point(63, 522);
            this.nud_Quantity.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            196608});
            this.nud_Quantity.Name = "nud_Quantity";
            this.nud_Quantity.Size = new System.Drawing.Size(120, 20);
            this.nud_Quantity.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(208, 526);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 51;
            this.label3.Text = "Unit Price:";
            // 
            // nud_Unit_Price
            // 
            this.nud_Unit_Price.DecimalPlaces = 2;
            this.nud_Unit_Price.Location = new System.Drawing.Point(270, 524);
            this.nud_Unit_Price.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            131072});
            this.nud_Unit_Price.Name = "nud_Unit_Price";
            this.nud_Unit_Price.Size = new System.Drawing.Size(120, 20);
            this.nud_Unit_Price.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(417, 380);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 53;
            this.label4.Text = " Comments";
            // 
            // txt_Comments
            // 
            this.txt_Comments.Location = new System.Drawing.Point(479, 380);
            this.txt_Comments.Name = "txt_Comments";
            this.txt_Comments.Size = new System.Drawing.Size(327, 96);
            this.txt_Comments.TabIndex = 8;
            this.txt_Comments.Text = "";
            // 
            // ckb_Active
            // 
            this.ckb_Active.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckb_Active.Checked = true;
            this.ckb_Active.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_Active.Location = new System.Drawing.Point(420, 482);
            this.ckb_Active.Name = "ckb_Active";
            this.ckb_Active.Size = new System.Drawing.Size(84, 24);
            this.ckb_Active.TabIndex = 9;
            this.ckb_Active.Text = "Active Ind:";
            this.ckb_Active.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 484);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 13);
            this.label5.TabIndex = 56;
            this.label5.Text = "Customer Order Number:";
            // 
            // txt_Customer_Order
            // 
            this.txt_Customer_Order.Location = new System.Drawing.Point(141, 481);
            this.txt_Customer_Order.Name = "txt_Customer_Order";
            this.txt_Customer_Order.Size = new System.Drawing.Size(100, 20);
            this.txt_Customer_Order.TabIndex = 5;
            // 
            // cmb_Fruit_Type
            // 
            this.cmb_Fruit_Type.FormattingEnabled = true;
            this.cmb_Fruit_Type.Location = new System.Drawing.Point(101, 414);
            this.cmb_Fruit_Type.Name = "cmb_Fruit_Type";
            this.cmb_Fruit_Type.Size = new System.Drawing.Size(201, 21);
            this.cmb_Fruit_Type.TabIndex = 3;
            this.cmb_Fruit_Type.SelectionChangeCommitted += new System.EventHandler(this.cmb_Fruit_Type_SelectionChangeCommitted);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 417);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 58;
            this.label6.Text = "Fruit Type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 524);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 49;
            this.label2.Text = "Quantity:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 537);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 60;
            this.label7.Text = "(in Kgs)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(208, 539);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 61;
            this.label8.Text = "(per Kg)";
            // 
            // customer1
            // 
            this.customer1.CFocused = false;
            this.customer1.Customer_Id = 0;
            this.customer1.Location = new System.Drawing.Point(13, 380);
            this.customer1.Name = "customer1";
            this.customer1.Size = new System.Drawing.Size(294, 28);
            this.customer1.TabIndex = 2;
            // 
            // Intended_Orders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 572);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmb_Fruit_Type);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_Customer_Order);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ckb_Active);
            this.Controls.Add(this.txt_Comments);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nud_Unit_Price);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nud_Quantity);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmb_Material_Num);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.customer1);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_reset);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Intended_Orders";
            this.Text = "FruPak.PF.Accounts.Intended Orders";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Intended_Orders_FormClosing);
            this.Load += new System.EventHandler(this.Intended_Orders_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Intended_Orders_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Quantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Unit_Price)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lbl_message;
        private Utils.UserControls.Customer customer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_Material_Num;
        private System.Windows.Forms.NumericUpDown nud_Quantity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nud_Unit_Price;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox txt_Comments;
        private System.Windows.Forms.CheckBox ckb_Active;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_Customer_Order;
        private System.Windows.Forms.ComboBox cmb_Fruit_Type;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}