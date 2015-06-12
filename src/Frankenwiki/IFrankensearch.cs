using System.Threading.Tasks;

namespace Frankenwiki
{
    public interface IFrankensearch
    {
        Task<FrankensearchResult[]> SearchAsync(string phrase);
    }
}