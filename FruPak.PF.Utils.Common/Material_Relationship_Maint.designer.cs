namespace PF.Utils.Common
{
    partial class Material_Relationship_Maint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Material_Relationship_Maint));
            this.btn_Execute = new System.Windows.Forms.Button();
            this.ckb_Rates = new System.Windows.Forms.CheckBox();
            this.ckb_stock = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btn_Execute
            // 
            this.btn_Execute.Location = new System.Drawing.Point(43, 95);
            this.btn_Execute.Name = "btn_Execute";
            this.btn_Execute.Size = new System.Drawing.Size(75, 23);
            this.btn_Execute.TabIndex = 3;
            this.btn_Execute.Text = "&Execute";
            this.btn_Execute.UseVisualStyleBackColor = true;
            this.btn_Execute.Click += new System.EventHandler(this.button1_Click);
            // 
            // ckb_Rates
            // 
            this.ckb_Rates.AutoSize = true;
            this.ckb_Rates.Location = new System.Drawing.Point(30, 31);
            this.ckb_Rates.Name = "ckb_Rates";
            this.ckb_Rates.Size = new System.Drawing.Size(115, 17);
            this.ckb_Rates.TabIndex = 1;
            this.ckb_Rates.Tag = "PF_A_Rates";
            this.ckb_Rates.Text = "&Rates Relationship";
            this.ckb_Rates.UseVisualStyleBackColor = true;
            this.ckb_Rates.CheckedChanged += new System.EventHandler(this.ckb_CheckedChanged);
            this.ckb_Rates.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            // 
            // ckb_stock
            // 
            this.ckb_stock.AutoSize = true;
            this.ckb_stock.Location = new System.Drawing.Point(30, 54);
            this.ckb_stock.Name = "ckb_stock";
            this.ckb_stock.Size = new System.Drawing.Size(115, 17);
            this.ckb_stock.TabIndex = 2;
            this.ckb_stock.Tag = "PF_Stock_Item";
            this.ckb_stock.Text = "&Stock Relationship";
            this.ckb_stock.UseVisualStyleBackColor = true;
            this.ckb_stock.CheckedChanged += new System.EventHandler(this.ckb_CheckedChanged);
            this.ckb_stock.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_1);
            this.ckb_stock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Enter_KeyPress);
            // 
            // Material_Relationship_Maint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 361);
            this.Controls.Add(this.ckb_stock);
            this.Controls.Add(this.ckb_Rates);
            this.Controls.Add(this.btn_Execute);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Material_Relationship_Maint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PF.Utils.Common.Material Relationship Maintenance";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Material_Relationship_Maint_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Execute;
        private System.Windows.Forms.CheckBox ckb_Rates;
        private System.Windows.Forms.CheckBox ckb_stock;
    }
}