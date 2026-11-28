using System.Linq;
using System.Text;
using Conduit.Features.Articles;
using Xunit;

namespace Conduit.IntegrationTests.Features.Articles;

public class ReadingTimeTests
{
    [Fact(DisplayName = "[QP-SMDOTNET-4-1] Returns 3 minutes for a 450-word body")]
    public void Returns_3_Minutes_For_A_450_Word_Body()
    {
        var body = Words(450);

        Assert.Equal(3, ReadingTime.Minutes(body));
    }

    [Fact(DisplayName = "[QP-SMDOTNET-4-2] Returns 1 minute for a 200-word body")]
    public void Returns_1_Minute_For_A_200_Word_Body()
    {
        var body = Words(200);

        Assert.Equal(1, ReadingTime.Minutes(body));
    }

    [Fact(DisplayName = "[QP-SMDOTNET-4-3] Rounds up to 2 minutes for a 201-word body")]
    public void Rounds_Up_To_2_Minutes_For_A_201_Word_Body()
    {
        var body = Words(201);

        Assert.Equal(2, ReadingTime.Minutes(body));
    }

    [Fact(DisplayName = "[QP-SMDOTNET-4-4] Returns 1 minute for a single-word body")]
    public void Returns_1_Minute_For_A_Single_Word_Body()
    {
        Assert.Equal(1, ReadingTime.Minutes("Hello"));
    }

    [Fact(DisplayName = "[QP-SMDOTNET-4-5] Returns 0 minutes for a null body")]
    public void Returns_0_Minutes_For_A_Null_Body()
    {
        Assert.Equal(0, ReadingTime.Minutes(null));
    }

    [Fact(DisplayName = "[QP-SMDOTNET-4-6] Returns 0 minutes for an empty body")]
    public void Returns_0_Minutes_For_An_Empty_Body()
    {
        Assert.Equal(0, ReadingTime.Minutes(string.Empty));
    }

    [Fact(DisplayName = "[QP-SMDOTNET-4-7] Returns 0 minutes for a whitespace-only body")]
    public void Returns_0_Minutes_For_A_Whitespace_Only_Body()
    {
        Assert.Equal(0, ReadingTime.Minutes(" \t\n  \t \n"));
    }

    [Fact(DisplayName = "[QP-SMDOTNET-4-8] Counts a run of mixed whitespace as one separator")]
    public void Counts_A_Run_Of_Mixed_Whitespace_As_One_Separator()
    {
        var separators = new[] { "  ", "\t\t", "\n\n", " \t\n" };
        var body = new StringBuilder();
        for (var i = 0; i < 200; i++)
        {
            if (i > 0)
            {
                body.Append(separators[i % separators.Length]);
            }

            body.Append("word");
        }

        Assert.Equal(1, ReadingTime.Minutes(body.ToString()));
    }

    private static string Words(int count) =>
        string.Join(" ", Enumerable.Repeat("word", count));
}
