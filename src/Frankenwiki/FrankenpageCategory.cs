namespace Frankenwiki
{
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