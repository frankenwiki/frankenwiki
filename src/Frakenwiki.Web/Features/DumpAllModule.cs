using Frankenwiki;
using Nancy;

namespace Frakenwiki.Web.Features
{
    public class DumpAllModule : NancyModule
    {
        public DumpAllModule(IFrankenstore store)
        {
            Get["/api/dump-all", true] = async (o, token) => Response.AsJson(await store.GetAllPagesAsync());
        }
    }
}