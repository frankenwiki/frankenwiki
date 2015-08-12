using System.Collections.Generic;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;

namespace Frankenwiki.Lucence
{
    public interface IFrankenLuceneIndexBuilder
    {
        void AddOrUpdateIndex(IEnumerable<Frankenpage> frankenpages);
        void ClearIndex();
    }

    public class FrankenLuceneIndexBuilder : IFrankenLuceneIndexBuilder
    {
        private RAMDirectory _indexDirectory;

        public FrankenLuceneIndexBuilder()
        {
            _indexDirectory = new RAMDirectory();
        }

        private Document MapPageToLucenceDocument(Frankenpage frankenPage)
        {
            var doc = new Document();
            doc.Add(new Field("slug", frankenPage.Title, Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("title", frankenPage.Title, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("markdown", frankenPage.Markdown, Field.Store.YES, Field.Index.ANALYZED));
            return doc;
        }

        public void AddOrUpdateIndex(IEnumerable<Frankenpage> frankenpages)
        {
            var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            using (var writer = new IndexWriter(_indexDirectory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                foreach (var frankenPage in frankenpages)
                {
                    var doc = MapPageToLucenceDocument(frankenPage);
                    writer.AddDocument(doc);
                }

                analyzer.Close();
                writer.Optimize();
                writer.Flush(true, true, false);
                writer.Dispose();
            }
        }

        public void ClearIndex()
        {
            var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            using (var writer = new IndexWriter(_indexDirectory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                writer.DeleteAll();
                
                analyzer.Close();
                writer.Dispose();
            }
        }
    }
}
