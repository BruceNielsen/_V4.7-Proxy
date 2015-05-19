namespace FruPak.PF.Dispatch
{
    partial class Shipping_Order
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Shipping_Order));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txt_Customer_Ref = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_Truck = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmb_Destination = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtp_Load_Date = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_Comments = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_Freight_Docket = new System.Windows.Forms.Label();
            this.txt_Freight_Docket = new System.Windows.Forms.TextBox();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.lbl_message = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.customer1 = new FruPak.PF.Utils.UserControls.Customer();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ckb_Hold_For_Payment = new System.Windows.Forms.CheckBox();
            this.panelShipping_Order = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.panelShipping_Order.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(15, 275);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(967, 279);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // txt_Customer_Ref
            // 
            this.txt_Customer_Ref.Location = new System.Drawing.Point(123, 119);
            this.txt_Customer_Ref.MaxLength = 20;
            this.txt_Customer_Ref.Name = "txt_Customer_Ref";
            this.txt_Customer_Ref.Size = new System.Drawing.Size(100, 20);
            this.txt_Customer_Ref.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Order Reference";
            // 
            // cmb_Truck
            // 
            this.cmb_Truck.FormattingEnabled = true;
            this.cmb_Truck.Location = new System.Drawing.Point(477, 61);
            this.cmb_Truck.Name = "cmb_Truck";
            this.cmb_Truck.Size = new System.Drawing.Size(121, 21);
            this.cmb_Truck.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(391, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Truck";
            // 
            // cmb_Destination
            // 
            this.cmb_Destination.FormattingEnabled = true;
            this.cmb_Destination.Location = new System.Drawing.Point(477, 89);
            this.cmb_Destination.Name = "cmb_Destination";
            this.cmb_Destination.Size = new System.Drawing.Size(121, 21);
            this.cmb_Destination.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(391, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Destination";
            // 
            // dtp_Load_Date
            // 
            this.dtp_Load_Date.Location = new System.Drawing.Point(123, 58);
            this.dtp_Load_Date.Name = "dtp_Load_Date";
            this.dtp_Load_Date.Size = new System.Drawing.Size(200, 20);
            this.dtp_Load_Date.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Load Date";
            // 
            // txt_Comments
            // 
            this.txt_Comments.Location = new System.Drawing.Point(96, 154);
            this.txt_Comments.Multiline = true;
            this.txt_Comments.Name = "txt_Comments";
            this.txt_Comments.Size = new System.Drawing.Size(502, 105);
            this.txt_Comments.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(29, 157);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Comments";
            // 
            // lbl_Freight_Docket
            // 
            this.lbl_Freight_Docket.AutoSize = true;
            this.lbl_Freight_Docket.Location = new System.Drawing.Point(391, 119);
            this.lbl_Freight_Docket.Name = "lbl_Freight_Docket";
            this.lbl_Freight_Docket.Size = new System.Drawing.Size(80, 13);
            this.lbl_Freight_Docket.TabIndex = 36;
            this.lbl_Freight_Docket.Text = "Freight Docket:";
            // 
            // txt_Freight_Docket
            // 
            this.txt_Freight_Docket.Location = new System.Drawing.Point(477, 116);
            this.txt_Freight_Docket.MaxLength = 20;
            this.txt_Freight_Docket.Name = "txt_Freight_Docket";
            this.txt_Freight_Docket.Size = new System.Drawing.Size(121, 20);
            this.txt_Freight_Docket.TabIndex = 6;
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(889, 5);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 14;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(727, 5);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_reset.TabIndex = 12;
            this.btn_reset.Text = "Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(646, 5);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 11;
            this.btn_Add.Text = "Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Location = new System.Drawing.Point(808, 5);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(75, 23);
            this.btn_Refresh.TabIndex = 13;
            this.btn_Refresh.Text = "Refresh";
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.Location = new System.Drawing.Point(12, 9);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(0, 17);
            this.lbl_message.TabIndex = 42;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(623, 109);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.Size = new System.Drawing.Size(359, 150);
            this.dataGridView2.TabIndex = 9;
            this.dataGridView2.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellLeave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(620, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(266, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "Enter the number of pallets/bins being used for delivery";
            // 
            // customer1
            // 
            this.customer1.CFocused = false;
            this.customer1.Customer_Id = 0;
            this.customer1.Location = new System.Drawing.Point(29, 84);
            this.customer1.Name = "customer1";
            this.customer1.Size = new System.Drawing.Size(294, 28);
            this.customer1.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(21, 20);
            this.textBox1.TabIndex = 67;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 68;
            this.label3.Text = "= Overdue Orders";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Location = new System.Drawing.Point(147, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(21, 20);
            this.textBox2.TabIndex = 69;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(174, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 13);
            this.label8.TabIndex = 70;
            this.label8.Text = "= Current Orders";
            // 
            // ckb_Hold_For_Payment
            // 
            this.ckb_Hold_For_Payment.AutoSize = true;
            this.ckb_Hold_For_Payment.Location = new System.Drawing.Point(623, 64);
            this.ckb_Hold_For_Payment.Name = "ckb_Hold_For_Payment";
            this.ckb_Hold_For_Payment.Size = new System.Drawing.Size(107, 17);
            this.ckb_Hold_For_Payment.TabIndex = 8;
            this.ckb_Hold_For_Payment.Text = "Hold for Payment";
            this.ckb_Hold_For_Payment.UseVisualStyleBackColor = true;
            // 
            // panelShipping_Order
            // 
            this.panelShipping_Order.Controls.Add(this.textBox1);
            this.panelShipping_Order.Controls.Add(this.label3);
            this.panelShipping_Order.Controls.Add(this.label8);
            this.panelShipping_Order.Controls.Add(this.textBox2);
            this.panelShipping_Order.Controls.Add(this.btn_Close);
            this.panelShipping_Order.Controls.Add(this.btn_Refresh);
            this.panelShipping_Order.Controls.Add(this.btn_Add);
            this.panelShipping_Order.Controls.Add(this.btn_reset);
            this.panelShipping_Order.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelShipping_Order.Location = new System.Drawing.Point(0, 578);
            this.panelShipping_Order.Name = "panelShipping_Order";
            this.panelShipping_Order.Size = new System.Drawing.Size(1006, 35);
            this.panelShipping_Order.TabIndex = 72;
            // 
            // Shipping_Order
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 613);
            this.Controls.Add(this.panelShipping_Order);
            this.Controls.Add(this.ckb_Hold_For_Payment);
            this.Controls.Add(this.customer1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.txt_Freight_Docket);
            this.Controls.Add(this.lbl_Freight_Docket);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt_Comments);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtp_Load_Date);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmb_Destination);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmb_Truck);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Customer_Ref);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Shipping_Order";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FruPak.PF.Dispatch.Shipping Order Summary";
            this.Load += new System.EventHandler(this.SizeAllColumns);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Shipping_Order_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.panelShipping_Order.ResumeLayout(false);
            this.panelShipping_Order.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txt_Customer_Ref;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_Truck;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmb_Destination;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtp_Load_Date;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_Comments;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_Freight_Docket;
        private System.Windows.Forms.TextBox txt_Freight_Docket;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label2;
        private Utils.UserControls.Customer customer1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox ckb_Hold_For_Payment;
        private System.Windows.Forms.Panel panelShipping_Order;
    }
}