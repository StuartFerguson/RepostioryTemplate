namespace EstateReporting.BusinessLogic.Tests
{
    using System;
    using System.Threading;
    using EventHandling;
    using Events;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Moq;
    using Repository;
    using Shared.Logger;
    using Shouldly;
    using Testing;
    using TransactionProcessor.Settlement.DomainEvents;
    using TransactionProcessor.Transaction.DomainEvents;
    using Xunit;

    public class SettlementDomainEventHandlerTests
    {
        [Fact]
        public void SettlementDomainEventHandler_CanBeCreated_IsCreated()
        {
            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            SettlementDomainEventHandler eventHandler = new SettlementDomainEventHandler(estateReportingRepository.Object);

            eventHandler.ShouldNotBeNull();
        }

        [Fact]
        public void SettlementDomainEventHandler_SettlementCreatedForDateEvent_EventIsHandled()
        {
            SettlementCreatedForDateEvent domainEvent = TestData.SettlementCreatedForDateEvent;
            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            SettlementDomainEventHandler eventHandler = new SettlementDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(domainEvent, CancellationToken.None); });
        }

        [Fact]
        public void SettlementDomainEventHandler_MerchantFeeAddedPendingSettlementEvent_EventIsHandled()
        {
            MerchantFeeAddedPendingSettlementEvent domainEvent = TestData.MerchantFeeAddedPendingSettlementEvent;
            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            SettlementDomainEventHandler eventHandler = new SettlementDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(domainEvent, CancellationToken.None); });
        }

        [Fact]
        public void SettlementDomainEventHandler_MerchantFeeAddedToTransactionEvent_EventIsHandled()
        {
            MerchantFeeAddedToTransactionEvent domainEvent = TestData.MerchantFeeAddedToTransactionEvent;
            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            SettlementDomainEventHandler eventHandler = new SettlementDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(domainEvent, CancellationToken.None); });
        }

        [Fact]
        public void SettlementDomainEventHandler_MerchantFeeSettledEvent_EventIsHandled()
        {
            MerchantFeeSettledEvent domainEvent = TestData.MerchantFeeSettledEvent;
            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            SettlementDomainEventHandler eventHandler = new SettlementDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(domainEvent, CancellationToken.None); });
        }

        [Fact]
        public void SettlementDomainEventHandler_SettlementCompletedEvent_EventIsHandled()
        {
            SettlementCompletedEvent domainEvent = TestData.SettlementCompletedEvent;
            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            SettlementDomainEventHandler eventHandler = new SettlementDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(domainEvent, CancellationToken.None); });
        }
    }
}