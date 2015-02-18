using Frakenwiki.Web;
using Frakenwiki.Web.Plumbing;
using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Nancy.Owin;
using Owin;

[assembly: OwinStartup(typeof (Startup))]

namespace Frakenwiki.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var container = IoC.BoomShakalaka();

            appBuilder.UseNancy(new NancyOptions
            {
                Bootstrapper = new Bootstrapper(container)
            });

            appBuilder.UseStageMarker(PipelineStage.MapHandler);
        }
    }
}