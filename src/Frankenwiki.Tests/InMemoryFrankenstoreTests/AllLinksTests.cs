using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace Frankenwiki.Tests.InMemoryFrankenstoreTests
{
    public class AllLinksTests
    {
        private Frankenpage _page;
        private Frankenpage _page2;
        private Frankenpage _page3;
        private Frankenpage _page4;

        [Fact]
        public void PageWithNoLinksHasNoLinks()
        {
            this
                .Given(x => x.GivenPageWithNoLinks())
                .Given(x => x.GivenThreeAuxillaryPages())
                .When(x => x.WhenThePagesAreIndexed())
                .Then(x => x.ThenPageHasNoLinks())
                .BDDfy();
        }

        [Fact]
        public void PageWithSomeLinksHasThoseLinks()
        {
            this.Given(x => x.GivenPageWithThreeLinks())
                .Given(x => x.GivenThreeAuxillaryPages())
                .When(x => x.WhenThePagesAreIndexed())
                .Then(x => x.ThenPageHasThreeLinks())
                .Then(x => x.ThenPageHasThreeCorrectLinks())
                .BDDfy();
        }

        void GivenThreeAuxillaryPages()
        {
            _page2 = GetFrankenpage("#Test 2", "test-2");
            _page3 = GetFrankenpage("#Test 3", "test-3");
            _page4 = GetFrankenpage("#Test 4", "test-4");
        }

        async void WhenThePagesAreIndexed()
        {
            var generator = new InMemoryFrankenstore();
            await generator.StoreAsync(new[] { _page, _page2, _page3, _page4 });
        }

        void ThenPageHasThreeLinks()
        {
            _page.AllLinks.Count().ShouldBe(3);
        }

        void ThenPageHasThreeCorrectLinks()
        {
            _page.AllLinks.ShouldContain(x => x.PageSlug == "test-2");
            _page.AllLinks.ShouldContain(x => x.PageSlug == "test-3");
            _page.AllLinks.ShouldContain(x => x.PageSlug == "test-4");
        }

        private void GivenPageWithThreeLinks()
        {
            _page = GetFrankenpage(
                @"
#Test 1

A [link is here](/wiki/test-2).

- one
- [two](/wiki/test-3)
- three
- [dupe of two](/wiki/test-3)

Html link <a href=""/wiki/test-4"">is here</a>.
", "test-1");
        }

        private void ThenPageHasNoLinks()
        {
            _page.AllLinks.ShouldBeEmpty();
        }

        private void GivenPageWithNoLinks()
        {
            _page = GetFrankenpage(
                @"
# Test 1

No links
", "test-1");
        }

        static Frankenpage GetFrankenpage(string markdown, string slug)
        {
            return new Frankenpage(
                slug: slug,
                markdown: markdown,
                html: Frankengenerator.Transform(markdown),
                title: slug,
                categories: new FrankenpageCategory[0]);
        }
    }
}
