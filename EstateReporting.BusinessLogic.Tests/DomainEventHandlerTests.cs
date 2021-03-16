namespace EstateReporting.BusinessLogic.Tests
{
    using System;
    using System.Threading;
    using EstateManagement.Contract.DomainEvents;
    using EstateManagement.Estate.DomainEvents;
    using EstateManagement.Merchant.DomainEvents;
    using Events;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Moq;
    using Repository;
    using Shared.Logger;
    using Shouldly;
    using Testing;
    using TransactionProcessor.Reconciliation.DomainEvents;
    using TransactionProcessor.Transaction.DomainEvents;
    using VoucherManagement.Voucher.DomainEvents;
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
        public void MerchantDomainEventHandler_CanBeCreated_IsCreated()
        {
            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            MerchantDomainEventHandler eventHandler = new MerchantDomainEventHandler(estateReportingRepository.Object);

            eventHandler.ShouldNotBeNull();
        }

        [Fact]
        public void TransactionDomainEventHandler_CanBeCreated_IsCreated()
        {
            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            eventHandler.ShouldNotBeNull();
        }

        [Fact]
        public void EstateDomainEventHandler_EstateCreatedEvent_EventIsHandled()
        {
            EstateCreatedEvent estateCreatedEvent = TestData.EstateCreatedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            EstateDomainEventHandler eventHandler = new EstateDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                                {
                                    await eventHandler.Handle(estateCreatedEvent, CancellationToken.None);
                                });
        }

        [Fact]
        public void EstateDomainEventHandler_SecurityUserAddedEvent_EventIsHandled()
        {
            SecurityUserAddedToEstateEvent securityUserAddedEvent = TestData.EstateSecurityUserAddedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            EstateDomainEventHandler eventHandler = new EstateDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(securityUserAddedEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void EstateDomainEventHandler_OperatorAddedToEstateEvent_EventIsHandled()
        {
            OperatorAddedToEstateEvent operatorAddedToEstateEvent = TestData.OperatorAddedToEstateEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            EstateDomainEventHandler eventHandler = new EstateDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(operatorAddedToEstateEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void MerchantDomainEventHandler_MerchantCreatedEvent_EventIsHandled()
        {
            MerchantCreatedEvent merchantCreatedEvent = TestData.MerchantCreatedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            MerchantDomainEventHandler eventHandler = new MerchantDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(merchantCreatedEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void MerchantDomainEventHandler_AddressAddedEvent_EventIsHandled()
        {
            AddressAddedEvent addressAddedEvent = TestData.AddressAddedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            MerchantDomainEventHandler eventHandler = new MerchantDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(addressAddedEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void MerchantDomainEventHandler_ContactAddedEvent_EventIsHandled()
        {
            ContactAddedEvent contactAddedEvent = TestData.ContactAddedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            MerchantDomainEventHandler eventHandler = new MerchantDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(contactAddedEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void MerchantDomainEventHandler_DeviceAddedToMerchantEvent_EventIsHandled()
        {
            DeviceAddedToMerchantEvent deviceAddedToMerchantEvent = TestData.DeviceAddedToMerchantEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            MerchantDomainEventHandler eventHandler = new MerchantDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(deviceAddedToMerchantEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void MerchantDomainEventHandler_SecurityUserAddedEvent_EventIsHandled()
        {
            SecurityUserAddedToMerchantEvent merchantSecurityUserAddedEvent = TestData.MerchantSecurityUserAddedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            MerchantDomainEventHandler eventHandler = new MerchantDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(merchantSecurityUserAddedEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void MerchantDomainEventHandler_OperatorAssignedToMerchantEvent_EventIsHandled()
        {
            OperatorAssignedToMerchantEvent operatorAssignedToMerchantEvent = TestData.OperatorAssignedToMerchantEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            MerchantDomainEventHandler eventHandler = new MerchantDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(operatorAssignedToMerchantEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void MerchantDomainEventHandler_MerchantBalanceChangedEvent_EventIsHandled()
        {
            MerchantBalanceChangedEvent merchantBalanceChangedEvent = TestData.MerchantBalanceChangedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            MerchantDomainEventHandler eventHandler = new MerchantDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(merchantBalanceChangedEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void TransactionDomainEventHandler_TransactionHasStartedEvent_EventIsHandled()
        {
            TransactionHasStartedEvent transactionHasStartedEvent = TestData.TransactionHasStartedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(transactionHasStartedEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void TransactionDomainEventHandler_AdditionalRequestDataRecordedEvent_EventIsHandled()
        {
            AdditionalRequestDataRecordedEvent additionalRequestDataRecordedEvent = TestData.AdditionalRequestDataRecordedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(additionalRequestDataRecordedEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void TransactionDomainEventHandler_AdditionalResponseDataRecordedEvent_EventIsHandled()
        {
            AdditionalResponseDataRecordedEvent additionalResponseDataRecordedEvent = TestData.AdditionalResponseDataRecordedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(additionalResponseDataRecordedEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void TransactionDomainEventHandler_TransactionHasBeenLocallyAuthorisedEvent_EventIsHandled()
        {
            TransactionHasBeenLocallyAuthorisedEvent transactionHasBeenLocallyAuthorisedEvent = TestData.TransactionHasBeenLocallyAuthorisedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(transactionHasBeenLocallyAuthorisedEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void TransactionDomainEventHandler_TransactionHasBeenLocallyDeclinedEvent_EventIsHandled()
        {
            TransactionHasBeenLocallyDeclinedEvent transactionHasBeenLocallyDeclinedEvent = TestData.TransactionHasBeenLocallyDeclinedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(transactionHasBeenLocallyDeclinedEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void TransactionDomainEventHandler_TransactionAuthorisedByOperatorEvent_EventIsHandled()
        {
            TransactionAuthorisedByOperatorEvent transactionAuthorisedByOperatorEvent = TestData.TransactionAuthorisedByOperatorEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(transactionAuthorisedByOperatorEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void TransactionDomainEventHandler_TransactionDeclinedByOperatorEvent_EventIsHandled()
        {
            TransactionDeclinedByOperatorEvent transactionDeclinedByOperatorEvent = TestData.TransactionDeclinedByOperatorEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(transactionDeclinedByOperatorEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void TransactionDomainEventHandler_TransactionHasBeenCompletedEvent_EventIsHandled()
        {
            TransactionHasBeenCompletedEvent transactionHasBeenCompletedEvent = TestData.TransactionHasBeenCompletedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(transactionHasBeenCompletedEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void TransactionDomainEventHandler_ProductDetailsAddedToTransactionEvent_EventIsHandled()
        {
            ProductDetailsAddedToTransactionEvent productDetailsAddedToTransactionEvent = TestData.ProductDetailsAddedToTransactionEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(productDetailsAddedToTransactionEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void TransactionDomainEventHandler_MerchantFeeAddedToTransactionEvent_EventIsHandled()
        {
            MerchantFeeAddedToTransactionEvent merchantFeeAddedToTransactionEvent = TestData.MerchantFeeAddedToTransactionEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(merchantFeeAddedToTransactionEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void TransactionDomainEventHandler_ServiceProviderFeeAddedToTransactionEvent_EventIsHandled()
        {
            ServiceProviderFeeAddedToTransactionEvent serviceProviderFeeAddedToTransactionEvent = TestData.ServiceProviderFeeAddedToTransactionEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(serviceProviderFeeAddedToTransactionEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void TransactionDomainEventHandler_ReconciliationHasStartedEvent_EventIsHandled()
        {
            ReconciliationHasStartedEvent reconciliationHasStartedEvent = TestData.ReconciliationHasStartedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(reconciliationHasStartedEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void TransactionDomainEventHandler_OverallTotalsRecordedEvent_EventIsHandled()
        {
            OverallTotalsRecordedEvent overallTotalsRecordedEvent = TestData.OverallTotalsRecordedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(overallTotalsRecordedEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void TransactionDomainEventHandler_ReconciliationHasBeenLocallyAuthorisedEvent_EventIsHandled()
        {
            ReconciliationHasBeenLocallyAuthorisedEvent reconciliationHasBeenLocallyAuthorisedEvent = TestData.ReconciliationHasBeenLocallyAuthorisedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(reconciliationHasBeenLocallyAuthorisedEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void TransactionDomainEventHandler_ReconciliationHasBeenLocallyDeclinedEvent_EventIsHandled()
        {
            ReconciliationHasBeenLocallyDeclinedEvent reconciliationHasBeenLocallyDeclinedEvent = TestData.ReconciliationHasBeenLocallyDeclinedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(reconciliationHasBeenLocallyDeclinedEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void TransactionDomainEventHandler_ReconciliationHasCompletedEvent_EventIsHandled()
        {
            ReconciliationHasCompletedEvent reconciliationHasCompletedEvent = TestData.ReconciliationHasCompletedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(reconciliationHasCompletedEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void TransactionDomainEventHandler_VoucherGeneratedEvent_EventIsHandled()
        {
            VoucherGeneratedEvent voucherGeneratedEvent = TestData.VoucherGeneratedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(voucherGeneratedEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void TransactionDomainEventHandler_VoucherIssuedEvent_EventIsHandled()
        {
            VoucherIssuedEvent voucherIssuedEvent = TestData.VoucherIssuedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(voucherIssuedEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void TransactionDomainEventHandler_VoucherFullyRedeemedEvent_EventIsHandled()
        {
            VoucherFullyRedeemedEvent voucherFullyRedeemedEvent = TestData.VoucherFullyRedeemedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            TransactionDomainEventHandler eventHandler = new TransactionDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(voucherFullyRedeemedEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void ContractDomainEventHandler_ContractCreatedEvent_EventIsHandled()
        {
            ContractCreatedEvent contractCreatedEvent  = TestData.ContractCreatedEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            ContractDomainEventHandler eventHandler = new ContractDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(contractCreatedEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void ContractDomainEventHandler_FixedValueProductAddedToContractEvent_EventIsHandled()
        {
            FixedValueProductAddedToContractEvent fixedValueProductAddedToContractEvent = TestData.FixedValueProductAddedToContractEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            ContractDomainEventHandler eventHandler = new ContractDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(fixedValueProductAddedToContractEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void ContractDomainEventHandler_VariableValueProductAddedToContractEvent_EventIsHandled()
        {
            VariableValueProductAddedToContractEvent variableValueProductAddedToContractEvent = TestData.VariableValueProductAddedToContractEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            ContractDomainEventHandler eventHandler = new ContractDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(variableValueProductAddedToContractEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void ContractDomainEventHandler_TransactionFeeForProductAddedToContractEvent_EventIsHandled()
        {
            TransactionFeeForProductAddedToContractEvent transactionFeeForProductAddedToContractEvent = TestData.TransactionFeeForProductAddedToContractEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            ContractDomainEventHandler eventHandler = new ContractDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(transactionFeeForProductAddedToContractEvent, CancellationToken.None);
                            });
        }

        [Fact]
        public void ContractDomainEventHandler_TransactionFeeForProductDisabledEvent_EventIsHandled()
        {
            TransactionFeeForProductDisabledEvent transactionFeeForProductDisabledEvent = TestData.TransactionFeeForProductDisabledEvent;

            Mock<IEstateReportingRepository> estateReportingRepository = new Mock<IEstateReportingRepository>();

            ContractDomainEventHandler eventHandler = new ContractDomainEventHandler(estateReportingRepository.Object);

            Logger.Initialise(NullLogger.Instance);

            Should.NotThrow(async () =>
                            {
                                await eventHandler.Handle(transactionFeeForProductDisabledEvent, CancellationToken.None);
                            });
        }
    }
}