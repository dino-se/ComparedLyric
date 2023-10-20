using ComparedLyric;
using ComparedLyric.Properties;

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
}