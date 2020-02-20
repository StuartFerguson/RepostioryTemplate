namespace EstateReporting.BusinessLogic.Tests
{
    using System;
    using System.Threading;
    using EstateManagement.Estate.DomainEvents;
    using Shared.Logger;
    using Shouldly;
    using Xunit;

    public class EstateDomainEventHandlerTests
    {
        [Fact]
        public void EstateDomainEventHandler_CanBeCreated_IsCreated()
        {
            EstateDomainEventHandler eventHandler = new EstateDomainEventHandler();

            eventHandler.ShouldNotBeNull();
        }

        [Fact]
        public void EstateDomainEventHandler_EstateCreatedEvent_EventIsHandled()
        {
            EstateCreatedEvent estateCreatedEvent = EstateCreatedEvent.Create(Guid.NewGuid(), "TestEstate1");
            EstateDomainEventHandler eventHandler = new EstateDomainEventHandler();

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                                {
                                    await eventHandler.Handle(estateCreatedEvent, CancellationToken.None);
                                });
        }
    }
}