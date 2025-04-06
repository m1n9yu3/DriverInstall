namespace DriverInstall_DotNet
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
            this.Install = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Start = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.Remove = new System.Windows.Forms.Button();
            this.DriverFileName = new System.Windows.Forms.TextBox();
            this.OpenDriverFIle = new System.Windows.Forms.Button();
            this.LogDisplay = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.clearButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Install
            // 
            this.Install.Location = new System.Drawing.Point(42, 97);
            this.Install.Name = "Install";
            this.Install.Size = new System.Drawing.Size(116, 49);
            this.Install.TabIndex = 0;
            this.Install.Text = "Install";
            this.Install.UseVisualStyleBackColor = true;
            this.Install.Click += new System.EventHandler(this.Install_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Location = new System.Drawing.Point(0, 645);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(648, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(188, 97);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(116, 49);
            this.Start.TabIndex = 2;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(335, 97);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(116, 49);
            this.Stop.TabIndex = 3;
            this.Stop.Text = "Stop";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // Remove
            // 
            this.Remove.Location = new System.Drawing.Point(490, 97);
            this.Remove.Name = "Remove";
            this.Remove.Size = new System.Drawing.Size(116, 49);
            this.Remove.TabIndex = 4;
            this.Remove.Text = "Remove";
            this.Remove.UseVisualStyleBackColor = true;
            this.Remove.Click += new System.EventHandler(this.Remove_Click);
            // 
            // DriverFileName
            // 
            this.DriverFileName.Location = new System.Drawing.Point(42, 44);
            this.DriverFileName.Name = "DriverFileName";
            this.DriverFileName.Size = new System.Drawing.Size(483, 28);
            this.DriverFileName.TabIndex = 5;
            this.DriverFileName.Text = "C:\\MDriver.sys";
            // 
            // OpenDriverFIle
            // 
            this.OpenDriverFIle.Location = new System.Drawing.Point(541, 45);
            this.OpenDriverFIle.Name = "OpenDriverFIle";
            this.OpenDriverFIle.Size = new System.Drawing.Size(64, 26);
            this.OpenDriverFIle.TabIndex = 6;
            this.OpenDriverFIle.Text = ".....";
            this.OpenDriverFIle.UseVisualStyleBackColor = true;
            this.OpenDriverFIle.Click += new System.EventHandler(this.OpenDriverFIle_Click);
            // 
            // LogDisplay
            // 
            this.LogDisplay.Location = new System.Drawing.Point(42, 216);
            this.LogDisplay.Multiline = true;
            this.LogDisplay.Name = "LogDisplay";
            this.LogDisplay.Size = new System.Drawing.Size(561, 379);
            this.LogDisplay.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.TabIndex = 8;
            this.label1.Text = "LOG:";
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(528, 167);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 39);
            this.clearButton.TabIndex = 9;
            this.clearButton.Text = "cls";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 667);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LogDisplay);
            this.Controls.Add(this.OpenDriverFIle);
            this.Controls.Add(this.DriverFileName);
            this.Controls.Add(this.Remove);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.Install);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Install;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Button Remove;
        private System.Windows.Forms.TextBox DriverFileName;
        private System.Windows.Forms.Button OpenDriverFIle;
        private System.Windows.Forms.TextBox LogDisplay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button clearButton;
    }
}

