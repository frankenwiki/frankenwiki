using System.Threading.Tasks;
using Frankenwiki.Configuration;
using Frankenwiki.Example.NancyWeb.Plumbing;
using Frankenwiki.Host.Nancy.Features;
using Frankenwiki.Tests.ObjectMothers;
using Nancy.Testing;
using NSubstitute;
using TestStack.BDDfy;
using Xunit;

namespace Frankenwiki.Tests.Example.NancyWeb.Features.PageModuleTests
{
    public class WithPageInStoreItServesThePage
    {
        private Frankenpage _page;
        private IFrankenstore _store;
        private Browser _browser;
        private BrowserResponse _response;

        public void GivenFrankenpage()
        {
            _page = ObjectMother.Frankenwiki.Frankenpages.Default
                .WithSlug("slug")
                .WithTitle("Grooving")
                .Build();
        }

        public void AndGivenStoreThatReturnsThePage()
        {
            _store = Substitute.For<IFrankenstore>();
            _store
                .GetPageAsync("slug")
                .Returns(Task.FromResult(_page));
        }

        public void AndGivenTheBrowser()
        {
            var configuration = FrankenwikiConfiguration.Create()
                .WithWikiSourcePath("test-wiki")
                .Build();

            _browser = new Browser(with =>
            {
                with.Module<PageModule>();
                with.Dependency(_store);
                with.Dependency(configuration);
            });
        }

        public void WhenRequestingThePage()
        {
            _response = _browser.Get("/wiki/slug");
        }

        public void ThenTheResultingPageIsTheConfiguredOne()
        {
            _response.Body["title"].AllShouldContain(_page.Title);
        }

        [Fact]
        public void Execute()
        {
            this.BDDfy();
        }
    }
}