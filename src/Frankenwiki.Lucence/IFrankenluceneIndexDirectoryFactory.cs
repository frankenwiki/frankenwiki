using Lucene.Net.Store;

namespace Frankenwiki.Lucence
{
    public interface IFrankenluceneIndexDirectoryFactory
    {
        Directory Get();
    }

    public class FrankenLuceneRAMIndexDirectoryFactory : IFrankenluceneIndexDirectoryFactory
    {
        private static RAMDirectory _indexDirectory = new RAMDirectory();

        public Directory Get()
        {
            return _indexDirectory;
        }
    }
}
