namespace EstateReporting.BusinessLogic.Tests
{
    using System.Threading;
    using Events;
    using Moq;
    using Repository;
    using Shared.Logger;
    using Shouldly;
    using Testing;
    using TransactionProcessor.Reconciliation.DomainEvents;
    using TransactionProcessor.Transaction.DomainEvents;
    using VoucherManagement.Voucher.DomainEvents;
    using Xunit;

    public class TransactionDomainEventHandlerTests
    {
        #region Methods

        [Fact]
        public void TransactionDomainEventHandler_AdditionalRequestDataRecordedEvent_EventIsHandled()
        {
            AdditionalRequestDataRecordedEvent additionalRequestDataRecordedEvent = TestData.AdditionalRequestDataRecordedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(additionalRequestDataRecordedEvent, CancellationToken.None); });
        }

        [Fact]
        public void TransactionDomainEventHandler_AdditionalResponseDataRecordedEvent_EventIsHandled()
        {
            AdditionalResponseDataRecordedEvent additionalResponseDataRecordedEvent = TestData.AdditionalResponseDataRecordedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(additionalResponseDataRecordedEvent, CancellationToken.None); });
        }

        [Fact]
        public void TransactionDomainEventHandler_CanBeCreated_IsCreated()
        {
            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            eventHandler.ShouldNotBeNull();
        }

        [Fact]
        public void TransactionDomainEventHandler_MerchantFeeAddedToTransactionEvent_EventIsHandled()
        {
            MerchantFeeAddedToTransactionEnrichedEvent merchantFeeAddedToTransactionEvent = TestData.MerchantFeeAddedToTransactionEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(merchantFeeAddedToTransactionEvent, CancellationToken.None); });
        }

        [Fact]
        public void TransactionDomainEventHandler_OverallTotalsRecordedEvent_EventIsHandled()
        {
            OverallTotalsRecordedEvent overallTotalsRecordedEvent = TestData.OverallTotalsRecordedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(overallTotalsRecordedEvent, CancellationToken.None); });
        }

        [Fact]
        public void TransactionDomainEventHandler_ProductDetailsAddedToTransactionEvent_EventIsHandled()
        {
            ProductDetailsAddedToTransactionEvent productDetailsAddedToTransactionEvent = TestData.ProductDetailsAddedToTransactionEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(productDetailsAddedToTransactionEvent, CancellationToken.None); });
        }

        [Fact]
        public void TransactionDomainEventHandler_ReconciliationHasBeenLocallyAuthorisedEvent_EventIsHandled()
        {
            ReconciliationHasBeenLocallyAuthorisedEvent reconciliationHasBeenLocallyAuthorisedEvent = TestData.ReconciliationHasBeenLocallyAuthorisedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(reconciliationHasBeenLocallyAuthorisedEvent, CancellationToken.None); });
        }

        [Fact]
        public void TransactionDomainEventHandler_ReconciliationHasBeenLocallyDeclinedEvent_EventIsHandled()
        {
            ReconciliationHasBeenLocallyDeclinedEvent reconciliationHasBeenLocallyDeclinedEvent = TestData.ReconciliationHasBeenLocallyDeclinedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(reconciliationHasBeenLocallyDeclinedEvent, CancellationToken.None); });
        }

        [Fact]
        public void TransactionDomainEventHandler_ReconciliationHasCompletedEvent_EventIsHandled()
        {
            ReconciliationHasCompletedEvent reconciliationHasCompletedEvent = TestData.ReconciliationHasCompletedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(reconciliationHasCompletedEvent, CancellationToken.None); });
        }

        [Fact]
        public void TransactionDomainEventHandler_ReconciliationHasStartedEvent_EventIsHandled()
        {
            ReconciliationHasStartedEvent reconciliationHasStartedEvent = TestData.ReconciliationHasStartedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(reconciliationHasStartedEvent, CancellationToken.None); });
        }

        [Fact]
        public void TransactionDomainEventHandler_ServiceProviderFeeAddedToTransactionEvent_EventIsHandled()
        {
            ServiceProviderFeeAddedToTransactionEnrichedEvent serviceProviderFeeAddedToTransactionEvent = TestData.ServiceProviderFeeAddedToTransactionEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(serviceProviderFeeAddedToTransactionEvent, CancellationToken.None); });
        }

        [Fact]
        public void TransactionDomainEventHandler_TransactionAuthorisedByOperatorEvent_EventIsHandled()
        {
            TransactionAuthorisedByOperatorEvent transactionAuthorisedByOperatorEvent = TestData.TransactionAuthorisedByOperatorEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(transactionAuthorisedByOperatorEvent, CancellationToken.None); });
        }

        [Fact]
        public void TransactionDomainEventHandler_TransactionDeclinedByOperatorEvent_EventIsHandled()
        {
            TransactionDeclinedByOperatorEvent transactionDeclinedByOperatorEvent = TestData.TransactionDeclinedByOperatorEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(transactionDeclinedByOperatorEvent, CancellationToken.None); });
        }

        [Fact]
        public void TransactionDomainEventHandler_TransactionHasBeenCompletedEvent_EventIsHandled()
        {
            TransactionHasBeenCompletedEvent transactionHasBeenCompletedEvent = TestData.TransactionHasBeenCompletedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(transactionHasBeenCompletedEvent, CancellationToken.None); });
        }

        [Fact]
        public void TransactionDomainEventHandler_TransactionHasBeenLocallyAuthorisedEvent_EventIsHandled()
        {
            TransactionHasBeenLocallyAuthorisedEvent transactionHasBeenLocallyAuthorisedEvent = TestData.TransactionHasBeenLocallyAuthorisedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(transactionHasBeenLocallyAuthorisedEvent, CancellationToken.None); });
        }

        [Fact]
        public void TransactionDomainEventHandler_TransactionHasBeenLocallyDeclinedEvent_EventIsHandled()
        {
            TransactionHasBeenLocallyDeclinedEvent transactionHasBeenLocallyDeclinedEvent = TestData.TransactionHasBeenLocallyDeclinedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(transactionHasBeenLocallyDeclinedEvent, CancellationToken.None); });
        }

        [Fact]
        public void TransactionDomainEventHandler_TransactionHasStartedEvent_EventIsHandled()
        {
            TransactionHasStartedEvent transactionHasStartedEvent = TestData.TransactionHasStartedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(transactionHasStartedEvent, CancellationToken.None); });
        }

        [Fact]
        public void TransactionDomainEventHandler_VoucherFullyRedeemedEvent_EventIsHandled()
        {
            VoucherFullyRedeemedEvent voucherFullyRedeemedEvent = TestData.VoucherFullyRedeemedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(voucherFullyRedeemedEvent, CancellationToken.None); });
        }

        [Fact]
        public void TransactionDomainEventHandler_VoucherGeneratedEvent_EventIsHandled()
        {
            VoucherGeneratedEvent voucherGeneratedEvent = TestData.VoucherGeneratedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(voucherGeneratedEvent, CancellationToken.None); });
        }

        [Fact]
        public void TransactionDomainEventHandler_VoucherIssuedEvent_EventIsHandled()
        {
            VoucherIssuedEvent voucherIssuedEvent = TestData.VoucherIssuedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(voucherIssuedEvent, CancellationToken.None); });
        }

        #endregion
    }
}