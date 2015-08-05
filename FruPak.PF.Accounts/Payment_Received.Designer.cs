namespace FruPak.PF.Accounts
{
    partial class Payment_Received
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
            PresentationControls.CheckBoxProperties checkBoxProperties1 = new PresentationControls.CheckBoxProperties();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Payment_Received));
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lbl_message = new System.Windows.Forms.Label();
            this.btn_Email = new System.Windows.Forms.Button();
            this.btn_Statement = new System.Windows.Forms.Button();
            this.cmb_test = new PresentationControls.CheckBoxComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_Month = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(714, 494);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 36);
            this.btn_Close.TabIndex = 8;
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(633, 494);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 36);
            this.btn_reset.TabIndex = 7;
            this.btn_reset.Text = "&Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(552, 494);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 36);
            this.btn_Add.TabIndex = 6;
            this.btn_Add.Text = "&Update";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 79);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(721, 356);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(12, 9);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(2, 19);
            this.lbl_message.TabIndex = 50;
            // 
            // btn_Email
            // 
            this.btn_Email.Location = new System.Drawing.Point(471, 494);
            this.btn_Email.Name = "btn_Email";
            this.btn_Email.Size = new System.Drawing.Size(75, 36);
            this.btn_Email.TabIndex = 5;
            this.btn_Email.Text = "&Email Office";
            this.btn_Email.UseVisualStyleBackColor = true;
            this.btn_Email.Click += new System.EventHandler(this.btn_Email_Click);
            // 
            // btn_Statement
            // 
            this.btn_Statement.Location = new System.Drawing.Point(390, 494);
            this.btn_Statement.Name = "btn_Statement";
            this.btn_Statement.Size = new System.Drawing.Size(75, 36);
            this.btn_Statement.TabIndex = 4;
            this.btn_Statement.Text = "&Statement";
            this.btn_Statement.UseVisualStyleBackColor = true;
            this.btn_Statement.Click += new System.EventHandler(this.btn_Statement_Click);
            // 
            // cmb_test
            // 
            checkBoxProperties1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmb_test.CheckBoxProperties = checkBoxProperties1;
            this.cmb_test.DisplayMemberSingleItem = "";
            this.cmb_test.FormattingEnabled = true;
            this.cmb_test.Location = new System.Drawing.Point(84, 45);
            this.cmb_test.Name = "cmb_test";
            this.cmb_test.Size = new System.Drawing.Size(255, 21);
            this.cmb_test.TabIndex = 1;
            this.cmb_test.CheckBoxCheckedChanged += new System.EventHandler(this.cmb_test_CheckBoxCheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 55;
            this.label1.Text = "Customer:";
            // 
            // cmb_Month
            // 
            this.cmb_Month.FormattingEnabled = true;
            this.cmb_Month.Location = new System.Drawing.Point(209, 503);
            this.cmb_Month.Name = "cmb_Month";
            this.cmb_Month.Size = new System.Drawing.Size(121, 21);
            this.cmb_Month.TabIndex = 3;
            // 
            // Payment_Received
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 540);
            this.Controls.Add(this.cmb_Month);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_test);
            this.Controls.Add(this.btn_Statement);
            this.Controls.Add(this.btn_Email);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_Add);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Payment_Received";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FruPak.PF.Accounts.Payment Received";
            this.Load += new System.EventHandler(this.Payment_Received_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Payment_Received_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.Button btn_Email;
        private System.Windows.Forms.Button btn_Statement;
        private PresentationControls.CheckBoxComboBox cmb_test;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_Month;
    }
}