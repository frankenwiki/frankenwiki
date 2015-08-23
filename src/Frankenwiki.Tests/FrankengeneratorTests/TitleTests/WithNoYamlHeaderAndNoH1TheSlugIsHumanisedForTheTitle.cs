using Frankenwiki.Domain.EventHandlers;
using NSubstitute;
using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace Frankenwiki.Tests.FrankengeneratorTests.TitleTests
{
    public class WithNoYamlHeaderAndNoH1TheSlugIsHumanisedForTheTitle
    {
        private Frankengenerator _generator;
        private InMemoryFrankenstore _store;
        private Frankenpage _page;
        private const string Slug = "with-no-yaml-header-and-no-h1s";

        public void GivenGenerator()
        {
            var domainEventBroker = Substitute.For<IDomainEventBroker>();
            _generator = new Frankengenerator(domainEventBroker);
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
            _page.Title.ShouldBe("With No Yaml Header And No H1s");
        }

        [Fact]
        public void Execute()
        {
            this.BDDfy();
        }
    }
}