namespace FruPak.PF.Utils.UserControls
{
    partial class BatchNum
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
            this.lbl_Batch = new System.Windows.Forms.Label();
            this.cmb_Batch = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lbl_Batch
            // 
            this.lbl_Batch.AutoSize = true;
            this.lbl_Batch.Location = new System.Drawing.Point(5, 6);
            this.lbl_Batch.Name = "lbl_Batch";
            this.lbl_Batch.Size = new System.Drawing.Size(38, 13);
            this.lbl_Batch.TabIndex = 28;
            this.lbl_Batch.Text = "Batch:";
            // 
            // cmb_Batch
            // 
            this.cmb_Batch.FormattingEnabled = true;
            this.cmb_Batch.Location = new System.Drawing.Point(49, 3);
            this.cmb_Batch.Name = "cmb_Batch";
            this.cmb_Batch.Size = new System.Drawing.Size(59, 21);
            this.cmb_Batch.TabIndex = 1;
            // 
            // BatchNum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbl_Batch);
            this.Controls.Add(this.cmb_Batch);
            this.Name = "BatchNum";
            this.Size = new System.Drawing.Size(114, 27);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Batch;
        private System.Windows.Forms.ComboBox cmb_Batch;
    }
}
