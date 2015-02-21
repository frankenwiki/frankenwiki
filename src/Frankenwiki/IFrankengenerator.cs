namespace Frankenwiki
{
    public interface IFrankengenerator
    {
        void GenerateFromSource(
            string sourcePath,
            IFrankenstore store);
    }
}