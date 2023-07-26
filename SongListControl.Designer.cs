namespace ComparedLyric
{
    partial class SongListControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.pb_SongListIcon = new System.Windows.Forms.PictureBox();
            this.lbl_SongListTitle = new System.Windows.Forms.Label();
            this.lbl_SongListTime = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_SongListIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.panel1.Controls.Add(this.pb_SongListIcon);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(87, 85);
            this.panel1.TabIndex = 0;
            this.panel1.MouseEnter += new System.EventHandler(this.ucRequest_MouseEnter);
            this.panel1.MouseLeave += new System.EventHandler(this.ucRequest_MouseLeave);
            // 
            // pb_SongListIcon
            // 
            this.pb_SongListIcon.Location = new System.Drawing.Point(6, 6);
            this.pb_SongListIcon.Name = "pb_SongListIcon";
            this.pb_SongListIcon.Size = new System.Drawing.Size(75, 75);
            this.pb_SongListIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_SongListIcon.TabIndex = 0;
            this.pb_SongListIcon.TabStop = false;
            this.pb_SongListIcon.MouseEnter += new System.EventHandler(this.ucRequest_MouseEnter);
            this.pb_SongListIcon.MouseLeave += new System.EventHandler(this.ucRequest_MouseLeave);
            // 
            // lbl_SongListTitle
            // 
            this.lbl_SongListTitle.AutoSize = true;
            this.lbl_SongListTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SongListTitle.Location = new System.Drawing.Point(93, 25);
            this.lbl_SongListTitle.Name = "lbl_SongListTitle";
            this.lbl_SongListTitle.Size = new System.Drawing.Size(32, 13);
            this.lbl_SongListTitle.TabIndex = 1;
            this.lbl_SongListTitle.Text = "Title";
            this.lbl_SongListTitle.MouseEnter += new System.EventHandler(this.ucRequest_MouseEnter);
            this.lbl_SongListTitle.MouseLeave += new System.EventHandler(this.ucRequest_MouseLeave);
            // 
            // lbl_SongListTime
            // 
            this.lbl_SongListTime.AutoSize = true;
            this.lbl_SongListTime.Location = new System.Drawing.Point(93, 47);
            this.lbl_SongListTime.Name = "lbl_SongListTime";
            this.lbl_SongListTime.Size = new System.Drawing.Size(34, 13);
            this.lbl_SongListTime.TabIndex = 2;
            this.lbl_SongListTime.Text = "00:00";
            this.lbl_SongListTime.MouseEnter += new System.EventHandler(this.ucRequest_MouseEnter);
            this.lbl_SongListTime.MouseLeave += new System.EventHandler(this.ucRequest_MouseLeave);
            // 
            // SongListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(233)))), ((int)(((byte)(238)))));
            this.Controls.Add(this.lbl_SongListTime);
            this.Controls.Add(this.lbl_SongListTitle);
            this.Controls.Add(this.panel1);
            this.Name = "SongListControl";
            this.Size = new System.Drawing.Size(270, 85);
            this.MouseEnter += new System.EventHandler(this.ucRequest_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.ucRequest_MouseLeave);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_SongListIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pb_SongListIcon;
        private System.Windows.Forms.Label lbl_SongListTitle;
        private System.Windows.Forms.Label lbl_SongListTime;
    }
}
