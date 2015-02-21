using Nancy;

namespace Frakenwiki.Web.Features
{
    public class DefaultModule : NancyModule
    {
        public DefaultModule()
        {
            Get["/"] = _ => Response.AsRedirect("/wiki");
        }
    }
}