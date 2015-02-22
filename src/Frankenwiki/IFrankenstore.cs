using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frankenwiki
{
    public interface IFrankenstore
    {
        Task StoreAsync(IEnumerable<Frankenpage> pages);
        Task<Frankenpage[]> GetAllPagesAsync();
        Task<Frankenpage> GetPageAsync(string slug);
        Task<Frankindex> GetIndexForCategory(string categorySlug);
        Task<FrankenpageCategory[]> GetAllCategoriesAsync();
        Task<FrankindexItem[]> GetPageIndicesAsync();
    }
}