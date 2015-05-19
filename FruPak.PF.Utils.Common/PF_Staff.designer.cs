namespace FruPak.PF.Utils.Common
{
    partial class PF_Staff
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PF_Staff));
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lbl_message = new System.Windows.Forms.Label();
            this.lbl_FirstName = new System.Windows.Forms.Label();
            this.txt_First_Name = new System.Windows.Forms.TextBox();
            this.txt_Last_Name = new System.Windows.Forms.TextBox();
            this.lbl_LastName = new System.Windows.Forms.Label();
            this.txt_Employee_Num = new System.Windows.Forms.TextBox();
            this.lbl_Emp_Num = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_Emp_Type = new System.Windows.Forms.ComboBox();
            this.txt_hourly_Rate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ckb_Active = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(755, 220);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 10;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(663, 220);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_reset.TabIndex = 9;
            this.btn_reset.Text = "Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(572, 220);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 8;
            this.btn_Add.Text = "Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(9, 41);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(543, 202);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(9, 9);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(2, 19);
            this.lbl_message.TabIndex = 25;
            // 
            // lbl_FirstName
            // 
            this.lbl_FirstName.AutoSize = true;
            this.lbl_FirstName.Location = new System.Drawing.Point(572, 43);
            this.lbl_FirstName.Name = "lbl_FirstName";
            this.lbl_FirstName.Size = new System.Drawing.Size(60, 13);
            this.lbl_FirstName.TabIndex = 26;
            this.lbl_FirstName.Text = "First Name:";
            // 
            // txt_First_Name
            // 
            this.txt_First_Name.Location = new System.Drawing.Point(638, 40);
            this.txt_First_Name.MaxLength = 20;
            this.txt_First_Name.Name = "txt_First_Name";
            this.txt_First_Name.Size = new System.Drawing.Size(100, 20);
            this.txt_First_Name.TabIndex = 2;
            this.txt_First_Name.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // txt_Last_Name
            // 
            this.txt_Last_Name.Location = new System.Drawing.Point(638, 66);
            this.txt_Last_Name.MaxLength = 20;
            this.txt_Last_Name.Name = "txt_Last_Name";
            this.txt_Last_Name.Size = new System.Drawing.Size(100, 20);
            this.txt_Last_Name.TabIndex = 3;
            this.txt_Last_Name.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // lbl_LastName
            // 
            this.lbl_LastName.AutoSize = true;
            this.lbl_LastName.Location = new System.Drawing.Point(572, 69);
            this.lbl_LastName.Name = "lbl_LastName";
            this.lbl_LastName.Size = new System.Drawing.Size(61, 13);
            this.lbl_LastName.TabIndex = 28;
            this.lbl_LastName.Text = "Last Name:";
            // 
            // txt_Employee_Num
            // 
            this.txt_Employee_Num.Location = new System.Drawing.Point(674, 92);
            this.txt_Employee_Num.MaxLength = 20;
            this.txt_Employee_Num.Name = "txt_Employee_Num";
            this.txt_Employee_Num.Size = new System.Drawing.Size(64, 20);
            this.txt_Employee_Num.TabIndex = 4;
            this.txt_Employee_Num.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // lbl_Emp_Num
            // 
            this.lbl_Emp_Num.AutoSize = true;
            this.lbl_Emp_Num.Location = new System.Drawing.Point(572, 95);
            this.lbl_Emp_Num.Name = "lbl_Emp_Num";
            this.lbl_Emp_Num.Size = new System.Drawing.Size(96, 13);
            this.lbl_Emp_Num.TabIndex = 30;
            this.lbl_Emp_Num.Text = "Employee Number:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(574, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Employment Type:";
            // 
            // cmb_Emp_Type
            // 
            this.cmb_Emp_Type.FormattingEnabled = true;
            this.cmb_Emp_Type.Items.AddRange(new object[] {
            "Permanent",
            "Seasonal"});
            this.cmb_Emp_Type.Location = new System.Drawing.Point(674, 121);
            this.cmb_Emp_Type.Name = "cmb_Emp_Type";
            this.cmb_Emp_Type.Size = new System.Drawing.Size(97, 21);
            this.cmb_Emp_Type.TabIndex = 5;
            this.cmb_Emp_Type.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // txt_hourly_Rate
            // 
            this.txt_hourly_Rate.Location = new System.Drawing.Point(677, 148);
            this.txt_hourly_Rate.MaxLength = 20;
            this.txt_hourly_Rate.Name = "txt_hourly_Rate";
            this.txt_hourly_Rate.Size = new System.Drawing.Size(94, 20);
            this.txt_hourly_Rate.TabIndex = 6;
            this.txt_hourly_Rate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(572, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Hourly Rate:";
            // 
            // ckb_Active
            // 
            this.ckb_Active.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckb_Active.Checked = true;
            this.ckb_Active.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_Active.Location = new System.Drawing.Point(575, 181);
            this.ckb_Active.Name = "ckb_Active";
            this.ckb_Active.Size = new System.Drawing.Size(84, 24);
            this.ckb_Active.TabIndex = 7;
            this.ckb_Active.Text = "Active Ind:";
            this.ckb_Active.UseVisualStyleBackColor = true;
            this.ckb_Active.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            this.ckb_Active.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Enter_KeyPress);
            // 
            // PF_Staff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 268);
            this.Controls.Add(this.ckb_Active);
            this.Controls.Add(this.txt_hourly_Rate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmb_Emp_Type);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Employee_Num);
            this.Controls.Add(this.lbl_Emp_Num);
            this.Controls.Add(this.txt_Last_Name);
            this.Controls.Add(this.lbl_LastName);
            this.Controls.Add(this.txt_First_Name);
            this.Controls.Add(this.lbl_FirstName);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_Add);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "PF_Staff";
            this.Text = "FruPak.PF.Utils.Common.Staff ";
            this.Load += new System.EventHandler(this.SizeAllColumns);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PF_Staff_KeyDown);
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
        private System.Windows.Forms.Label lbl_FirstName;
        private System.Windows.Forms.TextBox txt_First_Name;
        private System.Windows.Forms.TextBox txt_Last_Name;
        private System.Windows.Forms.Label lbl_LastName;
        private System.Windows.Forms.TextBox txt_Employee_Num;
        private System.Windows.Forms.Label lbl_Emp_Num;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_Emp_Type;
        private System.Windows.Forms.TextBox txt_hourly_Rate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ckb_Active;
    }
}