namespace widkeyPaperDiaper
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.loginB = new System.Windows.Forms.Button();
            this.autoB = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.logT = new System.Windows.Forms.RichTextBox();
            this.addB = new System.Windows.Forms.Button();
            this.deleteMail = new System.Windows.Forms.Button();
            this.inputT = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.testLog = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.deleteApp = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.appointmentGrid = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.mailGrid = new System.Windows.Forms.DataGridView();
            this.cardNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cardPasswordDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chineseNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.japaneseNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phoneDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.appointmentBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.appointmentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.appointmentBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.appointmentGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mailGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.appointmentBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.appointmentBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.appointmentBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // loginB
            // 
            this.loginB.Location = new System.Drawing.Point(927, 96);
            this.loginB.Name = "loginB";
            this.loginB.Size = new System.Drawing.Size(88, 36);
            this.loginB.TabIndex = 1;
            this.loginB.Text = "Login";
            this.loginB.UseVisualStyleBackColor = true;
            this.loginB.Visible = false;
            this.loginB.Click += new System.EventHandler(this.loginB_Click);
            // 
            // autoB
            // 
            this.autoB.Location = new System.Drawing.Point(172, 12);
            this.autoB.Name = "autoB";
            this.autoB.Size = new System.Drawing.Size(138, 48);
            this.autoB.TabIndex = 4;
            this.autoB.Text = "start";
            this.autoB.UseVisualStyleBackColor = true;
            this.autoB.Click += new System.EventHandler(this.autoB_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "rate:";
            // 
            // rate
            // 
            this.rate.Location = new System.Drawing.Point(57, 23);
            this.rate.Name = "rate";
            this.rate.Size = new System.Drawing.Size(68, 21);
            this.rate.TabIndex = 11;
            this.rate.Text = "1";
            this.rate.Validating += new System.ComponentModel.CancelEventHandler(this.rate_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(131, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "ms";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F);
            this.label3.Location = new System.Drawing.Point(353, 418);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "Copyright  Mushroom";
            // 
            // logT
            // 
            this.logT.Location = new System.Drawing.Point(666, 12);
            this.logT.Name = "logT";
            this.logT.ReadOnly = true;
            this.logT.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.logT.Size = new System.Drawing.Size(253, 338);
            this.logT.TabIndex = 9;
            this.logT.Text = "";
            this.logT.TextChanged += new System.EventHandler(this.logT_TextChanged);
            // 
            // addB
            // 
            this.addB.Location = new System.Drawing.Point(412, 156);
            this.addB.Name = "addB";
            this.addB.Size = new System.Drawing.Size(76, 25);
            this.addB.TabIndex = 5;
            this.addB.Text = "import";
            this.addB.UseVisualStyleBackColor = true;
            this.addB.Click += new System.EventHandler(this.addB_Click);
            // 
            // deleteMail
            // 
            this.deleteMail.Location = new System.Drawing.Point(503, 156);
            this.deleteMail.Name = "deleteMail";
            this.deleteMail.Size = new System.Drawing.Size(76, 25);
            this.deleteMail.TabIndex = 6;
            this.deleteMail.Text = "delete";
            this.deleteMail.UseVisualStyleBackColor = true;
            this.deleteMail.Click += new System.EventHandler(this.deleteB_Click);
            // 
            // inputT
            // 
            this.inputT.Location = new System.Drawing.Point(984, 27);
            this.inputT.Name = "inputT";
            this.inputT.Size = new System.Drawing.Size(180, 21);
            this.inputT.TabIndex = 2;
            this.inputT.Visible = false;
            this.inputT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_keyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(925, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "username:";
            this.label4.Visible = false;
            // 
            // testLog
            // 
            this.testLog.Location = new System.Drawing.Point(14, 440);
            this.testLog.Name = "testLog";
            this.testLog.ReadOnly = true;
            this.testLog.Size = new System.Drawing.Size(905, 268);
            this.testLog.TabIndex = 16;
            this.testLog.Text = "";
            this.testLog.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(984, 56);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(180, 21);
            this.textBox1.TabIndex = 3;
            this.textBox1.UseSystemPasswordChar = true;
            this.textBox1.Visible = false;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_keyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(925, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "password:";
            this.label5.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(529, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 32);
            this.button1.TabIndex = 18;
            this.button1.Text = "test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(501, 418);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 19;
            this.label6.Text = "label6";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(333, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(140, 48);
            this.button2.TabIndex = 20;
            this.button2.Text = "stop";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(18, 156);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(74, 25);
            this.button3.TabIndex = 5;
            this.button3.Text = "import";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.addDetails_Click);
            // 
            // deleteApp
            // 
            this.deleteApp.Location = new System.Drawing.Point(107, 156);
            this.deleteApp.Name = "deleteApp";
            this.deleteApp.Size = new System.Drawing.Size(76, 25);
            this.deleteApp.TabIndex = 6;
            this.deleteApp.Text = "delete";
            this.deleteApp.UseVisualStyleBackColor = true;
            this.deleteApp.Click += new System.EventHandler(this.deleteDetails_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 141);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 21;
            this.label7.Text = "details:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(415, 141);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 21;
            this.label8.Text = "emails:";
            // 
            // appointmentGrid
            // 
            this.appointmentGrid.AutoGenerateColumns = false;
            this.appointmentGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.appointmentGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cardNoDataGridViewTextBoxColumn,
            this.cardPasswordDataGridViewTextBoxColumn,
            this.chineseNameDataGridViewTextBoxColumn,
            this.japaneseNameDataGridViewTextBoxColumn,
            this.phoneDataGridViewTextBoxColumn});
            this.appointmentGrid.DataSource = this.appointmentBindingSource2;
            this.appointmentGrid.Location = new System.Drawing.Point(18, 187);
            this.appointmentGrid.Name = "appointmentGrid";
            this.appointmentGrid.RowTemplate.Height = 23;
            this.appointmentGrid.Size = new System.Drawing.Size(387, 228);
            this.appointmentGrid.TabIndex = 24;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(44, 67);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 71);
            this.pictureBox1.TabIndex = 25;
            this.pictureBox1.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(306, 108);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 26;
            this.label9.Text = "label9";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(290, 76);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 27;
            // 
            // mailGrid
            // 
            this.mailGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mailGrid.Location = new System.Drawing.Point(411, 187);
            this.mailGrid.Name = "mailGrid";
            this.mailGrid.RowTemplate.Height = 23;
            this.mailGrid.Size = new System.Drawing.Size(249, 228);
            this.mailGrid.TabIndex = 24;
            // 
            // cardNoDataGridViewTextBoxColumn
            // 
            this.cardNoDataGridViewTextBoxColumn.DataPropertyName = "CardNo";
            this.cardNoDataGridViewTextBoxColumn.HeaderText = "CardNo";
            this.cardNoDataGridViewTextBoxColumn.Name = "cardNoDataGridViewTextBoxColumn";
            this.cardNoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cardPasswordDataGridViewTextBoxColumn
            // 
            this.cardPasswordDataGridViewTextBoxColumn.DataPropertyName = "CardPassword";
            this.cardPasswordDataGridViewTextBoxColumn.HeaderText = "CardPassword";
            this.cardPasswordDataGridViewTextBoxColumn.Name = "cardPasswordDataGridViewTextBoxColumn";
            this.cardPasswordDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // chineseNameDataGridViewTextBoxColumn
            // 
            this.chineseNameDataGridViewTextBoxColumn.DataPropertyName = "ChineseName";
            this.chineseNameDataGridViewTextBoxColumn.HeaderText = "ChineseName";
            this.chineseNameDataGridViewTextBoxColumn.Name = "chineseNameDataGridViewTextBoxColumn";
            this.chineseNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // japaneseNameDataGridViewTextBoxColumn
            // 
            this.japaneseNameDataGridViewTextBoxColumn.DataPropertyName = "JapaneseName";
            this.japaneseNameDataGridViewTextBoxColumn.HeaderText = "JapaneseName";
            this.japaneseNameDataGridViewTextBoxColumn.Name = "japaneseNameDataGridViewTextBoxColumn";
            this.japaneseNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // phoneDataGridViewTextBoxColumn
            // 
            this.phoneDataGridViewTextBoxColumn.DataPropertyName = "Phone";
            this.phoneDataGridViewTextBoxColumn.HeaderText = "Phone";
            this.phoneDataGridViewTextBoxColumn.Name = "phoneDataGridViewTextBoxColumn";
            this.phoneDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // appointmentBindingSource2
            // 
            this.appointmentBindingSource2.DataSource = typeof(widkeyPaperDiaper.Appointment);
            // 
            // appointmentBindingSource
            // 
            this.appointmentBindingSource.DataSource = typeof(widkeyPaperDiaper.Appointment);
            // 
            // appointmentBindingSource1
            // 
            this.appointmentBindingSource1.DataSource = typeof(widkeyPaperDiaper.Appointment);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 458);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.mailGrid);
            this.Controls.Add(this.appointmentGrid);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.testLog);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.deleteApp);
            this.Controls.Add(this.inputT);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.deleteMail);
            this.Controls.Add(this.addB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.logT);
            this.Controls.Add(this.autoB);
            this.Controls.Add(this.loginB);
            this.Name = "Form1";
            this.Text = "PaperDiaperBooker";
            ((System.ComponentModel.ISupportInitialize)(this.appointmentGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mailGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.appointmentBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.appointmentBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.appointmentBindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button loginB;
        private System.Windows.Forms.Button autoB;
        private System.Windows.Forms.Label label1;
        public  System.Windows.Forms.TextBox rate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private  System.Windows.Forms.RichTextBox logT;
        private System.Windows.Forms.Button addB;
        public  System.Windows.Forms.Button deleteMail;
        public  System.Windows.Forms.TextBox inputT;
        private System.Windows.Forms.Label label4;
        private  System.Windows.Forms.RichTextBox testLog;
        public  System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        public System.Windows.Forms.Button deleteApp;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.DataGridView appointmentGrid;
        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.BindingSource appointmentBindingSource;
        private System.Windows.Forms.BindingSource appointmentBindingSource2;
        private System.Windows.Forms.BindingSource appointmentBindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cardNoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cardPasswordDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn chineseNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn japaneseNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn phoneDataGridViewTextBoxColumn;
        public System.Windows.Forms.DataGridView mailGrid;
    }
}

