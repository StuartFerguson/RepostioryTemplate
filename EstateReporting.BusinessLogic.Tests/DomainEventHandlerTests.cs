namespace EstateReporting.BusinessLogic.Tests
{
    using System;
    using System.Threading;
    using EstateManagement.Estate.DomainEvents;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Moq;
    using Repository;
    using Shared.Logger;
    using Shouldly;
    using Xunit;

    public class EstateDomainEventHandlerTests
    {
        [Fact]
        public void EstateDomainEventHandler_CanBeCreated_IsCreated()
        {
            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            EstateDomainEventHandler eventHandler = new EstateDomainEventHandler(estateReportingRepository.Object);

            eventHandler.ShouldNotBeNull();
        }

        [Fact]
        public void EstateDomainEventHandler_EstateCreatedEvent_EventIsHandled()
        {
            EstateCreatedEvent estateCreatedEvent = EstateCreatedEvent.Create(Guid.NewGuid(), "TestEstate1");

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            EstateDomainEventHandler eventHandler = new EstateDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                                {
                                    await eventHandler.Handle(estateCreatedEvent, CancellationToken.None);
                                });
        }
    }
}