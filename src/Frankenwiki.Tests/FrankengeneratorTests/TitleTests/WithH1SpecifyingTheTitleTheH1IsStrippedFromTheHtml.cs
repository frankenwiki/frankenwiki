using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace Frankenwiki.Tests.FrankengeneratorTests.TitleTests
{
    public class WithH1SpecifyingTheTitleTheH1IsStrippedFromTheHtml
    {
        private Frankengenerator _generator;
        private InMemoryFrankenstore _store;
        private Frankenpage _page;
        private const string Slug = "with-h1-specifying-the-title";

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

        public void ThenTheTargetIsInTheStore()
        {
            _page.ShouldNotBe(null);
        }

        public void AndThenThePageContentDoesNotIncludeTheHtml()
        {
            _page.Html.ShouldNotContain("<h1>Title in the text! Groovy.</h1>");
        }

        public void AndThenThePageContentIncludesTheRestOfTheContent()
        {
            _page.Html.Trim().ShouldBe("<p>:boom:</p>");
        }

        [Fact]
        public void Execute()
        {
            this.BDDfy();
        }
    }
}