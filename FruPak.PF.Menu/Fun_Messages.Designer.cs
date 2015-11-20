namespace PF.Menu
{
    partial class Fun_Messages
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fun_Messages));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lbl_message = new System.Windows.Forms.Label();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_User = new System.Windows.Forms.ComboBox();
            this.txt_Display_Message = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtp_End_Date = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtp_End_Time = new System.Windows.Forms.DateTimePicker();
            this.rdb_Message_Start = new System.Windows.Forms.RadioButton();
            this.rdb_Message_End = new System.Windows.Forms.RadioButton();
            this.rdb_Message_Excl = new System.Windows.Forms.RadioButton();
            this.cmb_Repeat_Type = new System.Windows.Forms.ComboBox();
            this.cmb_Repeat_interval = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 45);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(657, 282);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(12, 9);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(2, 19);
            this.lbl_message.TabIndex = 1;
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(921, 578);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 13;
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(829, 578);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_reset.TabIndex = 12;
            this.btn_reset.Text = "&Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(738, 578);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 11;
            this.btn_Add.Text = "&Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 361);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "User";
            // 
            // cmb_User
            // 
            this.cmb_User.FormattingEnabled = true;
            this.cmb_User.Location = new System.Drawing.Point(167, 358);
            this.cmb_User.Name = "cmb_User";
            this.cmb_User.Size = new System.Drawing.Size(176, 21);
            this.cmb_User.TabIndex = 2;
            // 
            // txt_Display_Message
            // 
            this.txt_Display_Message.Location = new System.Drawing.Point(167, 385);
            this.txt_Display_Message.Name = "txt_Display_Message";
            this.txt_Display_Message.Size = new System.Drawing.Size(502, 20);
            this.txt_Display_Message.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 388);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 42;
            this.label2.Text = "Displayed Message:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 420);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 43;
            this.label3.Text = "End Date:";
            // 
            // dtp_End_Date
            // 
            this.dtp_End_Date.Location = new System.Drawing.Point(167, 414);
            this.dtp_End_Date.Name = "dtp_End_Date";
            this.dtp_End_Date.Size = new System.Drawing.Size(201, 20);
            this.dtp_End_Date.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 443);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 45;
            this.label4.Text = "End Time:";
            // 
            // dtp_End_Time
            // 
            this.dtp_End_Time.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_End_Time.Location = new System.Drawing.Point(167, 440);
            this.dtp_End_Time.Name = "dtp_End_Time";
            this.dtp_End_Time.ShowUpDown = true;
            this.dtp_End_Time.Size = new System.Drawing.Size(103, 20);
            this.dtp_End_Time.TabIndex = 5;
            // 
            // rdb_Message_Start
            // 
            this.rdb_Message_Start.AutoSize = true;
            this.rdb_Message_Start.Location = new System.Drawing.Point(410, 419);
            this.rdb_Message_Start.Name = "rdb_Message_Start";
            this.rdb_Message_Start.Size = new System.Drawing.Size(210, 17);
            this.rdb_Message_Start.TabIndex = 8;
            this.rdb_Message_Start.TabStop = true;
            this.rdb_Message_Start.Text = "Include countdown at &Start of message";
            this.rdb_Message_Start.UseVisualStyleBackColor = true;
            // 
            // rdb_Message_End
            // 
            this.rdb_Message_End.AutoSize = true;
            this.rdb_Message_End.Location = new System.Drawing.Point(410, 444);
            this.rdb_Message_End.Name = "rdb_Message_End";
            this.rdb_Message_End.Size = new System.Drawing.Size(207, 17);
            this.rdb_Message_End.TabIndex = 9;
            this.rdb_Message_End.TabStop = true;
            this.rdb_Message_End.Text = "Include countdown at &End of message";
            this.rdb_Message_End.UseVisualStyleBackColor = true;
            // 
            // rdb_Message_Excl
            // 
            this.rdb_Message_Excl.AutoSize = true;
            this.rdb_Message_Excl.Location = new System.Drawing.Point(410, 467);
            this.rdb_Message_Excl.Name = "rdb_Message_Excl";
            this.rdb_Message_Excl.Size = new System.Drawing.Size(187, 17);
            this.rdb_Message_Excl.TabIndex = 10;
            this.rdb_Message_Excl.TabStop = true;
            this.rdb_Message_Excl.Text = "E&xclude countdown from message";
            this.rdb_Message_Excl.UseVisualStyleBackColor = true;
            // 
            // cmb_Repeat_Type
            // 
            this.cmb_Repeat_Type.FormattingEnabled = true;
            this.cmb_Repeat_Type.Location = new System.Drawing.Point(167, 467);
            this.cmb_Repeat_Type.Name = "cmb_Repeat_Type";
            this.cmb_Repeat_Type.Size = new System.Drawing.Size(103, 21);
            this.cmb_Repeat_Type.TabIndex = 6;
            this.cmb_Repeat_Type.SelectionChangeCommitted += new System.EventHandler(this.cmb_Repeat_Type_SelectionChangeCommitted);
            // 
            // cmb_Repeat_interval
            // 
            this.cmb_Repeat_interval.FormattingEnabled = true;
            this.cmb_Repeat_interval.Location = new System.Drawing.Point(276, 467);
            this.cmb_Repeat_interval.Name = "cmb_Repeat_interval";
            this.cmb_Repeat_interval.Size = new System.Drawing.Size(47, 21);
            this.cmb_Repeat_interval.TabIndex = 7;
            this.cmb_Repeat_interval.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(59, 475);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 52;
            this.label5.Text = "Repeat Interval:";
            // 
            // Fun_Messages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 613);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmb_Repeat_interval);
            this.Controls.Add(this.cmb_Repeat_Type);
            this.Controls.Add(this.rdb_Message_Excl);
            this.Controls.Add(this.rdb_Message_End);
            this.Controls.Add(this.rdb_Message_Start);
            this.Controls.Add(this.dtp_End_Time);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtp_End_Date);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_Display_Message);
            this.Controls.Add(this.cmb_User);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Fun_Messages";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PF.Menu.Fun Messages";
            this.Load += new System.EventHandler(this.Fun_Messages_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Fun_Messages_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_User;
        private System.Windows.Forms.TextBox txt_Display_Message;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtp_End_Date;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtp_End_Time;
        private System.Windows.Forms.RadioButton rdb_Message_Start;
        private System.Windows.Forms.RadioButton rdb_Message_End;
        private System.Windows.Forms.RadioButton rdb_Message_Excl;
        private System.Windows.Forms.ComboBox cmb_Repeat_Type;
        private System.Windows.Forms.ComboBox cmb_Repeat_interval;
        private System.Windows.Forms.Label label5;
    }
}