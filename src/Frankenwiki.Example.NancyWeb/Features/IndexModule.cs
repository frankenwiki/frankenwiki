using Nancy;

namespace Frankenwiki.Example.NancyWeb.Features
{
    public class IndexModule : NancyModule
    {
        public IndexModule(IFrankenstore store)
        {
            Get["/index", true] = async (o, token) =>
            {
                var indices = await store.GetPageIndicesAsync();

                return View["pages-index", indices];
            };
        }
    }
}