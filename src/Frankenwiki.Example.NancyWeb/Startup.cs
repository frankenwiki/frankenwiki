using Frankenwiki.Example.NancyWeb;
using Frankenwiki.Example.NancyWeb.Plumbing;
using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Nancy.Owin;
using Owin;

[assembly: OwinStartup(typeof (Startup))]

namespace Frankenwiki.Example.NancyWeb
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