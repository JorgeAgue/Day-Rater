namespace Day_Rater
{
    partial class Form2
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
            this.viewListBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // viewListBox1
            // 
            this.viewListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewListBox1.FormattingEnabled = true;
            this.viewListBox1.ItemHeight = 25;
            this.viewListBox1.Location = new System.Drawing.Point(0, 0);
            this.viewListBox1.Name = "viewListBox1";
            this.viewListBox1.Size = new System.Drawing.Size(1009, 959);
            this.viewListBox1.TabIndex = 0;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 959);
            this.Controls.Add(this.viewListBox1);
            this.Name = "Form2";
            this.Text = "View";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ListBox viewListBox1;
    }
}