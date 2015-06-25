namespace Frankenwiki.Host.Nancy.Models
{
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