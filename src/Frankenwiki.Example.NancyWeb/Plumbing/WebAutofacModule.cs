using Autofac;
using ConfigInjector.Configuration;
using Frankenwiki.Configuration;
using Frankenwiki.Domain.EventHandlers;

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

            builder.Register(_ => FrankenwikiConfiguration.Create()
                .WithWikiSourcePath(_.Resolve<WikiSourcePathSetting>())
                .Build())
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

            builder
                .RegisterType<DomainEventBroker>()
                .As<IDomainEventBroker>()
                .SingleInstance();
        }
    }
}