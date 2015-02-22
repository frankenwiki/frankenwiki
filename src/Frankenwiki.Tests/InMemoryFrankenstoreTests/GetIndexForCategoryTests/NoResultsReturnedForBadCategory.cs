using Frankenwiki.Tests.ObjectMothers;
using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace Frankenwiki.Tests.InMemoryFrankenstoreTests.GetIndexForCategoryTests
{
    public class NoResultsReturnedForBadCategory
    {
        private InMemoryFrankenstore _store;
        private Frankindex _index;

        public void GivenStoreWithItem()
        {
            _store = new InMemoryFrankenstore();
            _store.StoreAsync(new[]
            {
                ObjectMother.Frankenwiki.Frankenpages.Default
                    .WithCategory("random", "Random")
                    .Build()
            });
        }

        public void ThenWhenQueryingForCategory()
        {
            _index = _store.GetIndexForCategory("bad-category").Result;
        }

        public void ThenThereAreNoResults()
        {
            _index.Items.ShouldBeEmpty();
        }

        [Fact]
        public void Execute()
        {
            this.BDDfy();
        }
    }
}