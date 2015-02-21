using Autofac;
using Frankenwiki;

namespace Frakenwiki.Web.Plumbing
{
    public class WebAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<Frankengenerator>()
                .As<IFrankengenerator>();
            builder
                .RegisterType<InMemoryFrankenstore>()
                .As<IFrankenstore>()
                .SingleInstance();
        }
    }
}