namespace FruPak.PF.WorkOrder
{
    partial class QC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QC));
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.lbl_message = new System.Windows.Forms.Label();
            this.lbl_Grower = new System.Windows.Forms.Label();
            this.txt_grower = new System.Windows.Forms.TextBox();
            this.woDisplay1 = new FruPak.PF.Utils.UserControls.WODisplay();
            this.cmb_Defect = new FruPak.PF.Utils.UserControls.SeparatorComboBox();
            this.lbl_Defect = new System.Windows.Forms.Label();
            this.lbl_num_found = new System.Windows.Forms.Label();
            this.nud_num_Found = new System.Windows.Forms.NumericUpDown();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lbl_comments = new System.Windows.Forms.Label();
            this.txt_comments = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nud_num_Found)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(774, 185);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 8;
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(682, 185);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_reset.TabIndex = 7;
            this.btn_reset.Text = "&Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(591, 185);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 6;
            this.btn_Add.Text = "&Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(12, 9);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(2, 19);
            this.lbl_message.TabIndex = 27;
            // 
            // lbl_Grower
            // 
            this.lbl_Grower.AutoSize = true;
            this.lbl_Grower.Location = new System.Drawing.Point(17, 77);
            this.lbl_Grower.Name = "lbl_Grower";
            this.lbl_Grower.Size = new System.Drawing.Size(44, 13);
            this.lbl_Grower.TabIndex = 31;
            this.lbl_Grower.Text = "Grower:";
            // 
            // txt_grower
            // 
            this.txt_grower.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_grower.Location = new System.Drawing.Point(96, 74);
            this.txt_grower.Name = "txt_grower";
            this.txt_grower.ReadOnly = true;
            this.txt_grower.Size = new System.Drawing.Size(100, 20);
            this.txt_grower.TabIndex = 2;
            // 
            // woDisplay1
            // 
            this.woDisplay1.Enabled = false;
            this.woDisplay1.FruitType_Id = 0;
            this.woDisplay1.Location = new System.Drawing.Point(12, 39);
            this.woDisplay1.Name = "woDisplay1";
            this.woDisplay1.Process_Date = null;
            this.woDisplay1.Product_Id = 0;
            this.woDisplay1.Size = new System.Drawing.Size(846, 29);
            this.woDisplay1.TabIndex = 1;
            this.woDisplay1.Variety_Id = 0;
            this.woDisplay1.Work_Order_Id = 0;
            // 
            // cmb_Defect
            // 
            this.cmb_Defect.AutoAdjustItemHeight = false;
            this.cmb_Defect.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmb_Defect.FormattingEnabled = true;
            this.cmb_Defect.Location = new System.Drawing.Point(266, 73);
            this.cmb_Defect.Name = "cmb_Defect";
            this.cmb_Defect.SeparatorColor = System.Drawing.Color.Black;
            this.cmb_Defect.SeparatorMargin = 1;
            this.cmb_Defect.SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.cmb_Defect.SeparatorWidth = 1;
            this.cmb_Defect.Size = new System.Drawing.Size(284, 21);
            this.cmb_Defect.TabIndex = 3;
            // 
            // lbl_Defect
            // 
            this.lbl_Defect.AutoSize = true;
            this.lbl_Defect.Location = new System.Drawing.Point(218, 76);
            this.lbl_Defect.Name = "lbl_Defect";
            this.lbl_Defect.Size = new System.Drawing.Size(42, 13);
            this.lbl_Defect.TabIndex = 35;
            this.lbl_Defect.Text = "Defect:";
            // 
            // lbl_num_found
            // 
            this.lbl_num_found.AutoSize = true;
            this.lbl_num_found.Location = new System.Drawing.Point(580, 77);
            this.lbl_num_found.Name = "lbl_num_found";
            this.lbl_num_found.Size = new System.Drawing.Size(80, 13);
            this.lbl_num_found.TabIndex = 36;
            this.lbl_num_found.Text = "Number Found:";
            // 
            // nud_num_Found
            // 
            this.nud_num_Found.Location = new System.Drawing.Point(666, 74);
            this.nud_num_Found.Name = "nud_num_Found";
            this.nud_num_Found.Size = new System.Drawing.Size(67, 20);
            this.nud_num_Found.TabIndex = 4;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(20, 231);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(841, 207);
            this.dataGridView1.TabIndex = 40;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // lbl_comments
            // 
            this.lbl_comments.AutoSize = true;
            this.lbl_comments.Location = new System.Drawing.Point(17, 121);
            this.lbl_comments.Name = "lbl_comments";
            this.lbl_comments.Size = new System.Drawing.Size(59, 13);
            this.lbl_comments.TabIndex = 39;
            this.lbl_comments.Text = "Comments:";
            // 
            // txt_comments
            // 
            this.txt_comments.Location = new System.Drawing.Point(82, 121);
            this.txt_comments.Multiline = true;
            this.txt_comments.Name = "txt_comments";
            this.txt_comments.Size = new System.Drawing.Size(468, 87);
            this.txt_comments.TabIndex = 5;
            // 
            // QC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 456);
            this.Controls.Add(this.txt_comments);
            this.Controls.Add(this.lbl_comments);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.nud_num_Found);
            this.Controls.Add(this.lbl_num_found);
            this.Controls.Add(this.lbl_Defect);
            this.Controls.Add(this.cmb_Defect);
            this.Controls.Add(this.txt_grower);
            this.Controls.Add(this.lbl_Grower);
            this.Controls.Add(this.woDisplay1);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_Add);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "QC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FruPak.PF.WorkOrder.QC";
            this.Load += new System.EventHandler(this.SizeAllColumns);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.QC_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.nud_num_Found)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Label lbl_message;
        private FruPak.PF.Utils.UserControls.WODisplay woDisplay1;
        private System.Windows.Forms.Label lbl_Grower;
        private System.Windows.Forms.TextBox txt_grower;
        private FruPak.PF.Utils.UserControls.SeparatorComboBox cmb_Defect;
        private System.Windows.Forms.Label lbl_Defect;
        private System.Windows.Forms.Label lbl_num_found;
        private System.Windows.Forms.NumericUpDown nud_num_Found;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lbl_comments;
        private System.Windows.Forms.TextBox txt_comments;
    }
}