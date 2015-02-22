using System.Linq;
using Frankenwiki.Tests.ObjectMothers;
using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace Frankenwiki.Tests.InMemoryFrankenstoreTests.GetIndexForCategoryTests
{
    public class SingleResultReturnedForMatchingCategory
    {
        private InMemoryFrankenstore _store;
        private Frankindex _index;

        public void GivenStoreWithItem()
        {
            _store = new InMemoryFrankenstore();
            _store.StoreAsync(new[]
            {
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

        public void ThenThereIsOneValidResult()
        {
            var item = _index.Items.SingleOrDefault();
            item.ShouldSatisfyAllConditions(
                () => item.ShouldNotBe(null),
                () => item.PageSlug.ShouldBe("page-matching-category"),
                () => item.Title.ShouldBe("Page Matching Category"));
        }

        [Fact]
        public void Execute()
        {
            this.BDDfy();
        }
    }
}