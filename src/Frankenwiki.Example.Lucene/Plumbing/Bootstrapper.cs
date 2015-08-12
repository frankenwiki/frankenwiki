using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Autofac;
using Nancy;
using Nancy.Bootstrapper;

namespace Frankenwiki.Example.Lucene.Plumbing
{
    public class Bootstrapper : AutofacNancyBootstrapper
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