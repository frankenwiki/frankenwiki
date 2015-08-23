using System.Linq;
using Frankenwiki.Domain.EventHandlers;
using NSubstitute;
using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace Frankenwiki.Tests.FrankengeneratorTests.CategoriesTests
{
    public class SingleCategoryInYamlFrontMatterIsIdentified
    {
        private Frankengenerator _generator;
        private InMemoryFrankenstore _store;
        private Frankenpage _page;
        private const string Slug = "with-one-category-in-the-yaml";

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

        public void ThenThereIsOneCategoryInThePage()
        {
            _page.Categories.Length.ShouldBe(1);
        }

        public void AndThenTheSingleCategoryHasTheCorrectSlug()
        {
            _page.Categories.Single().Slug.ShouldBe("one-category");
        }

        public void AndThenTheSingleCategoryHasAHumanizedName()
        {
            _page.Categories.Single().Name.ShouldBe("One Category");
        }

        [Fact]
        public void Execute()
        {
            this.BDDfy();
        }
    }
}