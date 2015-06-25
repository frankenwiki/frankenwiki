using Autofac;
using ConfigInjector.Configuration;
using Frankenwiki.Configuration;

namespace Frankenwiki.Example.NancyWeb.Plumbing
{
    public class WebAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            ConfigurationConfigurator
                .RegisterConfigurationSettings()
                .FromAssemblies(typeof(WebAutofacModule).Assembly)
                .RegisterWithContainer(s => builder.RegisterInstance(s).AsSelf().SingleInstance())
                .AllowConfigurationEntriesThatDoNotHaveSettingsClasses(true)
                .DoYourThing();

            builder.Register(_ => new FrankenwikiConfiguration()
                .WithWikiSourcePath(_.Resolve<WikiSourcePathSetting>()))
                .AsSelf();

            builder
                .RegisterType<Frankengenerator>()
                .As<IFrankengenerator>();
            builder
                .RegisterType<InMemoryFrankenstore>()
                .As<IFrankenstore>()
                .SingleInstance();
            builder
                .RegisterType<InMemoryFrankensearch>()
                .As<IFrankensearch>()
                .SingleInstance();
        }
    }
}