using ComparedLyric;
using ComparedLyric.Properties;
using System;

class Playlist
{
    private readonly MainView mainViewInstance;
    internal const int songIndex = 12;

    public Playlist(MainView mainView)
    {
        mainViewInstance = mainView;
    }

    //[videoId] [Title] [Time] [Lyrics] [Image]
    internal object[,] songsList = new object[songIndex, 5]
    {
        { "olWvy0PiLfA","Compared Child", "3:36",
            ComparedChild.TimedLyricsText, Resources.Compared_Child },

        { "lGFEqEFJ410","Trapped in the Past", "3:50",
            TrappedInThePast.TimedLyricsText, Resources.Trapped_In_The_Past },

        { "Bq0ZINOzVng","Hide and Seek Alone", "3:14",
            HideAndSeekAlone.TimedLyricsText, Resources.Trapped_In_The_Past },

        { "D0ehC_8sQuU","It's Raining After All", "4:07",
            ItsRainingAfterAll.TimedLyricsText, Resources.Compared_Child },

        { "HwGFMez_Tnc","It's Raining Nevertheless", "3:44",
            ItsRainingNevertheless.TimedLyricsText, Resources.Compared_Child },

        { "LoK17z6xDwI","Under the Summer Breeze", "3:23",
            UnderTheSummerBreeze.TimedLyricsText, Resources.Compared_Child },

        { "74q3rOlcPaI","Revolutionary front", "3:40",
            RevolutionaryFront.TimedLyricsText, Resources.Compared_Child },

        { "4TmzJzGXbB4","Compared Child (TUYU Remix)", "3:36",
            ComparedChild.TimedLyricsText, Resources.Compared_Child_Remix },
        //No lyrics
        { "1cGQotpn8r4","Goodbye to Rock you", "3:28",
            GoodbyeToRockYou.TimedLyricsText, Resources.Compared_Child },
        //No lyrics
        { "vcw5THyM7Jo","If There Was An Endpoint.", "3:00",
            IfThereWasAnEndpoint.TimedLyricsText, Resources.Compared_Child },

        { "4QXCPuwBz2E","I'm getting on the bus to the other world, see ya!", "3:16",
            BusToTheOtherWorld.TimedLyricsText, Resources.Compared_Child },

        { "M7FH1dL51oU","Being low as dirt, taking what's important from me", "3:13",
            BeingLowAsDirt.TimedLyricsText, Resources.Compared_Child },
    };

    internal async void SongControl_Click(object sender, EventArgs e)
    {
        if (sender is SongListControl songControl)
        {
            for (int i = 0; i < songsList.GetLength(0); i++)
            {
                if (songControl.SongTitle == (string)songsList[i, 1])
                {
                    string songTitle = (string)songsList[i, 1];
                    string lyricsText = (string)songsList[i, 3];
                    string videoId = (string)songsList[i, 0];

                    await mainViewInstance.PlaySong(songTitle, lyricsText, videoId);
                    break;
                }
            }
        }
    }

    //internal async void SongControl_Click(object sender, EventArgs e)
    //{
    //    if (sender is SongListControl songControl)
    //    {
    //        switch (songControl.SongTitle)
    //        {
    //            case "Compared Child":
    //                await mainViewInstance.PlaySong("Compared Child",
    //                    ComparedChild.TimedLyricsText, "olWvy0PiLfA");
    //                break;
    //            case "Trapped in the Past":
    //                await mainViewInstance.PlaySong("Trapped in the Past",
    //                    TrappedInThePast.TimedLyricsText, "lGFEqEFJ410");
    //                break;
    //            case "Hide and Seek Alone":
    //                await mainViewInstance.PlaySong("Hide and Seek Alone",
    //                    HideAndSeekAlone.TimedLyricsText, "Bq0ZINOzVng");
    //                break;
    //            case "It's Raining After All":
    //                await mainViewInstance.PlaySong("It's Raining After All",
    //                    ItsRainingAfterAll.TimedLyricsText, "D0ehC_8sQuU");
    //                break;
    //            case "Under the Summer Breeze":
    //                await mainViewInstance.PlaySong("Under the Summer Breeze",
    //                    UnderTheSummerBreeze.TimedLyricsText, "LoK17z6xDwI");
    //                break;
    //            case "Goodbye to Rock you":
    //                await mainViewInstance.PlaySong("Goodbye to Rock you",
    //                    UnderTheSummerBreeze.TimedLyricsText, "1cGQotpn8r4");
    //                break;
    //            case "Revolutionary front":
    //                await mainViewInstance.PlaySong("Revolutionary front",
    //                    RevolutionaryFront.TimedLyricsText, "74q3rOlcPaI");
    //                break;
    //            case "If There Was An Endpoint.":
    //                await mainViewInstance.PlaySong("If There Was An Endpoint.",
    //                    IfThereWasAnEndpoint.TimedLyricsText, "vcw5THyM7Jo");
    //                break;
    //            case "I'm getting on the bus to the other world, see ya!":
    //                await mainViewInstance.PlaySong("I'm getting on the bus to the other world, see ya!",
    //                    BusToTheOtherWorld.TimedLyricsText, "4QXCPuwBz2E");
    //                break;
    //            case "Compared Child (TUYU Remix)":
    //                await mainViewInstance.PlaySong("Compared Child (TUYU Remix)",
    //                    ComparedChild.TimedLyricsText, "4TmzJzGXbB4");
    //                break;
    //            case "Being low as dirt, taking what's important from me":
    //                await mainViewInstance.PlaySong("Being low as dirt, taking what's important from me",
    //                    BeingLowAsDirt.TimedLyricsText, "M7FH1dL51oU");
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //}
}