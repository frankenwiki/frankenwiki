using System;
using Frankenwiki.Domain.EventHandlers;
using Frankenwiki.Events;

namespace Frankenwiki.Lucence
{
    public class FrankenwikiGeneratedEventHandler : IDomainEventHandler<FrankenwikiGeneratedEvent>
    {
        private readonly IFrankenstore _store;

        public FrankenwikiGeneratedEventHandler(IFrankenstore store)
        {
            _store = store;
        }

        public void Handle(FrankenwikiGeneratedEvent domainEvent)
        {
            throw new NotImplementedException();
        }
    }
}
