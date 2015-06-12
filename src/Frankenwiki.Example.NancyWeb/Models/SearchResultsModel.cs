using System.Collections.Generic;

namespace Frankenwiki.Example.NancyWeb.Models
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

    public class SearchResultModel
    {
        public SearchResultModel(FrankensearchResult result)
        {
            Title = result.Title;
            PageSlug = result.PageSlug;
        }

        public string PageSlug { get; private set; }
        public string Title { get; private set; }
    }
}