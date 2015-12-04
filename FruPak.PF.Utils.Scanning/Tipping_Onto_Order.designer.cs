namespace PF.Utils.Scanning
{
    partial class Tipping_Onto_Order
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tipping_Onto_Order));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txt_total = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Add = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_message = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmb_barcode2 = new System.Windows.Forms.ComboBox();
            this.btn_Close = new System.Windows.Forms.Button();
            this.barcode1 = new PF.Utils.UserControls.Barcode();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.treeViewTipOrder = new System.Windows.Forms.TreeView();
            this.buttonPasteThePalletBarcode = new System.Windows.Forms.Button();
            this.buttonClearTheBarcodeText = new System.Windows.Forms.Button();
            this.multiColumnComboBoxOrders = new Tools.MultiColumnComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(312, 22);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 5;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowTemplate.Height = 50;
            this.dataGridView1.Size = new System.Drawing.Size(528, 184);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // txt_total
            // 
            this.txt_total.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_total.Location = new System.Drawing.Point(253, 164);
            this.txt_total.Name = "txt_total";
            this.txt_total.ReadOnly = true;
            this.txt_total.Size = new System.Drawing.Size(50, 26);
            this.txt_total.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(203, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Total";
            // 
            // btn_Add
            // 
            this.btn_Add.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Add.Location = new System.Drawing.Point(630, 364);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(102, 30);
            this.btn_Add.TabIndex = 6;
            this.btn_Add.Text = "&Tip to Order";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Order";
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(18, 369);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(13, 20);
            this.lbl_message.TabIndex = 9;
            this.lbl_message.Text = ".";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(18, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Pallet to";
            // 
            // cmb_barcode2
            // 
            this.cmb_barcode2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_barcode2.FormattingEnabled = true;
            this.cmb_barcode2.Location = new System.Drawing.Point(98, 83);
            this.cmb_barcode2.Name = "cmb_barcode2";
            this.cmb_barcode2.Size = new System.Drawing.Size(208, 28);
            this.cmb_barcode2.TabIndex = 3;
            // 
            // btn_Close
            // 
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(738, 364);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(102, 30);
            this.btn_Close.TabIndex = 7;
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // barcode1
            // 
            this.barcode1.BarcodeValue = "";
            this.barcode1.Location = new System.Drawing.Point(13, 50);
            this.barcode1.Name = "barcode1";
            this.barcode1.Set_Label = "Barcode:";
            this.barcode1.Size = new System.Drawing.Size(294, 33);
            this.barcode1.TabIndex = 2;
            this.barcode1.txtChanged += new System.EventHandler(this.txt_Barcode_TextChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 400);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(852, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 13;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(160, 17);
            this.toolStripStatusLabel1.Text = "Order DropDown Columns: ";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(417, 17);
            this.toolStripStatusLabel2.Text = "Order_Id, Customer_Order, Freight_Docket, Comments, Mod_Date, Combined";
            // 
            // treeViewTipOrder
            // 
            this.treeViewTipOrder.Location = new System.Drawing.Point(312, 212);
            this.treeViewTipOrder.Name = "treeViewTipOrder";
            this.treeViewTipOrder.Size = new System.Drawing.Size(528, 146);
            this.treeViewTipOrder.TabIndex = 14;
            // 
            // buttonPasteThePalletBarcode
            // 
            this.buttonPasteThePalletBarcode.Location = new System.Drawing.Point(71, 335);
            this.buttonPasteThePalletBarcode.Name = "buttonPasteThePalletBarcode";
            this.buttonPasteThePalletBarcode.Size = new System.Drawing.Size(236, 23);
            this.buttonPasteThePalletBarcode.TabIndex = 83;
            this.buttonPasteThePalletBarcode.Text = "Paste the pallet barcode from the clipboard";
            this.buttonPasteThePalletBarcode.UseVisualStyleBackColor = true;
            this.buttonPasteThePalletBarcode.Click += new System.EventHandler(this.buttonPasteThePalletBarcode_Click);
            // 
            // buttonClearTheBarcodeText
            // 
            this.buttonClearTheBarcodeText.Location = new System.Drawing.Point(71, 306);
            this.buttonClearTheBarcodeText.Name = "buttonClearTheBarcodeText";
            this.buttonClearTheBarcodeText.Size = new System.Drawing.Size(236, 23);
            this.buttonClearTheBarcodeText.TabIndex = 84;
            this.buttonClearTheBarcodeText.Text = "Clear the barcode text";
            this.buttonClearTheBarcodeText.UseVisualStyleBackColor = true;
            this.buttonClearTheBarcodeText.Click += new System.EventHandler(this.buttonClearTheBarcodeText_Click);
            // 
            // multiColumnComboBoxOrders
            // 
            this.multiColumnComboBoxOrders.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.multiColumnComboBoxOrders.FormattingEnabled = true;
            this.multiColumnComboBoxOrders.Location = new System.Drawing.Point(98, 22);
            this.multiColumnComboBoxOrders.Name = "multiColumnComboBoxOrders";
            this.multiColumnComboBoxOrders.Size = new System.Drawing.Size(208, 21);
            this.multiColumnComboBoxOrders.TabIndex = 12;
            this.multiColumnComboBoxOrders.SelectedIndexChanged += new System.EventHandler(this.multiColumnComboBoxOrders_SelectedIndexChanged);
            this.multiColumnComboBoxOrders.SelectionChangeCommitted += new System.EventHandler(this.multiColumnComboBoxOrders_SelectionChangeCommitted);
            // 
            // Tipping_Onto_Order
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 422);
            this.Controls.Add(this.buttonClearTheBarcodeText);
            this.Controls.Add(this.buttonPasteThePalletBarcode);
            this.Controls.Add(this.treeViewTipOrder);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.multiColumnComboBoxOrders);
            this.Controls.Add(this.barcode1);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.cmb_barcode2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_total);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Tipping_Onto_Order";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PF.Utils.Scanning.Tipping Onto Order";
            this.Load += new System.EventHandler(this.SizeAllColumns);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Tipping_Onto_Order_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txt_total;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmb_barcode2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.TreeView treeViewTipOrder;
        private System.Windows.Forms.Button buttonClearTheBarcodeText;
        public System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.Button btn_Add;
        public System.Windows.Forms.Button btn_Close;
        public UserControls.Barcode barcode1;
        public Tools.MultiColumnComboBox multiColumnComboBoxOrders;
        public System.Windows.Forms.Button buttonPasteThePalletBarcode;
    }
}