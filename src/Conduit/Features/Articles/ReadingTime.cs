using System;

namespace Conduit.Features.Articles;

public static class ReadingTime
{
    private const int WordsPerMinute = 200;

    public static int Estimate(string? body)
    {
        if (string.IsNullOrWhiteSpace(body))
        {
            return 0;
        }

        var wordCount = body.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries).Length;
        var minutes = (wordCount + WordsPerMinute - 1) / WordsPerMinute;
        return Math.Max(1, minutes);
    }
}
