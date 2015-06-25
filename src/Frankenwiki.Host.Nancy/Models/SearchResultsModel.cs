using System.Collections.Generic;

namespace Frankenwiki.Host.Nancy.Models
{
    public class SearchResultsModel
    {
        public SearchResultsModel(IEnumerable<SearchResultModel> results, string phrase)
        {
            Phrase = phrase;
            Results = results;
        }

        public IEnumerable<SearchResultModel> Results { get; private set; }
        public string Phrase { get; private set; }
    }
}