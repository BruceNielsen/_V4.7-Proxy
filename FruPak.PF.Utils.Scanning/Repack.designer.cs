namespace PF.Utils.Scanning
{
    partial class Repack
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Repack));
            this.label1 = new System.Windows.Forms.Label();
            this.txt_barcode_from = new System.Windows.Forms.TextBox();
            this.cmb_Pallet_Type = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_Material = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Update = new System.Windows.Forms.Button();
            this.lbl_message = new System.Windows.Forms.Label();
            this.btn_Close = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_total = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_Barcode_To = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.labelBarcodeToBeCopied = new System.Windows.Forms.Label();
            this.buttonCopyBarcodeToClipboard = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonClear = new System.Windows.Forms.Button();
            this.labelBarcodeForSel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "From:";
            // 
            // txt_barcode_from
            // 
            this.txt_barcode_from.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_barcode_from.Location = new System.Drawing.Point(102, 83);
            this.txt_barcode_from.Name = "txt_barcode_from";
            this.txt_barcode_from.Size = new System.Drawing.Size(532, 26);
            this.txt_barcode_from.TabIndex = 2;
            this.txt_barcode_from.TextChanged += new System.EventHandler(this.txt_barcode_from_TextChanged);
            this.txt_barcode_from.Leave += new System.EventHandler(this.txt_barcode_Leave);
            // 
            // cmb_Pallet_Type
            // 
            this.cmb_Pallet_Type.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_Pallet_Type.FormattingEnabled = true;
            this.cmb_Pallet_Type.Location = new System.Drawing.Point(102, 37);
            this.cmb_Pallet_Type.Name = "cmb_Pallet_Type";
            this.cmb_Pallet_Type.Size = new System.Drawing.Size(532, 28);
            this.cmb_Pallet_Type.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Pallet Type:";
            // 
            // cmb_Material
            // 
            this.cmb_Material.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_Material.FormattingEnabled = true;
            this.cmb_Material.Location = new System.Drawing.Point(10, 141);
            this.cmb_Material.Name = "cmb_Material";
            this.cmb_Material.Size = new System.Drawing.Size(625, 28);
            this.cmb_Material.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Material Num:";
            // 
            // btn_Update
            // 
            this.btn_Update.Location = new System.Drawing.Point(470, 222);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(79, 55);
            this.btn_Update.TabIndex = 7;
            this.btn_Update.Text = "&Update";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(12, 9);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(2, 22);
            this.lbl_message.TabIndex = 9;
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(555, 222);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(79, 55);
            this.btn_Close.TabIndex = 8;
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(896, 254);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "Total";
            // 
            // txt_total
            // 
            this.txt_total.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_total.Location = new System.Drawing.Point(946, 251);
            this.txt_total.Name = "txt_total";
            this.txt_total.ReadOnly = true;
            this.txt_total.Size = new System.Drawing.Size(50, 26);
            this.txt_total.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 193);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 20);
            this.label5.TabIndex = 15;
            this.label5.Text = "To:";
            // 
            // txt_Barcode_To
            // 
            this.txt_Barcode_To.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Barcode_To.Location = new System.Drawing.Point(103, 251);
            this.txt_Barcode_To.Name = "txt_Barcode_To";
            this.txt_Barcode_To.Size = new System.Drawing.Size(335, 26);
            this.txt_Barcode_To.TabIndex = 6;
            this.txt_Barcode_To.Visible = false;
            this.txt_Barcode_To.TextChanged += new System.EventHandler(this.txt_Barcode_To_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(641, 37);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 5;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowTemplate.Height = 50;
            this.dataGridView1.Size = new System.Drawing.Size(355, 208);
            this.dataGridView1.TabIndex = 9;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // labelBarcodeToBeCopied
            // 
            this.labelBarcodeToBeCopied.AutoSize = true;
            this.labelBarcodeToBeCopied.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBarcodeToBeCopied.Location = new System.Drawing.Point(640, 257);
            this.labelBarcodeToBeCopied.Name = "labelBarcodeToBeCopied";
            this.labelBarcodeToBeCopied.Size = new System.Drawing.Size(13, 20);
            this.labelBarcodeToBeCopied.TabIndex = 19;
            this.labelBarcodeToBeCopied.Text = ".";
            this.labelBarcodeToBeCopied.Visible = false;
            // 
            // buttonCopyBarcodeToClipboard
            // 
            this.buttonCopyBarcodeToClipboard.Location = new System.Drawing.Point(102, 222);
            this.buttonCopyBarcodeToClipboard.Name = "buttonCopyBarcodeToClipboard";
            this.buttonCopyBarcodeToClipboard.Size = new System.Drawing.Size(108, 23);
            this.buttonCopyBarcodeToClipboard.TabIndex = 4;
            this.buttonCopyBarcodeToClipboard.Text = "&Copy to Clipboard";
            this.buttonCopyBarcodeToClipboard.UseVisualStyleBackColor = true;
            this.buttonCopyBarcodeToClipboard.Click += new System.EventHandler(this.buttonCopyBarcodeToClipboard_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(216, 222);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(108, 23);
            this.buttonClear.TabIndex = 5;
            this.buttonClear.Text = "Clear &Label";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Visible = false;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // labelBarcodeForSel
            // 
            this.labelBarcodeForSel.AutoSize = true;
            this.labelBarcodeForSel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBarcodeForSel.Location = new System.Drawing.Point(99, 193);
            this.labelBarcodeForSel.Name = "labelBarcodeForSel";
            this.labelBarcodeForSel.Size = new System.Drawing.Size(13, 20);
            this.labelBarcodeForSel.TabIndex = 20;
            this.labelBarcodeForSel.Text = ".";
            // 
            // Repack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 294);
            this.Controls.Add(this.labelBarcodeForSel);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonCopyBarcodeToClipboard);
            this.Controls.Add(this.labelBarcodeToBeCopied);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_Barcode_To);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_total);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.btn_Update);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmb_Material);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmb_Pallet_Type);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_barcode_from);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Repack";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PF.Utils.Scanning.Repack";
            this.Load += new System.EventHandler(this.SizeAllColumns);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Repack_KeyDown);
            this.Resize += new System.EventHandler(this.Repack_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_barcode_from;
        private System.Windows.Forms.ComboBox cmb_Pallet_Type;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_Material;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Update;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_total;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_Barcode_To;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label labelBarcodeToBeCopied;
        private System.Windows.Forms.Button buttonCopyBarcodeToClipboard;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Label labelBarcodeForSel;
    }
}