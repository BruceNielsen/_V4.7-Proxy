namespace FruPak.PF.Utils.UserControls
{
    partial class Test_Material_Number
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Test_Material_Number));
            this.materialNumber1 = new FruPak.PF.Utils.UserControls.MaterialNumber();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // materialNumber1
            // 
            this.materialNumber1.Location = new System.Drawing.Point(12, 26);
            this.materialNumber1.Material_Number = 0;
            this.materialNumber1.Name = "materialNumber1";
            this.materialNumber1.selectedBrand = 0;
            this.materialNumber1.selectedGrade = 0;
            this.materialNumber1.selectedGrowingMethod = 0;
            this.materialNumber1.selectedMaterial = 0;
            this.materialNumber1.selectedPackType = 0;
            this.materialNumber1.selectedProductGroup = 0;
            this.materialNumber1.selectedSize = 0;
            this.materialNumber1.selectedTrader = 0;
            this.materialNumber1.selectedTreatment = 0;
            this.materialNumber1.selectedVariety = 0;
            this.materialNumber1.selectedWeight = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.materialNumber1.Size = new System.Drawing.Size(882, 82);
            this.materialNumber1.TabIndex = 1;
            this.materialNumber1.Load += new System.EventHandler(this.materialNumber1_Load);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(391, 157);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(200, 159);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 2;
            // 
            // Test_Material_Number
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 262);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.materialNumber1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Test_Material_Number";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FruPak.PF.Utils.UserControls.Test Material Number";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Test_Material_Number_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialNumber materialNumber1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
    }
}