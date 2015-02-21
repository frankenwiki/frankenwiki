namespace Frankenwiki
{
    public class Frankenpage
    {
        public string Html { get; private set; }
        public string Markdown { get; private set; }
        public string Slug { get; private set; }

        public Frankenpage(
            string markdown, 
            string html,
            string slug)
        {
            Markdown = markdown;
            Html = html;
            Slug = slug;
        }
    }
}