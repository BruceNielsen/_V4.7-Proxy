namespace FruPak.PF.Dispatch
{
    partial class Shipping_Staff
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Shipping_Staff));
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_Staff = new System.Windows.Forms.ComboBox();
            this.lbl_finish = new System.Windows.Forms.Label();
            this.dtp_finish = new System.Windows.Forms.DateTimePicker();
            this.lbl_start = new System.Windows.Forms.Label();
            this.dtp_start = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lbl_message = new System.Windows.Forms.Label();
            this.rb_Pre_Order = new System.Windows.Forms.RadioButton();
            this.rb_Deliver = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(518, 81);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 8;
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(426, 81);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_reset.TabIndex = 7;
            this.btn_reset.Text = "&Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(338, 82);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 6;
            this.btn_Add.Text = "&Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 79;
            this.label1.Text = "Staff Member";
            // 
            // cmb_Staff
            // 
            this.cmb_Staff.FormattingEnabled = true;
            this.cmb_Staff.Location = new System.Drawing.Point(87, 52);
            this.cmb_Staff.Name = "cmb_Staff";
            this.cmb_Staff.Size = new System.Drawing.Size(200, 21);
            this.cmb_Staff.TabIndex = 1;
            this.cmb_Staff.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // lbl_finish
            // 
            this.lbl_finish.AutoSize = true;
            this.lbl_finish.Location = new System.Drawing.Point(467, 56);
            this.lbl_finish.Name = "lbl_finish";
            this.lbl_finish.Size = new System.Drawing.Size(34, 13);
            this.lbl_finish.TabIndex = 89;
            this.lbl_finish.Text = "Finish";
            // 
            // dtp_finish
            // 
            this.dtp_finish.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_finish.Location = new System.Drawing.Point(507, 53);
            this.dtp_finish.Name = "dtp_finish";
            this.dtp_finish.ShowUpDown = true;
            this.dtp_finish.Size = new System.Drawing.Size(100, 20);
            this.dtp_finish.TabIndex = 3;
            this.dtp_finish.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            this.dtp_finish.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Enter_KeyPress);
            // 
            // lbl_start
            // 
            this.lbl_start.AutoSize = true;
            this.lbl_start.Location = new System.Drawing.Point(310, 56);
            this.lbl_start.Name = "lbl_start";
            this.lbl_start.Size = new System.Drawing.Size(29, 13);
            this.lbl_start.TabIndex = 87;
            this.lbl_start.Text = "Start";
            // 
            // dtp_start
            // 
            this.dtp_start.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_start.Location = new System.Drawing.Point(350, 53);
            this.dtp_start.Name = "dtp_start";
            this.dtp_start.ShowUpDown = true;
            this.dtp_start.Size = new System.Drawing.Size(100, 20);
            this.dtp_start.TabIndex = 2;
            this.dtp_start.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(87, 122);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(426, 150);
            this.dataGridView1.TabIndex = 100;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.ForeColor = System.Drawing.Color.Black;
            this.lbl_message.Location = new System.Drawing.Point(11, 9);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(2, 22);
            this.lbl_message.TabIndex = 91;
            // 
            // rb_Pre_Order
            // 
            this.rb_Pre_Order.AutoSize = true;
            this.rb_Pre_Order.Checked = true;
            this.rb_Pre_Order.Location = new System.Drawing.Point(87, 84);
            this.rb_Pre_Order.Name = "rb_Pre_Order";
            this.rb_Pre_Order.Size = new System.Drawing.Size(91, 17);
            this.rb_Pre_Order.TabIndex = 4;
            this.rb_Pre_Order.TabStop = true;
            this.rb_Pre_Order.Text = "&Prepare Order";
            this.rb_Pre_Order.UseVisualStyleBackColor = true;
            // 
            // rb_Deliver
            // 
            this.rb_Deliver.AutoSize = true;
            this.rb_Deliver.Location = new System.Drawing.Point(184, 84);
            this.rb_Deliver.Name = "rb_Deliver";
            this.rb_Deliver.Size = new System.Drawing.Size(87, 17);
            this.rb_Deliver.TabIndex = 5;
            this.rb_Deliver.Text = "&Deliver Order";
            this.rb_Deliver.UseVisualStyleBackColor = true;
            // 
            // Shipping_Staff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 344);
            this.ControlBox = false;
            this.Controls.Add(this.rb_Deliver);
            this.Controls.Add(this.rb_Pre_Order);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lbl_finish);
            this.Controls.Add(this.dtp_finish);
            this.Controls.Add(this.lbl_start);
            this.Controls.Add(this.dtp_start);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_Staff);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Shipping_Staff";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FruPak.PF.Dispatch.Shipping Staff";
            this.Load += new System.EventHandler(this.SizeAllColumns);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Shipping_Staff_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_Staff;
        private System.Windows.Forms.Label lbl_finish;
        private System.Windows.Forms.DateTimePicker dtp_finish;
        private System.Windows.Forms.Label lbl_start;
        private System.Windows.Forms.DateTimePicker dtp_start;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.RadioButton rb_Pre_Order;
        private System.Windows.Forms.RadioButton rb_Deliver;
    }
}