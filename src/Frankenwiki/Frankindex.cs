namespace Frankenwiki
{
    public class Frankindex
    {
        public Frankindex(
            FrankindexItem[] items, 
            string slug, 
            string name, 
            string html)
        {
            Html = html;
            Name = name;
            Slug = slug;
            Items = items;
        }

        public FrankindexItem[] Items { get; private set; }
        public string Slug { get; private set; }
        public string Name { get; private set; }
        public string Html { get; private set; }
    }
}