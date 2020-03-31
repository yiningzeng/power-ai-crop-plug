namespace crop
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSubImageNum = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSavePath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbShift = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbJsonVersion = new System.Windows.Forms.TextBox();
            this.cbSaveOk = new System.Windows.Forms.CheckBox();
            this.cbCrop = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(80, 192);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "开始";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(12, 53);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(628, 21);
            this.tbPath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "power-ai-export目录下的power-ai文件";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "宽高几等分";
            // 
            // tbSubImageNum
            // 
            this.tbSubImageNum.Location = new System.Drawing.Point(94, 133);
            this.tbSubImageNum.Name = "tbSubImageNum";
            this.tbSubImageNum.Size = new System.Drawing.Size(100, 21);
            this.tbSubImageNum.TabIndex = 4;
            this.tbSubImageNum.Text = "2";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(646, 53);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(49, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "浏览";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "保存目录";
            // 
            // tbSavePath
            // 
            this.tbSavePath.Location = new System.Drawing.Point(14, 93);
            this.tbSavePath.Name = "tbSavePath";
            this.tbSavePath.ReadOnly = true;
            this.tbSavePath.Size = new System.Drawing.Size(626, 21);
            this.tbSavePath.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "截取覆盖像素";
            // 
            // tbShift
            // 
            this.tbShift.Location = new System.Drawing.Point(94, 162);
            this.tbShift.Name = "tbShift";
            this.tbShift.Size = new System.Drawing.Size(100, 21);
            this.tbShift.TabIndex = 9;
            this.tbShift.Text = "5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(249, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "json版本";
            // 
            // tbJsonVersion
            // 
            this.tbJsonVersion.Location = new System.Drawing.Point(308, 130);
            this.tbJsonVersion.Name = "tbJsonVersion";
            this.tbJsonVersion.Size = new System.Drawing.Size(100, 21);
            this.tbJsonVersion.TabIndex = 11;
            this.tbJsonVersion.Text = "3.6.2";
            // 
            // cbSaveOk
            // 
            this.cbSaveOk.AutoSize = true;
            this.cbSaveOk.Location = new System.Drawing.Point(251, 162);
            this.cbSaveOk.Name = "cbSaveOk";
            this.cbSaveOk.Size = new System.Drawing.Size(108, 16);
            this.cbSaveOk.TabIndex = 12;
            this.cbSaveOk.Text = "是否保留ok图片";
            this.cbSaveOk.UseVisualStyleBackColor = true;
            // 
            // cbCrop
            // 
            this.cbCrop.AutoSize = true;
            this.cbCrop.Location = new System.Drawing.Point(481, 132);
            this.cbCrop.Name = "cbCrop";
            this.cbCrop.Size = new System.Drawing.Size(120, 16);
            this.cbCrop.TabIndex = 13;
            this.cbCrop.Text = "是否已经裁剪图片";
            this.cbCrop.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(248, 192);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(111, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "修复边框超出问题";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 225);
            this.Controls.Add(this.cbCrop);
            this.Controls.Add(this.cbSaveOk);
            this.Controls.Add(this.tbJsonVersion);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbShift);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbSavePath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tbSubImageNum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "crop";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbSubImageNum;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSavePath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbShift;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbJsonVersion;
        private System.Windows.Forms.CheckBox cbSaveOk;
        private System.Windows.Forms.CheckBox cbCrop;
        private System.Windows.Forms.Button button3;
    }
}

