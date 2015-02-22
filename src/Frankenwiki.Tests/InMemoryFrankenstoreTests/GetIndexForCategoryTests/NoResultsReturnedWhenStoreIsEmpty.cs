using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace Frankenwiki.Tests.InMemoryFrankenstoreTests.GetIndexForCategoryTests
{
    public class NoResultsReturnedWhenStoreIsEmpty
    {
        private InMemoryFrankenstore _store;
        private Frankindex _index;

        public void GivenEmptyStore()
        {
            _store = new InMemoryFrankenstore();
        }

        public void ThenWhenQueryingForCategory()
        {
            _index = _store.GetIndexForCategory("random").Result;
        }

        public void ThenThereAreNoResults()
        {
            _index.Items.ShouldBeEmpty();
        }

        public void ThenTheSlugShouldBeTheCategorySlug()
        {
            _index.Slug.ShouldBe("random");
        }

        public void ThenTheNameShouldBeTheHumanizedSlug()
        {
            _index.Name.ShouldBe("Random");
        }

        [Fact]
        public void Execute()
        {
            this.BDDfy();
        }
    }
}