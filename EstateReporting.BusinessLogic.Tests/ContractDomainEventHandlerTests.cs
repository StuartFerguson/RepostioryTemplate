namespace EstateReporting.BusinessLogic.Tests
{
    using System.Threading;
    using EstateManagement.Contract.DomainEvents;
    using EventHandling;
    using Moq;
    using Repository;
    using Shared.Logger;
    using Shouldly;
    using Testing;
    using Xunit;

    public class ContractDomainEventHandlerTests
    {
        #region Methods

        [Fact]
        public void ContractDomainEventHandler_CanBeCreated_IsCreated()
        {
            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            ContractDomainEventHandler eventHandler = new ContractDomainEventHandler(estateReportingRepository.Object);

            eventHandler.ShouldNotBeNull();
        }

        [Fact]
        public void ContractDomainEventHandler_ContractCreatedEvent_EventIsHandled()
        {
            ContractCreatedEvent contractCreatedEvent = TestData.ContractCreatedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            ContractDomainEventHandler eventHandler = new ContractDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(contractCreatedEvent, CancellationToken.None); });
        }

        [Fact]
        public void ContractDomainEventHandler_FixedValueProductAddedToContractEvent_EventIsHandled()
        {
            FixedValueProductAddedToContractEvent fixedValueProductAddedToContractEvent = TestData.FixedValueProductAddedToContractEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            ContractDomainEventHandler eventHandler = new ContractDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(fixedValueProductAddedToContractEvent, CancellationToken.None); });
        }

        [Fact]
        public void ContractDomainEventHandler_TransactionFeeForProductAddedToContractEvent_EventIsHandled()
        {
            TransactionFeeForProductAddedToContractEvent transactionFeeForProductAddedToContractEvent = TestData.TransactionFeeForProductAddedToContractEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            ContractDomainEventHandler eventHandler = new ContractDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(transactionFeeForProductAddedToContractEvent, CancellationToken.None); });
        }

        [Fact]
        public void ContractDomainEventHandler_TransactionFeeForProductDisabledEvent_EventIsHandled()
        {
            TransactionFeeForProductDisabledEvent transactionFeeForProductDisabledEvent = TestData.TransactionFeeForProductDisabledEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            ContractDomainEventHandler eventHandler = new ContractDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(transactionFeeForProductDisabledEvent, CancellationToken.None); });
        }

        [Fact]
        public void ContractDomainEventHandler_VariableValueProductAddedToContractEvent_EventIsHandled()
        {
            VariableValueProductAddedToContractEvent variableValueProductAddedToContractEvent = TestData.VariableValueProductAddedToContractEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            ContractDomainEventHandler eventHandler = new ContractDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () => { await eventHandler.Handle(variableValueProductAddedToContractEvent, CancellationToken.None); });
        }

        #endregion
    }
}