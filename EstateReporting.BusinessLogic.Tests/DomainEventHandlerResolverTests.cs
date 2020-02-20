using System;
using Xunit;

namespace EstateReporting.BusinessLogic.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using EstateManagement.Estate.DomainEvents;
    using Microsoft.EntityFrameworkCore.Internal;
    using Shouldly;

    public class DomainEventHandlerResolverTests
    {
        [Fact]
        public void DomainEventHandlerResolver_CanBeCreated_IsCreated()
        {
            Dictionary<String, String[]> eventHandlerConfiguration = new Dictionary<String, String[]>();

            eventHandlerConfiguration.Add("TestEventType1", new String[] { "EstateReporting.BusinessLogic.EstateDomainEventHandler"});

            DomainEventHandlerResolver resolver = new DomainEventHandlerResolver(eventHandlerConfiguration);

            resolver.ShouldNotBeNull();
        }

        [Fact]
        public void DomainEventHandlerResolver_CanBeCreated_InvalidEventHandlerType_ErrorThrown()
        {
            Dictionary<String, String[]> eventHandlerConfiguration = new Dictionary<String, String[]>();

            eventHandlerConfiguration.Add("TestEventType1", new String[] { "EstateReporting.BusinessLogic.NonExistantDomainEventHandler" });

            Should.Throw<NotSupportedException>(() =>
                                                {
                                                    DomainEventHandlerResolver resolver = new DomainEventHandlerResolver(eventHandlerConfiguration);
                                                });
        }

        [Fact]
        public void DomainEventHandlerResolver_GetDomainEventHandlers_EstateCreatedEvent_EventHandlersReturned()
        {
            String handlerTypeName = "EstateReporting.BusinessLogic.EstateDomainEventHandler";
            Dictionary<String, String[]> eventHandlerConfiguration = new Dictionary<String, String[]>();

            EstateCreatedEvent estateCreatedEvent = EstateCreatedEvent.Create(Guid.NewGuid(), "TestEstate1");

            eventHandlerConfiguration.Add(estateCreatedEvent.GetType().FullName, new String[] { handlerTypeName });

            DomainEventHandlerResolver resolver = new DomainEventHandlerResolver(eventHandlerConfiguration);

            List<IDomainEventHandler> handlers = resolver.GetDomainEventHandlers(estateCreatedEvent);

            handlers.ShouldNotBeNull();
            EnumerableExtensions.Any(handlers).ShouldBeTrue();
            handlers.Count.ShouldBe(1);
            handlers.Single().GetType().FullName.ShouldBe(handlerTypeName);
        }

        [Fact]
        public void DomainEventHandlerResolver_GetDomainEventHandlers_EstateCreatedEvent_EventNotConfigured_EventHandlersReturned()
        {
            String handlerTypeName = "EstateReporting.BusinessLogic.EstateDomainEventHandler";
            Dictionary<String, String[]> eventHandlerConfiguration = new Dictionary<String, String[]>();

            EstateCreatedEvent estateCreatedEvent = EstateCreatedEvent.Create(Guid.NewGuid(), "TestEstate1");

            eventHandlerConfiguration.Add("RandomEvent", new String[] { handlerTypeName });

            DomainEventHandlerResolver resolver = new DomainEventHandlerResolver(eventHandlerConfiguration);

            List<IDomainEventHandler> handlers = resolver.GetDomainEventHandlers(estateCreatedEvent);

            handlers.ShouldBeNull();
        }

        [Fact]
        public void DomainEventHandlerResolver_GetDomainEventHandlers_EstateCreatedEvent_NoHandlersConfigured_EventHandlersReturned()
        {
            Dictionary<String, String[]> eventHandlerConfiguration = new Dictionary<String, String[]>();

            EstateCreatedEvent estateCreatedEvent = EstateCreatedEvent.Create(Guid.NewGuid(), "TestEstate1");

            DomainEventHandlerResolver resolver = new DomainEventHandlerResolver(eventHandlerConfiguration);

            List<IDomainEventHandler> handlers = resolver.GetDomainEventHandlers(estateCreatedEvent);

            handlers.ShouldBeNull();
        }

    }
}
