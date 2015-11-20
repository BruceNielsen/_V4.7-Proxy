namespace PF.Utils.Common
{
    partial class Grower
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Grower));
            this.cmb_Rpin = new System.Windows.Forms.ComboBox();
            this.lbl_Rpin = new System.Windows.Forms.Label();
            this.lbl_Global_Gap = new System.Windows.Forms.Label();
            this.txt_Global_Gap = new System.Windows.Forms.TextBox();
            this.cmb_Orchardist = new System.Windows.Forms.ComboBox();
            this.v = new System.Windows.Forms.Label();
            this.txt_GST = new System.Windows.Forms.TextBox();
            this.lbl_GST = new System.Windows.Forms.Label();
            this.ckb_Active = new System.Windows.Forms.CheckBox();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.lbl_message = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.customer1 = new PF.Utils.UserControls.Customer();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmb_Rpin
            // 
            this.cmb_Rpin.FormattingEnabled = true;
            this.cmb_Rpin.Location = new System.Drawing.Point(775, 54);
            this.cmb_Rpin.MaxLength = 5;
            this.cmb_Rpin.Name = "cmb_Rpin";
            this.cmb_Rpin.Size = new System.Drawing.Size(121, 21);
            this.cmb_Rpin.TabIndex = 2;
            this.cmb_Rpin.SelectedValueChanged += new System.EventHandler(this.cmb_Rpin_SelectedValueChanged);
            this.cmb_Rpin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // lbl_Rpin
            // 
            this.lbl_Rpin.AutoSize = true;
            this.lbl_Rpin.Location = new System.Drawing.Point(706, 57);
            this.lbl_Rpin.Name = "lbl_Rpin";
            this.lbl_Rpin.Size = new System.Drawing.Size(32, 13);
            this.lbl_Rpin.TabIndex = 1;
            this.lbl_Rpin.Text = "Rpin:";
            // 
            // lbl_Global_Gap
            // 
            this.lbl_Global_Gap.AutoSize = true;
            this.lbl_Global_Gap.Location = new System.Drawing.Point(706, 86);
            this.lbl_Global_Gap.Name = "lbl_Global_Gap";
            this.lbl_Global_Gap.Size = new System.Drawing.Size(63, 13);
            this.lbl_Global_Gap.TabIndex = 5;
            this.lbl_Global_Gap.Text = "Global Gap:";
            // 
            // txt_Global_Gap
            // 
            this.txt_Global_Gap.Location = new System.Drawing.Point(775, 83);
            this.txt_Global_Gap.MaxLength = 13;
            this.txt_Global_Gap.Name = "txt_Global_Gap";
            this.txt_Global_Gap.Size = new System.Drawing.Size(205, 20);
            this.txt_Global_Gap.TabIndex = 3;
            this.txt_Global_Gap.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // cmb_Orchardist
            // 
            this.cmb_Orchardist.FormattingEnabled = true;
            this.cmb_Orchardist.Location = new System.Drawing.Point(775, 109);
            this.cmb_Orchardist.Name = "cmb_Orchardist";
            this.cmb_Orchardist.Size = new System.Drawing.Size(205, 21);
            this.cmb_Orchardist.TabIndex = 4;
            this.cmb_Orchardist.SelectionChangeCommitted += new System.EventHandler(this.cmb_Orchardist_SelectionChangeCommitted);
            this.cmb_Orchardist.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // v
            // 
            this.v.AutoSize = true;
            this.v.Location = new System.Drawing.Point(706, 112);
            this.v.Name = "v";
            this.v.Size = new System.Drawing.Size(58, 13);
            this.v.TabIndex = 7;
            this.v.Text = "Orchardist:";
            // 
            // txt_GST
            // 
            this.txt_GST.Location = new System.Drawing.Point(775, 180);
            this.txt_GST.MaxLength = 8;
            this.txt_GST.Name = "txt_GST";
            this.txt_GST.Size = new System.Drawing.Size(150, 20);
            this.txt_GST.TabIndex = 6;
            this.txt_GST.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // lbl_GST
            // 
            this.lbl_GST.AutoSize = true;
            this.lbl_GST.Location = new System.Drawing.Point(706, 183);
            this.lbl_GST.Name = "lbl_GST";
            this.lbl_GST.Size = new System.Drawing.Size(32, 13);
            this.lbl_GST.TabIndex = 9;
            this.lbl_GST.Text = "GST:";
            // 
            // ckb_Active
            // 
            this.ckb_Active.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckb_Active.Checked = true;
            this.ckb_Active.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_Active.Location = new System.Drawing.Point(709, 206);
            this.ckb_Active.Name = "ckb_Active";
            this.ckb_Active.Size = new System.Drawing.Size(84, 24);
            this.ckb_Active.TabIndex = 7;
            this.ckb_Active.Text = "Active &Ind:";
            this.ckb_Active.UseVisualStyleBackColor = true;
            this.ckb_Active.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            this.ckb_Active.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Enter_KeyPress);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(903, 247);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 10;
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(811, 247);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_reset.TabIndex = 9;
            this.btn_reset.Text = "&Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(720, 247);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 8;
            this.btn_Add.Text = "&Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(9, 11);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(0, 17);
            this.lbl_message.TabIndex = 34;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 40);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(657, 531);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // customer1
            // 
            this.customer1.CFocused = false;
            this.customer1.Customer_Id = 0;
            this.customer1.Location = new System.Drawing.Point(700, 136);
            this.customer1.Name = "customer1";
            this.customer1.Size = new System.Drawing.Size(294, 28);
            this.customer1.TabIndex = 5;
            // 
            // Grower
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 613);
            this.Controls.Add(this.customer1);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.ckb_Active);
            this.Controls.Add(this.lbl_GST);
            this.Controls.Add(this.txt_GST);
            this.Controls.Add(this.v);
            this.Controls.Add(this.cmb_Orchardist);
            this.Controls.Add(this.lbl_Global_Gap);
            this.Controls.Add(this.txt_Global_Gap);
            this.Controls.Add(this.lbl_Rpin);
            this.Controls.Add(this.cmb_Rpin);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Grower";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PF.Utils.Common.Grower";
            this.Load += new System.EventHandler(this.SizeAllColumns);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Grower_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_Rpin;
        private System.Windows.Forms.Label lbl_Rpin;
        private System.Windows.Forms.Label lbl_Global_Gap;
        private System.Windows.Forms.TextBox txt_Global_Gap;
        private System.Windows.Forms.ComboBox cmb_Orchardist;
        private System.Windows.Forms.Label v;
        private System.Windows.Forms.TextBox txt_GST;
        private System.Windows.Forms.Label lbl_GST;
        private System.Windows.Forms.CheckBox ckb_Active;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.DataGridView dataGridView1;
        private UserControls.Customer customer1;
    }
}