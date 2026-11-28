using System;

namespace Conduit.Features.Articles;

public static class ReadingTime
{
    private const int WordsPerMinute = 200;

    public static int Minutes(string? body)
    {
        if (string.IsNullOrWhiteSpace(body))
        {
            return 0;
        }

        var words = body.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries).Length;
        return (words + WordsPerMinute - 1) / WordsPerMinute;
    }
}
