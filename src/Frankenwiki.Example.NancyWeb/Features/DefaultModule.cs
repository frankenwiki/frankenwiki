using Nancy;

namespace Frankenwiki.Example.NancyWeb.Features
{
    public class DefaultModule : NancyModule
    {
        public DefaultModule()
        {
            Get["/"] = _ => Response.AsRedirect("/wiki");
        }
    }
}