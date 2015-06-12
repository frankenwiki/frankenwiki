namespace Frankenwiki
{
    public class FrankensearchResult
    {
        public FrankensearchResult(string pageSlug, string title)
        {
            Title = title;
            PageSlug = pageSlug;
        }

        public string PageSlug { get; private set; }
        public string Title { get; private set; }
    }
}