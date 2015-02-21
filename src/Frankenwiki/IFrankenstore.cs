using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frankenwiki
{
    public interface IFrankenstore
    {
        Task StoreAsync(IEnumerable<Frankenpage> pages);
        Task<Frankenpage[]> GetAllPagesAsync();
        Task<Frankenpage> GetPageAsync(string slug);
    }
}