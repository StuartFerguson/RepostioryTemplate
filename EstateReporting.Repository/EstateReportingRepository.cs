namespace EstateReporting.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading;
    using System.Threading.Tasks;
    using BusinessLogic.Events;
    using Database;
    using Database.Entities;
    using EstateManagement.Contract.DomainEvents;
    using EstateManagement.Estate.DomainEvents;
    using EstateManagement.Merchant.DomainEvents;
    using FileProcessor.File.DomainEvents;
    using FileProcessor.FileImportLog.DomainEvents;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Newtonsoft.Json;
    using Shared.EntityFramework;
    using Shared.Exceptions;
    using Shared.Logger;
    using TransactionProcessor.Reconciliation.DomainEvents;
    using TransactionProcessor.Settlement.DomainEvents;
    using TransactionProcessor.Transaction.DomainEvents;
    using VoucherManagement.Voucher.DomainEvents;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="EstateReporting.Repository.IEstateReportingRepository" />
    public class EstateReportingRepository : IEstateReportingRepository
    {
        #region Fields

        /// <summary>
        /// The additional request fields
        /// </summary>
        private readonly List<String> AdditionalRequestFields = new List<String>
                                                                {
                                                                    "Amount",
                                                                    "CustomerAccountNumber"
                                                                };

        /// <summary>
        /// The additional response fields
        /// </summary>
        private readonly List<String> AdditionalResponseFields = new List<String>();

        /// <summary>
        /// The database context factory
        /// </summary>
        private readonly Shared.EntityFramework.IDbContextFactory<EstateReportingContext> DbContextFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EstateReportingRepository" /> class.
        /// </summary>
        /// <param name="dbContextFactory">The database context factory.</param>
        public EstateReportingRepository(Shared.EntityFramework.IDbContextFactory<EstateReportingContext> dbContextFactory)
        {
            this.DbContextFactory = dbContextFactory;
        }

        #endregion

        #region Methods

        public async Task CreateSettlement(SettlementCreatedForDateEvent domainEvent,
                                           CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            Settlement settlement = new Settlement
                                    {
                                        EstateId = estateId,
                                        IsCompleted = false,
                                        SettlementDate = domainEvent.SettlementDate.Date,
                                        SettlementId = domainEvent.SettlementId
                                    };

            await context.Settlements.AddAsync(settlement, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task AddPendingMerchantFeeToSettlement(MerchantFeeAddedPendingSettlementEvent domainEvent,
                                                            CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            MerchantSettlementFee merchantSettlementFee = new MerchantSettlementFee
                                                          {
                                                              SettlementId = domainEvent.SettlementId,
                                                              EstateId = domainEvent.EstateId,
                                                              CalculatedValue = domainEvent.CalculatedValue,
                                                              FeeCalculatedDateTime = domainEvent.FeeCalculatedDateTime,
                                                              FeeId = domainEvent.FeeId,
                                                              FeeValue = domainEvent.FeeValue,
                                                              IsSettled = false,
                                                              MerchantId = domainEvent.MerchantId,
                                                              TransactionId = domainEvent.TransactionId
                                                          };
            await context.MerchantSettlementFees.AddAsync(merchantSettlementFee, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task MarkMerchantFeeAsSettled(MerchantFeeSettledEvent domainEvent,
                                                   CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            var merchantFee = await context.MerchantSettlementFees.Where(m => m.EstateId == domainEvent.EstateId &&
                                                                        m.MerchantId == domainEvent.MerchantId && m.TransactionId == domainEvent.TransactionId &&
                                                                        m.SettlementId == domainEvent.SettlementId && m.FeeId == domainEvent.FeeId)
                                     .SingleOrDefaultAsync(cancellationToken);
            if (merchantFee == null)
            {
                throw new NotFoundException("Merchant Fee not found to update as settled");
            }

            merchantFee.IsSettled = true;
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task MarkSettlementAsCompleted(SettlementCompletedEvent domainEvent,
                                                    CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            var settlement = await context.Settlements.SingleOrDefaultAsync(s => s.SettlementId == domainEvent.SettlementId, cancellationToken);

            if (settlement == null)
            {
                throw new NotFoundException($"No settlement with Id {domainEvent.SettlementId} found to mark as completed");
            }

            settlement.IsCompleted = true;
            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds the contract.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task AddContract(ContractCreatedEvent domainEvent,
                                      CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            Contract contract = new Contract
                                {
                                    OperatorId = domainEvent.OperatorId,
                                    EstateId = domainEvent.EstateId,
                                    ContractId = domainEvent.ContractId,
                                    Description = domainEvent.Description
                                };

            await context.Contracts.AddAsync(contract, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds the contract product.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task AddContractProduct(VariableValueProductAddedToContractEvent domainEvent,
                                             CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            ContractProduct contractProduct = new ContractProduct
                                              {
                                                  EstateId = domainEvent.EstateId,
                                                  ContractId = domainEvent.ContractId,
                                                  ProductId = domainEvent.ProductId,
                                                  DisplayText = domainEvent.DisplayText,
                                                  ProductName = domainEvent.ProductName,
                                                  Value = null
                                              };

            await context.ContractProducts.AddAsync(contractProduct, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Disables the contract product transaction fee.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="NotFoundException">Transaction Fee with Id [{domainEvent.TransactionFeeId}] not found in the Read Model</exception>
        public async Task DisableContractProductTransactionFee(TransactionFeeForProductDisabledEvent domainEvent,
                                                               CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            ContractProductTransactionFee transactionFee = await context.ContractProductTransactionFees.SingleOrDefaultAsync(t => t.TransactionFeeId == domainEvent.TransactionFeeId);

            if (transactionFee == null)
            {
                throw new NotFoundException($"Transaction Fee with Id [{domainEvent.TransactionFeeId}] not found in the Read Model");
            }

            transactionFee.IsEnabled = false;

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds the contract product.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task AddContractProduct(FixedValueProductAddedToContractEvent domainEvent,
                                             CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            ContractProduct contractProduct = new ContractProduct
                                              {
                                                  EstateId = domainEvent.EstateId,
                                                  ContractId = domainEvent.ContractId,
                                                  ProductId = domainEvent.ProductId,
                                                  DisplayText = domainEvent.DisplayText,
                                                  ProductName = domainEvent.ProductName,
                                                  Value = domainEvent.Value
                                              };

            await context.ContractProducts.AddAsync(contractProduct, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds the contract product transaction fee.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task AddContractProductTransactionFee(TransactionFeeForProductAddedToContractEvent domainEvent,
                                                           CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            ContractProductTransactionFee contractProductTransactionFee = new ContractProductTransactionFee
                                                                          {
                                                                              EstateId = domainEvent.EstateId,
                                                                              ContractId = domainEvent.ContractId,
                                                                              ProductId = domainEvent.ProductId,
                                                                              Description = domainEvent.Description,
                                                                              Value = domainEvent.Value,
                                                                              TransactionFeeId = domainEvent.TransactionFeeId,
                                                                              CalculationType = domainEvent.CalculationType,
                                                                              IsEnabled = true,
                                                                              FeeType = domainEvent.FeeType
                                                                          };

            await context.ContractProductTransactionFees.AddAsync(contractProductTransactionFee, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds the estate.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task AddEstate(EstateCreatedEvent domainEvent,
                                    CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            // Add the estate to the read model
            Estate estate = new Estate
                            {
                                CreatedDateTime = domainEvent.EventTimestamp.DateTime,
                                EstateId = estateId,
                                Name = domainEvent.EstateName
                            };
            await context.Estates.AddAsync(estate, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds the estate operator.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task AddEstateOperator(OperatorAddedToEstateEvent domainEvent,
                                            CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            EstateOperator estateOperator = new EstateOperator
                                            {
                                                EstateId = domainEvent.EstateId,
                                                Name = domainEvent.Name,
                                                OperatorId = domainEvent.OperatorId,
                                                RequireCustomMerchantNumber = domainEvent.RequireCustomMerchantNumber,
                                                RequireCustomTerminalNumber = domainEvent.RequireCustomTerminalNumber
                                            };

            await context.EstateOperators.AddAsync(estateOperator, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds the estate security user.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task AddEstateSecurityUser(SecurityUserAddedToEstateEvent domainEvent,
                                                CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            EstateSecurityUser estateSecurityUser = new EstateSecurityUser
                                                    {
                                                        CreatedDateTime = domainEvent.EventTimestamp.DateTime,
                                                        EstateId = domainEvent.EstateId,
                                                        EmailAddress = domainEvent.EmailAddress,
                                                        SecurityUserId = domainEvent.SecurityUserId
                                                    };

            await context.EstateSecurityUsers.AddAsync(estateSecurityUser, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds the file line to file.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="NotFoundException">File with Id {domainEvent.FileId} not found for estate Id {estateId}</exception>
        public async Task AddFileLineToFile(FileLineAddedEvent domainEvent,
                                            CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            File file = await context.Files.SingleOrDefaultAsync(f => f.FileId == domainEvent.FileId);

            if (file == null)
            {
                throw new NotFoundException($"File with Id {domainEvent.FileId} not found for estate Id {estateId}");
            }

            FileLine fileLine = new FileLine
                                {
                                    EstateId = domainEvent.EstateId,
                                    FileId = domainEvent.FileId,
                                    LineNumber = domainEvent.LineNumber,
                                    FileLineData = domainEvent.FileLine,
                                    Status = "P"  // Pending
                                };

            await context.FileLines.AddAsync(fileLine, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds the file to import log.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="NotFoundException">Import log with Id {domainEvent.FileImportLogId} not found for estate Id {estateId}</exception>
        public async Task AddFileToImportLog(FileAddedToImportLogEvent domainEvent,
                                             CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            FileImportLog fileImportLog = await context.FileImportLogs.SingleOrDefaultAsync(f => f.FileImportLogId == domainEvent.FileImportLogId);

            if (fileImportLog == null)
            {
                throw new NotFoundException($"Import log with Id {domainEvent.FileImportLogId} not found for estate Id {estateId}");
            }

            FileImportLogFile fileImportLogFile = new FileImportLogFile
                                                  {
                                                      MerchantId = domainEvent.MerchantId,
                                                      EstateId = domainEvent.EstateId,
                                                      FileImportLogId = domainEvent.FileImportLogId,
                                                      FileId = domainEvent.FileId,
                                                      FilePath = domainEvent.FilePath,
                                                      FileProfileId = domainEvent.FileProfileId,
                                                      FileUploadedDateTime = domainEvent.FileUploadedDateTime,
                                                      OriginalFileName = domainEvent.OriginalFileName,
                                                      UserId = domainEvent.UserId
                                                  };

            await context.FileImportLogFiles.AddAsync(fileImportLogFile, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds the generated voucher.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task AddGeneratedVoucher(VoucherGeneratedEvent domainEvent,
                                              CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            Voucher voucher = new Voucher
                              {
                                  EstateId = domainEvent.EstateId,
                                  ExpiryDate = domainEvent.ExpiryDateTime,
                                  IsGenerated = true,
                                  IsIssued = false,
                                  OperatorIdentifier = domainEvent.OperatorIdentifier,
                                  Value = domainEvent.Value,
                                  VoucherCode = domainEvent.VoucherCode,
                                  VoucherId = domainEvent.VoucherId,
                                  TransactionId = domainEvent.TransactionId,
                                  GenerateDateTime = domainEvent.GeneratedDateTime
                              };

            await context.Vouchers.AddAsync(voucher, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds the merchant.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task AddMerchant(MerchantCreatedEvent domainEvent,
                                      CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            Merchant merchant = new Merchant
                                {
                                    EstateId = domainEvent.EstateId,
                                    MerchantId = domainEvent.MerchantId,
                                    Name = domainEvent.MerchantName,
                                    CreatedDateTime = domainEvent.DateCreated
            };

            await context.Merchants.AddAsync(merchant, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateMerchant(SettlementScheduleChangedEvent domainEvent,
                                         CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            var merchant = await context.Merchants.SingleOrDefaultAsync(m => m.EstateId == domainEvent.EstateId && m.MerchantId == domainEvent.MerchantId);

            if (merchant == null)
            {
                throw new NotFoundException($"Merchant not found with Id {domainEvent.MerchantId}");
            }

            merchant.SettlementSchedule = domainEvent.SettlementSchedule;
            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds the merchant address.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task AddMerchantAddress(AddressAddedEvent domainEvent,
                                             CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            MerchantAddress merchantAddress = new MerchantAddress
                                              {
                                                  EstateId = domainEvent.EstateId,
                                                  MerchantId = domainEvent.MerchantId,
                                                  CreatedDateTime = domainEvent.EventTimestamp.DateTime,
                                                  AddressId = domainEvent.AddressId,
                                                  AddressLine1 = domainEvent.AddressLine1,
                                                  AddressLine2 = domainEvent.AddressLine2,
                                                  AddressLine3 = domainEvent.AddressLine3,
                                                  AddressLine4 = domainEvent.AddressLine4,
                                                  Country = domainEvent.Country,
                                                  PostalCode = domainEvent.PostalCode,
                                                  Region = domainEvent.Region,
                                                  Town = domainEvent.Town
                                              };

            await context.MerchantAddresses.AddAsync(merchantAddress, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds the merchant contact.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task AddMerchantContact(ContactAddedEvent domainEvent,
                                             CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            MerchantContact merchantContact = new MerchantContact
                                              {
                                                  EstateId = domainEvent.EstateId,
                                                  MerchantId = domainEvent.MerchantId,
                                                  Name = domainEvent.ContactName,
                                                  ContactId = domainEvent.ContactId,
                                                  EmailAddress = domainEvent.ContactEmailAddress,
                                                  PhoneNumber = domainEvent.ContactPhoneNumber,
                                                  CreatedDateTime = domainEvent.EventTimestamp.DateTime
            };

            await context.MerchantContacts.AddAsync(merchantContact, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds the merchant device.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task AddMerchantDevice(DeviceAddedToMerchantEvent domainEvent,
                                            CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            MerchantDevice merchantDevice = new MerchantDevice
                                            {
                                                EstateId = domainEvent.EstateId,
                                                MerchantId = domainEvent.MerchantId,
                                                DeviceId = domainEvent.DeviceId,
                                                DeviceIdentifier = domainEvent.DeviceIdentifier,
                                                CreatedDateTime = domainEvent.EventTimestamp.DateTime
            };

            await context.MerchantDevices.AddAsync(merchantDevice, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds the merchant operator.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task AddMerchantOperator(OperatorAssignedToMerchantEvent domainEvent,
                                              CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            MerchantOperator merchantOperator = new MerchantOperator
                                                {
                                                    Name = domainEvent.Name,
                                                    EstateId = domainEvent.EstateId,
                                                    MerchantId = domainEvent.MerchantId,
                                                    MerchantNumber = domainEvent.MerchantNumber,
                                                    OperatorId = domainEvent.OperatorId,
                                                    TerminalNumber = domainEvent.TerminalNumber
                                                };

            await context.MerchantOperators.AddAsync(merchantOperator, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds the merchant security user.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task AddMerchantSecurityUser(SecurityUserAddedToMerchantEvent domainEvent,
                                                  CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            MerchantSecurityUser merchantSecurityUser = new MerchantSecurityUser
                                                        {
                                                            EstateId = domainEvent.EstateId,
                                                            MerchantId = domainEvent.MerchantId,
                                                            EmailAddress = domainEvent.EmailAddress,
                                                            SecurityUserId = domainEvent.SecurityUserId,
                                                            CreatedDateTime = domainEvent.EventTimestamp.DateTime
            };

            await context.MerchantSecurityUsers.AddAsync(merchantSecurityUser, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds the product details to transaction.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="NotFoundException">Transaction with Id [{domainEvent.TransactionId}] not found in the Read Model</exception>
        public async Task AddProductDetailsToTransaction(ProductDetailsAddedToTransactionEvent domainEvent,
                                                         CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            Transaction transaction =
                await context.Transactions.SingleOrDefaultAsync(t => t.TransactionId == domainEvent.TransactionId, cancellationToken:cancellationToken);

            if (transaction == null)
            {
                throw new NotFoundException($"Transaction with Id [{domainEvent.TransactionId}] not found in the Read Model");
            }

            transaction.ContractId = domainEvent.ContractId;
            transaction.ProductId = domainEvent.ProductId;

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Completes the reconciliation.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="NotFoundException">Reconciliation with Id [{domainEvent.TransactionId}] not found in the Read Model</exception>
        public async Task CompleteReconciliation(ReconciliationHasCompletedEvent domainEvent,
                                                 CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            Reconciliation reconciliation =
                await context.Reconciliations.SingleOrDefaultAsync(t => t.TransactionId == domainEvent.TransactionId, cancellationToken: cancellationToken);

            if (reconciliation == null)
            {
                throw new NotFoundException($"Reconciliation with Id [{domainEvent.TransactionId}] not found in the Read Model");
            }

            reconciliation.IsCompleted = true;

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds the fee details to transaction.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="NotFoundException">Transaction with Id [{domainEvent.TransactionId}] not found in the Read Model</exception>
        public async Task AddFeeDetailsToTransaction(MerchantFeeAddedToTransactionEnrichedEvent domainEvent,
                                                     CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            Transaction transaction =
                await context.Transactions.SingleOrDefaultAsync(t => t.TransactionId == domainEvent.TransactionId, cancellationToken: cancellationToken);

            if (transaction == null)
            {
                throw new NotFoundException($"Transaction with Id [{domainEvent.TransactionId}] not found in the Read Model");
            }

            TransactionFee transactionFee = new TransactionFee
                                            {
                                                FeeId = domainEvent.FeeId,
                                                CalculatedValue = domainEvent.CalculatedValue,
                                                CalculationType = domainEvent.FeeCalculationType,
                                                EventId = domainEvent.EventId,
                                                FeeType = 0,
                                                FeeValue = domainEvent.FeeValue,
                                                TransactionId = domainEvent.TransactionId
                                            };

            await context.TransactionFees.AddAsync(transactionFee, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds the fee details to transaction.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="NotFoundException">Transaction with Id [{domainEvent.TransactionId}] not found in the Read Model</exception>
        public async Task AddFeeDetailsToTransaction(ServiceProviderFeeAddedToTransactionEnrichedEvent domainEvent,
                                                     CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            Transaction transaction =
                await context.Transactions.SingleOrDefaultAsync(t => t.TransactionId == domainEvent.TransactionId, cancellationToken: cancellationToken);

            if (transaction == null)
            {
                throw new NotFoundException($"Transaction with Id [{domainEvent.TransactionId}] not found in the Read Model");
            }

            TransactionFee transactionFee = new TransactionFee
                                            {
                                                FeeId = domainEvent.FeeId,
                                                CalculatedValue = domainEvent.CalculatedValue,
                                                CalculationType = domainEvent.FeeCalculationType,
                                                EventId = domainEvent.EventId,
                                                FeeType = 1,
                                                FeeValue = domainEvent.FeeValue,
                                                TransactionId = domainEvent.TransactionId
                                            };

            await context.TransactionFees.AddAsync(transactionFee, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds the file.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task AddFile(FileCreatedEvent domainEvent,
                                  CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            File file = new File
                        {
                            MerchantId = domainEvent.MerchantId,
                            FileImportLogId = domainEvent.FileImportLogId,
                            EstateId = domainEvent.EstateId,
                            UserId = domainEvent.UserId,
                            FileId = domainEvent.FileId,
                            FileProfileId = domainEvent.FileProfileId,
                            FileLocation = domainEvent.FileLocation,
                            FileReceivedDateTime = domainEvent.FileReceivedDateTime,
                        };

            await context.Files.AddAsync(file, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds the file import log.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task AddFileImportLog(ImportLogCreatedEvent domainEvent,
                                           CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            FileImportLog fileImportLog = new FileImportLog
                                      {
                                          EstateId = domainEvent.EstateId,
                                          FileImportLogId = domainEvent.FileImportLogId,
                                          ImportLogDateTime = domainEvent.ImportLogDateTime
                                      };

            await context.FileImportLogs.AddAsync(fileImportLog, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Completes the transaction.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="NotFoundException">Transaction with Id [{domainEvent.TransactionId}] not found in the Read Model</exception>
        public async Task CompleteTransaction(TransactionHasBeenCompletedEvent domainEvent,
                                              CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            Transaction transaction =
                await context.Transactions.SingleOrDefaultAsync(t => t.TransactionId == domainEvent.TransactionId, cancellationToken:cancellationToken);

            if (transaction == null)
            {
                throw new NotFoundException($"Transaction with Id [{domainEvent.TransactionId}] not found in the Read Model");
            }

            transaction.IsCompleted = true;

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Creates the read model.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task CreateReadModel(EstateCreatedEvent domainEvent,
                                          CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            Logger.LogInformation($"About to run migrations on Read Model database for estate [{estateId}]");

            // Ensure the db is at the latest version
            await context.MigrateAsync(cancellationToken);

            Logger.LogInformation($"Read Model database for estate [{estateId}] migrated to latest version");
        }

        /// <summary>
        /// Records the transaction additional request data.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task RecordTransactionAdditionalRequestData(AdditionalRequestDataRecordedEvent domainEvent,
                                                                 CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            TransactionAdditionalRequestData additionalRequestData = new TransactionAdditionalRequestData
                                                                     {
                                                                         EstateId = domainEvent.EstateId,
                                                                         MerchantId = domainEvent.MerchantId,
                                                                         TransactionId = domainEvent.TransactionId
                                                                     };

            foreach (String additionalRequestField in this.AdditionalRequestFields)
            {
                Logger.LogInformation($"Field to look for [{additionalRequestField}]");
            }

            foreach (KeyValuePair<String, String> additionalRequestField in domainEvent.AdditionalTransactionRequestMetadata)
            {
                Logger.LogInformation($"Key: [{additionalRequestField.Key}] Value: [{additionalRequestField.Value}]");
            }

            foreach (String additionalRequestField in this.AdditionalRequestFields)
            {
                if (domainEvent.AdditionalTransactionRequestMetadata.Any(m => m.Key.ToLower() == additionalRequestField.ToLower()))
                {
                    Type dbTableType = additionalRequestData.GetType();
                    PropertyInfo propertyInfo = dbTableType.GetProperty(additionalRequestField);

                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(additionalRequestData,
                                              domainEvent.AdditionalTransactionRequestMetadata.Single(m => m.Key.ToLower() == additionalRequestField.ToLower()).Value);
                    }
                    else
                    {
                        Logger.LogInformation("propertyInfo == null");
                    }
                }
            }

            await context.TransactionsAdditionalRequestData.AddAsync(additionalRequestData, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Records the transaction additional response data.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task RecordTransactionAdditionalResponseData(AdditionalResponseDataRecordedEvent domainEvent,
                                                                  CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            TransactionAdditionalResponseData additionalResponseData = new TransactionAdditionalResponseData
                                                                       {
                                                                           EstateId = domainEvent.EstateId,
                                                                           MerchantId = domainEvent.MerchantId,
                                                                           TransactionId = domainEvent.TransactionId
                                                                       };

            foreach (String additionalResponseField in this.AdditionalResponseFields)
            {
                if (domainEvent.AdditionalTransactionResponseMetadata.Any(m => m.Key.ToLower() == additionalResponseField.ToLower()))
                {
                    Type dbTableType = additionalResponseData.GetType();
                    PropertyInfo propertyInfo = dbTableType.GetProperty(additionalResponseField);

                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(additionalResponseData, domainEvent.AdditionalTransactionResponseMetadata.Single(m => m.Key.ToLower() == additionalResponseField.ToLower()).Value);
                    }
                }
            }

            await context.TransactionsAdditionalResponseData.AddAsync(additionalResponseData, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Starts the reconciliation.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task StartReconciliation(ReconciliationHasStartedEvent domainEvent,
                                              CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            Reconciliation reconciliation = new Reconciliation
                                      {
                                          EstateId = domainEvent.EstateId,
                                          MerchantId = domainEvent.MerchantId,
                                          TransactionDate = domainEvent.TransactionDateTime.Date,
                                          TransactionDateTime = domainEvent.TransactionDateTime,
                                          TransactionTime = domainEvent.TransactionDateTime.TimeOfDay,
                                          TransactionId = domainEvent.TransactionId,
                                      };

            await context.Reconciliations.AddAsync(reconciliation, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Starts the transaction.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task StartTransaction(TransactionHasStartedEvent domainEvent,
                                           CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            Transaction transaction = new Transaction
                                      {
                                          EstateId = domainEvent.EstateId,
                                          MerchantId = domainEvent.MerchantId,
                                          TransactionDate = domainEvent.TransactionDateTime.Date,
                                          TransactionDateTime = domainEvent.TransactionDateTime,
                                          TransactionTime = domainEvent.TransactionDateTime.TimeOfDay,
                                          TransactionId = domainEvent.TransactionId,
                                          TransactionNumber = domainEvent.TransactionNumber,
                                          TransactionReference = domainEvent.TransactionReference,
                                          TransactionType = domainEvent.TransactionType,
                                          DeviceIdentifier = domainEvent.DeviceIdentifier
                                      };

            await context.Transactions.AddAsync(transaction, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Updates the file line.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task UpdateFileLine(FileLineProcessingSuccessfulEvent domainEvent,
                                         CancellationToken cancellationToken)
        {
            await this.UpdateFileLineStatus(domainEvent.EstateId, domainEvent.FileId, domainEvent.LineNumber, "S", cancellationToken);
        }

        /// <summary>
        /// Updates the file line status.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="lineNumber">The line number.</param>
        /// <param name="newStatus">The new status.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="NotFoundException">FileLine number {lineNumber} in File Id {fileId} not found for estate Id {estateId}</exception>
        private async Task UpdateFileLineStatus(Guid estateId,
                                                          Guid fileId,
                                                          Int32 lineNumber,
                                                          String newStatus,
                                                          CancellationToken cancellationToken)
        {
            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            var fileLine = await context.FileLines.SingleOrDefaultAsync(f => f.FileId == fileId && f.LineNumber == lineNumber);

            if (fileLine == null)
            {
                throw new NotFoundException($"FileLine number {lineNumber} in File Id {fileId} not found for estate Id {estateId}");
            }

            fileLine.Status = newStatus;

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Updates the file line.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task UpdateFileLine(FileLineProcessingFailedEvent domainEvent,
                                         CancellationToken cancellationToken)
        {
            await this.UpdateFileLineStatus(domainEvent.EstateId, domainEvent.FileId, domainEvent.LineNumber, "F", cancellationToken);
        }

        /// <summary>
        /// Updates the file line.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task UpdateFileLine(FileLineProcessingIgnoredEvent domainEvent,
                                         CancellationToken cancellationToken)
        {
            await this.UpdateFileLineStatus(domainEvent.EstateId, domainEvent.FileId, domainEvent.LineNumber, "I", cancellationToken);
        }

        /// <summary>
        /// Updates the reconciliation overall totals.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="NotFoundException">Reconciliation with Id [{domainEvent.TransactionId}] not found in the Read Model</exception>
        public async Task UpdateReconciliationOverallTotals(OverallTotalsRecordedEvent domainEvent,
                                                            CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            Reconciliation reconciliation =
                await context.Reconciliations.SingleOrDefaultAsync(t => t.TransactionId == domainEvent.TransactionId, cancellationToken: cancellationToken);

            if (reconciliation == null)
            {
                throw new NotFoundException($"Reconciliation with Id [{domainEvent.TransactionId}] not found in the Read Model");
            }

            reconciliation.TransactionCount = domainEvent.TransactionCount;
            reconciliation.TransactionValue = domainEvent.TransactionValue;

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Updates the reconciliation status.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="NotFoundException">Reconciliation with Id [{domainEvent.TransactionId}] not found in the Read Model</exception>
        public async Task UpdateReconciliationStatus(ReconciliationHasBeenLocallyAuthorisedEvent domainEvent,
                                                     CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            Reconciliation reconciliation =
                await context.Reconciliations.SingleOrDefaultAsync(t => t.TransactionId == domainEvent.TransactionId, cancellationToken: cancellationToken);

            if (reconciliation == null)
            {
                throw new NotFoundException($"Reconciliation with Id [{domainEvent.TransactionId}] not found in the Read Model");
            }

            reconciliation.IsAuthorised = true;
            reconciliation.ResponseCode = domainEvent.ResponseCode;
            reconciliation.ResponseMessage = domainEvent.ResponseMessage;

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Updates the reconciliation status.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="NotFoundException">Reconciliation with Id [{domainEvent.TransactionId}] not found in the Read Model</exception>
        public async Task UpdateReconciliationStatus(ReconciliationHasBeenLocallyDeclinedEvent domainEvent,
                                                     CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            Reconciliation reconciliation =
                await context.Reconciliations.SingleOrDefaultAsync(t => t.TransactionId == domainEvent.TransactionId, cancellationToken: cancellationToken);

            if (reconciliation == null)
            {
                throw new NotFoundException($"Reconciliation with Id [{domainEvent.TransactionId}] not found in the Read Model");
            }

            reconciliation.IsAuthorised = false;
            reconciliation.ResponseCode = domainEvent.ResponseCode;
            reconciliation.ResponseMessage = domainEvent.ResponseMessage;

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Updates the transaction authorisation.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="NotFoundException">Transaction with Id [{domainEvent.TransactionId}] not found in the Read Model</exception>
        public async Task UpdateTransactionAuthorisation(TransactionHasBeenLocallyAuthorisedEvent domainEvent,
                                                         CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            Transaction transaction =
                await context.Transactions.SingleOrDefaultAsync(t => t.TransactionId == domainEvent.TransactionId, cancellationToken:cancellationToken);

            if (transaction == null)
            {
                throw new NotFoundException($"Transaction with Id [{domainEvent.TransactionId}] not found in the Read Model");
            }

            transaction.IsAuthorised = true;
            transaction.ResponseCode = domainEvent.ResponseCode;
            transaction.AuthorisationCode = domainEvent.AuthorisationCode;
            transaction.ResponseMessage = domainEvent.ResponseMessage;

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Updates the transaction authorisation.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="NotFoundException">Transaction with Id [{domainEvent.TransactionId}] not found in the Read Model</exception>
        public async Task UpdateTransactionAuthorisation(TransactionHasBeenLocallyDeclinedEvent domainEvent,
                                                         CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            Transaction transaction =
                await context.Transactions.SingleOrDefaultAsync(t => t.TransactionId == domainEvent.TransactionId, cancellationToken:cancellationToken);

            if (transaction == null)
            {
                throw new NotFoundException($"Transaction with Id [{domainEvent.TransactionId}] not found in the Read Model");
            }

            transaction.IsAuthorised = false;
            transaction.ResponseCode = domainEvent.ResponseCode;
            transaction.ResponseMessage = domainEvent.ResponseMessage;

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Updates the transaction authorisation.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="NotFoundException">Transaction with Id [{domainEvent.TransactionId}] not found in the Read Model</exception>
        public async Task UpdateTransactionAuthorisation(TransactionAuthorisedByOperatorEvent domainEvent,
                                                         CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            Transaction transaction =
                await context.Transactions.SingleOrDefaultAsync(t => t.TransactionId == domainEvent.TransactionId, cancellationToken:cancellationToken);

            if (transaction == null)
            {
                throw new NotFoundException($"Transaction with Id [{domainEvent.TransactionId}] not found in the Read Model");
            }

            transaction.IsAuthorised = true;
            transaction.ResponseCode = domainEvent.ResponseCode;
            transaction.AuthorisationCode = domainEvent.AuthorisationCode;
            transaction.ResponseMessage = domainEvent.ResponseMessage;
            transaction.OperatorIdentifier = domainEvent.OperatorIdentifier;

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Updates the transaction authorisation.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="NotFoundException">Transaction with Id [{domainEvent.TransactionId}] not found in the Read Model</exception>
        public async Task UpdateTransactionAuthorisation(TransactionDeclinedByOperatorEvent domainEvent,
                                                         CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            Transaction transaction =
                await context.Transactions.SingleOrDefaultAsync(t => t.TransactionId == domainEvent.TransactionId, cancellationToken:cancellationToken);

            if (transaction == null)
            {
                throw new NotFoundException($"Transaction with Id [{domainEvent.TransactionId}] not found in the Read Model");
            }

            transaction.IsAuthorised = false;
            transaction.ResponseCode = domainEvent.ResponseCode;
            transaction.ResponseMessage = domainEvent.ResponseMessage;
            transaction.OperatorIdentifier = domainEvent.OperatorIdentifier;

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Updates the voucher issue details.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="NotFoundException">Voucher with Id [{domainEvent.VoucherId}] not found in the Read Model</exception>
        public async Task UpdateVoucherIssueDetails(VoucherIssuedEvent domainEvent,
                                                    CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            Voucher voucher = await context.Vouchers.SingleOrDefaultAsync(v => v.VoucherId == domainEvent.VoucherId);

            if (voucher == null)
            {
                throw new NotFoundException($"Voucher with Id [{domainEvent.VoucherId}] not found in the Read Model");
            }

            voucher.IsIssued = true;
            voucher.RecipientEmail = domainEvent.RecipientEmail;
            voucher.RecipientMobile = domainEvent.RecipientMobile;
            voucher.IssuedDateTime = domainEvent.IssuedDateTime;

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Updates the voucher redemption details.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="NotFoundException">Voucher with Id [{domainEvent.VoucherId}] not found in the Read Model</exception>
        public async Task UpdateVoucherRedemptionDetails(VoucherFullyRedeemedEvent domainEvent,
                                                         CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            Voucher voucher = await context.Vouchers.SingleOrDefaultAsync(v => v.VoucherId == domainEvent.VoucherId);

            if (voucher == null)
            {
                throw new NotFoundException($"Voucher with Id [{domainEvent.VoucherId}] not found in the Read Model");
            }

            voucher.IsRedeemed = true;
            voucher.RedeemedDateTime = domainEvent.RedeemedDateTime;

            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateFileAsComplete(FileProcessingCompletedEvent domainEvent,
                                               CancellationToken cancellationToken)
        {
            EstateReportingContext context = await this.DbContextFactory.GetContext(domainEvent.EstateId, cancellationToken);

            var file = await context.Files.SingleOrDefaultAsync(f => f.FileId == domainEvent.FileId);

            if (file == null)
            {
                throw new NotFoundException($"File Id {domainEvent.FileId} not found for estate Id {domainEvent.EstateId}");
            }

            file.IsCompleted = true;

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Inserts the merchant balance record.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task InsertMerchantBalanceRecord(MerchantBalanceChangedEvent domainEvent,
                                                      CancellationToken cancellationToken)
        {
            

            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            // Find the existing event
            MerchantBalanceHistory balanceRecord = await context.MerchantBalanceHistories.SingleOrDefaultAsync(m => m.EventId == domainEvent.EventId, cancellationToken);

            if (balanceRecord == null)
            {
                MerchantBalanceHistory merchantBalanceHistory = new MerchantBalanceHistory
                                                                {
                                                                    AvailableBalance = domainEvent.AvailableBalance,
                                                                    Balance = domainEvent.Balance,
                                                                    ChangeAmount = domainEvent.ChangeAmount,
                                                                    EstateId = domainEvent.EstateId,
                                                                    EventId = domainEvent.EventId,
                                                                    MerchantId = domainEvent.MerchantId,
                                                                    Reference = domainEvent.Reference,
                                                                    EntryDateTime = domainEvent.EventCreatedDateTime,
                                                                    TransactionId = domainEvent.AggregateId == domainEvent.MerchantId ? Guid.Empty : domainEvent.AggregateId
                                                                };

                await context.MerchantBalanceHistories.AddAsync(merchantBalanceHistory, cancellationToken);
            }
            else
            {
                balanceRecord.AvailableBalance = domainEvent.AvailableBalance;
                balanceRecord.Balance = domainEvent.Balance;
                balanceRecord.ChangeAmount = domainEvent.ChangeAmount;
                balanceRecord.Reference = domainEvent.Reference;
                balanceRecord.EntryDateTime = domainEvent.EventCreatedDateTime;
                balanceRecord.TransactionId = domainEvent.AggregateId == domainEvent.MerchantId ? Guid.Empty : domainEvent.AggregateId;
            }
            
            await context.SaveChangesAsync(cancellationToken);

        }

        

        #endregion
    }
}