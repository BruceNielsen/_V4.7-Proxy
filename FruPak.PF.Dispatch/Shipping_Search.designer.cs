namespace FruPak.PF.Dispatch
{
    partial class Shipping_Search
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Shipping_Search));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_Close = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Selected = new System.Windows.Forms.TextBox();
            this.chb_All = new System.Windows.Forms.CheckBox();
            this.chb_PS = new System.Windows.Forms.CheckBox();
            this.chb_COA = new System.Windows.Forms.CheckBox();
            this.chb_PC = new System.Windows.Forms.CheckBox();
            this.chb_ADDR = new System.Windows.Forms.CheckBox();
            this.btn_Print = new System.Windows.Forms.Button();
            this.lbl_message = new System.Windows.Forms.Label();
            this.btn_View = new System.Windows.Forms.Button();
            this.btn_Email = new System.Windows.Forms.Button();
            this.customer1 = new FruPak.PF.Utils.UserControls.Customer();
            this.rdb_list_curr = new System.Windows.Forms.RadioButton();
            this.rdb_List_All = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panelShipping_Search = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelShipping_Search.SuspendLayout();
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
            this.dataGridView1.Size = new System.Drawing.Size(982, 347);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.dataGridView1.BindingContextChanged += new System.EventHandler(this.dataGridView1_BindingContextChanged);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(919, 5);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 32);
            this.btn_Close.TabIndex = 13;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(532, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Selected Order Number:";
            // 
            // txt_Selected
            // 
            this.txt_Selected.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Selected.Location = new System.Drawing.Point(659, 63);
            this.txt_Selected.Name = "txt_Selected";
            this.txt_Selected.ReadOnly = true;
            this.txt_Selected.Size = new System.Drawing.Size(135, 13);
            this.txt_Selected.TabIndex = 5;
            // 
            // chb_All
            // 
            this.chb_All.AutoSize = true;
            this.chb_All.Location = new System.Drawing.Point(12, 14);
            this.chb_All.Name = "chb_All";
            this.chb_All.Size = new System.Drawing.Size(37, 17);
            this.chb_All.TabIndex = 5;
            this.chb_All.Text = "All";
            this.chb_All.UseVisualStyleBackColor = true;
            this.chb_All.CheckedChanged += new System.EventHandler(this.chb_All_CheckedChanged);
            this.chb_All.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            this.chb_All.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Enter_KeyPress);
            // 
            // chb_PS
            // 
            this.chb_PS.AutoSize = true;
            this.chb_PS.Location = new System.Drawing.Point(55, 14);
            this.chb_PS.Name = "chb_PS";
            this.chb_PS.Size = new System.Drawing.Size(85, 17);
            this.chb_PS.TabIndex = 6;
            this.chb_PS.Text = "Packing Slip";
            this.chb_PS.UseVisualStyleBackColor = true;
            this.chb_PS.CheckedChanged += new System.EventHandler(this.chb_PS_CheckedChanged);
            this.chb_PS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // chb_COA
            // 
            this.chb_COA.AutoSize = true;
            this.chb_COA.Location = new System.Drawing.Point(146, 14);
            this.chb_COA.Name = "chb_COA";
            this.chb_COA.Size = new System.Drawing.Size(126, 17);
            this.chb_COA.TabIndex = 7;
            this.chb_COA.Text = "Certificate of Analysis";
            this.chb_COA.UseVisualStyleBackColor = true;
            this.chb_COA.CheckedChanged += new System.EventHandler(this.chb_COA_CheckedChanged);
            this.chb_COA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // chb_PC
            // 
            this.chb_PC.AutoSize = true;
            this.chb_PC.Location = new System.Drawing.Point(278, 14);
            this.chb_PC.Name = "chb_PC";
            this.chb_PC.Size = new System.Drawing.Size(77, 17);
            this.chb_PC.TabIndex = 8;
            this.chb_PC.Text = "Pallet Card";
            this.chb_PC.UseVisualStyleBackColor = true;
            this.chb_PC.CheckedChanged += new System.EventHandler(this.chb_PC_CheckedChanged);
            this.chb_PC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // chb_ADDR
            // 
            this.chb_ADDR.AutoSize = true;
            this.chb_ADDR.Location = new System.Drawing.Point(361, 14);
            this.chb_ADDR.Name = "chb_ADDR";
            this.chb_ADDR.Size = new System.Drawing.Size(93, 17);
            this.chb_ADDR.TabIndex = 9;
            this.chb_ADDR.Text = "Address Label";
            this.chb_ADDR.UseVisualStyleBackColor = true;
            this.chb_ADDR.CheckedChanged += new System.EventHandler(this.chb_ADDR_CheckedChanged);
            this.chb_ADDR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // btn_Print
            // 
            this.btn_Print.Location = new System.Drawing.Point(757, 5);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(75, 32);
            this.btn_Print.TabIndex = 11;
            this.btn_Print.Text = "Print";
            this.btn_Print.UseVisualStyleBackColor = true;
            this.btn_Print.Click += new System.EventHandler(this.btn_Print_Click);
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(14, 14);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(0, 24);
            this.lbl_message.TabIndex = 12;
            // 
            // btn_View
            // 
            this.btn_View.Location = new System.Drawing.Point(838, 5);
            this.btn_View.Name = "btn_View";
            this.btn_View.Size = new System.Drawing.Size(75, 32);
            this.btn_View.TabIndex = 12;
            this.btn_View.Text = "View";
            this.btn_View.UseVisualStyleBackColor = true;
            this.btn_View.Click += new System.EventHandler(this.btn_View_Click);
            // 
            // btn_Email
            // 
            this.btn_Email.Location = new System.Drawing.Point(684, 5);
            this.btn_Email.Name = "btn_Email";
            this.btn_Email.Size = new System.Drawing.Size(67, 32);
            this.btn_Email.TabIndex = 10;
            this.btn_Email.Text = "Email";
            this.btn_Email.UseVisualStyleBackColor = true;
            this.btn_Email.Click += new System.EventHandler(this.btn_Email_Click);
            // 
            // customer1
            // 
            this.customer1.CFocused = false;
            this.customer1.Customer_Id = 0;
            this.customer1.Location = new System.Drawing.Point(15, 48);
            this.customer1.Name = "customer1";
            this.customer1.Size = new System.Drawing.Size(294, 28);
            this.customer1.TabIndex = 1;
            this.customer1.CustomerChanged += new System.EventHandler(this.cmb_Customers_SelectedIndexChanged);
            // 
            // rdb_list_curr
            // 
            this.rdb_list_curr.AutoSize = true;
            this.rdb_list_curr.Location = new System.Drawing.Point(331, 56);
            this.rdb_list_curr.Name = "rdb_list_curr";
            this.rdb_list_curr.Size = new System.Drawing.Size(78, 17);
            this.rdb_list_curr.TabIndex = 2;
            this.rdb_list_curr.Text = "List Current";
            this.rdb_list_curr.UseVisualStyleBackColor = true;
            this.rdb_list_curr.CheckedChanged += new System.EventHandler(this.rdb_list_CheckedChanged);
            // 
            // rdb_List_All
            // 
            this.rdb_List_All.AutoSize = true;
            this.rdb_List_All.Checked = true;
            this.rdb_List_All.Location = new System.Drawing.Point(415, 56);
            this.rdb_List_All.Name = "rdb_List_All";
            this.rdb_List_All.Size = new System.Drawing.Size(55, 17);
            this.rdb_List_All.TabIndex = 3;
            this.rdb_List_All.TabStop = true;
            this.rdb_List_All.Text = "List All";
            this.rdb_List_All.UseVisualStyleBackColor = true;
            this.rdb_List_All.CheckedChanged += new System.EventHandler(this.rdb_list_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(173, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 13);
            this.label8.TabIndex = 74;
            this.label8.Text = "= Current Orders";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Location = new System.Drawing.Point(146, 37);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(21, 20);
            this.textBox2.TabIndex = 73;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 72;
            this.label3.Text = "= Overdue Orders";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(12, 37);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(21, 20);
            this.textBox1.TabIndex = 71;
            // 
            // panelShipping_Search
            // 
            this.panelShipping_Search.Controls.Add(this.chb_All);
            this.panelShipping_Search.Controls.Add(this.label8);
            this.panelShipping_Search.Controls.Add(this.chb_PS);
            this.panelShipping_Search.Controls.Add(this.textBox2);
            this.panelShipping_Search.Controls.Add(this.chb_COA);
            this.panelShipping_Search.Controls.Add(this.label3);
            this.panelShipping_Search.Controls.Add(this.chb_PC);
            this.panelShipping_Search.Controls.Add(this.textBox1);
            this.panelShipping_Search.Controls.Add(this.chb_ADDR);
            this.panelShipping_Search.Controls.Add(this.btn_Email);
            this.panelShipping_Search.Controls.Add(this.btn_Print);
            this.panelShipping_Search.Controls.Add(this.btn_View);
            this.panelShipping_Search.Controls.Add(this.btn_Close);
            this.panelShipping_Search.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelShipping_Search.Location = new System.Drawing.Point(0, 448);
            this.panelShipping_Search.Name = "panelShipping_Search";
            this.panelShipping_Search.Size = new System.Drawing.Size(1006, 67);
            this.panelShipping_Search.TabIndex = 75;
            // 
            // Shipping_Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 515);
            this.Controls.Add(this.panelShipping_Search);
            this.Controls.Add(this.rdb_List_All);
            this.Controls.Add(this.rdb_list_curr);
            this.Controls.Add(this.customer1);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.txt_Selected);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Shipping_Search";
            this.Text = "FruPak.PF.Dispatch.Shipping Search";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShippingSearch_FormClosing);
            this.Load += new System.EventHandler(this.SizeAllColumns);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Shipping_Search_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelShipping_Search.ResumeLayout(false);
            this.panelShipping_Search.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Selected;
        private System.Windows.Forms.CheckBox chb_All;
        private System.Windows.Forms.CheckBox chb_PS;
        private System.Windows.Forms.CheckBox chb_COA;
        private System.Windows.Forms.CheckBox chb_PC;
        private System.Windows.Forms.CheckBox chb_ADDR;
        private System.Windows.Forms.Button btn_Print;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.Button btn_View;
        private System.Windows.Forms.Button btn_Email;
        private Utils.UserControls.Customer customer1;
        private System.Windows.Forms.RadioButton rdb_list_curr;
        private System.Windows.Forms.RadioButton rdb_List_All;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panelShipping_Search;
    }
}