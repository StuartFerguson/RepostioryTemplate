namespace EstateReporting.BusinessLogic.Tests
{
    using System.Threading;
    using EstateManagement.Estate.DomainEvents;
    using Moq;
    using Repository;
    using Shared.Logger;
    using Shouldly;
    using Testing;
    using Xunit;

    public class EstateDomainEventHandlerTests
    {
        #region Methods

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
            EstateCreatedEvent estateCreatedEvent = TestData.EstateCreatedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            EstateDomainEventHandler eventHandler = new EstateDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(estateCreatedEvent, CancellationToken.None); });
        }

        [Fact]
        public void EstateDomainEventHandler_EstateReferenceAllocatedEvent_EventIsHandled()
        {
            EstateReferenceAllocatedEvent estateReferenceAllocatedEvent = TestData.EstateReferenceAllocatedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            EstateDomainEventHandler eventHandler = new EstateDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(estateReferenceAllocatedEvent, CancellationToken.None); });
        }

        [Fact]
        public void EstateDomainEventHandler_OperatorAddedToEstateEvent_EventIsHandled()
        {
            OperatorAddedToEstateEvent operatorAddedToEstateEvent = TestData.OperatorAddedToEstateEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            EstateDomainEventHandler eventHandler = new EstateDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(operatorAddedToEstateEvent, CancellationToken.None); });
        }

        [Fact]
        public void EstateDomainEventHandler_SecurityUserAddedEvent_EventIsHandled()
        {
            SecurityUserAddedToEstateEvent securityUserAddedEvent = TestData.EstateSecurityUserAddedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            EstateDomainEventHandler eventHandler = new EstateDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(securityUserAddedEvent, CancellationToken.None); });
        }

        #endregion
    }
}