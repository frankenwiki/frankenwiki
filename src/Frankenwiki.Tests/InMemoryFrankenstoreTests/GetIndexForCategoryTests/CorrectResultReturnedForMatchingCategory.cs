using System.Linq;
using Frankenwiki.Tests.ObjectMothers;
using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace Frankenwiki.Tests.InMemoryFrankenstoreTests.GetIndexForCategoryTests
{
    public class CorrectResultReturnedForMatchingCategory
    {
        private InMemoryFrankenstore _store;
        private Frankindex _index;

        public void GivenStoreWithItems()
        {
            _store = new InMemoryFrankenstore();
            _store.StoreAsync(new[]
            {
                ObjectMother.Frankenwiki.Frankenpages.Default
                    .WithSlug("non-matching-category")
                    .WithTitle("Non-Matching Category")
                    .WithCategory("other", "other")
                    .Build(),
                ObjectMother.Frankenwiki.Frankenpages.Default
                    .WithSlug("page-matching-category")
                    .WithTitle("Page Matching Category")
                    .WithCategory("random", "Random")
                    .Build()
            });
        }

        public void ThenWhenQueryingForCategory()
        {
            _index = _store.GetIndexForCategory("random").Result;
        }

        public void AndThenThereIsOnlyOneResult()
        {
            _index.Items.Count().ShouldBe(1);
        }

        [Fact]
        public void Execute()
        {
            this.BDDfy();
        }
    }
}