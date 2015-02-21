using System;
using System.Threading.Tasks;
using Nancy;

namespace Frankenwiki.Example.NancyWeb.Features
{
    public class PageModule : NancyModule
    {
        public PageModule(IFrankenstore store)
        {
            Get["/wiki", true] = async (o, token) => await GetResponseForSlug(store, "index");
            Get["/wiki/{slug*}", true] = async (o, token) =>
            {
                var slug = (string) o.slug;
                return await GetResponseForSlug(store, slug);
            };
        }

        private async Task<dynamic> GetResponseForSlug(IFrankenstore store, string slug)
        {
            var page = await store.GetPageAsync(slug);

            return page == null
                ? (dynamic)new NotFoundResponse()
                : View["page-template", page];
        }
    }
}