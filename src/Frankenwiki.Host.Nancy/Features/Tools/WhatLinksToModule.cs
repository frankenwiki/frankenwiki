using Nancy;

namespace Frankenwiki.Host.Nancy.Features.Tools
{
    public class WhatLinksToModule : NancyModule
    {
        public WhatLinksToModule(IFrankenstore store)
        {
            Get["/what-links-to/{slug*}", true] = async (o, token) =>
            {
                var slug = (string) o.slug;
                var page = await store.GetPageAsync(slug);

                return View["tools-what-links-here", page];
            };
        }
    }
}