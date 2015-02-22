using System.Linq;
using Frankenwiki.Tests.ObjectMothers;
using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace Frankenwiki.Tests.InMemoryFrankenstoreTests.GetAllCategoriesTests
{
    public class CategoriesWhenThereArePagesInSameCategory
    {
        private InMemoryFrankenstore _store;
        private FrankenpageCategory[] _categories;

        public void GivenStoreWithNoPages()
        {
            _store = new InMemoryFrankenstore();
            _store.StoreAsync(new[]
            {
                ObjectMother.Frankenwiki.Frankenpages.Default
                    .WithCategory("one", "One")
                    .WithCategory("two", "Two")
                    .Build(),
                ObjectMother.Frankenwiki.Frankenpages.Default
                    .WithCategory("two", "Two")
                    .Build(),
            });
        }

        public void WhenGettingAllCategories()
        {
            _categories = _store.GetAllCategoriesAsync().Result;
        }

        public void ThenThereAreTwoCategories()
        {
            _categories.Count().ShouldBe(2);
            _categories.ShouldAllBe(x => x.Slug == "one" || x.Slug == "two");
        }

        [Fact]
        public void Execute()
        {
            this.BDDfy();
        }
    }
}