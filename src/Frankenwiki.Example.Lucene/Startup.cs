using Frankenwiki.Example.Lucene.Plumbing;
using Microsoft.Owin.Extensions;
using Nancy.Owin;
using Owin;

namespace Frankenwiki.Example.Lucene
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