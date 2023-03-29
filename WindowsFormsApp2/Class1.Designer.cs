namespace WindowsFormsApp2
{
    partial class Class1
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.metroButton4 = new MetroFramework.Controls.MetroButton();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer_send_img = new System.Windows.Forms.Timer(this.components);
            this.metroTile1 = new MetroFramework.Controls.MetroTile();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(23, 54);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(538, 270);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // metroButton4
            // 
            this.metroButton4.Location = new System.Drawing.Point(704, 11);
            this.metroButton4.Name = "metroButton4";
            this.metroButton4.Size = new System.Drawing.Size(83, 40);
            this.metroButton4.TabIndex = 6;
            this.metroButton4.Text = "나가기";
            this.metroButton4.UseSelectable = true;
            this.metroButton4.Click += new System.EventHandler(this.metroButton4_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(593, 99);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(147, 172);
            this.listBox1.TabIndex = 7;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer_send_img
            // 
            this.timer_send_img.Tick += new System.EventHandler(this.timer_send_img_Tick);
            // 
            // metroTile1
            // 
            this.metroTile1.ActiveControl = null;
            this.metroTile1.Location = new System.Drawing.Point(624, 325);
            this.metroTile1.Name = "metroTile1";
            this.metroTile1.Size = new System.Drawing.Size(116, 65);
            this.metroTile1.TabIndex = 8;
            this.metroTile1.Text = "metroTile1";
            this.metroTile1.UseSelectable = true;
            // 
            // Class1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.metroTile1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.metroButton4);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Class1";
            this.Text = "공지사항";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Class1_FormClosing);
            this.Load += new System.EventHandler(this.Class1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private MetroFramework.Controls.MetroButton metroButton4;
        private System.Windows.Forms.ListBox listBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timer_send_img;
        private MetroFramework.Controls.MetroTile metroTile1;
    }
}