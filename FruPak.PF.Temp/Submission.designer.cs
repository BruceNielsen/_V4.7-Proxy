namespace FruPak.PF.Temp
{
    partial class Submission
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Submission));
            this.cmb_Trader = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_Block = new System.Windows.Forms.ComboBox();
            this.lbl_message = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Grower_Ref = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtp_Sub_Date = new System.Windows.Forms.DateTimePicker();
            this.dtp_Harvest_Date = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nud_Pick_Num = new System.Windows.Forms.NumericUpDown();
            this.txt_Comments = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.grower1 = new FruPak.PF.Utils.UserControls.Grower();
            this.ckb_Print_all = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Pick_Num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmb_Trader
            // 
            this.cmb_Trader.FormattingEnabled = true;
            this.cmb_Trader.Location = new System.Drawing.Point(75, 44);
            this.cmb_Trader.Name = "cmb_Trader";
            this.cmb_Trader.Size = new System.Drawing.Size(201, 21);
            this.cmb_Trader.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Trader:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Block:";
            // 
            // cmb_Block
            // 
            this.cmb_Block.FormattingEnabled = true;
            this.cmb_Block.Location = new System.Drawing.Point(75, 130);
            this.cmb_Block.Name = "cmb_Block";
            this.cmb_Block.Size = new System.Drawing.Size(121, 21);
            this.cmb_Block.TabIndex = 3;
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(12, 9);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(0, 17);
            this.lbl_message.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(263, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Grower Reference:";
            // 
            // txt_Grower_Ref
            // 
            this.txt_Grower_Ref.Location = new System.Drawing.Point(366, 133);
            this.txt_Grower_Ref.MaxLength = 50;
            this.txt_Grower_Ref.Name = "txt_Grower_Ref";
            this.txt_Grower_Ref.Size = new System.Drawing.Size(158, 20);
            this.txt_Grower_Ref.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(310, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Submission Date:";
            // 
            // dtp_Sub_Date
            // 
            this.dtp_Sub_Date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_Sub_Date.Location = new System.Drawing.Point(405, 45);
            this.dtp_Sub_Date.Name = "dtp_Sub_Date";
            this.dtp_Sub_Date.Size = new System.Drawing.Size(104, 20);
            this.dtp_Sub_Date.TabIndex = 4;
            // 
            // dtp_Harvest_Date
            // 
            this.dtp_Harvest_Date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_Harvest_Date.Location = new System.Drawing.Point(405, 71);
            this.dtp_Harvest_Date.Name = "dtp_Harvest_Date";
            this.dtp_Harvest_Date.Size = new System.Drawing.Size(104, 20);
            this.dtp_Harvest_Date.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(310, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Harvest Date:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(310, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Pick Num:";
            // 
            // nud_Pick_Num
            // 
            this.nud_Pick_Num.Location = new System.Drawing.Point(405, 97);
            this.nud_Pick_Num.Name = "nud_Pick_Num";
            this.nud_Pick_Num.Size = new System.Drawing.Size(104, 20);
            this.nud_Pick_Num.TabIndex = 6;
            // 
            // txt_Comments
            // 
            this.txt_Comments.Location = new System.Drawing.Point(80, 172);
            this.txt_Comments.MaxLength = 50;
            this.txt_Comments.Multiline = true;
            this.txt_Comments.Name = "txt_Comments";
            this.txt_Comments.Size = new System.Drawing.Size(444, 89);
            this.txt_Comments.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Comments:";
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Location = new System.Drawing.Point(292, 267);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(75, 23);
            this.btn_Refresh.TabIndex = 11;
            this.btn_Refresh.Text = "Re&fresh";
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(373, 267);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 12;
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(211, 267);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_reset.TabIndex = 10;
            this.btn_reset.Text = "&Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(130, 267);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 9;
            this.btn_Add.Text = "&Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(15, 313);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(974, 150);
            this.dataGridView1.TabIndex = 14;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // grower1
            // 
            this.grower1.bol_test = false;
            this.grower1.Grower_Id = 0;
            this.grower1.Location = new System.Drawing.Point(15, 71);
            this.grower1.Name = "grower1";
            this.grower1.Orchardist_Id = 0;
            this.grower1.Size = new System.Drawing.Size(268, 53);
            this.grower1.TabIndex = 2;
            this.grower1.GrowerChanged += new System.EventHandler(this.grower1_GrowerChanged);
            // 
            // ckb_Print_all
            // 
            this.ckb_Print_all.AutoSize = true;
            this.ckb_Print_all.Location = new System.Drawing.Point(526, 272);
            this.ckb_Print_all.Name = "ckb_Print_all";
            this.ckb_Print_all.Size = new System.Drawing.Size(207, 17);
            this.ckb_Print_all.TabIndex = 13;
            this.ckb_Print_all.Text = "Re&Print All Bins cards for a Submission";
            this.ckb_Print_all.UseVisualStyleBackColor = true;
            // 
            // Submission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 485);
            this.Controls.Add(this.ckb_Print_all);
            this.Controls.Add(this.grower1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_Refresh);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.txt_Comments);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.nud_Pick_Num);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtp_Harvest_Date);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtp_Sub_Date);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_Grower_Ref);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmb_Block);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_Trader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Submission";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FruPak.PF.Temp.Submission";
            this.Load += new System.EventHandler(this.SizeAllColumns);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Submission_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.nud_Pick_Num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_Trader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_Block;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Grower_Ref;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtp_Sub_Date;
        private System.Windows.Forms.DateTimePicker dtp_Harvest_Date;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nud_Pick_Num;
        private System.Windows.Forms.TextBox txt_Comments;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.DataGridView dataGridView1;
        private Utils.UserControls.Grower grower1;
        private System.Windows.Forms.CheckBox ckb_Print_all;
    }
}

