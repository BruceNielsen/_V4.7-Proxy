namespace PF.Utils.UserControls
{
    partial class Fruit
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_Variety = new System.Windows.Forms.Label();
            this.cmb_Variety = new System.Windows.Forms.ComboBox();
            this.lbl_Fruit_Type = new System.Windows.Forms.Label();
            this.cmb_Fruit_Type = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lbl_Variety
            // 
            this.lbl_Variety.AutoSize = true;
            this.lbl_Variety.Location = new System.Drawing.Point(-1, 33);
            this.lbl_Variety.Name = "lbl_Variety";
            this.lbl_Variety.Size = new System.Drawing.Size(39, 13);
            this.lbl_Variety.TabIndex = 24;
            this.lbl_Variety.Text = "Variety";
            // 
            // cmb_Variety
            // 
            this.cmb_Variety.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_Variety.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_Variety.FormattingEnabled = true;
            this.cmb_Variety.Location = new System.Drawing.Point(59, 30);
            this.cmb_Variety.Name = "cmb_Variety";
            this.cmb_Variety.Size = new System.Drawing.Size(200, 21);
            this.cmb_Variety.TabIndex = 2;
            this.cmb_Variety.SelectedIndexChanged += new System.EventHandler(this.cmb_Variety_SelectedIndexChanged);
            // 
            // lbl_Fruit_Type
            // 
            this.lbl_Fruit_Type.AutoSize = true;
            this.lbl_Fruit_Type.Location = new System.Drawing.Point(-1, 6);
            this.lbl_Fruit_Type.Name = "lbl_Fruit_Type";
            this.lbl_Fruit_Type.Size = new System.Drawing.Size(54, 13);
            this.lbl_Fruit_Type.TabIndex = 23;
            this.lbl_Fruit_Type.Text = "Fruit Type";
            // 
            // cmb_Fruit_Type
            // 
            this.cmb_Fruit_Type.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_Fruit_Type.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_Fruit_Type.FormattingEnabled = true;
            this.cmb_Fruit_Type.Location = new System.Drawing.Point(59, 3);
            this.cmb_Fruit_Type.Name = "cmb_Fruit_Type";
            this.cmb_Fruit_Type.Size = new System.Drawing.Size(121, 21);
            this.cmb_Fruit_Type.TabIndex = 1;
            this.cmb_Fruit_Type.SelectedIndexChanged += new System.EventHandler(this.cmb_Fruit_Type_SelectedIndexChanged);
            // 
            // Fruit
            // 
            this.Controls.Add(this.lbl_Variety);
            this.Controls.Add(this.cmb_Variety);
            this.Controls.Add(this.lbl_Fruit_Type);
            this.Controls.Add(this.cmb_Fruit_Type);
            this.Name = "Fruit";
            this.Size = new System.Drawing.Size(265, 55);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Variety;
        private System.Windows.Forms.ComboBox cmb_Variety;
        private System.Windows.Forms.Label lbl_Fruit_Type;
        private System.Windows.Forms.ComboBox cmb_Fruit_Type;

    }
}
