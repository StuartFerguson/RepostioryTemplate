using System;
using Xunit;

namespace EstateReporting.BusinessLogic.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using EstateManagement.Estate.DomainEvents;
    using Microsoft.EntityFrameworkCore.Internal;
    using Moq;
    using Repository;
    using Shared.EventStore.EventHandling;
    using Shouldly;
    using Testing;

    public class DomainEventHandlerResolverTests
    {
        [Fact]
        public void DomainEventHandlerResolver_CanBeCreated_IsCreated()
        {
            Dictionary<String, String[]> eventHandlerConfiguration = new Dictionary<String, String[]>();

            eventHandlerConfiguration.Add("TestEventType1", new String[] { "EstateReporting.BusinessLogic.EventHandling.EstateDomainEventHandler, EstateReporting.BusinessLogic" });

            Mock<IDomainEventHandler> domainEventHandler = new Mock<IDomainEventHandler>();
            Func<Type, IDomainEventHandler> createDomainEventHandlerFunc = (type) => { return domainEventHandler.Object; };
            DomainEventHandlerResolver resolver = new DomainEventHandlerResolver(eventHandlerConfiguration, createDomainEventHandlerFunc);

            resolver.ShouldNotBeNull();
        }

        [Fact]
        public void DomainEventHandlerResolver_CanBeCreated_InvalidEventHandlerType_ErrorThrown()
        {
            Dictionary<String, String[]> eventHandlerConfiguration = new Dictionary<String, String[]>();

            eventHandlerConfiguration.Add("TestEventType1", new String[] { "EstateReporting.BusinessLogic.EventHandling.NonExistantDomainEventHandler, EstateReporting.BusinessLogic" });

            Mock<IDomainEventHandler> domainEventHandler = new Mock<IDomainEventHandler>();
            Func<Type, IDomainEventHandler> createDomainEventHandlerFunc = (type) => { return domainEventHandler.Object; };
            
            Should.Throw<NotSupportedException>(() => new DomainEventHandlerResolver(eventHandlerConfiguration, createDomainEventHandlerFunc));
        }

        [Fact]
        public void DomainEventHandlerResolver_GetDomainEventHandlers_EstateCreatedEvent_EventHandlersReturned()
        {
            String handlerTypeName = "EstateReporting.BusinessLogic.EventHandling.EstateDomainEventHandler, EstateReporting.BusinessLogic";
            Dictionary<String, String[]> eventHandlerConfiguration = new Dictionary<String, String[]>();

            EstateCreatedEvent estateCreatedEvent = TestData.EstateCreatedEvent;

            eventHandlerConfiguration.Add(estateCreatedEvent.GetType().Name, new String[] { handlerTypeName });

            Mock<IDomainEventHandler> domainEventHandler = new Mock<IDomainEventHandler>();
            Func<Type, IDomainEventHandler> createDomainEventHandlerFunc = (type) => { return domainEventHandler.Object; };

            DomainEventHandlerResolver resolver = new DomainEventHandlerResolver(eventHandlerConfiguration, createDomainEventHandlerFunc);

            List<IDomainEventHandler> handlers = resolver.GetDomainEventHandlers(estateCreatedEvent);

            handlers.ShouldNotBeNull();
            handlers.Any().ShouldBeTrue();
            handlers.Count.ShouldBe(1);
        }

        [Fact]
        public void DomainEventHandlerResolver_GetDomainEventHandlers_EstateCreatedEvent_EventNotConfigured_EventHandlersReturned()
        {
            String handlerTypeName = "EstateReporting.BusinessLogic.EventHandling.EstateDomainEventHandler, EstateReporting.BusinessLogic";
            Dictionary<String, String[]> eventHandlerConfiguration = new Dictionary<String, String[]>();

            EstateCreatedEvent estateCreatedEvent = TestData.EstateCreatedEvent;

            eventHandlerConfiguration.Add("RandomEvent", new String[] { handlerTypeName });
            Mock<IDomainEventHandler> domainEventHandler = new Mock<IDomainEventHandler>();
            Func<Type, IDomainEventHandler> createDomainEventHandlerFunc = (type) => { return domainEventHandler.Object; };

            DomainEventHandlerResolver resolver = new DomainEventHandlerResolver(eventHandlerConfiguration, createDomainEventHandlerFunc);

            List<IDomainEventHandler> handlers = resolver.GetDomainEventHandlers(estateCreatedEvent);

            handlers.ShouldBeNull();
        }

        [Fact]
        public void DomainEventHandlerResolver_GetDomainEventHandlers_EstateCreatedEvent_NoHandlersConfigured_EventHandlersReturned()
        {
            Dictionary<String, String[]> eventHandlerConfiguration = new Dictionary<String, String[]>();

            EstateCreatedEvent estateCreatedEvent = TestData.EstateCreatedEvent;
            Mock<IDomainEventHandler> domainEventHandler = new Mock<IDomainEventHandler>();

            Func<Type, IDomainEventHandler> createDomainEventHandlerFunc = (type) => { return domainEventHandler.Object;};

            DomainEventHandlerResolver resolver = new DomainEventHandlerResolver(eventHandlerConfiguration, createDomainEventHandlerFunc);

            List<IDomainEventHandler> handlers = resolver.GetDomainEventHandlers(estateCreatedEvent);

            handlers.ShouldBeNull();
        }

    }
}
