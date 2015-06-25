using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Frankenwiki.Plumbing;
using Humanizer;

namespace Frankenwiki
{
    public class InMemoryFrankenstore : IFrankenstore
    {
        private readonly IDictionary<string, Frankenpage> _pages = new Dictionary<string, Frankenpage>();

        public Task StoreAsync(IEnumerable<Frankenpage> pages)
        {
            _pages.AddRange(pages, p => p.Slug);

            IndexAllPages();

            return Task.FromResult(0);
        }

        void IndexAllPages()
        {
            // TODO GetPageIndicesAsync
            // TODO GetAllCategoriesAsync
            // TODO GetIndexForCategory for each category slug

            foreach (var page in _pages)
            {
                page.Value.SetAllLinksToMe(new Lazy<IEnumerable<string>>(() => 
                    (from linkingPage in _pages
                    where linkingPage.Value.AllLinks.Contains(page.Key)
                    select linkingPage.Key).Distinct()));
            }

        }

        public Task<Frankenpage[]> GetAllPagesAsync()
        {
            return Task.FromResult(_pages.Values.ToArray());
        }

        public Task<Frankenpage> GetPageAsync(string slug)
        {
            var page = _pages.ContainsKey(slug) ? _pages[slug] : null;
            return Task.FromResult(page);
        }

        public async Task<Frankindex> GetIndexForCategory(string categorySlug)
        {
            var items =
                from page in _pages.Values
                where page.Categories.Any(x => x.Slug == categorySlug)
                select new FrankindexItem(page.Slug, page.Title);

            var categoryDescription = await GetPageAsync("category-" + categorySlug);

            var index = new Frankindex(
                items.ToArray(),
                categorySlug,
                categorySlug.Humanize(LetterCasing.Title),
                categoryDescription != null
                    ? categoryDescription.Html
                    : string.Empty);

            return index;
        }

        public Task<FrankenpageCategory[]> GetAllCategoriesAsync()
        {
            var categories = _pages.Values
                .SelectMany(x => x.Categories)
                .DistinctBy(x => x.Slug)
                .ToArray();

            return Task.FromResult(categories);
        }

        public Task<FrankindexItem[]> GetPageIndicesAsync()
        {
            var indices =
                from page in _pages
                select new FrankindexItem(page.Value.Slug, page.Value.Title);

            return Task.FromResult(indices.ToArray());
        }
    }
}