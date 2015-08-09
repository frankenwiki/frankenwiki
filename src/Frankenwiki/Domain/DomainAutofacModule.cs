using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Frankenwiki.Domain.EventHandlers;

namespace Frankenwiki.Domain
{
    public class DomainAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DomainEventBroker>()
                   .As<IDomainEventBroker>()
                   .InstancePerLifetimeScope();
        }
    }
}
