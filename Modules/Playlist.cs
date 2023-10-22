using ComparedLyric;
using ComparedLyric.Properties;
using System;

class Playlist
{
    private readonly MainView mainViewInstance;
    internal const int songIndex = 10;

    public Playlist(MainView mainView)
    {
        mainViewInstance = mainView;
    }

    //[Title] [Time] [Image]
    internal object[,] songsList = new object[songIndex, 3]
    {
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

    internal async void SongControl_Click(object sender, EventArgs e)
    {
        if (sender is SongListControl songControl)
        {
            switch (songControl.SongTitle)
            {
                case "Compared Child":
                    await mainViewInstance.PlaySong("Compared Child", ComparedChild.TimedLyricsText, "olWvy0PiLfA");
                    break;
                case "Trapped in the Past":
                    await mainViewInstance.PlaySong("Trapped in the Past", TrappedInThePast.TimedLyricsText, "lGFEqEFJ410");
                    break;
                case "Hide and Seek Alone":
                    await mainViewInstance.PlaySong("Hide and Seek Alone", HideAndSeekAlone.TimedLyricsText, "Bq0ZINOzVng");
                    break;
                case "It's Raining After All":
                    await mainViewInstance.PlaySong("It's Raining After All", ItsRainingAfterAll.TimedLyricsText, "D0ehC_8sQuU");
                    break;
                case "Under the Summer Breeze":
                    await mainViewInstance.PlaySong("Under the Summer Breeze", UnderTheSummerBreeze.TimedLyricsText, "LoK17z6xDwI");
                    break;
                case "Goodbye to Rock you":
                    await mainViewInstance.PlaySong("Goodbye to Rock you", UnderTheSummerBreeze.TimedLyricsText, "1cGQotpn8r4");
                    break;
                case "If There Was An Endpoint.":
                    await mainViewInstance.PlaySong("If There Was An Endpoint.", IfThereWasAnEndpoint.TimedLyricsText, "vcw5THyM7Jo");
                    break;
                case "I'm getting on the bus to the other world, see ya!":
                    await mainViewInstance.PlaySong("I'm getting on the bus to the other world, see ya!", BusToTheOtherWorld.TimedLyricsText, "4QXCPuwBz2E");
                    break;
                case "Compared Child (TUYU Remix)":
                    await mainViewInstance.PlaySong("Compared Child (TUYU Remix)", ComparedChild.TimedLyricsText, "4TmzJzGXbB4");
                    break;
                case "Being low as dirt, taking what's important from me":
                    await mainViewInstance.PlaySong("Being low as dirt, taking what's important from me", BeingLowAsDirt.TimedLyricsText, "M7FH1dL51oU");
                    break;
                default:
                    break;
            }
        }
    }
}