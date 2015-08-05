namespace FruPak.PF.WorkOrder
{
    partial class WO_Create
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WO_Create));
            this.dtp_date = new System.Windows.Forms.DateTimePicker();
            this.lbl_Date = new System.Windows.Forms.Label();
            this.cmb_Product = new System.Windows.Forms.ComboBox();
            this.lbl_Product = new System.Windows.Forms.Label();
            this.txt_batches = new System.Windows.Forms.TextBox();
            this.lbl_Batches = new System.Windows.Forms.Label();
            this.lbl_bin_tipped = new System.Windows.Forms.Label();
            this.txt_bins_tipped = new System.Windows.Forms.TextBox();
            this.lbl_Current_Batch = new System.Windows.Forms.Label();
            this.txt_current_batch = new System.Windows.Forms.TextBox();
            this.lbl_ctn_Batch = new System.Windows.Forms.Label();
            this.txt_ctn_batch = new System.Windows.Forms.TextBox();
            this.lbl_total_ctns = new System.Windows.Forms.Label();
            this.txt_total_ctn = new System.Windows.Forms.TextBox();
            this.dtp_start = new System.Windows.Forms.DateTimePicker();
            this.lbl_start = new System.Windows.Forms.Label();
            this.lbl_finish = new System.Windows.Forms.Label();
            this.dtp_finish = new System.Windows.Forms.DateTimePicker();
            this.txt_comments = new System.Windows.Forms.TextBox();
            this.lbl_comments = new System.Windows.Forms.Label();
            this.btn_Print = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.btn_Reset = new System.Windows.Forms.Button();
            this.btn_Current = new System.Windows.Forms.Button();
            this.lbl_message = new System.Windows.Forms.Label();
            this.txt_WorkOrder = new System.Windows.Forms.TextBox();
            this.lbl_WorkOrder = new System.Windows.Forms.Label();
            this.gb_results = new System.Windows.Forms.GroupBox();
            this.btn_Close = new System.Windows.Forms.Button();
            this.lbl_Trader = new System.Windows.Forms.Label();
            this.cmb_Trader = new System.Windows.Forms.ComboBox();
            this.lbl_Growing_method = new System.Windows.Forms.Label();
            this.cmb_Growing_Method = new System.Windows.Forms.ComboBox();
            this.btn_Update = new System.Windows.Forms.Button();
            this.grower1 = new FruPak.PF.Utils.UserControls.Grower();
            this.fruit1 = new FruPak.PF.Utils.UserControls.Fruit();
            this.gb_results.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtp_date
            // 
            this.dtp_date.Location = new System.Drawing.Point(93, 85);
            this.dtp_date.Name = "dtp_date";
            this.dtp_date.Size = new System.Drawing.Size(200, 20);
            this.dtp_date.TabIndex = 1;
            // 
            // lbl_Date
            // 
            this.lbl_Date.AutoSize = true;
            this.lbl_Date.Location = new System.Drawing.Point(30, 90);
            this.lbl_Date.Name = "lbl_Date";
            this.lbl_Date.Size = new System.Drawing.Size(30, 13);
            this.lbl_Date.TabIndex = 1;
            this.lbl_Date.Text = "Date";
            // 
            // cmb_Product
            // 
            this.cmb_Product.FormattingEnabled = true;
            this.cmb_Product.Location = new System.Drawing.Point(93, 236);
            this.cmb_Product.Name = "cmb_Product";
            this.cmb_Product.Size = new System.Drawing.Size(121, 21);
            this.cmb_Product.TabIndex = 6;
            // 
            // lbl_Product
            // 
            this.lbl_Product.AutoSize = true;
            this.lbl_Product.Location = new System.Drawing.Point(34, 239);
            this.lbl_Product.Name = "lbl_Product";
            this.lbl_Product.Size = new System.Drawing.Size(44, 13);
            this.lbl_Product.TabIndex = 3;
            this.lbl_Product.Text = "Product";
            // 
            // txt_batches
            // 
            this.txt_batches.Location = new System.Drawing.Point(442, 239);
            this.txt_batches.MaxLength = 2;
            this.txt_batches.Name = "txt_batches";
            this.txt_batches.Size = new System.Drawing.Size(40, 20);
            this.txt_batches.TabIndex = 8;
            // 
            // lbl_Batches
            // 
            this.lbl_Batches.AutoSize = true;
            this.lbl_Batches.Location = new System.Drawing.Point(359, 242);
            this.lbl_Batches.Name = "lbl_Batches";
            this.lbl_Batches.Size = new System.Drawing.Size(71, 13);
            this.lbl_Batches.TabIndex = 5;
            this.lbl_Batches.Text = "Num Batches";
            // 
            // lbl_bin_tipped
            // 
            this.lbl_bin_tipped.AutoSize = true;
            this.lbl_bin_tipped.Location = new System.Drawing.Point(11, 22);
            this.lbl_bin_tipped.Name = "lbl_bin_tipped";
            this.lbl_bin_tipped.Size = new System.Drawing.Size(63, 13);
            this.lbl_bin_tipped.TabIndex = 17;
            this.lbl_bin_tipped.Text = "Bins Tipped";
            // 
            // txt_bins_tipped
            // 
            this.txt_bins_tipped.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_bins_tipped.Location = new System.Drawing.Point(87, 20);
            this.txt_bins_tipped.Name = "txt_bins_tipped";
            this.txt_bins_tipped.ReadOnly = true;
            this.txt_bins_tipped.Size = new System.Drawing.Size(79, 13);
            this.txt_bins_tipped.TabIndex = 16;
            this.txt_bins_tipped.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl_Current_Batch
            // 
            this.lbl_Current_Batch.AutoSize = true;
            this.lbl_Current_Batch.Location = new System.Drawing.Point(11, 48);
            this.lbl_Current_Batch.Name = "lbl_Current_Batch";
            this.lbl_Current_Batch.Size = new System.Drawing.Size(72, 13);
            this.lbl_Current_Batch.TabIndex = 19;
            this.lbl_Current_Batch.Text = "Current Batch";
            // 
            // txt_current_batch
            // 
            this.txt_current_batch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_current_batch.Location = new System.Drawing.Point(87, 46);
            this.txt_current_batch.Name = "txt_current_batch";
            this.txt_current_batch.ReadOnly = true;
            this.txt_current_batch.Size = new System.Drawing.Size(79, 13);
            this.txt_current_batch.TabIndex = 18;
            this.txt_current_batch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl_ctn_Batch
            // 
            this.lbl_ctn_Batch.AutoSize = true;
            this.lbl_ctn_Batch.Location = new System.Drawing.Point(11, 74);
            this.lbl_ctn_Batch.Name = "lbl_ctn_Batch";
            this.lbl_ctn_Batch.Size = new System.Drawing.Size(70, 13);
            this.lbl_ctn_Batch.TabIndex = 21;
            this.lbl_ctn_Batch.Text = "Ctns in Batch";
            // 
            // txt_ctn_batch
            // 
            this.txt_ctn_batch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_ctn_batch.Location = new System.Drawing.Point(87, 72);
            this.txt_ctn_batch.Name = "txt_ctn_batch";
            this.txt_ctn_batch.ReadOnly = true;
            this.txt_ctn_batch.Size = new System.Drawing.Size(79, 13);
            this.txt_ctn_batch.TabIndex = 20;
            this.txt_ctn_batch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl_total_ctns
            // 
            this.lbl_total_ctns.AutoSize = true;
            this.lbl_total_ctns.Location = new System.Drawing.Point(11, 100);
            this.lbl_total_ctns.Name = "lbl_total_ctns";
            this.lbl_total_ctns.Size = new System.Drawing.Size(55, 13);
            this.lbl_total_ctns.TabIndex = 23;
            this.lbl_total_ctns.Text = "Total Ctns";
            // 
            // txt_total_ctn
            // 
            this.txt_total_ctn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_total_ctn.Location = new System.Drawing.Point(87, 98);
            this.txt_total_ctn.Name = "txt_total_ctn";
            this.txt_total_ctn.ReadOnly = true;
            this.txt_total_ctn.Size = new System.Drawing.Size(79, 13);
            this.txt_total_ctn.TabIndex = 22;
            this.txt_total_ctn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dtp_start
            // 
            this.dtp_start.CustomFormat = "HH:mm:ss";
            this.dtp_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_start.Location = new System.Drawing.Point(93, 120);
            this.dtp_start.Name = "dtp_start";
            this.dtp_start.ShowUpDown = true;
            this.dtp_start.Size = new System.Drawing.Size(78, 20);
            this.dtp_start.TabIndex = 2;
            // 
            // lbl_start
            // 
            this.lbl_start.AutoSize = true;
            this.lbl_start.Location = new System.Drawing.Point(31, 120);
            this.lbl_start.Name = "lbl_start";
            this.lbl_start.Size = new System.Drawing.Size(29, 13);
            this.lbl_start.TabIndex = 25;
            this.lbl_start.Text = "Start";
            // 
            // lbl_finish
            // 
            this.lbl_finish.AutoSize = true;
            this.lbl_finish.Location = new System.Drawing.Point(231, 123);
            this.lbl_finish.Name = "lbl_finish";
            this.lbl_finish.Size = new System.Drawing.Size(34, 13);
            this.lbl_finish.TabIndex = 27;
            this.lbl_finish.Text = "Finish";
            // 
            // dtp_finish
            // 
            this.dtp_finish.CustomFormat = "HH:mm:ss";
            this.dtp_finish.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_finish.Location = new System.Drawing.Point(271, 120);
            this.dtp_finish.Name = "dtp_finish";
            this.dtp_finish.ShowUpDown = true;
            this.dtp_finish.Size = new System.Drawing.Size(74, 20);
            this.dtp_finish.TabIndex = 3;
            // 
            // txt_comments
            // 
            this.txt_comments.Location = new System.Drawing.Point(95, 332);
            this.txt_comments.Multiline = true;
            this.txt_comments.Name = "txt_comments";
            this.txt_comments.Size = new System.Drawing.Size(396, 137);
            this.txt_comments.TabIndex = 10;
            // 
            // lbl_comments
            // 
            this.lbl_comments.AutoSize = true;
            this.lbl_comments.Location = new System.Drawing.Point(31, 335);
            this.lbl_comments.Name = "lbl_comments";
            this.lbl_comments.Size = new System.Drawing.Size(56, 13);
            this.lbl_comments.TabIndex = 33;
            this.lbl_comments.Text = "Comments";
            // 
            // btn_Print
            // 
            this.btn_Print.Location = new System.Drawing.Point(93, 525);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(75, 23);
            this.btn_Print.TabIndex = 12;
            this.btn_Print.Text = "&Print";
            this.btn_Print.UseVisualStyleBackColor = true;
            this.btn_Print.Click += new System.EventHandler(this.btn_Print_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(255, 525);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 14;
            this.btn_Add.Text = "&Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // btn_Reset
            // 
            this.btn_Reset.Location = new System.Drawing.Point(336, 525);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(75, 23);
            this.btn_Reset.TabIndex = 15;
            this.btn_Reset.Text = "&Reset";
            this.btn_Reset.UseVisualStyleBackColor = true;
            this.btn_Reset.Click += new System.EventHandler(this.btn_Reset_Click);
            // 
            // btn_Current
            // 
            this.btn_Current.Location = new System.Drawing.Point(93, 475);
            this.btn_Current.Name = "btn_Current";
            this.btn_Current.Size = new System.Drawing.Size(97, 26);
            this.btn_Current.TabIndex = 11;
            this.btn_Current.Text = "&Set to Current";
            this.btn_Current.UseVisualStyleBackColor = true;
            this.btn_Current.Click += new System.EventHandler(this.btn_Current_Click);
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_message.ForeColor = System.Drawing.Color.Red;
            this.lbl_message.Location = new System.Drawing.Point(33, 9);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(0, 20);
            this.lbl_message.TabIndex = 38;
            // 
            // txt_WorkOrder
            // 
            this.txt_WorkOrder.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_WorkOrder.Enabled = false;
            this.txt_WorkOrder.Location = new System.Drawing.Point(114, 44);
            this.txt_WorkOrder.Name = "txt_WorkOrder";
            this.txt_WorkOrder.ReadOnly = true;
            this.txt_WorkOrder.Size = new System.Drawing.Size(100, 13);
            this.txt_WorkOrder.TabIndex = 39;
            // 
            // lbl_WorkOrder
            // 
            this.lbl_WorkOrder.AutoSize = true;
            this.lbl_WorkOrder.Location = new System.Drawing.Point(30, 46);
            this.lbl_WorkOrder.Name = "lbl_WorkOrder";
            this.lbl_WorkOrder.Size = new System.Drawing.Size(62, 13);
            this.lbl_WorkOrder.TabIndex = 40;
            this.lbl_WorkOrder.Text = "Work Order";
            // 
            // gb_results
            // 
            this.gb_results.Controls.Add(this.txt_current_batch);
            this.gb_results.Controls.Add(this.txt_bins_tipped);
            this.gb_results.Controls.Add(this.lbl_bin_tipped);
            this.gb_results.Controls.Add(this.lbl_Current_Batch);
            this.gb_results.Controls.Add(this.txt_ctn_batch);
            this.gb_results.Controls.Add(this.lbl_ctn_Batch);
            this.gb_results.Controls.Add(this.txt_total_ctn);
            this.gb_results.Controls.Add(this.lbl_total_ctns);
            this.gb_results.Location = new System.Drawing.Point(408, 44);
            this.gb_results.Name = "gb_results";
            this.gb_results.Size = new System.Drawing.Size(172, 126);
            this.gb_results.TabIndex = 42;
            this.gb_results.TabStop = false;
            this.gb_results.Text = "Results";
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(417, 525);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 16;
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // lbl_Trader
            // 
            this.lbl_Trader.AutoSize = true;
            this.lbl_Trader.Location = new System.Drawing.Point(34, 156);
            this.lbl_Trader.Name = "lbl_Trader";
            this.lbl_Trader.Size = new System.Drawing.Size(38, 13);
            this.lbl_Trader.TabIndex = 47;
            this.lbl_Trader.Text = "Trader";
            // 
            // cmb_Trader
            // 
            this.cmb_Trader.FormattingEnabled = true;
            this.cmb_Trader.Location = new System.Drawing.Point(93, 153);
            this.cmb_Trader.Name = "cmb_Trader";
            this.cmb_Trader.Size = new System.Drawing.Size(200, 21);
            this.cmb_Trader.TabIndex = 4;
            // 
            // lbl_Growing_method
            // 
            this.lbl_Growing_method.AutoSize = true;
            this.lbl_Growing_method.Location = new System.Drawing.Point(351, 268);
            this.lbl_Growing_method.Name = "lbl_Growing_method";
            this.lbl_Growing_method.Size = new System.Drawing.Size(85, 13);
            this.lbl_Growing_method.TabIndex = 51;
            this.lbl_Growing_method.Text = "Growing Method";
            // 
            // cmb_Growing_Method
            // 
            this.cmb_Growing_Method.FormattingEnabled = true;
            this.cmb_Growing_Method.Location = new System.Drawing.Point(442, 265);
            this.cmb_Growing_Method.Name = "cmb_Growing_Method";
            this.cmb_Growing_Method.Size = new System.Drawing.Size(136, 21);
            this.cmb_Growing_Method.TabIndex = 9;
            // 
            // btn_Update
            // 
            this.btn_Update.Location = new System.Drawing.Point(174, 525);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(75, 23);
            this.btn_Update.TabIndex = 13;
            this.btn_Update.Text = "&Update";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // grower1
            // 
            this.grower1.bol_test = false;
            this.grower1.Grower_Id = 0;
            this.grower1.Location = new System.Drawing.Point(34, 180);
            this.grower1.Name = "grower1";
            this.grower1.Orchardist_Id = 0;
            this.grower1.Size = new System.Drawing.Size(268, 53);
            this.grower1.TabIndex = 5;
            // 
            // fruit1
            // 
            this.fruit1.Block_Id = 0;
            this.fruit1.FruitType_Id = 0;
            this.fruit1.FruitVariety_Id = 0;
            this.fruit1.Location = new System.Drawing.Point(33, 260);
            this.fruit1.Name = "fruit1";
            this.fruit1.Size = new System.Drawing.Size(269, 54);
            this.fruit1.TabIndex = 7;
            this.fruit1.FruitVarietyChanged += new System.EventHandler(this.fruit1_FruitVarietyChanged);
            this.fruit1.FruitTypeChanged += new System.EventHandler(this.fruit1_FruitTypeChanged);
            // 
            // WO_Create
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 613);
            this.Controls.Add(this.grower1);
            this.Controls.Add(this.btn_Update);
            this.Controls.Add(this.lbl_Growing_method);
            this.Controls.Add(this.cmb_Growing_Method);
            this.Controls.Add(this.lbl_Trader);
            this.Controls.Add(this.cmb_Trader);
            this.Controls.Add(this.fruit1);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.gb_results);
            this.Controls.Add(this.lbl_WorkOrder);
            this.Controls.Add(this.txt_WorkOrder);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.btn_Current);
            this.Controls.Add(this.btn_Reset);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.btn_Print);
            this.Controls.Add(this.lbl_comments);
            this.Controls.Add(this.txt_comments);
            this.Controls.Add(this.lbl_finish);
            this.Controls.Add(this.dtp_finish);
            this.Controls.Add(this.lbl_start);
            this.Controls.Add(this.dtp_start);
            this.Controls.Add(this.lbl_Batches);
            this.Controls.Add(this.txt_batches);
            this.Controls.Add(this.lbl_Product);
            this.Controls.Add(this.cmb_Product);
            this.Controls.Add(this.lbl_Date);
            this.Controls.Add(this.dtp_date);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "WO_Create";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FruPak.PF.WorkOrder.Work Order Create";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WO_Create_KeyDown);
            this.gb_results.ResumeLayout(false);
            this.gb_results.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtp_date;
        private System.Windows.Forms.Label lbl_Date;
        private System.Windows.Forms.ComboBox cmb_Product;
        private System.Windows.Forms.Label lbl_Product;
        private System.Windows.Forms.TextBox txt_batches;
        private System.Windows.Forms.Label lbl_Batches;
        private System.Windows.Forms.Label lbl_bin_tipped;
        private System.Windows.Forms.TextBox txt_bins_tipped;
        private System.Windows.Forms.Label lbl_Current_Batch;
        private System.Windows.Forms.TextBox txt_current_batch;
        private System.Windows.Forms.Label lbl_ctn_Batch;
        private System.Windows.Forms.TextBox txt_ctn_batch;
        private System.Windows.Forms.Label lbl_total_ctns;
        private System.Windows.Forms.TextBox txt_total_ctn;
        private System.Windows.Forms.DateTimePicker dtp_start;
        private System.Windows.Forms.Label lbl_start;
        private System.Windows.Forms.Label lbl_finish;
        private System.Windows.Forms.DateTimePicker dtp_finish;
        private System.Windows.Forms.TextBox txt_comments;
        private System.Windows.Forms.Label lbl_comments;
        private System.Windows.Forms.Button btn_Print;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btn_Reset;
        private System.Windows.Forms.Button btn_Current;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.TextBox txt_WorkOrder;
        private System.Windows.Forms.Label lbl_WorkOrder;

        private System.Windows.Forms.GroupBox gb_results;
        private System.Windows.Forms.Button btn_Close;
        private FruPak.PF.Utils.UserControls.Fruit fruit1;
        private System.Windows.Forms.Label lbl_Trader;
        private System.Windows.Forms.ComboBox cmb_Trader;
        private System.Windows.Forms.Label lbl_Growing_method;
        private System.Windows.Forms.ComboBox cmb_Growing_Method;
        private System.Windows.Forms.Button btn_Update;
        private Utils.UserControls.Grower grower1;
        
    }
}

