using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace Frankenwiki.Tests.FrankengeneratorTests.CategoriesTests
{
    public class NoCategoriesInYamlFrontMatterEqualsNoCategories
    {
        private Frankengenerator _generator;
        private InMemoryFrankenstore _store;
        private Frankenpage _page;
        private const string Slug = "with-no-categories-in-the-yaml";

        public void GivenGenerator()
        {
            _generator = new Frankengenerator();
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

        public void ThenThereAreNoCategoriesInThePage()
        {
            _page.Categories.ShouldBeEmpty();
        }

        [Fact]
        public void Execute()
        {
            this.BDDfy();
        }
    }
    public class SingleCategoryInYamlFrontMatterIsIdentified
    {
        private Frankengenerator _generator;
        private InMemoryFrankenstore _store;
        private Frankenpage _page;
        private const string Slug = "with-one-category-in-the-yaml";

        public void GivenGenerator()
        {
            _generator = new Frankengenerator();
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
    public class MultipleCategoriesInYamlFrontMatterAreIdentified
    {
        private Frankengenerator _generator;
        private InMemoryFrankenstore _store;
        private Frankenpage _page;
        private const string Slug = "with-two-categories-in-the-yaml";

        public void GivenGenerator()
        {
            _generator = new Frankengenerator();
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
