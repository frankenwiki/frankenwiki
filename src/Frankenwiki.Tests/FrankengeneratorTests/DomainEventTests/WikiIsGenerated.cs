using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frankenwiki.Domain.EventHandlers;
using Frankenwiki.Events;
using NSubstitute;
using TestStack.BDDfy;
using Xunit;

namespace Frankenwiki.Tests.FrankengeneratorTests.DomainEventTests
{
    public class WikiIsGenerated
    {
        private Frankengenerator _generator;
        private IFrankenstore _store;
        private IDomainEventBroker _domainEventBroker;

        public void GivenGenerator()
        {
            _domainEventBroker = Substitute.For<IDomainEventBroker>();
            _generator = new Frankengenerator(_domainEventBroker);
        }

        public void AndGivenAnInMemoryStore()
        {
            _store = new InMemoryFrankenstore();
        }

        public void WhenDocumentIsGenerated()
        {
            _generator.GenerateFromSource("test-wiki", _store);
        }

        public void ThenFrankenwikiGeneratedEventRaised()
        {
            _domainEventBroker.Received().Raised(Arg.Any<FrankenwikiGeneratedEvent>());
        }

        [Fact]
        public void Execute()
        {
            this.BDDfy();
        }

    }
}
