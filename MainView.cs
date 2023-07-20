using CefSharp;
using CefSharp.WinForms;
using System;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace ComparedLyric
{
    public partial class MainView : Form
    {
        private string[] lyricsLines;
        private double videoStartTime = 0;
        private bool isVideoStarted = false;

        public MainView()
        {
            InitializeComponent();
            InitializeTimedLyrics();
            GenerateSongList();
        }

        private void InitializeTimedLyrics()
        {
            var settings = new CefSettings();
            settings.CefCommandLineArgs.Add("autoplay-policy", "no-user-gesture-required");
            Cef.Initialize(settings);
        }

        private bool ParseTimestamp(string line, out TimeSpan timestamp, out string lyricsText)
        {
            int startPos = line.IndexOf('[');
            int endPos = line.IndexOf(']');

            if (startPos >= 0 && endPos > startPos)
            {
                string timestampStr = line.Substring(startPos + 1, endPos - startPos - 1);
                lyricsText = line.Substring(endPos + 1).Trim();

                if (TimeSpan.TryParseExact(timestampStr, @"mm\:ss\.fff", null, out timestamp))
                {
                    return true;
                }
            }

            timestamp = TimeSpan.Zero;
            lyricsText = string.Empty;
            return false;
        }

        private void tmrRefreshData_Tick(object sender, EventArgs e)
        {
            // JavaScript
            chromiumWebBrowser1.GetMainFrame().EvaluateScriptAsync(@"
                var video = document.querySelector('video');
                if (video !== null && !isNaN(video.currentTime)) {
                    video.currentTime;
                } else {
                    null;
                }
            ").ContinueWith(t =>
            {
                if (!t.IsFaulted && !t.IsCanceled && t.Result != null && t.Result.Result != null)
                {
                    double currentTime = Convert.ToDouble(t.Result.Result);

                    if (!isVideoStarted && currentTime > 0)
                    {
                        isVideoStarted = true;
                        videoStartTime = currentTime;
                    }

                    this.Invoke(new Action(() =>
                    {
                        string formattedTime = TimeSpan.FromSeconds(currentTime).ToString(@"mm\:ss");
                        lbl_Time.Text = formattedTime;

                        if (!isVideoStarted)
                            return;

                        double elapsedTime = currentTime - videoStartTime;
                        int currentLineIndex = FindLyricsLineIndexByTime(elapsedTime);

                        if (currentLineIndex >= 0 && currentLineIndex < lyricsLines.Length)
                        {
                            string currentLine = lyricsLines[currentLineIndex].Substring(10);
                            if (ParseTimestamp(currentLine, out _, out string lyricsText))
                            {
                                lblLyrics.Visible = true;
                                lblLyrics.Text = lyricsText;
                            }
                        }
                    }));
                }
            });
        }

        private int FindLyricsLineIndexByTime(double elapsedTime)
        {
            for (int i = 0; i < lyricsLines.Length; i++)
            {
                if (ParseTimestamp(lyricsLines[i], out TimeSpan timestamp, out _))
                {
                    if (elapsedTime * 1000 <= timestamp.TotalMilliseconds)
                    {
                        return i - 1;
                    }
                }
            }
            return lyricsLines.Length - 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lyricsLines = ComparedChild.TimedLyricsText
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            lbl_Title.Text = "Compared Child";
            string videoId = "olWvy0PiLfA";
            string embedUrl = $"https://www.youtube.com/embed/{videoId}?autoplay=1";

            chromiumWebBrowser1.Load(embedUrl);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lyricsLines = TrappedInThePast.TimedLyricsText
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            lbl_Title.Text = "Trapped in the Past";
            string videoId = "lGFEqEFJ410";
            string embedUrl = $"https://www.youtube.com/embed/{videoId}?autoplay=1";

            chromiumWebBrowser1.Load(embedUrl);
        }

        private void GenerateSongList()
        {
            flp_SongList.Controls.Clear();

            SongListControl[] listSongs = new SongListControl[2];

            string[] songTitle = new string[2] { "Compared Child", "Trapped in the Past" };
            string[] songTime = new string[2] { "","" };
            Image[] songIcon = new Image[2] { Properties.Resources.Compared_Child, Properties.Resources.Trapped_In_The_Past};

            for (int i = 0; i < listSongs.Length; i++)
            {
                listSongs[i] = new SongListControl();

                listSongs[i].SongIcon = songIcon[i];
                listSongs[i].SongTitle = songTitle[i];
                listSongs[i].SongTime = songTime[i];

                flp_SongList.Controls.Add(listSongs[i]);

                listSongs[i].Click += new EventHandler(this.UserControl_Click);
            }
        }

        void UserControl_Click(object sender, EventArgs e)
        {
            lyricsLines = TrappedInThePast.TimedLyricsText
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            lbl_Title.Text = "Trapped in the Past";
            string videoId = "lGFEqEFJ410";
            string embedUrl = $"https://www.youtube.com/embed/{videoId}?autoplay=1";

            chromiumWebBrowser1.Load(embedUrl);
        }
    }
}
