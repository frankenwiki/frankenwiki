using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Frankenwiki.Domain.EventHandlers
{
    public interface IDomainEventBroker
    {
        void Raised<T>(T DomainEvent) where T : IDomainEvent;
    }

    public class DomainEventBroker : IDomainEventBroker
    {
        private readonly ILifetimeScope _scope;

        public DomainEventBroker(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public void Raised<T>(T domainEvent) where T : IDomainEvent
        {
            using (var childScope = _scope.BeginLifetimeScope())
            {
                var handlers = childScope.Resolve<IEnumerable<IDomainEventHandler<T>>>();
                foreach (var handler in handlers)
                    handler.Handle(domainEvent);
            }
        }
    }
}
