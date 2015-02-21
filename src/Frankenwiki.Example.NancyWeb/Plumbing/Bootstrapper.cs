using System.Linq;
using Autofac;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;

namespace Frankenwiki.Example.NancyWeb.Plumbing
{
    public class Bootstrapper :AutofacNancyBootstrapper
    {
        private static readonly string[] NeverExpireExtensions = {".css", ".js"};

        public Bootstrapper(ILifetimeScope container) : base(container)
        {
        }

        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            //var site = container.Resolve<StaticSiteKeySetting>();
            var generator = container.Resolve<IFrankengenerator>();
            var store = container.Resolve<IFrankenstore>();
            generator.GenerateFromSource(@"C:\source\bendetat\frankenwiki\test-wiki", store);
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);

            var staticConvention = StaticContentConventionBuilder.AddDirectory("/", "ui");

            nancyConventions.StaticContentsConventions.Add((context, path) =>
            {
                var response = staticConvention(context, path);
                return response != null && NeverExpireExtensions.Any(context.Request.Path.EndsWith)
                    ? response.WithHeader("Expires", "Thu, 5 Apr 2063 20:00:00 GMT")
                        .WithHeader("Cache-Control", "public, max-age=31536000")
                    : response;
            });
        }
    }
}