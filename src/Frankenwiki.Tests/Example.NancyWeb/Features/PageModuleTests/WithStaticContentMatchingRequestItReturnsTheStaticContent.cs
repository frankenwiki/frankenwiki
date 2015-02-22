using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frankenwiki.Example.NancyWeb.Features;
using Frankenwiki.Example.NancyWeb.Plumbing;
using Nancy.Testing;
using NSubstitute;
using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace Frankenwiki.Tests.Example.NancyWeb.Features.PageModuleTests
{
    public class WithStaticContentMatchingRequestItReturnsTheStaticContent
    {
        private IFrankenstore _store;
        private Browser _browser;
        private BrowserResponse _response;

        public void GivenStoreThatDoesNotHaveTheRequestedSlug()
        {
            _store = Substitute.For<IFrankenstore>();
            _store
                .GetPageAsync("static-content/ghost-cat.jpg")
                .Returns(Task.FromResult(default(Frankenpage)));
        }

        public void AndGivenTheBrowser()
        {
            _browser = new Browser(with =>
            {
                with.Module<PageModule>();
                with.Dependency(_store);
                with.Dependency(new WikiSourcePathSetting { Value = "test-wiki" });
            });
        }

        public void WhenRequestingThePage()
        {
            _response = _browser.Get("/wiki/static-content/ghost-cat.jpg");
        }

        public void ThenTheResponseIsJpegImage()
        {
            _response.ContentType.ShouldBe("image/jpeg");
        }

        [Fact]
        public void Execute()
        {
            this.BDDfy();
        }
    }
}
