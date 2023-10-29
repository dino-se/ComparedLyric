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
        private readonly Playlist playlist;
        private double videoStartTime = 0;
        private bool isVideoStarted = false;
        public MainView()
        {
            InitializeComponent();
            timedLyrics = new TimedLyrics();
            playlist = new Playlist(this);
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

            SongListControl[] listSongs = new SongListControl[Playlist.songIndex];

            for (int i = 0; i < listSongs.Length; i++)
            {
                listSongs[i] = new SongListControl()
                {
                    SongTitle = (string)playlist.songsList[i, 1],
                    SongTime = (string)playlist.songsList[i, 2],
                    SongIcon = (Image)playlist.songsList[i, 4]
                };

                flp_SongList.Controls.Add(listSongs[i]);

                listSongs[i].Click += new EventHandler(playlist.SongControl_Click);
            }
        }

        internal async Task PlaySong(string songTitle, string timedLyricsText, string videoId)
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
