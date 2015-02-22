using System;
using System.IO;
using System.Threading.Tasks;
using Frankenwiki.Example.NancyWeb.Plumbing;
using Nancy;

namespace Frankenwiki.Example.NancyWeb.Features
{
    public class PageModule : NancyModule
    {
        private readonly IRootPathProvider _rootPathProvider;
        private readonly WikiSourcePathSetting _wikiSourcePath;

        public PageModule(
            IFrankenstore store,
            IRootPathProvider rootPathProvider, WikiSourcePathSetting wikiSourcePath)
        {
            _rootPathProvider = rootPathProvider;
            _wikiSourcePath = wikiSourcePath;

            var strategies = new Func<string, Task<dynamic>>[]
            {
                slug => GetStaticResourceForSlug(slug),
                slug => GetFrankenpageResponseForSlug(store, slug),
            };

            Get["/wiki", true] = async (o, token) => await GetFrankenpageResponseForSlug(store, "index");
            Get["/wiki/{slug*}", true] = async (o, token) =>
            {
                var slug = (string) o.slug;
                // TODO HACK this is to allow routes like /wiki/cat.jpg/ which without the
                // trailing slash will fail because of a NancyFx bug in 1.1.0:
                // https://github.com/NancyFx/Nancy/issues/1829
                slug = slug.TrimEnd('/');

                foreach (var strategy in strategies)
                {
                    var result = await strategy(slug);
                    if (result != null)
                    {
                        return result;
                    }
                }

                return (dynamic) new NotFoundResponse();
            };
            Get["/echo/{slug*}"] = _ => _.slug;
            Get["/echo2/{slug}/hi"] = _ => _.slug;
        }

        // note the async is required otherwise messing with Task.FromResult<dynamic> is just painful
        private async Task<dynamic> GetStaticResourceForSlug(string slug)
        {
            var path = Path.Combine(_rootPathProvider.GetRootPath(), _wikiSourcePath, slug);

            return File.Exists(path) ? Response.AsFile(path) : null;
        }

        private async Task<dynamic> GetFrankenpageResponseForSlug(IFrankenstore store, string slug)
        {
            var page = await store.GetPageAsync(slug);

            return page == null
                ? null
                : View["page-template", page];
        }
    }
}