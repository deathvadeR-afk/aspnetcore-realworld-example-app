using System.Linq;
using Conduit.Features.Articles;
using Xunit;

namespace Conduit.IntegrationTests.Features.Articles;

public class ReadingTimeTests
{
    [Fact]
    public void Returns_3_Minutes_For_A_450_Word_Body()
    {
        Assert.Equal(3, ReadingTime.Minutes(Words(450, " ")));
    }

    [Fact]
    public void Returns_1_Minute_For_A_200_Word_Body()
    {
        Assert.Equal(1, ReadingTime.Minutes(Words(200, " ")));
    }

    [Fact]
    public void Returns_2_Minutes_For_A_201_Word_Body()
    {
        Assert.Equal(2, ReadingTime.Minutes(Words(201, " ")));
    }

    [Fact]
    public void Returns_1_Minute_For_A_Single_Word_Body()
    {
        Assert.Equal(1, ReadingTime.Minutes("Hello"));
    }

    [Fact]
    public void Returns_0_Minutes_For_A_Null_Body()
    {
        Assert.Equal(0, ReadingTime.Minutes(null));
    }

    [Fact]
    public void Returns_0_Minutes_For_An_Empty_Body()
    {
        Assert.Equal(0, ReadingTime.Minutes(""));
    }

    [Fact]
    public void Returns_0_Minutes_For_A_Whitespace_Only_Body()
    {
        Assert.Equal(0, ReadingTime.Minutes("   \t\n"));
    }

    [Fact]
    public void Counts_Mixed_Whitespace_Runs_As_Single_Separators()
    {
        Assert.Equal(2, ReadingTime.Minutes(Words(201, " \t\n")));
    }

    private static string Words(int count, string separator) =>
        string.Join(separator, Enumerable.Repeat("word", count));
}
