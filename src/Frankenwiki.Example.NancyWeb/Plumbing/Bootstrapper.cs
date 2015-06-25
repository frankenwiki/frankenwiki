using System.Configuration;
using System.IO;
using System.Linq;
using Autofac;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;

namespace Frankenwiki.Example.NancyWeb.Plumbing
{
    public class Bootstrapper :AutofacNancyBootstrapper
    {
        public Bootstrapper(ILifetimeScope container) : base(container)
        {
        }

        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            var generator = container.Resolve<IFrankengenerator>();
            var store = container.Resolve<IFrankenstore>();
            var rootPathProvider = container.Resolve<IRootPathProvider>();
            var wikiSourcePathSetting = container.Resolve<WikiSourcePathSetting>();
            var sourcePath = Path.Combine(rootPathProvider.GetRootPath(), wikiSourcePathSetting);

            generator.GenerateFromSource(sourcePath, store);
        }
    }
}