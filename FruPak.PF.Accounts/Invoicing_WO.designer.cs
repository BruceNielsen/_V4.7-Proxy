namespace FruPak.PF.Accounts
{
    partial class Invoicing_WO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Invoicing_WO));
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.lbl_message = new System.Windows.Forms.Label();
            this.lbl_Trader = new System.Windows.Forms.Label();
            this.cmb_Trader = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmb_Rates = new System.Windows.Forms.ComboBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Add_Rates = new System.Windows.Forms.Button();
            this.txt_comments = new System.Windows.Forms.TextBox();
            this.lbl_comments = new System.Windows.Forms.Label();
            this.btn_email = new System.Windows.Forms.Button();
            this.btn_Print = new System.Windows.Forms.Button();
            this.lbl_Order = new System.Windows.Forms.Label();
            this.txt_order_num = new System.Windows.Forms.TextBox();
            this.dtp_Invoice_Date = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.lbl_freight_charge = new System.Windows.Forms.Label();
            this.nud_freight = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_freight)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(919, 465);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 13;
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(838, 465);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_reset.TabIndex = 12;
            this.btn_reset.Text = "&Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(367, 144);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(156, 23);
            this.btn_Add.TabIndex = 5;
            this.btn_Add.Text = "Add &Work Orders to Invoice";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(12, 9);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(2, 19);
            this.lbl_message.TabIndex = 27;
            // 
            // lbl_Trader
            // 
            this.lbl_Trader.AutoSize = true;
            this.lbl_Trader.Location = new System.Drawing.Point(16, 84);
            this.lbl_Trader.Name = "lbl_Trader";
            this.lbl_Trader.Size = new System.Drawing.Size(41, 13);
            this.lbl_Trader.TabIndex = 28;
            this.lbl_Trader.Text = "Trader:";
            // 
            // cmb_Trader
            // 
            this.cmb_Trader.FormattingEnabled = true;
            this.cmb_Trader.Location = new System.Drawing.Point(89, 81);
            this.cmb_Trader.Name = "cmb_Trader";
            this.cmb_Trader.Size = new System.Drawing.Size(164, 21);
            this.cmb_Trader.TabIndex = 2;
            this.cmb_Trader.SelectedIndexChanged += new System.EventHandler(this.cmb_Trader_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 255);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(513, 224);
            this.dataGridView1.TabIndex = 35;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // cmb_Rates
            // 
            this.cmb_Rates.FormattingEnabled = true;
            this.cmb_Rates.Location = new System.Drawing.Point(16, 186);
            this.cmb_Rates.Name = "cmb_Rates";
            this.cmb_Rates.Size = new System.Drawing.Size(345, 21);
            this.cmb_Rates.TabIndex = 3;
            this.cmb_Rates.Visible = false;
            this.cmb_Rates.SelectionChangeCommitted += new System.EventHandler(this.cmb_Rates_SelectionChangeCommitted);
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(595, 465);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 9;
            this.btn_Save.Text = "&Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Add_Rates
            // 
            this.btn_Add_Rates.Location = new System.Drawing.Point(367, 184);
            this.btn_Add_Rates.Name = "btn_Add_Rates";
            this.btn_Add_Rates.Size = new System.Drawing.Size(158, 23);
            this.btn_Add_Rates.TabIndex = 6;
            this.btn_Add_Rates.Text = "&Add Extra charges to Invoice";
            this.btn_Add_Rates.UseVisualStyleBackColor = true;
            this.btn_Add_Rates.Visible = false;
            this.btn_Add_Rates.Click += new System.EventHandler(this.btn_Add_Rates_Click);
            // 
            // txt_comments
            // 
            this.txt_comments.Location = new System.Drawing.Point(622, 255);
            this.txt_comments.Multiline = true;
            this.txt_comments.Name = "txt_comments";
            this.txt_comments.Size = new System.Drawing.Size(374, 166);
            this.txt_comments.TabIndex = 8;
            // 
            // lbl_comments
            // 
            this.lbl_comments.AutoSize = true;
            this.lbl_comments.Location = new System.Drawing.Point(544, 255);
            this.lbl_comments.Name = "lbl_comments";
            this.lbl_comments.Size = new System.Drawing.Size(59, 13);
            this.lbl_comments.TabIndex = 39;
            this.lbl_comments.Text = "Comments:";
            // 
            // btn_email
            // 
            this.btn_email.Location = new System.Drawing.Point(676, 465);
            this.btn_email.Name = "btn_email";
            this.btn_email.Size = new System.Drawing.Size(75, 23);
            this.btn_email.TabIndex = 10;
            this.btn_email.Text = "&Email";
            this.btn_email.UseVisualStyleBackColor = true;
            this.btn_email.Visible = false;
            this.btn_email.Click += new System.EventHandler(this.btn_email_Click);
            // 
            // btn_Print
            // 
            this.btn_Print.Location = new System.Drawing.Point(757, 465);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(75, 23);
            this.btn_Print.TabIndex = 11;
            this.btn_Print.Text = "&Print";
            this.btn_Print.UseVisualStyleBackColor = true;
            this.btn_Print.Visible = false;
            this.btn_Print.Click += new System.EventHandler(this.btn_Print_Click);
            // 
            // lbl_Order
            // 
            this.lbl_Order.AutoSize = true;
            this.lbl_Order.Location = new System.Drawing.Point(540, 217);
            this.lbl_Order.Name = "lbl_Order";
            this.lbl_Order.Size = new System.Drawing.Size(76, 13);
            this.lbl_Order.TabIndex = 42;
            this.lbl_Order.Text = "Order Number:";
            // 
            // txt_order_num
            // 
            this.txt_order_num.Location = new System.Drawing.Point(622, 214);
            this.txt_order_num.MaxLength = 20;
            this.txt_order_num.Name = "txt_order_num";
            this.txt_order_num.Size = new System.Drawing.Size(142, 20);
            this.txt_order_num.TabIndex = 7;
            // 
            // dtp_Invoice_Date
            // 
            this.dtp_Invoice_Date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_Invoice_Date.Location = new System.Drawing.Point(90, 44);
            this.dtp_Invoice_Date.Name = "dtp_Invoice_Date";
            this.dtp_Invoice_Date.Size = new System.Drawing.Size(111, 20);
            this.dtp_Invoice_Date.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 66;
            this.label1.Text = "Invoice Date";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(259, 41);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.Size = new System.Drawing.Size(666, 97);
            this.dataGridView2.TabIndex = 68;
            // 
            // lbl_freight_charge
            // 
            this.lbl_freight_charge.AutoSize = true;
            this.lbl_freight_charge.Location = new System.Drawing.Point(159, 221);
            this.lbl_freight_charge.Name = "lbl_freight_charge";
            this.lbl_freight_charge.Size = new System.Drawing.Size(79, 13);
            this.lbl_freight_charge.TabIndex = 70;
            this.lbl_freight_charge.Text = "Freight Charge:";
            this.lbl_freight_charge.Visible = false;
            // 
            // nud_freight
            // 
            this.nud_freight.DecimalPlaces = 2;
            this.nud_freight.Location = new System.Drawing.Point(241, 217);
            this.nud_freight.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nud_freight.Name = "nud_freight";
            this.nud_freight.Size = new System.Drawing.Size(120, 20);
            this.nud_freight.TabIndex = 4;
            this.nud_freight.Visible = false;
            // 
            // Invoicing_WO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 549);
            this.Controls.Add(this.lbl_freight_charge);
            this.Controls.Add(this.nud_freight);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dtp_Invoice_Date);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_order_num);
            this.Controls.Add(this.lbl_Order);
            this.Controls.Add(this.btn_Print);
            this.Controls.Add(this.btn_email);
            this.Controls.Add(this.lbl_comments);
            this.Controls.Add(this.txt_comments);
            this.Controls.Add(this.btn_Add_Rates);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.cmb_Rates);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cmb_Trader);
            this.Controls.Add(this.lbl_Trader);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_Add);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Invoicing_WO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FruPak.PF.Accounts.Invoicing Work Order";
            this.Load += new System.EventHandler(this.SizeAllColumns);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Invoicing_WO_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_freight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.Label lbl_Trader;
        private System.Windows.Forms.ComboBox cmb_Trader;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cmb_Rates;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_Add_Rates;
        private System.Windows.Forms.TextBox txt_comments;
        private System.Windows.Forms.Label lbl_comments;
        private System.Windows.Forms.Button btn_email;
        private System.Windows.Forms.Button btn_Print;
        private System.Windows.Forms.Label lbl_Order;
        private System.Windows.Forms.TextBox txt_order_num;
        private System.Windows.Forms.DateTimePicker dtp_Invoice_Date;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label lbl_freight_charge;
        private System.Windows.Forms.NumericUpDown nud_freight;
    }
}

