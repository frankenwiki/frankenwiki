namespace Frankenwiki
{
    public class FrankindexItem
    {
        public FrankindexItem(string pageSlug, string title)
        {
            Title = title;
            PageSlug = pageSlug;
        }

        public string PageSlug { get; private set; }
        public string Title { get; private set; }
    }
}