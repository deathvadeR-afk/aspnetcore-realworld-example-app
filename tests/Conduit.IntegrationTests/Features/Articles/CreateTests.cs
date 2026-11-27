using System.Linq;
using System.Threading.Tasks;
using Conduit.Features.Articles;
using Xunit;

namespace Conduit.IntegrationTests.Features.Articles;

public class CreateTests : SliceFixture
{
    [Fact]
    public async Task Duplicate_Tags_Are_Ignored_For_New_And_Existing_Tags()
    {
        var article = await ArticleHelpers.CreateArticle(
            this,
            new Create.Command(
                new Create.ArticleData
                {
                    Title = "Duplicates",
                    Description = "d",
                    Body = "b",
                    TagList = ["x", "x"],
                }
            )
        );
        Assert.Equal("x", Assert.Single(article.TagList));
        var handler = new Create.Handler(
            GetDbContext(),
            new StubCurrentUserAccessor(Users.UserHelpers.DefaultUserName)
        );
        var result = await handler.Handle(
            new Create.Command(
                new Create.ArticleData
                {
                    Title = "Existing duplicates",
                    Description = "d",
                    Body = "b",
                    TagList = ["x", "x"],
                }
            ),
            default
        );
        Assert.Equal("x", Assert.Single(result.Article.TagList));
    }

    [Fact]
    public async Task Expect_Create_Article()
    {
        var command = new Create.Command(
            new Create.ArticleData
            {
                Title = "Test article dsergiu77",
                Description = "Description of the test article",
                Body = "Body of the test article",
                TagList = ["tag1", "tag2"],
            }
        );

        var article = await ArticleHelpers.CreateArticle(this, command);

        Assert.NotNull(article);
        Assert.Equal(article.Title, command.Article.Title);
        Assert.Equal(article.TagList.Count(), command.Article.TagList?.Count());
    }

    [Fact(DisplayName = "[QP-SMDOTNET-3-1] Keeps the hyphen in the slug of a hyphenated title")]
    public async Task Keeps_Hyphen_In_Slug_Of_Hyphenated_Title()
    {
        var article = await ArticleHelpers.CreateArticle(
            this,
            new Create.Command(
                new Create.ArticleData
                {
                    Title = "Rock-and-Roll Basics",
                    Description = "d",
                    Body = "b",
                }
            )
        );

        Assert.Equal("rock-and-roll-basics", article.Slug);
    }

    [Fact(DisplayName = "[QP-SMDOTNET-3-2] Keeps repeated hyphens in the slug without collapsing them")]
    public async Task Keeps_Repeated_Hyphens_In_Slug_Without_Collapsing_Them()
    {
        var article = await ArticleHelpers.CreateArticle(
            this,
            new Create.Command(
                new Create.ArticleData
                {
                    Title = "Rock--Roll",
                    Description = "d",
                    Body = "b",
                }
            )
        );

        Assert.Equal("rock--roll", article.Slug);
    }

    [Fact(DisplayName = "[QP-SMDOTNET-3-3] Keeps a hyphen at the start and end of a title in the slug")]
    public async Task Keeps_Hyphen_At_Start_And_End_Of_Title_In_Slug()
    {
        var article = await ArticleHelpers.CreateArticle(
            this,
            new Create.Command(
                new Create.ArticleData
                {
                    Title = "-Rock-",
                    Description = "d",
                    Body = "b",
                }
            )
        );

        Assert.Equal("-rock-", article.Slug);
    }
}
