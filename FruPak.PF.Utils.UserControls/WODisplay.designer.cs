namespace FruPak.PF.Utils.UserControls
{
    partial class WODisplay
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
            this.lbl_Date = new System.Windows.Forms.Label();
            this.txt_Process_Date = new System.Windows.Forms.TextBox();
            this.lbl_Work_Order = new System.Windows.Forms.Label();
            this.txt_Work_Order = new System.Windows.Forms.TextBox();
            this.lbl_Product = new System.Windows.Forms.Label();
            this.txt_Product = new System.Windows.Forms.TextBox();
            this.txt_Fruit_Type = new System.Windows.Forms.TextBox();
            this.txt_Fruit_Variety = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbl_Date
            // 
            this.lbl_Date.AutoSize = true;
            this.lbl_Date.Location = new System.Drawing.Point(203, 6);
            this.lbl_Date.Name = "lbl_Date";
            this.lbl_Date.Size = new System.Drawing.Size(33, 13);
            this.lbl_Date.TabIndex = 0;
            this.lbl_Date.Text = "Date:";
            // 
            // txt_Process_Date
            // 
            this.txt_Process_Date.Location = new System.Drawing.Point(242, 3);
            this.txt_Process_Date.Name = "txt_Process_Date";
            this.txt_Process_Date.ReadOnly = true;
            this.txt_Process_Date.Size = new System.Drawing.Size(100, 20);
            this.txt_Process_Date.TabIndex = 2;
            // 
            // lbl_Work_Order
            // 
            this.lbl_Work_Order.AutoSize = true;
            this.lbl_Work_Order.Location = new System.Drawing.Point(3, 7);
            this.lbl_Work_Order.Name = "lbl_Work_Order";
            this.lbl_Work_Order.Size = new System.Drawing.Size(72, 13);
            this.lbl_Work_Order.TabIndex = 2;
            this.lbl_Work_Order.Text = "Work Order #";
            // 
            // txt_Work_Order
            // 
            this.txt_Work_Order.Location = new System.Drawing.Point(84, 3);
            this.txt_Work_Order.Name = "txt_Work_Order";
            this.txt_Work_Order.ReadOnly = true;
            this.txt_Work_Order.Size = new System.Drawing.Size(100, 20);
            this.txt_Work_Order.TabIndex = 1;
            // 
            // lbl_Product
            // 
            this.lbl_Product.AutoSize = true;
            this.lbl_Product.Location = new System.Drawing.Point(362, 6);
            this.lbl_Product.Name = "lbl_Product";
            this.lbl_Product.Size = new System.Drawing.Size(47, 13);
            this.lbl_Product.TabIndex = 4;
            this.lbl_Product.Text = "Product:";
            // 
            // txt_Product
            // 
            this.txt_Product.Location = new System.Drawing.Point(415, 3);
            this.txt_Product.Name = "txt_Product";
            this.txt_Product.ReadOnly = true;
            this.txt_Product.Size = new System.Drawing.Size(100, 20);
            this.txt_Product.TabIndex = 3;
            // 
            // txt_Fruit_Type
            // 
            this.txt_Fruit_Type.Location = new System.Drawing.Point(531, 3);
            this.txt_Fruit_Type.Name = "txt_Fruit_Type";
            this.txt_Fruit_Type.ReadOnly = true;
            this.txt_Fruit_Type.Size = new System.Drawing.Size(100, 20);
            this.txt_Fruit_Type.TabIndex = 4;
            // 
            // txt_Fruit_Variety
            // 
            this.txt_Fruit_Variety.Location = new System.Drawing.Point(637, 3);
            this.txt_Fruit_Variety.Name = "txt_Fruit_Variety";
            this.txt_Fruit_Variety.ReadOnly = true;
            this.txt_Fruit_Variety.Size = new System.Drawing.Size(200, 20);
            this.txt_Fruit_Variety.TabIndex = 5;
            // 
            // WODisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txt_Fruit_Variety);
            this.Controls.Add(this.txt_Fruit_Type);
            this.Controls.Add(this.txt_Product);
            this.Controls.Add(this.lbl_Product);
            this.Controls.Add(this.txt_Work_Order);
            this.Controls.Add(this.lbl_Work_Order);
            this.Controls.Add(this.txt_Process_Date);
            this.Controls.Add(this.lbl_Date);
            this.Name = "WODisplay";
            this.Size = new System.Drawing.Size(846, 29);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Date;
        private System.Windows.Forms.TextBox txt_Process_Date;
        private System.Windows.Forms.Label lbl_Work_Order;
        private System.Windows.Forms.TextBox txt_Work_Order;
        private System.Windows.Forms.Label lbl_Product;
        private System.Windows.Forms.TextBox txt_Product;
        private System.Windows.Forms.TextBox txt_Fruit_Type;
        private System.Windows.Forms.TextBox txt_Fruit_Variety;
    }
}
