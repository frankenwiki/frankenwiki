using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace Frankenwiki.Tests.FrankengeneratorTests.TitleTests
{
    public class WithH1SpecifyingTheTitle
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

        public void AndThenThePageTitleIsCorrect()
        {
            _page.Title.ShouldBe("Title in the text! Groovy.");
        }

        [Fact]
        public void Execute()
        {
            this.BDDfy();
        }
    }
}