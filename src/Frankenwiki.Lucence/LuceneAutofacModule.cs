using Autofac;
using Frankenwiki.Domain.EventHandlers;
using Frankenwiki.Events;

namespace Frankenwiki.Lucence
{
    public class LuceneAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<FrankenwikiGeneratedEventHandler>()
                .As<IDomainEventHandler<FrankenwikiGeneratedEvent>>()
                .SingleInstance();
            builder
                .RegisterType<FrankenLuceneRAMIndexDirectoryFactory>()
                .As<IFrankenluceneIndexDirectoryFactory>()
                .SingleInstance();
            builder
                .RegisterType<FrankenLuceneIndexBuilder>()
                .As<IFrankenLuceneIndexBuilder>()
                .SingleInstance();
        }
    }
}