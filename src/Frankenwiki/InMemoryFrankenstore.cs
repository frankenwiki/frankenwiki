using System.Collections.Generic;
using System.Linq;
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

            return Task.FromResult(0);
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
    }
}