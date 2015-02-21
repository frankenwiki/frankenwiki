using Autofac;
using ConfigInjector.Configuration;

namespace Frankenwiki.Example.NancyWeb.Plumbing
{
    public static class IoC
    {
        public static IContainer BoomShakalaka()
        {
            var builder = new ContainerBuilder();

            var assemblies = new[]
            {
                typeof (WebAutofacModule).Assembly,
            };

            ConfigurationConfigurator
                .RegisterConfigurationSettings()
                .FromAssemblies(assemblies)
                .RegisterWithContainer(s => builder.RegisterInstance(s).AsSelf().SingleInstance())
                .AllowConfigurationEntriesThatDoNotHaveSettingsClasses(true)
                .DoYourThing();

            builder.RegisterAssemblyModules(assemblies);

            return builder.Build();
        }
    }
}