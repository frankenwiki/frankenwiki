using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;

namespace Frankenwiki.Lucence
{
    public class FrankenLuceneSearcher : IFrankensearch
    {
        private Directory _indexDirectory;

        public FrankenLuceneSearcher(IFrankenluceneIndexDirectoryFactory _indexDirectoryFactory)
        {
            _indexDirectory = _indexDirectoryFactory.Get();
        }


        private static Query parseQuery(string searchQuery, QueryParser parser)
        {
            Query query;
            try
            {
                query = parser.Parse(searchQuery.Trim());
            }
            catch (ParseException)
            {
                query = parser.Parse(QueryParser.Escape(searchQuery.Trim()));
            }
            return query;
        }

        private FrankensearchResult MapLuceneDocumentToSearchResult(Document doc)
        {
            return new FrankensearchResult(doc.Get("slug"), doc.Get("title"));
        }

        private FrankensearchResult[] MapScoreDocsToSeachResults(IEnumerable<ScoreDoc> hits, IndexSearcher searcher)
        {
            return hits.Select(hit => MapLuceneDocumentToSearchResult(searcher.Doc(hit.Doc))).ToArray();
        }

        public Task<FrankensearchResult[]> SearchAsync(string phrase)
        {
            // set up lucene searcher
            using (var searcher = new IndexSearcher(_indexDirectory, false))
            {
                var hitsLimit = 1000;
                var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);

                var parser = new MultiFieldQueryParser
                 (Lucene.Net.Util.Version.LUCENE_30, new[] { "title", "markdown" }, analyzer);
                var query = parseQuery(phrase, parser);
                var scoredDocs = searcher.Search(query, null, hitsLimit, Sort.RELEVANCE).ScoreDocs;
                var results = MapScoreDocsToSeachResults(scoredDocs, searcher);
                analyzer.Close();
                searcher.Dispose();

                return Task.FromResult(results);
            }
        }
    }
}
