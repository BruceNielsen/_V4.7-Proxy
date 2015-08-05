namespace FruPak.PF.Utils.Security
{
    partial class Menu_Panel_Maintenance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu_Panel_Maintenance));
            this.btn_Update_Relationships = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.txt_Menu_Description = new System.Windows.Forms.TextBox();
            this.txt_SubMenu_Name = new System.Windows.Forms.TextBox();
            this.lbl_description = new System.Windows.Forms.Label();
            this.lbl_Name = new System.Windows.Forms.Label();
            this.lbl_message = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_Close = new System.Windows.Forms.Button();
            this.cmb_Menu = new System.Windows.Forms.ComboBox();
            this.lbl_Menu = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Update_Relationships
            // 
            this.btn_Update_Relationships.Location = new System.Drawing.Point(749, 427);
            this.btn_Update_Relationships.Name = "btn_Update_Relationships";
            this.btn_Update_Relationships.Size = new System.Drawing.Size(163, 23);
            this.btn_Update_Relationships.TabIndex = 9;
            this.btn_Update_Relationships.Text = "&Update Relationships";
            this.btn_Update_Relationships.UseVisualStyleBackColor = true;
            this.btn_Update_Relationships.Visible = false;
            this.btn_Update_Relationships.Click += new System.EventHandler(this.btn_Add_Members_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(799, 207);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_reset.TabIndex = 6;
            this.btn_reset.Text = "&Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(708, 207);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 5;
            this.btn_Add.Text = "&Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // txt_Menu_Description
            // 
            this.txt_Menu_Description.Location = new System.Drawing.Point(659, 103);
            this.txt_Menu_Description.Multiline = true;
            this.txt_Menu_Description.Name = "txt_Menu_Description";
            this.txt_Menu_Description.Size = new System.Drawing.Size(340, 85);
            this.txt_Menu_Description.TabIndex = 4;
            // 
            // txt_SubMenu_Name
            // 
            this.txt_SubMenu_Name.Location = new System.Drawing.Point(678, 67);
            this.txt_SubMenu_Name.MaxLength = 20;
            this.txt_SubMenu_Name.Name = "txt_SubMenu_Name";
            this.txt_SubMenu_Name.Size = new System.Drawing.Size(196, 20);
            this.txt_SubMenu_Name.TabIndex = 3;
            // 
            // lbl_description
            // 
            this.lbl_description.AutoSize = true;
            this.lbl_description.Location = new System.Drawing.Point(583, 106);
            this.lbl_description.Name = "lbl_description";
            this.lbl_description.Size = new System.Drawing.Size(63, 13);
            this.lbl_description.TabIndex = 23;
            this.lbl_description.Text = "Description:";
            // 
            // lbl_Name
            // 
            this.lbl_Name.AutoSize = true;
            this.lbl_Name.Location = new System.Drawing.Point(583, 70);
            this.lbl_Name.Name = "lbl_Name";
            this.lbl_Name.Size = new System.Drawing.Size(87, 13);
            this.lbl_Name.TabIndex = 22;
            this.lbl_Name.Text = "SubMenu Name:";
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(19, 13);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(0, 17);
            this.lbl_message.TabIndex = 21;
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
            this.dataGridView1.Location = new System.Drawing.Point(10, 40);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(564, 410);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(890, 207);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 7;
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // cmb_Menu
            // 
            this.cmb_Menu.FormattingEnabled = true;
            this.cmb_Menu.Location = new System.Drawing.Point(678, 37);
            this.cmb_Menu.Name = "cmb_Menu";
            this.cmb_Menu.Size = new System.Drawing.Size(196, 21);
            this.cmb_Menu.TabIndex = 2;
            // 
            // lbl_Menu
            // 
            this.lbl_Menu.AutoSize = true;
            this.lbl_Menu.Location = new System.Drawing.Point(583, 41);
            this.lbl_Menu.Name = "lbl_Menu";
            this.lbl_Menu.Size = new System.Drawing.Size(37, 13);
            this.lbl_Menu.TabIndex = 32;
            this.lbl_Menu.Text = "Menu:";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(659, 247);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.Size = new System.Drawing.Size(337, 174);
            this.dataGridView2.TabIndex = 8;
            this.dataGridView2.Visible = false;
            this.dataGridView2.Click += new System.EventHandler(this.dataGridView2_Click);
            // 
            // Menu_Panel_Maintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 462);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.lbl_Menu);
            this.Controls.Add(this.cmb_Menu);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_Update_Relationships);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.txt_Menu_Description);
            this.Controls.Add(this.txt_SubMenu_Name);
            this.Controls.Add(this.lbl_description);
            this.Controls.Add(this.lbl_Name);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Menu_Panel_Maintenance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FruPak.PF.Utils.Security.Menu Panel Maintenance";
            this.Load += new System.EventHandler(this.SizeAllColumns);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Menu_Panel_Maintenance_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Update_Relationships;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.TextBox txt_Menu_Description;
        private System.Windows.Forms.TextBox txt_SubMenu_Name;
        private System.Windows.Forms.Label lbl_description;
        private System.Windows.Forms.Label lbl_Name;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.ComboBox cmb_Menu;
        private System.Windows.Forms.Label lbl_Menu;
        private System.Windows.Forms.DataGridView dataGridView2;
    }
}