using System.Linq;
using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace Frankenwiki.Tests.FrankenpageTests
{
    public class AllLinksTests
    {
        private Frankenpage _page;

        [Fact]
        public void PageWithNoLinksHasNoLinks()
        {
            this
                .Given(x => x.GivenPageWithNoLinks())
                .Then(x => x.ThenPageHasNoLinks())
                .BDDfy();
        }

        [Fact]
        public void PageWithSomeLinksHasThoseLinks()
        {
            this.Given(x => x.GivenPageWithThreeLinks())
                .Then(x => x.ThenPageHasThreeLinks())
                .Then(x => x.ThenPageHasThreeCorrectLinks())
                .BDDfy();
        }

        void ThenPageHasThreeLinks()
        {
            _page.AllLinks.Count().ShouldBe(3);
        }

        void ThenPageHasThreeCorrectLinks()
        {
            _page.AllLinks.ShouldContain("/test-2");
            _page.AllLinks.ShouldContain("/test-3");
            _page.AllLinks.ShouldContain("/test-4");
        }

        private void GivenPageWithThreeLinks()
        {
            var md = @"
#Test 2

A [link is here](/test-2).

- one
- [two](/test-3)
- three
- [dupe of two](/test-3)

Html link <a href=""/test-4"">is here</a>.
";

            _page = new Frankenpage(
                slug: "test-1",
                markdown: md,
                html: Frankengenerator.Transform(md),
                title: "Test 1",
                categories: new FrankenpageCategory[0]);
        }

        private void ThenPageHasNoLinks()
        {
            _page.AllLinks.ShouldBeEmpty();
        }

        private void GivenPageWithNoLinks()
        {
            var md = @"
# Test 1

No links
";
            _page = new Frankenpage(
                "test-1",
                md,
                html: Frankengenerator.Transform(md),
                title: "Test 1",
                categories: new FrankenpageCategory[0]);
        }
    }
}