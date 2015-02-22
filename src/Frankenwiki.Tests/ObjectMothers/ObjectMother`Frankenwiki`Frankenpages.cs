using System.Collections.Generic;
using System.Linq;
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
                    private readonly IList<FrankenpageCategory> _categories = new List<FrankenpageCategory>();

                    public override Frankenpage Build()
                    {
                        return new Frankenpage(
                            Get(x => x.Slug, "slug"),
                            Get(x => x.Markdown, "markdown"),
                            Get(x => x.Html, "html"),
                            Get(x => x.Title, "title"),
                            _categories.ToArray());
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

                    public FrankenpageBuilder WithCategory(string slug, string name)
                    {
                        _categories.Add(new FrankenpageCategory(slug, name));
                        return this;
                    }

                    public FrankenpageBuilder WithHtml(string html)
                    {
                        Set(x => x.Html, html);

                        return this;
                    }
                }
            }
        }
    }
}