namespace Frankenwiki.Configuration
{
    /// <summary>
    /// Register your instance of this with your IOC container
    /// </summary>
    public class FrankenwikiConfiguration
    {
        public static FrankenwikiConfigurationBuilder Create()
        {
            return new FrankenwikiConfigurationBuilder();
        }

        public string WikiSourcePath { get; private set; }

        internal FrankenwikiConfiguration(
            string wikiSourcePath)
        {
            WikiSourcePath = wikiSourcePath;
        }
    }
}