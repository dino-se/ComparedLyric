using System;

public class LyricsParser
{
    public bool ParseTimestamp(string line, out TimeSpan timestamp, out string lyricsText)
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

    public int FindLyricsLineIndexByTime(string[] lyricsLines, double elapsedTime)
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
}
