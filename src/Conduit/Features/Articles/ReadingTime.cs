using System;

namespace Conduit.Features.Articles;

public static class ReadingTime
{
    private const int WORDS_PER_MINUTE = 200;

    public static int Minutes(string? body)
    {
        if (string.IsNullOrWhiteSpace(body))
        {
            return 0;
        }

        var words = 0;
        var inWord = false;
        foreach (var c in body)
        {
            if (char.IsWhiteSpace(c))
            {
                inWord = false;
            }
            else if (!inWord)
            {
                inWord = true;
                words++;
            }
        }

        return Math.Max(1, (words + WORDS_PER_MINUTE - 1) / WORDS_PER_MINUTE);
    }
}
