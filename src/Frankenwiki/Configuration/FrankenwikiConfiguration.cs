namespace Frankenwiki.Configuration
{
    /// <summary>
    /// Register your instance of this with your IOC container
    /// </summary>
    public class FrankenwikiConfiguration
    {
        public FrankenwikiConfiguration WithWikiSourcePath(string wikiSourcePath)
        {
            WikiSourcePath = wikiSourcePath;
            return this;
        }

        public string WikiSourcePath { get; private set; }
    }
}