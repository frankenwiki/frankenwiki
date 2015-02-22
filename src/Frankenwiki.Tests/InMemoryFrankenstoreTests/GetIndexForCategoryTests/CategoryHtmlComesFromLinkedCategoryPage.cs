using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frankenwiki.Tests.ObjectMothers;
using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace Frankenwiki.Tests.InMemoryFrankenstoreTests.GetIndexForCategoryTests
{
    public class CategoryHtmlComesFromLinkedCategoryPage
    {
        private InMemoryFrankenstore _store;
        private Frankindex _index;

        public void GivenStoreWithItems()
        {
            _store = new InMemoryFrankenstore();
            _store.StoreAsync(new[]
            {
                ObjectMother.Frankenwiki.Frankenpages.Default
                    .WithSlug("category-random")
                    .WithTitle("Random category disambiguation (this is ignored)")
                    .WithHtml("<p>Text about the random category</p>")
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

        public void AndThenTheIndexHtmlComesFromTheDisambiguationPage()
        {
            _index.Html.ShouldBe(_store.GetPageAsync("category-random").Result.Html);
        }

        [Fact]
        public void Execute()
        {
            this.BDDfy();
        }
    }
}
