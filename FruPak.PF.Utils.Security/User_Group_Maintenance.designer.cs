namespace PF.Utils.Security
{
    partial class User_Group_Maintenance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(User_Group_Maintenance));
            this.lbl_Name = new System.Windows.Forms.Label();
            this.lbl_description = new System.Windows.Forms.Label();
            this.txt_Group_Name = new System.Windows.Forms.TextBox();
            this.txt_Group_Description = new System.Windows.Forms.TextBox();
            this.btn_Add = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lbl_message = new System.Windows.Forms.Label();
            this.btn_Update_Relationships = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Name
            // 
            this.lbl_Name.AutoSize = true;
            this.lbl_Name.Location = new System.Drawing.Point(578, 40);
            this.lbl_Name.Name = "lbl_Name";
            this.lbl_Name.Size = new System.Drawing.Size(70, 13);
            this.lbl_Name.TabIndex = 0;
            this.lbl_Name.Text = "Group Name:";
            // 
            // lbl_description
            // 
            this.lbl_description.AutoSize = true;
            this.lbl_description.Location = new System.Drawing.Point(578, 76);
            this.lbl_description.Name = "lbl_description";
            this.lbl_description.Size = new System.Drawing.Size(63, 13);
            this.lbl_description.TabIndex = 1;
            this.lbl_description.Text = "Description:";
            // 
            // txt_Group_Name
            // 
            this.txt_Group_Name.Location = new System.Drawing.Point(654, 37);
            this.txt_Group_Name.MaxLength = 20;
            this.txt_Group_Name.Name = "txt_Group_Name";
            this.txt_Group_Name.Size = new System.Drawing.Size(196, 20);
            this.txt_Group_Name.TabIndex = 2;
            // 
            // txt_Group_Description
            // 
            this.txt_Group_Description.Location = new System.Drawing.Point(654, 73);
            this.txt_Group_Description.Multiline = true;
            this.txt_Group_Description.Name = "txt_Group_Description";
            this.txt_Group_Description.Size = new System.Drawing.Size(340, 85);
            this.txt_Group_Description.TabIndex = 3;
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(703, 177);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 4;
            this.btn_Add.Text = "&Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(794, 177);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_reset.TabIndex = 5;
            this.btn_reset.Text = "&Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(886, 177);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 6;
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(12, 36);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(560, 410);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(21, 9);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(0, 17);
            this.lbl_message.TabIndex = 8;
            // 
            // btn_Update_Relationships
            // 
            this.btn_Update_Relationships.Location = new System.Drawing.Point(749, 423);
            this.btn_Update_Relationships.Name = "btn_Update_Relationships";
            this.btn_Update_Relationships.Size = new System.Drawing.Size(163, 23);
            this.btn_Update_Relationships.TabIndex = 8;
            this.btn_Update_Relationships.Text = "&Update Users in Group";
            this.btn_Update_Relationships.UseVisualStyleBackColor = true;
            this.btn_Update_Relationships.Visible = false;
            this.btn_Update_Relationships.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(657, 217);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(337, 199);
            this.checkedListBox1.TabIndex = 7;
            this.checkedListBox1.Visible = false;
            this.checkedListBox1.Click += new System.EventHandler(this.checkedListBox1_Click);
            // 
            // User_Group_Maintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 473);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.btn_Update_Relationships);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.txt_Group_Description);
            this.Controls.Add(this.txt_Group_Name);
            this.Controls.Add(this.lbl_description);
            this.Controls.Add(this.lbl_Name);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "User_Group_Maintenance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PF.Utils.Security.User Group Maintenance";
            this.Load += new System.EventHandler(this.SizeAllColumns);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.User_Group_Maintenance_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Name;
        private System.Windows.Forms.Label lbl_description;
        private System.Windows.Forms.TextBox txt_Group_Name;
        private System.Windows.Forms.TextBox txt_Group_Description;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.Button btn_Update_Relationships;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
    }
}