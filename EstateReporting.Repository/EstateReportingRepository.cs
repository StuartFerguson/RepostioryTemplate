namespace EstateReporting.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Database;
    using Database.Entities;
    using EstateManagement.Contract.DomainEvents;
    using EstateManagement.Estate.DomainEvents;
    using EstateManagement.Merchant.DomainEvents;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Shared.EntityFramework;
    using Shared.Exceptions;
    using Shared.Logger;
    using TransactionProcessor.Transaction.DomainEvents;
    using EstateSecurityUserAddedEvent = EstateManagement.Estate.DomainEvents.SecurityUserAddedEvent;
    using MerchantSecurityUserAddedEvent = EstateManagement.Merchant.DomainEvents.SecurityUserAddedEvent;

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
        private readonly IDbContextFactory<EstateReportingContext> DbContextFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EstateReportingRepository" /> class.
        /// </summary>
        /// <param name="dbContextFactory">The database context factory.</param>
        public EstateReportingRepository(IDbContextFactory<EstateReportingContext> dbContextFactory)
        {
            this.DbContextFactory = dbContextFactory;
        }

        #endregion

        #region Methods

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
                                CreatedDateTime = domainEvent.EventCreatedDateTime,
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
        public async Task AddEstateSecurityUser(EstateSecurityUserAddedEvent domainEvent,
                                                CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            EstateSecurityUser estateSecurityUser = new EstateSecurityUser
                                                    {
                                                        CreatedDateTime = domainEvent.EventCreatedDateTime,
                                                        EstateId = domainEvent.EstateId,
                                                        EmailAddress = domainEvent.EmailAddress,
                                                        SecurityUserId = domainEvent.SecurityUserId
                                                    };

            await context.EstateSecurityUsers.AddAsync(estateSecurityUser, cancellationToken);

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
                                    CreatedDateTime = domainEvent.EventCreatedDateTime
                                };

            await context.Merchants.AddAsync(merchant, cancellationToken);

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
                                                  CreatedDateTime = domainEvent.EventCreatedDateTime,
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
                                                  MerchantId = domainEvent.AggregateId,
                                                  Name = domainEvent.ContactName,
                                                  ContactId = domainEvent.ContactId,
                                                  EmailAddress = domainEvent.ContactEmailAddress,
                                                  PhoneNumber = domainEvent.ContactPhoneNumber,
                                                  CreatedDateTime = domainEvent.EventCreatedDateTime
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
                                                CreatedDateTime = domainEvent.EventCreatedDateTime
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
        public async Task AddMerchantSecurityUser(MerchantSecurityUserAddedEvent domainEvent,
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
                                                            CreatedDateTime = domainEvent.EventCreatedDateTime
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
        /// Adds the fee details to transaction.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="NotFoundException">Transaction with Id [{domainEvent.TransactionId}] not found in the Read Model</exception>
        public async Task AddFeeDetailsToTransaction(MerchantFeeAddedToTransactionEvent domainEvent,
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
        public async Task AddFeeDetailsToTransaction(ServiceProviderFeeAddedToTransactionEvent domainEvent,
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
        /// Gets the transactions for estate by date.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<TransactionsByDayModel> GetTransactionsForEstateByDate(Guid estateId,
                                                                                 String startDate,
                                                                                 String endDate,
                                                                                 CancellationToken cancellationToken)
        {
            TransactionsByDayModel model = new TransactionsByDayModel
                                           {
                                               TransactionDayModels = new List<TransactionDayModel>()
                                           };

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.TransactionsView.Where(t => t.EstateId == estateId &&
                                                             t.TransactionDate >= queryStartDate.Date && t.TransactionDate <= queryEndDate.Date && t.IsAuthorised &&
                                                             t.TransactionType == "Sale").GroupBy(txn => txn.TransactionDate,
                                                                                                  (txndate,
                                                                                                   txns) => new
                                                                                                            {
                                                                                                                Key = txndate,
                                                                                                                NumberofTransactions = txns.Count(),
                                                                                                                ValueOfTransactions = txns.Sum(t => t.Amount)
                                                                                                            }).ToListAsync(cancellationToken);

            result.ForEach(r => model.TransactionDayModels.Add(new TransactionDayModel
                                                               {
                                                                   CurrencyCode = "",
                                                                   Date = r.Key,
                                                                   NumberOfTransactions = r.NumberofTransactions,
                                                                   ValueOfTransactions = r.ValueOfTransactions
                                                               }));

            return model;
        }

        /// <summary>
        /// Gets the transactions for merchant by date.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<TransactionsByDayModel> GetTransactionsForMerchantByDate(Guid estateId,
                                                                                   Guid merchantId,
                                                                                   String startDate,
                                                                                   String endDate,
                                                                                   CancellationToken cancellationToken)
        {
            TransactionsByDayModel model = new TransactionsByDayModel
            {
                TransactionDayModels = new List<TransactionDayModel>()
            };

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.TransactionsView.Where(t => t.EstateId == estateId &&
                                                                   t.MerchantId == merchantId &&
                                                             t.TransactionDate >= queryStartDate.Date && t.TransactionDate <= queryEndDate.Date && t.IsAuthorised &&
                                                             t.TransactionType == "Sale").GroupBy(txn => txn.TransactionDate,
                                                                                                  (txndate,
                                                                                                   txns) => new
                                                                                                   {
                                                                                                       Key = txndate,
                                                                                                       NumberofTransactions = txns.Count(),
                                                                                                       ValueOfTransactions = txns.Sum(t => t.Amount)
                                                                                                   }).ToListAsync(cancellationToken);

            result.ForEach(r => model.TransactionDayModels.Add(new TransactionDayModel
            {
                CurrencyCode = "",
                Date = r.Key,
                NumberOfTransactions = r.NumberofTransactions,
                ValueOfTransactions = r.ValueOfTransactions
            }));

            return model;
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
                if (domainEvent.AdditionalTransactionRequestMetadata.ContainsKey(additionalRequestField))
                {
                    Type dbTableType = additionalRequestData.GetType();
                    PropertyInfo propertyInfo = dbTableType.GetProperty(additionalRequestField);

                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(additionalRequestData, domainEvent.AdditionalTransactionRequestMetadata[additionalRequestField]);
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
                if (domainEvent.AdditionalTransactionResponseMetadata.ContainsKey(additionalResponseField))
                {
                    Type dbTableType = additionalResponseData.GetType();
                    PropertyInfo propertyInfo = dbTableType.GetProperty(additionalResponseField);

                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(additionalResponseData, domainEvent.AdditionalTransactionResponseMetadata[additionalResponseField]);
                    }
                }
            }

            await context.TransactionsAdditionalResponseData.AddAsync(additionalResponseData, cancellationToken);

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

        #endregion
    }
}