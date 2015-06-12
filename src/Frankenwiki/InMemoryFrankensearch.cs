using System.Linq;
using System.Threading.Tasks;

namespace Frankenwiki
{
    public class InMemoryFrankensearch : IFrankensearch
    {
        private readonly IFrankenstore _store;

        public InMemoryFrankensearch(IFrankenstore store)
        {
            _store = store;
        }

        public async Task<FrankensearchResult[]> SearchAsync(string phrase)
        {
            var phraseInsensitive = phrase.ToLower();

            var results =
                from item in await _store.GetAllPagesAsync()
                where item.Title.ToLower().Contains(phraseInsensitive) || item.Markdown.ToLower().Contains(phraseInsensitive)
                select new FrankensearchResult(
                    item.Slug,
                    item.Title);

            return results.ToArray();
        }
    }
}