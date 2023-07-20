using System;
using System.Drawing;
using System.Windows.Forms;

namespace ComparedLyric
{
    public partial class SongListControl : UserControl
    {
        private Image _icon;
        private string _title;
        private string _time;
        public SongListControl()
        {
            InitializeComponent();
            AddEvent();
        }

        public Image SongIcon
        {
            get { return _icon; }
            set { _icon = value; pb_SongListIcon.Image = value; }
        }

        public string SongTitle
        {
            get { return _title; }
            set { _title = value; lbl_SongListTitle.Text = value; }
        }

        public string SongTime
        {
            get { return _time; }
            set { _time = value; lbl_SongListTime.Text = value; }
        }

        private void ucRequest_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(217, 229, 242);
        }

        private void ucRequest_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void AddEvent()
        {
            pb_SongListIcon.Click += new EventHandler((object sender, EventArgs e) => OnClick(e));
            lbl_SongListTitle.Click += new EventHandler((object sender, EventArgs e) => this.OnClick(e));
            lbl_SongListTime.Click += new EventHandler((object sender, EventArgs e) => this.OnClick(e));
            panel1.Click += new EventHandler((object sender, EventArgs e) => this.OnClick(e));
        }
    }
}
