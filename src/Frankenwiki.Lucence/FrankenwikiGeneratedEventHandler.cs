using System;
using Frankenwiki.Domain.EventHandlers;
using Frankenwiki.Events;

namespace Frankenwiki.Lucence
{
    public class FrankenwikiGeneratedEventHandler : IDomainEventHandler<FrankenwikiGeneratedEvent>
    {
        private readonly IFrankenstore _store;
        private readonly IFrankenLuceneIndexBuilder _indexBuilder;

        public FrankenwikiGeneratedEventHandler(IFrankenstore store,
                                                IFrankenLuceneIndexBuilder indexBuilder)
        {
            _store = store;
            _indexBuilder = indexBuilder;
        }

        public void Handle(FrankenwikiGeneratedEvent domainEvent)
        {
            var pages = _store.GetAllPagesAsync().Result;
            _indexBuilder.AddOrUpdateIndex(pages);
        }
    }
}
