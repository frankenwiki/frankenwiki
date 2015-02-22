using Frankenwiki.Tests.ObjectMothers.Plumbing;

namespace Frankenwiki.Tests.ObjectMothers
{
    public static partial class ObjectMother
    {
        public static partial class Frankenwiki
        {
            public static partial class Frankenpages
            {
                public static FrankenpageBuilder Default
                {
                    get { return new FrankenpageBuilder(); }
                }

                public class FrankenpageBuilder : BuilderFor<Frankenpage>
                {
                    public override Frankenpage Build()
                    {
                        return new Frankenpage(
                            Get(x => x.Slug, "slug"),
                            Get(x => x.Markdown, "markdown"),
                            Get(x => x.Html, "html"),
                            Get(x => x.Title, "title"));
                    }

                    public FrankenpageBuilder WithSlug(string slug)
                    {
                        Set(x => x.Slug, slug);
                        return this;
                    }

                    public FrankenpageBuilder WithTitle(string title)
                    {
                        Set(x => x.Title, title);
                        return this;
                    }
                }
            }
        }
    }
}