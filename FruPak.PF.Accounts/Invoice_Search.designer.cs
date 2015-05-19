namespace FruPak.PF.Accounts
{
    partial class Invoice_Search
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Invoice_Search));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_Close = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Selected = new System.Windows.Forms.TextBox();
            this.lbl_message = new System.Windows.Forms.Label();
            this.btn_Email_Customer = new System.Windows.Forms.Button();
            this.btn_Print = new System.Windows.Forms.Button();
            this.ckb_Customer = new System.Windows.Forms.CheckBox();
            this.ckb_Office = new System.Windows.Forms.CheckBox();
            this.btn_View = new System.Windows.Forms.Button();
            this.ckb_COA = new System.Windows.Forms.CheckBox();
            this.customer1 = new FruPak.PF.Utils.UserControls.Customer();
            this.panelInvoice_Search = new System.Windows.Forms.Panel();
            this.buttonSendDebugInfo = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelInvoice_Search.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 84);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(723, 317);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(660, 45);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 34);
            this.btn_Close.TabIndex = 9;
            this.btn_Close.Text = "Close";
            this.toolTip1.SetToolTip(this.btn_Close, "Closes the form");
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(534, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Selected Invoice Number:";
            this.label1.TextChanged += new System.EventHandler(this.label1_TextChanged);
            // 
            // txt_Selected
            // 
            this.txt_Selected.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Selected.Location = new System.Drawing.Point(661, 48);
            this.txt_Selected.Name = "txt_Selected";
            this.txt_Selected.ReadOnly = true;
            this.txt_Selected.Size = new System.Drawing.Size(135, 13);
            this.txt_Selected.TabIndex = 5;
            this.txt_Selected.TextChanged += new System.EventHandler(this.txt_Selected_TextChanged);
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(14, 14);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(0, 17);
            this.lbl_message.TabIndex = 12;
            // 
            // btn_Email_Customer
            // 
            this.btn_Email_Customer.Enabled = false;
            this.btn_Email_Customer.Location = new System.Drawing.Point(6, 42);
            this.btn_Email_Customer.Name = "btn_Email_Customer";
            this.btn_Email_Customer.Size = new System.Drawing.Size(170, 38);
            this.btn_Email_Customer.TabIndex = 8;
            this.btn_Email_Customer.Text = "Email to Customer";
            this.toolTip1.SetToolTip(this.btn_Email_Customer, "Send a copy of the invoice to the customerA copy is also emailed to the office.");
            this.btn_Email_Customer.UseVisualStyleBackColor = true;
            this.btn_Email_Customer.Click += new System.EventHandler(this.btn_Email_Customer_Click);
            // 
            // btn_Print
            // 
            this.btn_Print.Location = new System.Drawing.Point(6, 42);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(170, 38);
            this.btn_Print.TabIndex = 7;
            this.btn_Print.Text = "Print";
            this.btn_Print.UseVisualStyleBackColor = true;
            this.btn_Print.Click += new System.EventHandler(this.btn_Print_Click);
            // 
            // ckb_Customer
            // 
            this.ckb_Customer.AutoSize = true;
            this.ckb_Customer.Location = new System.Drawing.Point(6, 19);
            this.ckb_Customer.Name = "ckb_Customer";
            this.ckb_Customer.Size = new System.Drawing.Size(70, 17);
            this.ckb_Customer.TabIndex = 3;
            this.ckb_Customer.Text = "Customer";
            this.toolTip1.SetToolTip(this.ckb_Customer, "Print an invoice for the customer");
            this.ckb_Customer.UseVisualStyleBackColor = true;
            this.ckb_Customer.CheckedChanged += new System.EventHandler(this.ckb_CheckedChanged);
            // 
            // ckb_Office
            // 
            this.ckb_Office.AutoSize = true;
            this.ckb_Office.Location = new System.Drawing.Point(122, 19);
            this.ckb_Office.Name = "ckb_Office";
            this.ckb_Office.Size = new System.Drawing.Size(54, 17);
            this.ckb_Office.TabIndex = 4;
            this.ckb_Office.Text = "Office";
            this.toolTip1.SetToolTip(this.ckb_Office, "Email a copy of the invoice to the office");
            this.ckb_Office.UseVisualStyleBackColor = true;
            this.ckb_Office.CheckedChanged += new System.EventHandler(this.ckb_CheckedChanged);
            // 
            // btn_View
            // 
            this.btn_View.Location = new System.Drawing.Point(6, 35);
            this.btn_View.Name = "btn_View";
            this.btn_View.Size = new System.Drawing.Size(112, 38);
            this.btn_View.TabIndex = 6;
            this.btn_View.Text = "View Invoice";
            this.toolTip1.SetToolTip(this.btn_View, "View the customer invoice");
            this.btn_View.UseVisualStyleBackColor = true;
            this.btn_View.Click += new System.EventHandler(this.btn_View_Click);
            // 
            // ckb_COA
            // 
            this.ckb_COA.AutoSize = true;
            this.ckb_COA.Location = new System.Drawing.Point(6, 19);
            this.ckb_COA.Name = "ckb_COA";
            this.ckb_COA.Size = new System.Drawing.Size(173, 17);
            this.ckb_COA.TabIndex = 5;
            this.ckb_COA.Text = "Include a Certificate of Analysis";
            this.toolTip1.SetToolTip(this.ckb_COA, "Include a Certificate of Analysis to be sent with the email as an attachment");
            this.ckb_COA.UseVisualStyleBackColor = true;
            this.ckb_COA.CheckedChanged += new System.EventHandler(this.ckb_COA_CheckedChanged);
            // 
            // customer1
            // 
            this.customer1.CFocused = false;
            this.customer1.Customer_Id = 0;
            this.customer1.Location = new System.Drawing.Point(12, 48);
            this.customer1.Name = "customer1";
            this.customer1.Size = new System.Drawing.Size(295, 28);
            this.customer1.TabIndex = 1;
            this.customer1.CustomerChanged += new System.EventHandler(this.cmb_Customers_SelectedIndexChanged);
            // 
            // panelInvoice_Search
            // 
            this.panelInvoice_Search.Controls.Add(this.buttonSendDebugInfo);
            this.panelInvoice_Search.Controls.Add(this.groupBox3);
            this.panelInvoice_Search.Controls.Add(this.groupBox2);
            this.panelInvoice_Search.Controls.Add(this.groupBox1);
            this.panelInvoice_Search.Controls.Add(this.btn_Close);
            this.panelInvoice_Search.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelInvoice_Search.Location = new System.Drawing.Point(0, 407);
            this.panelInvoice_Search.Name = "panelInvoice_Search";
            this.panelInvoice_Search.Size = new System.Drawing.Size(747, 91);
            this.panelInvoice_Search.TabIndex = 61;
            // 
            // buttonSendDebugInfo
            // 
            this.buttonSendDebugInfo.Location = new System.Drawing.Point(554, 3);
            this.buttonSendDebugInfo.Name = "buttonSendDebugInfo";
            this.buttonSendDebugInfo.Size = new System.Drawing.Size(181, 23);
            this.buttonSendDebugInfo.TabIndex = 13;
            this.buttonSendDebugInfo.Text = "Send Debug Info";
            this.buttonSendDebugInfo.UseVisualStyleBackColor = true;
            this.buttonSendDebugInfo.Click += new System.EventHandler(this.buttonSendDebugInfo_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_View);
            this.groupBox3.Location = new System.Drawing.Point(12, 10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(124, 74);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Invoice";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_Print);
            this.groupBox2.Controls.Add(this.ckb_Office);
            this.groupBox2.Controls.Add(this.ckb_Customer);
            this.groupBox2.Location = new System.Drawing.Point(142, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(199, 80);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Print/Email";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_Email_Customer);
            this.groupBox1.Controls.Add(this.ckb_COA);
            this.groupBox1.Location = new System.Drawing.Point(347, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(201, 81);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Customer";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // Invoice_Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 498);
            this.Controls.Add(this.panelInvoice_Search);
            this.Controls.Add(this.customer1);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.txt_Selected);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Invoice_Search";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FruPak.PF.Accounts.Invoice Search";
            this.Load += new System.EventHandler(this.SizeAllColumns);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Invoice_Search_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelInvoice_Search.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Selected;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.Button btn_Email_Customer;
        private System.Windows.Forms.Button btn_Print;
        private System.Windows.Forms.CheckBox ckb_Customer;
        private System.Windows.Forms.CheckBox ckb_Office;
        private System.Windows.Forms.Button btn_View;
        private System.Windows.Forms.CheckBox ckb_COA;
        private Utils.UserControls.Customer customer1;
        private System.Windows.Forms.Panel panelInvoice_Search;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button buttonSendDebugInfo;
    }
}