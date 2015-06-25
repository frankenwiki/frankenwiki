using Nancy;

namespace Frankenwiki.Host.Nancy.Features
{
    public class DefaultModule : NancyModule
    {
        public DefaultModule()
        {
            Get["/"] = _ => Response.AsRedirect("/wiki");
        }
    }
}