using Frankenwiki.Domain.EventHandlers;
using NSubstitute;
using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace Frankenwiki.Tests.FrankengeneratorTests.CategoriesTests
{
    public class MultipleCategoriesInYamlFrontMatterAreIdentified
    {
        private Frankengenerator _generator;
        private InMemoryFrankenstore _store;
        private Frankenpage _page;
        private const string Slug = "with-two-categories-in-the-yaml";

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

        public void ThenThereAreTwoCategoriesInThePage()
        {
            _page.Categories.Length.ShouldBe(2);
        }

        public void AndThenTheSingleCategoryHasTheCorrectSlug()
        {
            _page.Categories.ShouldContain(x => x.Slug == "one-category");
            _page.Categories.ShouldContain(x => x.Slug == "two-category");
        }

        public void AndThenTheSingleCategoryHasAHumanizedName()
        {
            _page.Categories.ShouldContain(x => x.Name == "One Category");
            _page.Categories.ShouldContain(x => x.Name == "Two Category");
        }

        [Fact]
        public void Execute()
        {
            this.BDDfy();
        }
    }
}