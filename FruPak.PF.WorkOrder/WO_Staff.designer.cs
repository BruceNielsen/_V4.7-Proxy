namespace FruPak.PF.WorkOrder
{
    partial class WO_Staff
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WO_Staff));
            this.cmb_Staff = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.ckb_Process_Hours = new System.Windows.Forms.CheckBox();
            this.btn_Add = new System.Windows.Forms.Button();
            this.lbl_message = new System.Windows.Forms.Label();
            this.nud_Extra_Hours = new System.Windows.Forms.NumericUpDown();
            this.rb_Perm_staff = new System.Windows.Forms.RadioButton();
            this.rb_Seasonal_Staff = new System.Windows.Forms.RadioButton();
            this.rb_Casual_Staff = new System.Windows.Forms.RadioButton();
            this.lbl_Emp_Num = new System.Windows.Forms.Label();
            this.txt_Emp_Num = new System.Windows.Forms.TextBox();
            this.grb_Employ = new System.Windows.Forms.GroupBox();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.lbl_Extra_hrs = new System.Windows.Forms.Label();
            this.woDisplay1 = new FruPak.PF.Utils.UserControls.WODisplay();
            this.txt_hourly_rate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Extra_Hours)).BeginInit();
            this.grb_Employ.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmb_Staff
            // 
            this.cmb_Staff.FormattingEnabled = true;
            this.cmb_Staff.Location = new System.Drawing.Point(88, 90);
            this.cmb_Staff.Name = "cmb_Staff";
            this.cmb_Staff.Size = new System.Drawing.Size(200, 21);
            this.cmb_Staff.TabIndex = 2;
            this.cmb_Staff.TextUpdate += new System.EventHandler(this.cmb_Staff_TextUpdate);
            this.cmb_Staff.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(8, 192);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(846, 172);
            this.dataGridView1.TabIndex = 45;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 58;
            this.label1.Text = "Staff Member";
            // 
            // ckb_Process_Hours
            // 
            this.ckb_Process_Hours.AutoSize = true;
            this.ckb_Process_Hours.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckb_Process_Hours.Location = new System.Drawing.Point(12, 120);
            this.ckb_Process_Hours.Name = "ckb_Process_Hours";
            this.ckb_Process_Hours.Size = new System.Drawing.Size(98, 17);
            this.ckb_Process_Hours.TabIndex = 3;
            this.ckb_Process_Hours.Text = "Process Hours:";
            this.ckb_Process_Hours.UseVisualStyleBackColor = true;
            this.ckb_Process_Hours.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(301, 163);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 10;
            this.btn_Add.Text = "&Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(9, 9);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(0, 17);
            this.lbl_message.TabIndex = 65;
            // 
            // nud_Extra_Hours
            // 
            this.nud_Extra_Hours.DecimalPlaces = 2;
            this.nud_Extra_Hours.Location = new System.Drawing.Point(223, 119);
            this.nud_Extra_Hours.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nud_Extra_Hours.Name = "nud_Extra_Hours";
            this.nud_Extra_Hours.Size = new System.Drawing.Size(65, 20);
            this.nud_Extra_Hours.TabIndex = 4;
            this.nud_Extra_Hours.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nud_Extra_Hours.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            this.nud_Extra_Hours.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Enter_KeyPress);
            // 
            // rb_Perm_staff
            // 
            this.rb_Perm_staff.AutoSize = true;
            this.rb_Perm_staff.Location = new System.Drawing.Point(7, 19);
            this.rb_Perm_staff.Name = "rb_Perm_staff";
            this.rb_Perm_staff.Size = new System.Drawing.Size(76, 17);
            this.rb_Perm_staff.TabIndex = 5;
            this.rb_Perm_staff.Tag = "P";
            this.rb_Perm_staff.Text = "&Permanent";
            this.rb_Perm_staff.UseVisualStyleBackColor = true;
            // 
            // rb_Seasonal_Staff
            // 
            this.rb_Seasonal_Staff.AutoSize = true;
            this.rb_Seasonal_Staff.Checked = true;
            this.rb_Seasonal_Staff.Location = new System.Drawing.Point(89, 20);
            this.rb_Seasonal_Staff.Name = "rb_Seasonal_Staff";
            this.rb_Seasonal_Staff.Size = new System.Drawing.Size(69, 17);
            this.rb_Seasonal_Staff.TabIndex = 6;
            this.rb_Seasonal_Staff.TabStop = true;
            this.rb_Seasonal_Staff.Tag = "S";
            this.rb_Seasonal_Staff.Text = "&Seasonal";
            this.rb_Seasonal_Staff.UseVisualStyleBackColor = true;
            // 
            // rb_Casual_Staff
            // 
            this.rb_Casual_Staff.AutoSize = true;
            this.rb_Casual_Staff.Location = new System.Drawing.Point(164, 20);
            this.rb_Casual_Staff.Name = "rb_Casual_Staff";
            this.rb_Casual_Staff.Size = new System.Drawing.Size(57, 17);
            this.rb_Casual_Staff.TabIndex = 7;
            this.rb_Casual_Staff.Tag = "C";
            this.rb_Casual_Staff.Text = "Cas&ual";
            this.rb_Casual_Staff.UseVisualStyleBackColor = true;
            // 
            // lbl_Emp_Num
            // 
            this.lbl_Emp_Num.AutoSize = true;
            this.lbl_Emp_Num.Location = new System.Drawing.Point(227, 22);
            this.lbl_Emp_Num.Name = "lbl_Emp_Num";
            this.lbl_Emp_Num.Size = new System.Drawing.Size(81, 13);
            this.lbl_Emp_Num.TabIndex = 71;
            this.lbl_Emp_Num.Text = "Employee Num:";
            // 
            // txt_Emp_Num
            // 
            this.txt_Emp_Num.Location = new System.Drawing.Point(309, 19);
            this.txt_Emp_Num.Name = "txt_Emp_Num";
            this.txt_Emp_Num.Size = new System.Drawing.Size(70, 20);
            this.txt_Emp_Num.TabIndex = 8;
            this.txt_Emp_Num.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // grb_Employ
            // 
            this.grb_Employ.Controls.Add(this.txt_Emp_Num);
            this.grb_Employ.Controls.Add(this.rb_Perm_staff);
            this.grb_Employ.Controls.Add(this.lbl_Emp_Num);
            this.grb_Employ.Controls.Add(this.rb_Seasonal_Staff);
            this.grb_Employ.Controls.Add(this.rb_Casual_Staff);
            this.grb_Employ.Location = new System.Drawing.Point(358, 81);
            this.grb_Employ.Name = "grb_Employ";
            this.grb_Employ.Size = new System.Drawing.Size(392, 75);
            this.grb_Employ.TabIndex = 73;
            this.grb_Employ.TabStop = false;
            this.grb_Employ.Visible = false;
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(389, 162);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_reset.TabIndex = 11;
            this.btn_reset.Text = "&Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(481, 162);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 12;
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // lbl_Extra_hrs
            // 
            this.lbl_Extra_hrs.AutoSize = true;
            this.lbl_Extra_hrs.Location = new System.Drawing.Point(147, 121);
            this.lbl_Extra_hrs.Name = "lbl_Extra_hrs";
            this.lbl_Extra_hrs.Size = new System.Drawing.Size(65, 13);
            this.lbl_Extra_hrs.TabIndex = 77;
            this.lbl_Extra_hrs.Text = "Extra Hours:";
            // 
            // woDisplay1
            // 
            this.woDisplay1.FruitType_Id = 0;
            this.woDisplay1.Location = new System.Drawing.Point(15, 31);
            this.woDisplay1.Name = "woDisplay1";
            this.woDisplay1.Process_Date = null;
            this.woDisplay1.Product_Id = 0;
            this.woDisplay1.Size = new System.Drawing.Size(846, 29);
            this.woDisplay1.TabIndex = 1;
            this.woDisplay1.Variety_Id = 0;
            this.woDisplay1.Work_Order_Id = 0;
            // 
            // txt_hourly_rate
            // 
            this.txt_hourly_rate.Location = new System.Drawing.Point(667, 126);
            this.txt_hourly_rate.Name = "txt_hourly_rate";
            this.txt_hourly_rate.Size = new System.Drawing.Size(70, 20);
            this.txt_hourly_rate.TabIndex = 9;
            this.txt_hourly_rate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(585, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 73;
            this.label2.Text = "Hourly Rate:";
            // 
            // WO_Staff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 391);
            this.Controls.Add(this.txt_hourly_rate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.woDisplay1);
            this.Controls.Add(this.lbl_Extra_hrs);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.grb_Employ);
            this.Controls.Add(this.nud_Extra_Hours);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.ckb_Process_Hours);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cmb_Staff);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "WO_Staff";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FruPak.PF.WorkOrder.Work Order Staff";
            this.Load += new System.EventHandler(this.SizeAllColumns);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WO_Staff_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Extra_Hours)).EndInit();
            this.grb_Employ.ResumeLayout(false);
            this.grb_Employ.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_Staff;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ckb_Process_Hours;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.NumericUpDown nud_Extra_Hours;
        private System.Windows.Forms.RadioButton rb_Perm_staff;
        private System.Windows.Forms.RadioButton rb_Seasonal_Staff;
        private System.Windows.Forms.RadioButton rb_Casual_Staff;
        private System.Windows.Forms.Label lbl_Emp_Num;
        private System.Windows.Forms.TextBox txt_Emp_Num;
        private System.Windows.Forms.GroupBox grb_Employ;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Label lbl_Extra_hrs;
        private FruPak.PF.Utils.UserControls.WODisplay woDisplay1;
        private System.Windows.Forms.TextBox txt_hourly_rate;
        private System.Windows.Forms.Label label2;
    }
}