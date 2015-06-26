namespace Frankenwiki.Configuration
{
    /// <summary>
    /// Used to construct a FrankenwikiConfiguration instance. Use FrankenwikiConfiguration.Create() to get a builder.
    /// </summary>
    public class FrankenwikiConfigurationBuilder
    {
        private string _wikiSourcePath;

        internal FrankenwikiConfigurationBuilder()
        {
        }

        /// <summary>
        /// Set the source path of the wiki. This is used for resolving static resource URLs.
        /// </summary>
        /// <param name="wikiSourcePath"></param>
        /// <returns></returns>
        public FrankenwikiConfigurationBuilder WithWikiSourcePath(string wikiSourcePath)
        {
            _wikiSourcePath = wikiSourcePath;
            return this;
        }

        public FrankenwikiConfiguration Build()
        {
            return new FrankenwikiConfiguration(
                _wikiSourcePath);
        }
    }
}