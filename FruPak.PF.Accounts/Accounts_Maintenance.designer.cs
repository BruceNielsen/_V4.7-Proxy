namespace PF.Accounts
{
    partial class Accounts_Maintenance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Accounts_Maintenance));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lbl_message = new System.Windows.Forms.Label();
            this.lbl_Code = new System.Windows.Forms.Label();
            this.lbl_Description = new System.Windows.Forms.Label();
            this.txt_Description = new System.Windows.Forms.TextBox();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.ckb_Active = new System.Windows.Forms.CheckBox();
            this.lbl_Value = new System.Windows.Forms.Label();
            this.txt_Value = new System.Windows.Forms.TextBox();
            this.cmb_Code = new System.Windows.Forms.ComboBox();
            this.nud_Code = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Code)).BeginInit();
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
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
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
            this.lbl_Code.Location = new System.Drawing.Point(585, 67);
            this.lbl_Code.Name = "lbl_Code";
            this.lbl_Code.Size = new System.Drawing.Size(35, 13);
            this.lbl_Code.TabIndex = 2;
            this.lbl_Code.Text = "Code:";
            // 
            // lbl_Description
            // 
            this.lbl_Description.AutoSize = true;
            this.lbl_Description.Location = new System.Drawing.Point(585, 93);
            this.lbl_Description.Name = "lbl_Description";
            this.lbl_Description.Size = new System.Drawing.Size(63, 13);
            this.lbl_Description.TabIndex = 4;
            this.lbl_Description.Text = "Description:";
            // 
            // txt_Description
            // 
            this.txt_Description.Location = new System.Drawing.Point(654, 90);
            this.txt_Description.Multiline = true;
            this.txt_Description.Name = "txt_Description";
            this.txt_Description.Size = new System.Drawing.Size(340, 85);
            this.txt_Description.TabIndex = 3;
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(877, 247);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 8;
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(785, 247);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_reset.TabIndex = 7;
            this.btn_reset.Text = "&Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(694, 247);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 6;
            this.btn_Add.Text = "&Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // ckb_Active
            // 
            this.ckb_Active.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckb_Active.Checked = true;
            this.ckb_Active.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_Active.Location = new System.Drawing.Point(588, 208);
            this.ckb_Active.Name = "ckb_Active";
            this.ckb_Active.Size = new System.Drawing.Size(84, 24);
            this.ckb_Active.TabIndex = 5;
            this.ckb_Active.Text = "Active &Ind:";
            this.ckb_Active.UseVisualStyleBackColor = true;
            this.ckb_Active.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            this.ckb_Active.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Enter_KeyPress);
            // 
            // lbl_Value
            // 
            this.lbl_Value.AutoSize = true;
            this.lbl_Value.Location = new System.Drawing.Point(585, 184);
            this.lbl_Value.Name = "lbl_Value";
            this.lbl_Value.Size = new System.Drawing.Size(37, 13);
            this.lbl_Value.TabIndex = 41;
            this.lbl_Value.Text = "Value:";
            this.lbl_Value.Visible = false;
            // 
            // txt_Value
            // 
            this.txt_Value.Location = new System.Drawing.Point(654, 181);
            this.txt_Value.MaxLength = 10;
            this.txt_Value.Name = "txt_Value";
            this.txt_Value.Size = new System.Drawing.Size(123, 20);
            this.txt_Value.TabIndex = 4;
            this.txt_Value.Visible = false;
            this.txt_Value.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // cmb_Code
            // 
            this.cmb_Code.FormattingEnabled = true;
            this.cmb_Code.Location = new System.Drawing.Point(654, 59);
            this.cmb_Code.Name = "cmb_Code";
            this.cmb_Code.Size = new System.Drawing.Size(121, 21);
            this.cmb_Code.TabIndex = 1;
            this.cmb_Code.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // nud_Code
            // 
            this.nud_Code.Location = new System.Drawing.Point(781, 59);
            this.nud_Code.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nud_Code.Name = "nud_Code";
            this.nud_Code.Size = new System.Drawing.Size(40, 20);
            this.nud_Code.TabIndex = 2;
            this.nud_Code.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // Accounts_Maintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 613);
            this.Controls.Add(this.nud_Code);
            this.Controls.Add(this.cmb_Code);
            this.Controls.Add(this.txt_Value);
            this.Controls.Add(this.lbl_Value);
            this.Controls.Add(this.ckb_Active);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.txt_Description);
            this.Controls.Add(this.lbl_Description);
            this.Controls.Add(this.lbl_Code);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Accounts_Maintenance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PF.Accounts.Maintenance";
            this.Load += new System.EventHandler(this.SizeAllColumns);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Accounts_Maintenance_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Code)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.Label lbl_Code;
        private System.Windows.Forms.Label lbl_Description;
        private System.Windows.Forms.TextBox txt_Description;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.CheckBox ckb_Active;
        private System.Windows.Forms.Label lbl_Value;
        private System.Windows.Forms.TextBox txt_Value;
        private System.Windows.Forms.ComboBox cmb_Code;
        private System.Windows.Forms.NumericUpDown nud_Code;
    }
}

