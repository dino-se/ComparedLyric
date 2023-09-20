using CefSharp;
using CefSharp.WinForms;
using ComparedLyric.Properties;
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

        private void GenerateSongList()
        {
            flp_SongList.Controls.Clear();

            const int songIndex = 7;

            SongListControl[] listSongs = new SongListControl[songIndex];

            string[] songTitle = new string[songIndex] {
                "Compared Child",
                "Trapped in the Past",
                "Hide and Seek Alone",
                "It's Raining After All",
                "Under the Summer Breeze",
                "Compared Child (TUYU Remix)",
                "Under Heroine"};
            string[] songTime = new string[songIndex] {
                "3:36", "3:51", "3:13", "4:07","N/A", "3:27", "N/A"};
            Image[] songIcon = new Image[7] {
                Resources.Compared_Child,
                Resources.Trapped_In_The_Past,
                Resources.Trapped_In_The_Past,
                Resources.Compared_Child,
                Resources.Compared_Child,
                Resources.Compared_Child_Remix,
                Resources.Compared_Child};

            for (int i = 0; i < listSongs.Length; i++)
            {
                listSongs[i] = new SongListControl()
                {
                    SongIcon = songIcon[i],
                    SongTitle = songTitle[i],
                    SongTime = songTime[i]
                };

                flp_SongList.Controls.Add(listSongs[i]);

                listSongs[i].Click += new EventHandler(this.SongControl_Click);
            }
        }

        void SongControl_Click(object sender, EventArgs e)
        {
            if (sender is SongListControl songControl)
            {
                switch (songControl.SongTitle)
                {
                    case "Compared Child":
                        PlaySong("Compared Child", ComparedChild.TimedLyricsText, "olWvy0PiLfA");
                        break;
                    case "Trapped in the Past":
                        PlaySong("Trapped in the Past", TrappedInThePast.TimedLyricsText, "lGFEqEFJ410");
                        break;
                    case "Hide and Seek Alone":
                        PlaySong("Hide and Seek Alone", HideAndSeekAlone.TimedLyricsText, "Bq0ZINOzVng");
                        break;
                    case "It's Raining After All":
                        PlaySong("It's Raining After All", ItsRainingAfterAll.TimedLyricsText, "D0ehC_8sQuU");
                        break;
                    case "Under the Summer Breeze":
                        PlaySong("Under the Summer Breeze", UnderTheSummerBreeze.TimedLyricsText, "LoK17z6xDwI");
                        break;
                    case "Compared Child (TUYU Remix)":
                        PlaySong("Compared Child (TUYU Remix)", ComparedChild.TimedLyricsText, "4TmzJzGXbB4");
                        break;
                    case "Under Heroine":
                        PlaySong("Compared Child (TUYU Remix)", ComparedChild.TimedLyricsText, "mHnt8TVbC9M");
                        break;
                    default:
                        break;
                }
            }
        }

        private void PlaySong(string songTitle, string timedLyricsText, string videoId)
        {
            lyricsLines = timedLyricsText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            lbl_Title.Text = songTitle;
            string embedUrl = $"https://www.youtube.com/embed/{videoId}?autoplay=1";
            chromiumWebBrowser1.Load(embedUrl);
        }
    }
}
