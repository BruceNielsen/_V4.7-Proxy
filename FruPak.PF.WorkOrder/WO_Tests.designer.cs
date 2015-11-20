namespace PF.WorkOrder
{
    partial class WO_Tests
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WO_Tests));
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.dtp_start = new System.Windows.Forms.DateTimePicker();
            this.lbl_Time = new System.Windows.Forms.Label();
            this.lbl_message = new System.Windows.Forms.Label();
            this.woDisplay1 = new PF.Utils.UserControls.WODisplay();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lbl_test = new System.Windows.Forms.Label();
            this.cmb_Test = new System.Windows.Forms.ComboBox();
            this.batchNum1 = new PF.Utils.UserControls.BatchNum();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(776, 76);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 7;
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(684, 76);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_reset.TabIndex = 6;
            this.btn_reset.Text = "&Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(593, 76);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 5;
            this.btn_Add.Text = "&Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // dtp_start
            // 
            this.dtp_start.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_start.Location = new System.Drawing.Point(181, 75);
            this.dtp_start.Name = "dtp_start";
            this.dtp_start.ShowUpDown = true;
            this.dtp_start.Size = new System.Drawing.Size(100, 20);
            this.dtp_start.TabIndex = 3;
            this.dtp_start.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // lbl_Time
            // 
            this.lbl_Time.AutoSize = true;
            this.lbl_Time.Location = new System.Drawing.Point(142, 78);
            this.lbl_Time.Name = "lbl_Time";
            this.lbl_Time.Size = new System.Drawing.Size(33, 13);
            this.lbl_Time.TabIndex = 28;
            this.lbl_Time.Text = "Time:";
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(12, 9);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(0, 17);
            this.lbl_message.TabIndex = 29;
            // 
            // woDisplay1
            // 
            this.woDisplay1.Enabled = false;
            this.woDisplay1.FruitType_Id = 0;
            this.woDisplay1.Location = new System.Drawing.Point(15, 40);
            this.woDisplay1.Name = "woDisplay1";
            this.woDisplay1.Process_Date = null;
            this.woDisplay1.Product_Id = 0;
            this.woDisplay1.Size = new System.Drawing.Size(846, 29);
            this.woDisplay1.TabIndex = 1;
            this.woDisplay1.Variety_Id = 0;
            this.woDisplay1.Work_Order_Id = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(21, 127);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(830, 239);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellLeave);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // lbl_test
            // 
            this.lbl_test.AutoSize = true;
            this.lbl_test.Location = new System.Drawing.Point(300, 78);
            this.lbl_test.Name = "lbl_test";
            this.lbl_test.Size = new System.Drawing.Size(31, 13);
            this.lbl_test.TabIndex = 31;
            this.lbl_test.Text = "Test:";
            // 
            // cmb_Test
            // 
            this.cmb_Test.FormattingEnabled = true;
            this.cmb_Test.Location = new System.Drawing.Point(337, 75);
            this.cmb_Test.Name = "cmb_Test";
            this.cmb_Test.Size = new System.Drawing.Size(121, 21);
            this.cmb_Test.TabIndex = 4;
            this.cmb_Test.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            this.cmb_Test.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Enter_KeyPress);
            // 
            // batchNum1
            // 
            this.batchNum1.Batch_Num = 0;
            this.batchNum1.Location = new System.Drawing.Point(15, 72);
            this.batchNum1.Name = "batchNum1";
            this.batchNum1.Size = new System.Drawing.Size(114, 27);
            this.batchNum1.TabIndex = 2;
            this.batchNum1.Work_Order_Id = 0;
            // 
            // WO_Tests
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 376);
            this.Controls.Add(this.batchNum1);
            this.Controls.Add(this.cmb_Test);
            this.Controls.Add(this.lbl_test);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.lbl_Time);
            this.Controls.Add(this.dtp_start);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.woDisplay1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "WO_Tests";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PF.WorkOrder.Work Order Tests";
            this.Load += new System.EventHandler(this.SizeAllColumns);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WO_Tests_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PF.Utils.UserControls.WODisplay woDisplay1;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.DateTimePicker dtp_start;
        private System.Windows.Forms.Label lbl_Time;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lbl_test;
        private System.Windows.Forms.ComboBox cmb_Test;
        private Utils.UserControls.BatchNum batchNum1;
    }
}