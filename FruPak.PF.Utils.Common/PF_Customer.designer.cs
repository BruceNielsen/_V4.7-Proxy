namespace FruPak.PF.Utils.Common
{
    partial class PF_Customer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PF_Customer));
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lbl_message = new System.Windows.Forms.Label();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.lbl_Name = new System.Windows.Forms.Label();
            this.lbl_contact = new System.Windows.Forms.Label();
            this.cmb_Outlook = new System.Windows.Forms.ComboBox();
            this.ckb_Active = new System.Windows.Forms.CheckBox();
            this.panelUtils_Common_Customer = new System.Windows.Forms.Panel();
            this.buttonAutoLink = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelUtils_Common_Customer.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(666, 3);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 7;
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(574, 3);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_reset.TabIndex = 6;
            this.btn_reset.Text = "&Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(483, 3);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 5;
            this.btn_Add.Text = "&Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(5, 41);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(980, 314);
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
            this.lbl_message.TabIndex = 64;
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(75, 30);
            this.txt_name.MaxLength = 1024;
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(329, 20);
            this.txt_name.TabIndex = 3;
            // 
            // lbl_Name
            // 
            this.lbl_Name.AutoSize = true;
            this.lbl_Name.Location = new System.Drawing.Point(2, 33);
            this.lbl_Name.Name = "lbl_Name";
            this.lbl_Name.Size = new System.Drawing.Size(41, 13);
            this.lbl_Name.TabIndex = 71;
            this.lbl_Name.Text = "Name :";
            // 
            // lbl_contact
            // 
            this.lbl_contact.AutoSize = true;
            this.lbl_contact.Location = new System.Drawing.Point(2, 6);
            this.lbl_contact.Name = "lbl_contact";
            this.lbl_contact.Size = new System.Drawing.Size(67, 13);
            this.lbl_contact.TabIndex = 73;
            this.lbl_contact.Text = "Outlook Link";
            // 
            // cmb_Outlook
            // 
            this.cmb_Outlook.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmb_Outlook.FormattingEnabled = true;
            this.cmb_Outlook.Location = new System.Drawing.Point(75, 3);
            this.cmb_Outlook.Name = "cmb_Outlook";
            this.cmb_Outlook.Size = new System.Drawing.Size(329, 21);
            this.cmb_Outlook.TabIndex = 2;
            this.cmb_Outlook.SelectionChangeCommitted += new System.EventHandler(this.cmb_Outlook_SelectionChangeCommitted);
            // 
            // ckb_Active
            // 
            this.ckb_Active.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckb_Active.Checked = true;
            this.ckb_Active.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_Active.Location = new System.Drawing.Point(5, 56);
            this.ckb_Active.Name = "ckb_Active";
            this.ckb_Active.Size = new System.Drawing.Size(84, 24);
            this.ckb_Active.TabIndex = 4;
            this.ckb_Active.Text = "Active &Ind:";
            this.ckb_Active.UseVisualStyleBackColor = true;
            // 
            // panelUtils_Common_Customer
            // 
            this.panelUtils_Common_Customer.Controls.Add(this.buttonAutoLink);
            this.panelUtils_Common_Customer.Controls.Add(this.cmb_Outlook);
            this.panelUtils_Common_Customer.Controls.Add(this.ckb_Active);
            this.panelUtils_Common_Customer.Controls.Add(this.btn_Add);
            this.panelUtils_Common_Customer.Controls.Add(this.lbl_contact);
            this.panelUtils_Common_Customer.Controls.Add(this.btn_reset);
            this.panelUtils_Common_Customer.Controls.Add(this.btn_Close);
            this.panelUtils_Common_Customer.Controls.Add(this.txt_name);
            this.panelUtils_Common_Customer.Controls.Add(this.lbl_Name);
            this.panelUtils_Common_Customer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelUtils_Common_Customer.Location = new System.Drawing.Point(0, 361);
            this.panelUtils_Common_Customer.Name = "panelUtils_Common_Customer";
            this.panelUtils_Common_Customer.Size = new System.Drawing.Size(997, 86);
            this.panelUtils_Common_Customer.TabIndex = 75;
            // 
            // buttonAutoLink
            // 
            this.buttonAutoLink.Location = new System.Drawing.Point(828, 3);
            this.buttonAutoLink.Name = "buttonAutoLink";
            this.buttonAutoLink.Size = new System.Drawing.Size(75, 21);
            this.buttonAutoLink.TabIndex = 74;
            this.buttonAutoLink.Text = "Auto-Link";
            this.buttonAutoLink.UseVisualStyleBackColor = true;
            this.buttonAutoLink.Visible = false;
            this.buttonAutoLink.Click += new System.EventHandler(this.buttonAutoLink_Click);
            // 
            // PF_Customer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 447);
            this.Controls.Add(this.panelUtils_Common_Customer);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "PF_Customer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FruPak.PF.Utils.Common.Customer";
            this.Load += new System.EventHandler(this.SizeAllColumns);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PF_Customer_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelUtils_Common_Customer.ResumeLayout(false);
            this.panelUtils_Common_Customer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.Label lbl_Name;
        private System.Windows.Forms.Label lbl_contact;
        private System.Windows.Forms.ComboBox cmb_Outlook;
        private System.Windows.Forms.CheckBox ckb_Active;
        private System.Windows.Forms.Panel panelUtils_Common_Customer;
        private System.Windows.Forms.Button buttonAutoLink;
    }
}