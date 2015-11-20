namespace PF.Utils.UserControls
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_Variety = new System.Windows.Forms.Label();
            this.cmb_Grower = new System.Windows.Forms.ComboBox();
            this.lbl_Fruit_Type = new System.Windows.Forms.Label();
            this.cmb_Orchardist = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lbl_Variety
            // 
            this.lbl_Variety.AutoSize = true;
            this.lbl_Variety.Location = new System.Drawing.Point(-1, 33);
            this.lbl_Variety.Name = "lbl_Variety";
            this.lbl_Variety.Size = new System.Drawing.Size(41, 13);
            this.lbl_Variety.TabIndex = 28;
            this.lbl_Variety.Text = "Grower";
            // 
            // cmb_Grower
            // 
            this.cmb_Grower.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_Grower.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_Grower.FormattingEnabled = true;
            this.cmb_Grower.Location = new System.Drawing.Point(60, 29);
            this.cmb_Grower.Name = "cmb_Grower";
            this.cmb_Grower.Size = new System.Drawing.Size(200, 21);
            this.cmb_Grower.TabIndex = 2;
            this.cmb_Grower.SelectedIndexChanged += new System.EventHandler(this.cmb_Grower_SelectedIndexChanged);
            // 
            // lbl_Fruit_Type
            // 
            this.lbl_Fruit_Type.AutoSize = true;
            this.lbl_Fruit_Type.Location = new System.Drawing.Point(-1, 6);
            this.lbl_Fruit_Type.Name = "lbl_Fruit_Type";
            this.lbl_Fruit_Type.Size = new System.Drawing.Size(55, 13);
            this.lbl_Fruit_Type.TabIndex = 27;
            this.lbl_Fruit_Type.Text = "Orchardist";
            // 
            // cmb_Orchardist
            // 
            this.cmb_Orchardist.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_Orchardist.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_Orchardist.FormattingEnabled = true;
            this.cmb_Orchardist.Location = new System.Drawing.Point(60, 3);
            this.cmb_Orchardist.Name = "cmb_Orchardist";
            this.cmb_Orchardist.Size = new System.Drawing.Size(200, 21);
            this.cmb_Orchardist.TabIndex = 1;
            this.cmb_Orchardist.SelectedIndexChanged += new System.EventHandler(this.cmb_Orchardist_SelectedIndexChanged);
            // 
            // Grower
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.lbl_Variety);
            this.Controls.Add(this.cmb_Grower);
            this.Controls.Add(this.lbl_Fruit_Type);
            this.Controls.Add(this.cmb_Orchardist);
            this.Name = "Grower";
            this.Size = new System.Drawing.Size(268, 53);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Variety;
        private System.Windows.Forms.ComboBox cmb_Grower;
        private System.Windows.Forms.Label lbl_Fruit_Type;
        private System.Windows.Forms.ComboBox cmb_Orchardist;
    }
}
