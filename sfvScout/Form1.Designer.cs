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
            this.loginB = new System.Windows.Forms.Button();
            this.autoB = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.logT = new System.Windows.Forms.RichTextBox();
            this.addB = new System.Windows.Forms.Button();
            this.deleteB = new System.Windows.Forms.Button();
            this.inputT = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.urlList = new System.Windows.Forms.CheckedListBox();
            this.testLog = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cardNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chineseName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.japaneseName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.addB.Location = new System.Drawing.Point(450, 91);
            this.addB.Name = "addB";
            this.addB.Size = new System.Drawing.Size(76, 25);
            this.addB.TabIndex = 5;
            this.addB.Text = "import";
            this.addB.UseVisualStyleBackColor = true;
            this.addB.Click += new System.EventHandler(this.addB_Click);
            // 
            // deleteB
            // 
            this.deleteB.Location = new System.Drawing.Point(541, 91);
            this.deleteB.Name = "deleteB";
            this.deleteB.Size = new System.Drawing.Size(76, 25);
            this.deleteB.TabIndex = 6;
            this.deleteB.Text = "delete";
            this.deleteB.UseVisualStyleBackColor = true;
            this.deleteB.Click += new System.EventHandler(this.deleteB_Click);
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
            // urlList
            // 
            this.urlList.FormattingEnabled = true;
            this.urlList.Location = new System.Drawing.Point(450, 122);
            this.urlList.Name = "urlList";
            this.urlList.Size = new System.Drawing.Size(197, 84);
            this.urlList.TabIndex = 7;
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
            this.button1.Location = new System.Drawing.Point(234, 149);
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
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(107, 156);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(76, 25);
            this.button4.TabIndex = 6;
            this.button4.Text = "delete";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.deleteDetails_Click);
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
            this.label8.Location = new System.Drawing.Point(453, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 21;
            this.label8.Text = "emails:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cardNumber,
            this.password,
            this.chineseName,
            this.japaneseName,
            this.phone});
            this.dataGridView1.Location = new System.Drawing.Point(18, 187);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(387, 228);
            this.dataGridView1.TabIndex = 24;
            // 
            // cardNumber
            // 
            this.cardNumber.HeaderText = "cardNumber";
            this.cardNumber.Name = "cardNumber";
            // 
            // password
            // 
            this.password.HeaderText = "password";
            this.password.Name = "password";
            // 
            // chineseName
            // 
            this.chineseName.HeaderText = "chineseName";
            this.chineseName.Name = "chineseName";
            // 
            // japaneseName
            // 
            this.japaneseName.HeaderText = "japaneseName";
            this.japaneseName.Name = "japaneseName";
            // 
            // phone
            // 
            this.phone.HeaderText = "phone";
            this.phone.Name = "phone";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 458);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.testLog);
            this.Controls.Add(this.urlList);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.inputT);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.deleteB);
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
        public  System.Windows.Forms.Button deleteB;
        public  System.Windows.Forms.TextBox inputT;
        private System.Windows.Forms.Label label4;
        public  System.Windows.Forms.CheckedListBox urlList;
        private  System.Windows.Forms.RichTextBox testLog;
        public  System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        public System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cardNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn password;
        private System.Windows.Forms.DataGridViewTextBoxColumn chineseName;
        private System.Windows.Forms.DataGridViewTextBoxColumn japaneseName;
        private System.Windows.Forms.DataGridViewTextBoxColumn phone;
    }
}

