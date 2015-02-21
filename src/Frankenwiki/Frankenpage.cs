namespace Frankenwiki
{
    public class Frankenpage
    {
        public string Html { get; private set; }
        public string Markdown { get; private set; }
        public string Slug { get; private set; }
        public FrankenpageCategory[] Categories { get; private set; }
        public string Title { get; private set; }

        public Frankenpage(
            string slug,
            string markdown, 
            string html,
            string title)
        {
            Markdown = markdown;
            Html = html;
            Slug = slug;
            Categories = new[]
            {
                new FrankenpageCategory("twelve-tone-technique", "Twelve-tone technique"),
                new FrankenpageCategory("disambiguation-pages", "Disambiguation pages"),
            };
            Title = title;
        }
    }

    public class FrankenpageCategory
    {
        public string Slug { get; private set; }
        public string Name { get; private set; }

        public FrankenpageCategory(
            string slug,
            string name)
        {
            Slug = slug;
            Name = name;
        }
    }
}