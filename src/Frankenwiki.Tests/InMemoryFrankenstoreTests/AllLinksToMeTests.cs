using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using TestStack.BDDfy;
using TestStack.BDDfy.Configuration;
using Xunit;

namespace Frankenwiki.Tests.InMemoryFrankenstoreTests
{
    public class AllLinksToMeTests
    {
        private Frankenpage _primaryPage;
        private Frankenpage _secondaryPage;

        [Fact]
        public void PageWithNoLinksToItHasNoLinksToMe()
        {
            this
                .Given(x => x.GivenPrimaryPage())
                .Given(x => x.GivenSecondaryPageWithNoReturningLinks())
                .When(x => x.WhenThePagesAreIndexed())
                .Then(x => x.ThenThePrimaryPageHasNoLinksToMe())
                .BDDfy();
        }

        [Fact]
        public void PageWithALinkToItHasOneLinkToMe()
        {
            this
                .Given(x => x.GivenPrimaryPage())
                .Given(x => x.GivenSecondaryPageWithOneReturningLink())
                .When(x => x.WhenThePagesAreIndexed())
                .Then(x => x.ThenThePrimaryPageHasTheCorrectLinkToMe())
                .BDDfy();
        }

        void GivenSecondaryPageWithOneReturningLink()
        {
            _secondaryPage = GetFrankenpage(@"
# Test 2

Content for secondary page with one returning link:

[returning link](/test-1)
", "/test-2");
        }

        void ThenThePrimaryPageHasTheCorrectLinkToMe()
        {
            _primaryPage.AllLinksToMe.Count().ShouldBe(1);
            _primaryPage.AllLinksToMe.Single().ShouldBe("/test-2");
        }

        void ThenThePrimaryPageHasNoLinksToMe()
        {
            _primaryPage.AllLinksToMe.ShouldBeEmpty();
        }

        async void WhenThePagesAreIndexed()
        {
            var generator = new InMemoryFrankenstore();
            await generator.StoreAsync(new[] {_primaryPage, _secondaryPage});
        }

        void GivenSecondaryPageWithNoReturningLinks()
        {
            _secondaryPage = GetFrankenpage(@"
# Test 2

Content for secondary page with no returning links.
", "/test-2");
        }

        private void GivenPrimaryPage()
        {
            _primaryPage = GetFrankenpage(@"
# Test 1

Content for primary page
", "/test-1");
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
