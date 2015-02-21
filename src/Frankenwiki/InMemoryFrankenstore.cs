using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frankenwiki.Plumbing;

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
    }
}