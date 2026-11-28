using System.Linq;
using System.Text;
using Conduit.Features.Articles;
using Xunit;

namespace Conduit.IntegrationTests.Features.Articles;

public class ReadingTimeTests
{
    [Fact(DisplayName = "[QP-SMDOTNET-7-1] Returns 3 minutes for a 450-word body")]
    public void Returns_3_Minutes_For_A_450_Word_Body()
    {
        var body = BuildSpacedBody(450);

        var minutes = ReadingTime.Estimate(body);

        Assert.Equal(3, minutes);
    }

    [Fact(DisplayName = "[QP-SMDOTNET-7-2] Returns 1 minute for a 200-word body")]
    public void Returns_1_Minute_For_A_200_Word_Body()
    {
        var body = BuildSpacedBody(200);

        var minutes = ReadingTime.Estimate(body);

        Assert.Equal(1, minutes);
    }

    [Fact(DisplayName = "[QP-SMDOTNET-7-3] Rounds up to 2 minutes for a 201-word body")]
    public void Rounds_Up_To_2_Minutes_For_A_201_Word_Body()
    {
        var body = BuildSpacedBody(201);

        var minutes = ReadingTime.Estimate(body);

        Assert.Equal(2, minutes);
    }

    [Fact(DisplayName = "[QP-SMDOTNET-7-4] Returns at least 1 minute for a single-word body")]
    public void Returns_At_Least_1_Minute_For_A_Single_Word_Body()
    {
        var minutes = ReadingTime.Estimate("word");

        Assert.Equal(1, minutes);
    }

    [Fact(DisplayName = "[QP-SMDOTNET-7-5] Returns 0 for a null body")]
    public void Returns_0_For_A_Null_Body()
    {
        var minutes = ReadingTime.Estimate(null);

        Assert.Equal(0, minutes);
    }

    [Fact(DisplayName = "[QP-SMDOTNET-7-6] Returns 0 for an empty body")]
    public void Returns_0_For_An_Empty_Body()
    {
        var minutes = ReadingTime.Estimate("");

        Assert.Equal(0, minutes);
    }

    [Fact(DisplayName = "[QP-SMDOTNET-7-7] Returns 0 for a whitespace-only body")]
    public void Returns_0_For_A_Whitespace_Only_Body()
    {
        var minutes = ReadingTime.Estimate(" \t\n");

        Assert.Equal(0, minutes);
    }

    [Fact(DisplayName = "[QP-SMDOTNET-7-8] Counts words separated by mixed whitespace runs as separate words")]
    public void Counts_Words_Separated_By_Mixed_Whitespace_As_Separate_Words()
    {
        var body = BuildMixedWhitespaceBody(200);

        var minutes = ReadingTime.Estimate(body);

        Assert.Equal(1, minutes);
    }

    private static string BuildSpacedBody(int wordCount) =>
        string.Join(" ", Enumerable.Repeat("word", wordCount));

    private static string BuildMixedWhitespaceBody(int wordCount)
    {
        string[] separators = [" ", "  ", "\t", "\n", " \t\r\n", "\r\n\r\n", "   \t  "];
        var builder = new StringBuilder();
        for (var i = 0; i < wordCount; i++)
        {
            if (i > 0)
            {
                builder.Append(separators[i % separators.Length]);
            }

            builder.Append("word");
        }

        return builder.ToString();
    }
}
