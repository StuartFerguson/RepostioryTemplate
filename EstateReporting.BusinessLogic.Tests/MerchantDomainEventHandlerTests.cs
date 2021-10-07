namespace EstateReporting.BusinessLogic.Tests
{
    using System.Threading;
    using EstateManagement.Merchant.DomainEvents;
    using Events;
    using Moq;
    using Repository;
    using Shared.Logger;
    using Shouldly;
    using Testing;
    using Xunit;

    public class MerchantDomainEventHandlerTests
    {
        #region Methods

        [Fact]
        public void MerchantDomainEventHandler_AddressAddedEvent_EventIsHandled()
        {
            AddressAddedEvent addressAddedEvent = TestData.AddressAddedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            MerchantDomainEventHandler eventHandler = new MerchantDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(addressAddedEvent, CancellationToken.None); });
        }

        [Fact]
        public void MerchantDomainEventHandler_CanBeCreated_IsCreated()
        {
            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            MerchantDomainEventHandler eventHandler = new MerchantDomainEventHandler(estateReportingRepository.Object);

            eventHandler.ShouldNotBeNull();
        }

        [Fact]
        public void MerchantDomainEventHandler_ContactAddedEvent_EventIsHandled()
        {
            ContactAddedEvent contactAddedEvent = TestData.ContactAddedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            MerchantDomainEventHandler eventHandler = new MerchantDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(contactAddedEvent, CancellationToken.None); });
        }

        [Fact]
        public void MerchantDomainEventHandler_DeviceAddedToMerchantEvent_EventIsHandled()
        {
            DeviceAddedToMerchantEvent deviceAddedToMerchantEvent = TestData.DeviceAddedToMerchantEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            MerchantDomainEventHandler eventHandler = new MerchantDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(deviceAddedToMerchantEvent, CancellationToken.None); });
        }

        [Fact]
        public void MerchantDomainEventHandler_MerchantBalanceChangedEvent_EventIsHandled()
        {
            MerchantBalanceChangedEvent merchantBalanceChangedEvent = TestData.MerchantBalanceChangedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            MerchantDomainEventHandler eventHandler = new MerchantDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(merchantBalanceChangedEvent, CancellationToken.None); });
        }

        [Fact]
        public void MerchantDomainEventHandler_MerchantCreatedEvent_EventIsHandled()
        {
            MerchantCreatedEvent merchantCreatedEvent = TestData.MerchantCreatedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            MerchantDomainEventHandler eventHandler = new MerchantDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(merchantCreatedEvent, CancellationToken.None); });
        }

        [Fact]
        public void MerchantDomainEventHandler_OperatorAssignedToMerchantEvent_EventIsHandled()
        {
            OperatorAssignedToMerchantEvent operatorAssignedToMerchantEvent = TestData.OperatorAssignedToMerchantEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            MerchantDomainEventHandler eventHandler = new MerchantDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(operatorAssignedToMerchantEvent, CancellationToken.None); });
        }

        [Fact]
        public void MerchantDomainEventHandler_SecurityUserAddedEvent_EventIsHandled()
        {
            SecurityUserAddedToMerchantEvent merchantSecurityUserAddedEvent = TestData.MerchantSecurityUserAddedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            MerchantDomainEventHandler eventHandler = new MerchantDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(merchantSecurityUserAddedEvent, CancellationToken.None); });
        }

        [Fact]
        public void MerchantDomainEventHandler_SettlementScheduleChangedEvent_EventIsHandled()
        {
            SettlementScheduleChangedEvent settlementScheduleChangedEvent = TestData.SettlementScheduleChangedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            MerchantDomainEventHandler eventHandler = new MerchantDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(settlementScheduleChangedEvent, CancellationToken.None); });
        }

        #endregion
    }
}