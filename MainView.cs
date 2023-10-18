using CefSharp;
using CefSharp.WinForms;
using ComparedLyric.Properties;
using System;
using System.Drawing;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComparedLyric
{
    public partial class MainView : Form
    {
        private readonly TimedLyrics timedLyrics;
        private double videoStartTime = 0;
        private bool isVideoStarted = false;
        public MainView()
        {
            InitializeComponent();
            timedLyrics = new TimedLyrics();
            GenerateSongList();
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
                        int currentLineIndex = timedLyrics.FindLyricsLineIndexByTime(elapsedTime);

                        if (currentLineIndex >= 0 && currentLineIndex < timedLyrics.lyricsLines.Length)
                        {
                            string currentLine = timedLyrics.lyricsLines[currentLineIndex].Substring(10);
                            if (timedLyrics.ParseTimestamp(currentLine, out _, out string lyricsText))
                            {
                                lblLyrics.Visible = true;
                                lblLyrics.Text = lyricsText;
                            }
                        }
                    }));
                }
            });
        }

        private void GenerateSongList()
        {
            flp_SongList.Controls.Clear();

            const int songIndex = 10;

            SongListControl[] listSongs = new SongListControl[songIndex];

            //[Title] [Time] [Image]
            object[,] songsList = new object[songIndex, 3] {
                { "Compared Child", "3:36", Resources.Compared_Child },
                { "Trapped in the Past", "3:50", Resources.Trapped_In_The_Past },
                { "Hide and Seek Alone", "3:14", Resources.Trapped_In_The_Past },
                { "It's Raining After All", "4:07", Resources.Compared_Child },
                { "Under the Summer Breeze", "3:23", Resources.Compared_Child },
                { "Compared Child (TUYU Remix)", "3:36", Resources.Compared_Child_Remix },
                { "Goodbye to Rock you", "3:28", Resources.Compared_Child },
                { "If There Was An Endpoint.", "3:00", Resources.Compared_Child },
                { "I'm getting on the bus to the other world, see ya!", "3:16", Resources.Compared_Child },
                { "Being low as dirt, taking what's important from me", "3:13", Resources.Compared_Child },
            };

            //Image[] songIcon = new Image[songIndex] {
            //    Resources.Compared_Child,
            //    Resources.Trapped_In_The_Past,
            //    Resources.Trapped_In_The_Past,
            //    Resources.Compared_Child,
            //    Resources.Compared_Child,
            //    Resources.Compared_Child_Remix,
            //    Resources.Compared_Child,
            //    Resources.Compared_Child,
            //    Resources.Compared_Child,
            //    Resources.Compared_Child};

            for (int i = 0; i < listSongs.Length; i++)
            {
                listSongs[i] = new SongListControl()
                {
                    SongIcon = (Image)songsList[i, 2],
                    SongTitle = (string)songsList[i, 0],
                    SongTime = (string)songsList[i, 1]
                };

                flp_SongList.Controls.Add(listSongs[i]);

                listSongs[i].Click += new EventHandler(this.SongControl_Click);
            }
        }

        async void SongControl_Click(object sender, EventArgs e)
        {
            if (sender is SongListControl songControl)
            {
                switch (songControl.SongTitle)
                {
                    case "Compared Child":
                        await PlaySong("Compared Child", ComparedChild.TimedLyricsText, "olWvy0PiLfA");
                        break;
                    case "Trapped in the Past":
                        await PlaySong("Trapped in the Past", TrappedInThePast.TimedLyricsText, "lGFEqEFJ410");
                        break;
                    case "Hide and Seek Alone":
                        await PlaySong("Hide and Seek Alone", HideAndSeekAlone.TimedLyricsText, "Bq0ZINOzVng");
                        break;
                    case "It's Raining After All":
                        await PlaySong("It's Raining After All", ItsRainingAfterAll.TimedLyricsText, "D0ehC_8sQuU");
                        break;
                    case "Under the Summer Breeze":
                        await PlaySong("Under the Summer Breeze", UnderTheSummerBreeze.TimedLyricsText, "LoK17z6xDwI");
                        break;
                    case "Goodbye to Rock you":
                        await PlaySong("Goodbye to Rock you", UnderTheSummerBreeze.TimedLyricsText, "1cGQotpn8r4");
                        break;
                    case "If There Was An Endpoint.":
                        await PlaySong("If There Was An Endpoint.", IfThereWasAnEndpoint.TimedLyricsText, "vcw5THyM7Jo");
                        break;
                    case "I'm getting on the bus to the other world, see ya!":
                        await PlaySong("I'm getting on the bus to the other world, see ya!", BusToTheOtherWorld.TimedLyricsText, "4QXCPuwBz2E");
                        break;
                    case "Compared Child (TUYU Remix)":
                        await PlaySong("Compared Child (TUYU Remix)", ComparedChild.TimedLyricsText, "4TmzJzGXbB4");
                        break;
                    case "Being low as dirt, taking what's important from me":
                        await PlaySong("Being low as dirt, taking what's important from me", BeingLowAsDirt.TimedLyricsText, "M7FH1dL51oU");
                        break;
                    default:
                        break;
                }
            }
        }

        private async Task PlaySong(string songTitle, string timedLyricsText, string videoId)
        {
            await Task.Run(() =>
            {
                timedLyrics.lyricsLines = timedLyricsText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                lbl_Title.Text = songTitle;
                string embedUrl = $"https://www.youtube.com/embed/{videoId}?autoplay=1";
                chromiumWebBrowser1.Load(embedUrl);
            });
        }
    }
}
