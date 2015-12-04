namespace PF.Accounts
{
    partial class Invoicing_Sales
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Invoicing_Sales));
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.lbl_message = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_Save = new System.Windows.Forms.Button();
            this.txt_comments = new System.Windows.Forms.TextBox();
            this.lbl_comments = new System.Windows.Forms.Label();
            this.btn_email = new System.Windows.Forms.Button();
            this.btn_Print = new System.Windows.Forms.Button();
            this.lbl_Order = new System.Windows.Forms.Label();
            this.txt_order_num = new System.Windows.Forms.TextBox();
            this.btn_View = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.ckb_COA = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtp_Invoice_Date = new System.Windows.Forms.DateTimePicker();
            this.btn_Add_Rates = new System.Windows.Forms.Button();
            this.cmb_Rates = new System.Windows.Forms.ComboBox();
            this.nud_freight = new System.Windows.Forms.NumericUpDown();
            this.lbl_freight_charge = new System.Windows.Forms.Label();
            this.btn_delete = new System.Windows.Forms.Button();
            this.buttonSendDebugInfo = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buttonOpenPdfFileLocation = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.numericUpDownQuantity = new System.Windows.Forms.NumericUpDown();
            this.labelQuantity = new System.Windows.Forms.Label();
            this.buttonCopyTheMaterialEntry = new System.Windows.Forms.Button();
            this.labelMaterialCopy = new System.Windows.Forms.Label();
            this.labelCopyInstructions = new System.Windows.Forms.Label();
            this.customer1 = new PF.Utils.UserControls.Customer();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_freight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(1283, 598);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(55, 23);
            this.btn_Close.TabIndex = 17;
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(1222, 598);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(55, 23);
            this.btn_reset.TabIndex = 16;
            this.btn_reset.Text = "&Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(552, 147);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(143, 23);
            this.btn_Add.TabIndex = 8;
            this.btn_Add.Text = "&Add Orders to Invoice";
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
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 217);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(1326, 362);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(919, 598);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(55, 23);
            this.btn_Save.TabIndex = 12;
            this.btn_Save.Text = "&Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // txt_comments
            // 
            this.txt_comments.Location = new System.Drawing.Point(12, 598);
            this.txt_comments.Multiline = true;
            this.txt_comments.Name = "txt_comments";
            this.txt_comments.Size = new System.Drawing.Size(722, 81);
            this.txt_comments.TabIndex = 10;
            this.txt_comments.TextChanged += new System.EventHandler(this.txt_comments_TextChanged);
            this.txt_comments.Validated += new System.EventHandler(this.txt_comments_Validated);
            // 
            // lbl_comments
            // 
            this.lbl_comments.AutoSize = true;
            this.lbl_comments.Location = new System.Drawing.Point(12, 582);
            this.lbl_comments.Name = "lbl_comments";
            this.lbl_comments.Size = new System.Drawing.Size(59, 13);
            this.lbl_comments.TabIndex = 39;
            this.lbl_comments.Text = "Comments:";
            // 
            // btn_email
            // 
            this.btn_email.Location = new System.Drawing.Point(980, 598);
            this.btn_email.Name = "btn_email";
            this.btn_email.Size = new System.Drawing.Size(114, 23);
            this.btn_email.TabIndex = 13;
            this.btn_email.Text = "&Email to Customer";
            this.btn_email.UseVisualStyleBackColor = true;
            this.btn_email.Visible = false;
            this.btn_email.Click += new System.EventHandler(this.btn_email_Click);
            // 
            // btn_Print
            // 
            this.btn_Print.Location = new System.Drawing.Point(1161, 598);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(55, 23);
            this.btn_Print.TabIndex = 15;
            this.btn_Print.Text = "&Print";
            this.btn_Print.UseVisualStyleBackColor = true;
            this.btn_Print.Visible = false;
            this.btn_Print.Click += new System.EventHandler(this.btn_Print_Click);
            // 
            // lbl_Order
            // 
            this.lbl_Order.AutoSize = true;
            this.lbl_Order.Location = new System.Drawing.Point(770, 191);
            this.lbl_Order.Name = "lbl_Order";
            this.lbl_Order.Size = new System.Drawing.Size(76, 13);
            this.lbl_Order.TabIndex = 42;
            this.lbl_Order.Text = "Order Number:";
            // 
            // txt_order_num
            // 
            this.txt_order_num.Location = new System.Drawing.Point(852, 188);
            this.txt_order_num.MaxLength = 20;
            this.txt_order_num.Name = "txt_order_num";
            this.txt_order_num.Size = new System.Drawing.Size(142, 20);
            this.txt_order_num.TabIndex = 9;
            this.txt_order_num.TextChanged += new System.EventHandler(this.txt_order_num_TextChanged);
            // 
            // btn_View
            // 
            this.btn_View.Location = new System.Drawing.Point(1100, 598);
            this.btn_View.Name = "btn_View";
            this.btn_View.Size = new System.Drawing.Size(55, 23);
            this.btn_View.TabIndex = 14;
            this.btn_View.Text = "&View";
            this.btn_View.UseVisualStyleBackColor = true;
            this.btn_View.Visible = false;
            this.btn_View.Click += new System.EventHandler(this.btn_view_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(552, 44);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.Size = new System.Drawing.Size(442, 97);
            this.dataGridView2.TabIndex = 7;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            // 
            // ckb_COA
            // 
            this.ckb_COA.AutoSize = true;
            this.ckb_COA.Location = new System.Drawing.Point(740, 598);
            this.ckb_COA.Name = "ckb_COA";
            this.ckb_COA.Size = new System.Drawing.Size(173, 17);
            this.ckb_COA.TabIndex = 11;
            this.ckb_COA.Text = "&Include a Certificate of Analysis";
            this.ckb_COA.UseVisualStyleBackColor = true;
            this.ckb_COA.Visible = false;
            this.ckb_COA.CheckedChanged += new System.EventHandler(this.ckb_COA_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 59;
            this.label1.Text = "Invoice Date";
            // 
            // dtp_Invoice_Date
            // 
            this.dtp_Invoice_Date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_Invoice_Date.Location = new System.Drawing.Point(90, 51);
            this.dtp_Invoice_Date.Name = "dtp_Invoice_Date";
            this.dtp_Invoice_Date.Size = new System.Drawing.Size(111, 20);
            this.dtp_Invoice_Date.TabIndex = 1;
            this.dtp_Invoice_Date.ValueChanged += new System.EventHandler(this.dtp_Invoice_Date_ValueChanged);
            // 
            // btn_Add_Rates
            // 
            this.btn_Add_Rates.Location = new System.Drawing.Point(323, 188);
            this.btn_Add_Rates.Name = "btn_Add_Rates";
            this.btn_Add_Rates.Size = new System.Drawing.Size(158, 23);
            this.btn_Add_Rates.TabIndex = 4;
            this.btn_Add_Rates.Text = "Add E&xtra charges to Invoice";
            this.btn_Add_Rates.UseVisualStyleBackColor = true;
            this.btn_Add_Rates.Click += new System.EventHandler(this.btn_Add_Rates_Click);
            // 
            // cmb_Rates
            // 
            this.cmb_Rates.FormattingEnabled = true;
            this.cmb_Rates.Location = new System.Drawing.Point(16, 190);
            this.cmb_Rates.Name = "cmb_Rates";
            this.cmb_Rates.Size = new System.Drawing.Size(301, 21);
            this.cmb_Rates.TabIndex = 3;
            this.cmb_Rates.SelectedIndexChanged += new System.EventHandler(this.cmb_Rates_SelectedIndexChanged);
            this.cmb_Rates.SelectionChangeCommitted += new System.EventHandler(this.cmb_Rates_SelectionChangeCommitted);
            // 
            // nud_freight
            // 
            this.nud_freight.DecimalPlaces = 2;
            this.nud_freight.Location = new System.Drawing.Point(581, 188);
            this.nud_freight.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nud_freight.Name = "nud_freight";
            this.nud_freight.Size = new System.Drawing.Size(55, 20);
            this.nud_freight.TabIndex = 5;
            this.nud_freight.ValueChanged += new System.EventHandler(this.nud_freight_ValueChanged);
            // 
            // lbl_freight_charge
            // 
            this.lbl_freight_charge.AutoSize = true;
            this.lbl_freight_charge.Location = new System.Drawing.Point(504, 191);
            this.lbl_freight_charge.Name = "lbl_freight_charge";
            this.lbl_freight_charge.Size = new System.Drawing.Size(71, 13);
            this.lbl_freight_charge.TabIndex = 65;
            this.lbl_freight_charge.Text = "Extra Charge:";
            // 
            // btn_delete
            // 
            this.btn_delete.Location = new System.Drawing.Point(980, 656);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(84, 23);
            this.btn_delete.TabIndex = 66;
            this.btn_delete.TabStop = false;
            this.btn_delete.Text = "button1";
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Visible = false;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // buttonSendDebugInfo
            // 
            this.buttonSendDebugInfo.Image = ((System.Drawing.Image)(resources.GetObject("buttonSendDebugInfo.Image")));
            this.buttonSendDebugInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSendDebugInfo.Location = new System.Drawing.Point(1100, 627);
            this.buttonSendDebugInfo.Name = "buttonSendDebugInfo";
            this.buttonSendDebugInfo.Size = new System.Drawing.Size(238, 23);
            this.buttonSendDebugInfo.TabIndex = 18;
            this.buttonSendDebugInfo.Text = "Send &Debug Info";
            this.toolTip1.SetToolTip(this.buttonSendDebugInfo, "Sends an email with debug info and a screenshot to: processerrors@FP.co.nz");
            this.buttonSendDebugInfo.UseVisualStyleBackColor = true;
            this.buttonSendDebugInfo.Visible = false;
            this.buttonSendDebugInfo.Click += new System.EventHandler(this.buttonSendDebugInfo_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // buttonOpenPdfFileLocation
            // 
            this.buttonOpenPdfFileLocation.Image = ((System.Drawing.Image)(resources.GetObject("buttonOpenPdfFileLocation.Image")));
            this.buttonOpenPdfFileLocation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOpenPdfFileLocation.Location = new System.Drawing.Point(1100, 656);
            this.buttonOpenPdfFileLocation.Name = "buttonOpenPdfFileLocation";
            this.buttonOpenPdfFileLocation.Size = new System.Drawing.Size(238, 23);
            this.buttonOpenPdfFileLocation.TabIndex = 19;
            this.buttonOpenPdfFileLocation.Text = "&Open Pdf File Location";
            this.buttonOpenPdfFileLocation.UseVisualStyleBackColor = true;
            this.buttonOpenPdfFileLocation.Visible = false;
            this.buttonOpenPdfFileLocation.Click += new System.EventHandler(this.buttonOpenPdfFileLocation_Click);
            // 
            // numericUpDownQuantity
            // 
            this.numericUpDownQuantity.Location = new System.Drawing.Point(709, 189);
            this.numericUpDownQuantity.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numericUpDownQuantity.Name = "numericUpDownQuantity";
            this.numericUpDownQuantity.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownQuantity.TabIndex = 67;
            this.numericUpDownQuantity.ValueChanged += new System.EventHandler(this.numericUpDownQuantity_ValueChanged);
            // 
            // labelQuantity
            // 
            this.labelQuantity.AutoSize = true;
            this.labelQuantity.Location = new System.Drawing.Point(654, 191);
            this.labelQuantity.Name = "labelQuantity";
            this.labelQuantity.Size = new System.Drawing.Size(49, 13);
            this.labelQuantity.TabIndex = 68;
            this.labelQuantity.Text = "Quantity:";
            // 
            // buttonCopyTheMaterialEntry
            // 
            this.buttonCopyTheMaterialEntry.Location = new System.Drawing.Point(1003, 81);
            this.buttonCopyTheMaterialEntry.Name = "buttonCopyTheMaterialEntry";
            this.buttonCopyTheMaterialEntry.Size = new System.Drawing.Size(177, 23);
            this.buttonCopyTheMaterialEntry.TabIndex = 69;
            this.buttonCopyTheMaterialEntry.Text = "Copy the Material Entry";
            this.buttonCopyTheMaterialEntry.UseVisualStyleBackColor = true;
            this.buttonCopyTheMaterialEntry.Click += new System.EventHandler(this.buttonCopyTheMaterialEntry_Click);
            // 
            // labelMaterialCopy
            // 
            this.labelMaterialCopy.Location = new System.Drawing.Point(1000, 107);
            this.labelMaterialCopy.Name = "labelMaterialCopy";
            this.labelMaterialCopy.Size = new System.Drawing.Size(338, 63);
            this.labelMaterialCopy.TabIndex = 70;
            this.labelMaterialCopy.Text = ".";
            // 
            // labelCopyInstructions
            // 
            this.labelCopyInstructions.Location = new System.Drawing.Point(1000, 44);
            this.labelCopyInstructions.Name = "labelCopyInstructions";
            this.labelCopyInstructions.Size = new System.Drawing.Size(338, 34);
            this.labelCopyInstructions.TabIndex = 71;
            this.labelCopyInstructions.Text = "If you get a rates warning, highlight the missing rate item in the datagrid and p" +
    "ress the copy button";
            // 
            // customer1
            // 
            this.customer1.CFocused = false;
            this.customer1.Customer_Id = 0;
            this.customer1.Location = new System.Drawing.Point(19, 87);
            this.customer1.Name = "customer1";
            this.customer1.Size = new System.Drawing.Size(298, 28);
            this.customer1.TabIndex = 2;
            this.customer1.CustomerChanged += new System.EventHandler(this.cmb_Trader_SelectedIndexChanged);
            // 
            // Invoicing_Sales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 691);
            this.Controls.Add(this.labelCopyInstructions);
            this.Controls.Add(this.labelMaterialCopy);
            this.Controls.Add(this.buttonCopyTheMaterialEntry);
            this.Controls.Add(this.labelQuantity);
            this.Controls.Add(this.numericUpDownQuantity);
            this.Controls.Add(this.buttonOpenPdfFileLocation);
            this.Controls.Add(this.buttonSendDebugInfo);
            this.Controls.Add(this.btn_delete);
            this.Controls.Add(this.lbl_freight_charge);
            this.Controls.Add(this.nud_freight);
            this.Controls.Add(this.customer1);
            this.Controls.Add(this.btn_Add_Rates);
            this.Controls.Add(this.cmb_Rates);
            this.Controls.Add(this.dtp_Invoice_Date);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ckb_COA);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.btn_View);
            this.Controls.Add(this.txt_order_num);
            this.Controls.Add(this.lbl_Order);
            this.Controls.Add(this.btn_Print);
            this.Controls.Add(this.btn_email);
            this.Controls.Add(this.lbl_comments);
            this.Controls.Add(this.txt_comments);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_Add);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Invoicing_Sales";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PF.Accounts.Invoicing Sales";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Invoicing_Sales_FormClosing);
            this.Load += new System.EventHandler(this.SizeAllColumns);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Invoicing_Sales_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_freight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.Label lbl_comments;
        private System.Windows.Forms.Button btn_Print;
        private System.Windows.Forms.Label lbl_Order;
        private System.Windows.Forms.Button btn_View;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtp_Invoice_Date;
        private System.Windows.Forms.Label lbl_freight_charge;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.Button buttonSendDebugInfo;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonOpenPdfFileLocation;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label labelQuantity;
        private System.Windows.Forms.Label labelMaterialCopy;
        private System.Windows.Forms.Label labelCopyInstructions;
        public System.Windows.Forms.Button btn_Close;
        public System.Windows.Forms.Button btn_Add;
        public System.Windows.Forms.Button btn_Save;
        public System.Windows.Forms.TextBox txt_comments;
        public System.Windows.Forms.Button btn_email;
        public System.Windows.Forms.TextBox txt_order_num;
        public System.Windows.Forms.CheckBox ckb_COA;
        public System.Windows.Forms.Button btn_Add_Rates;
        public System.Windows.Forms.ComboBox cmb_Rates;
        public Utils.UserControls.Customer customer1;
        public System.Windows.Forms.NumericUpDown nud_freight;
        public System.Windows.Forms.NumericUpDown numericUpDownQuantity;
        public System.Windows.Forms.Button buttonCopyTheMaterialEntry;
        public System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.DataGridView dataGridView2;
    }
}

