namespace EstateReporting.Repository
{
    using System.Threading;
    using System.Threading.Tasks;
    using BusinessLogic.Events;
    using EstateManagement.Contract.DomainEvents;
    using EstateManagement.Estate.DomainEvents;
    using EstateManagement.Merchant.DomainEvents;
    using FileProcessor.File.DomainEvents;
    using FileProcessor.FileImportLog.DomainEvents;
    using TransactionProcessor.Reconciliation.DomainEvents;
    using TransactionProcessor.Settlement.DomainEvents;
    using TransactionProcessor.Transaction.DomainEvents;
    using VoucherManagement.Voucher.DomainEvents;

    /// <summary>
    /// 
    /// </summary>
    public interface IEstateReportingRepository
    {
        #region Methods

        Task CreateSettlement(SettlementCreatedForDateEvent domainEvent,
                            CancellationToken cancellationToken);

        Task AddPendingMerchantFeeToSettlement(MerchantFeeAddedPendingSettlementEvent domainEvent,
                                               CancellationToken cancellationToken);

        Task MarkMerchantFeeAsSettled(MerchantFeeSettledEvent domainEvent,
                                               CancellationToken cancellationToken);

        Task MarkSettlementAsCompleted(SettlementCompletedEvent domainEvent,
                                       CancellationToken cancellationToken);

        /// <summary>
        /// Adds the contract.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddContract(ContractCreatedEvent domainEvent,
                         CancellationToken cancellationToken);

        /// <summary>
        /// Adds the contract product.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddContractProduct(VariableValueProductAddedToContractEvent domainEvent,
                                CancellationToken cancellationToken);

        /// <summary>
        /// Adds the contract product.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddContractProduct(FixedValueProductAddedToContractEvent domainEvent,
                                CancellationToken cancellationToken);

        /// <summary>
        /// Adds the contract product transaction fee.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddContractProductTransactionFee(TransactionFeeForProductAddedToContractEvent domainEvent,
                                              CancellationToken cancellationToken);

        /// <summary>
        /// Adds the estate.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddEstate(EstateCreatedEvent domainEvent,
                       CancellationToken cancellationToken);

        /// <summary>
        /// Adds the estate operator.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddEstateOperator(OperatorAddedToEstateEvent domainEvent,
                               CancellationToken cancellationToken);

        Task UpdateEstate(EstateReferenceAllocatedEvent domainEvent, CancellationToken cancellationToken);

        Task UpdateMerchant(MerchantReferenceAllocatedEvent domainEvent,
                            CancellationToken cancellationToken);

        /// <summary>
        /// Adds the estate security user.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddEstateSecurityUser(SecurityUserAddedToEstateEvent domainEvent,
                                   CancellationToken cancellationToken);

        /// <summary>
        /// Adds the fee details to transaction.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddFeeDetailsToTransaction(MerchantFeeAddedToTransactionEnrichedEvent domainEvent,
                                        CancellationToken cancellationToken);

        /// <summary>
        /// Adds the fee details to transaction.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddFeeDetailsToTransaction(ServiceProviderFeeAddedToTransactionEnrichedEvent domainEvent,
                                        CancellationToken cancellationToken);

        /// <summary>
        /// Adds the file.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddFile(FileCreatedEvent domainEvent,
                     CancellationToken cancellationToken);

        /// <summary>
        /// Adds the file import log.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddFileImportLog(ImportLogCreatedEvent domainEvent,
                              CancellationToken cancellationToken);

        /// <summary>
        /// Adds the file line to file.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddFileLineToFile(FileLineAddedEvent domainEvent,
                               CancellationToken cancellationToken);

        /// <summary>
        /// Adds the file to import log.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddFileToImportLog(FileAddedToImportLogEvent domainEvent,
                                CancellationToken cancellationToken);

        /// <summary>
        /// Adds the generated voucher.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddGeneratedVoucher(VoucherGeneratedEvent domainEvent,
                                 CancellationToken cancellationToken);

        /// <summary>
        /// Adds the merchant.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddMerchant(MerchantCreatedEvent domainEvent,
                         CancellationToken cancellationToken);
        
        Task UpdateMerchant(SettlementScheduleChangedEvent domainEvent,
                         CancellationToken cancellationToken);

        /// <summary>
        /// Adds the merchant address.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddMerchantAddress(AddressAddedEvent domainEvent,
                                CancellationToken cancellationToken);

        /// <summary>
        /// Adds the merchant contact.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddMerchantContact(ContactAddedEvent domainEvent,
                                CancellationToken cancellationToken);

        /// <summary>
        /// Adds the merchant device.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddMerchantDevice(DeviceAddedToMerchantEvent domainEvent,
                               CancellationToken cancellationToken);

        /// <summary>
        /// Adds the merchant operator.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddMerchantOperator(OperatorAssignedToMerchantEvent domainEvent,
                                 CancellationToken cancellationToken);

        /// <summary>
        /// Adds the merchant security user.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddMerchantSecurityUser(SecurityUserAddedToMerchantEvent domainEvent,
                                     CancellationToken cancellationToken);

        /// <summary>
        /// Adds the product details to transaction.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddProductDetailsToTransaction(ProductDetailsAddedToTransactionEvent domainEvent,
                                            CancellationToken cancellationToken);

        /// <summary>
        /// Completes the reconciliation.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task CompleteReconciliation(ReconciliationHasCompletedEvent domainEvent,
                                    CancellationToken cancellationToken);

        /// <summary>
        /// Completes the transaction.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task CompleteTransaction(TransactionHasBeenCompletedEvent domainEvent,
                                 CancellationToken cancellationToken);

        /// <summary>
        /// Creates the read model.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task CreateReadModel(EstateCreatedEvent domainEvent,
                             CancellationToken cancellationToken);

        /// <summary>
        /// Disables the contract product transaction fee.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task DisableContractProductTransactionFee(TransactionFeeForProductDisabledEvent domainEvent,
                                                  CancellationToken cancellationToken);

        /// <summary>
        /// Inserts the merchant balance record.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task InsertMerchantBalanceRecord(MerchantBalanceChangedEvent domainEvent,
                                         CancellationToken cancellationToken);

        /// <summary>
        /// Records the transaction additional request data.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task RecordTransactionAdditionalRequestData(AdditionalRequestDataRecordedEvent domainEvent,
                                                    CancellationToken cancellationToken);

        /// <summary>
        /// Records the transaction additional response data.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task RecordTransactionAdditionalResponseData(AdditionalResponseDataRecordedEvent domainEvent,
                                                     CancellationToken cancellationToken);

        /// <summary>
        /// Starts the reconciliation.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task StartReconciliation(ReconciliationHasStartedEvent domainEvent,
                                 CancellationToken cancellationToken);

        /// <summary>
        /// Starts the transaction.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task StartTransaction(TransactionHasStartedEvent domainEvent,
                              CancellationToken cancellationToken);

        /// <summary>
        /// Updates the file line.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task UpdateFileLine(FileLineProcessingSuccessfulEvent domainEvent,
                            CancellationToken cancellationToken);

        /// <summary>
        /// Updates the file line.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task UpdateFileLine(FileLineProcessingFailedEvent domainEvent,
                            CancellationToken cancellationToken);

        /// <summary>
        /// Updates the file line.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task UpdateFileLine(FileLineProcessingIgnoredEvent domainEvent,
                            CancellationToken cancellationToken);

        /// <summary>
        /// Updates the reconciliation overall totals.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task UpdateReconciliationOverallTotals(OverallTotalsRecordedEvent domainEvent,
                                               CancellationToken cancellationToken);

        /// <summary>
        /// Updates the reconciliation status.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task UpdateReconciliationStatus(ReconciliationHasBeenLocallyAuthorisedEvent domainEvent,
                                        CancellationToken cancellationToken);

        /// <summary>
        /// Updates the reconciliation status.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task UpdateReconciliationStatus(ReconciliationHasBeenLocallyDeclinedEvent domainEvent,
                                        CancellationToken cancellationToken);

        /// <summary>
        /// Updates the transaction authorisation.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task UpdateTransactionAuthorisation(TransactionHasBeenLocallyAuthorisedEvent domainEvent,
                                            CancellationToken cancellationToken);

        /// <summary>
        /// Updates the transaction authorisation.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task UpdateTransactionAuthorisation(TransactionHasBeenLocallyDeclinedEvent domainEvent,
                                            CancellationToken cancellationToken);

        /// <summary>
        /// Updates the transaction authorisation.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task UpdateTransactionAuthorisation(TransactionAuthorisedByOperatorEvent domainEvent,
                                            CancellationToken cancellationToken);

        /// <summary>
        /// Updates the transaction authorisation.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task UpdateTransactionAuthorisation(TransactionDeclinedByOperatorEvent domainEvent,
                                            CancellationToken cancellationToken);

        /// <summary>
        /// Updates the voucher issue details.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task UpdateVoucherIssueDetails(VoucherIssuedEvent domainEvent,
                                       CancellationToken cancellationToken);

        /// <summary>
        /// Updates the voucher redemption details.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task UpdateVoucherRedemptionDetails(VoucherFullyRedeemedEvent domainEvent,
                                            CancellationToken cancellationToken);

        /// <summary>
        /// Updates the file as complete.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task UpdateFileAsComplete(FileProcessingCompletedEvent domainEvent,
                                  CancellationToken cancellationToken);

        #endregion
    }
}