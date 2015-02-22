using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace Frankenwiki.Tests.InMemoryFrankenstoreTests.GetAllCategoriesTests
{
    public class NoCategoriesWhenThereAreNoPages
    {
        private InMemoryFrankenstore _store;
        private FrankenpageCategory[] _categories;

        public void GivenStoreWithNoPages()
        {
            _store = new InMemoryFrankenstore();
        }

        public void WhenGettingAllCategories()
        {
            _categories = _store.GetAllCategoriesAsync().Result;
        }

        public void ThenThereAreNoCategories()
        {
            _categories.ShouldBeEmpty();
        }

        [Fact]
        public void Execute()
        {
            this.BDDfy();
        }
    }
}