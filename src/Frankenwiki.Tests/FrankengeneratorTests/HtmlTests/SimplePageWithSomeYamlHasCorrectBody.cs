using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace Frankenwiki.Tests.FrankengeneratorTests.HtmlTests
{
    public class SimplePageWithSomeYamlHasCorrectBody
    {
        private Frankengenerator _generator;
        private InMemoryFrankenstore _store;
        private Frankenpage _page;
        private const string Slug = "simple-test-page-with-some-yaml";

        public void GivenGenerator()
        {
            _generator = new Frankengenerator();
        }

        public void AndGivenAnInMemoryStore()
        {
            _store = new InMemoryFrankenstore();
        }

        public void WhenProcessingFiles()
        {
            _generator.GenerateFromSource("test-wiki", _store);
            _page = _store.GetPageAsync(Slug)
                .Result;
        }

        public void ThenThePageHtmlDoesNotIncludeTheYaml()
        {
            _page.Html.Trim().ShouldBe("<p><strong>Test</strong></p>");
        }

        [Fact]
        public void Execute()
        {
            this.BDDfy();
        }
    }
}