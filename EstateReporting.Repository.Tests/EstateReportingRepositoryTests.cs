using System;
using Xunit;

namespace EstateReporting.Repository.Tests
{
    using System.Threading;
    using System.Threading.Tasks;
    using Database;
    using Database.Entities;
    using EstateManagement.Estate.DomainEvents;
    using EstateReporting.Tests;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Moq;
    using Shared.EntityFramework;
    using Shared.Exceptions;
    using Shared.Logger;
    using Shouldly;

    public class EstateReportingRepositoryTests
    {
        public EstateReportingRepositoryTests()
        {
            Logger.Initialise(NullLogger.Instance);
        }

        [Fact]
        public void EstateReportingRepository_CanBeCreated_IsCreated()
        {
            Mock<IDbContextFactory<EstateReportingContext>> dbContextFactory = new Mock<IDbContextFactory<EstateReportingContext>>();

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            reportingRepository.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_CreateReadModel_ReadModelCreated(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            Mock<IDbContextFactory<EstateReportingContext>> dbContextFactory = new Mock<IDbContextFactory<EstateReportingContext>>();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            Should.NotThrow(async () => { await reportingRepository.CreateReadModel(TestData.EstateCreatedEvent, CancellationToken.None); });
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_AddEstate_EstateAdded(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            Mock<IDbContextFactory<EstateReportingContext>> dbContextFactory = new Mock<IDbContextFactory<EstateReportingContext>>();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.AddEstate(TestData.EstateCreatedEvent, CancellationToken.None);

            Estate estate = await context.Estates.SingleOrDefaultAsync(e => e.EstateId == TestData.EstateId);
            estate.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_AddEstateSecurityUser_EstateSecurityUserAdded(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            Mock<IDbContextFactory<EstateReportingContext>> dbContextFactory = new Mock<IDbContextFactory<EstateReportingContext>>();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.AddEstateSecurityUser(TestData.EstateSecurityUserAddedEvent, CancellationToken.None);

            EstateSecurityUser estateSecurityUser = await context.EstateSecurityUsers.SingleOrDefaultAsync(e => e.SecurityUserId == TestData.EstateSecurityUserId);
            estateSecurityUser.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_AddEstateOperator_EstateOperatorAdded(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            Mock<IDbContextFactory<EstateReportingContext>> dbContextFactory = new Mock<IDbContextFactory<EstateReportingContext>>();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.AddEstateOperator(TestData.OperatorAddedToEstateEvent, CancellationToken.None);

            EstateOperator estateOperator = await context.EstateOperators.SingleOrDefaultAsync(e => e.OperatorId == TestData.OperatorId);
            estateOperator.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_AddMerchant_MerchantAdded(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            Mock<IDbContextFactory<EstateReportingContext>> dbContextFactory = new Mock<IDbContextFactory<EstateReportingContext>>();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.AddMerchant(TestData.MerchantCreatedEvent, CancellationToken.None);

            Merchant merchant = await context.Merchants.SingleOrDefaultAsync(e => e.MerchantId == TestData.MerchantId && e.EstateId == TestData.EstateId);
            merchant.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_AddMerchantAddress_MerchantAddressAdded(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            Mock<IDbContextFactory<EstateReportingContext>> dbContextFactory = new Mock<IDbContextFactory<EstateReportingContext>>();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.AddMerchantAddress(TestData.AddressAddedEvent, CancellationToken.None);

            MerchantAddress merchantAddress = await context.MerchantAddresses.SingleOrDefaultAsync(e => e.MerchantId == TestData.MerchantId && e.AddressId == TestData.AddressId && e.EstateId == TestData.EstateId);
            merchantAddress.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_AddMerchantContact_MerchantContactAdded(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            Mock<IDbContextFactory<EstateReportingContext>> dbContextFactory = new Mock<IDbContextFactory<EstateReportingContext>>();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.AddMerchantContact(TestData.ContactAddedEvent, CancellationToken.None);

            MerchantContact merchantContact = await context.MerchantContacts.SingleOrDefaultAsync(e => e.MerchantId == TestData.MerchantId && e.ContactId == TestData.ContactId && e.EstateId == TestData.EstateId);
            merchantContact.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_AddMerchantDevice_MerchantDeviceAdded(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            Mock<IDbContextFactory<EstateReportingContext>> dbContextFactory = new Mock<IDbContextFactory<EstateReportingContext>>();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.AddMerchantDevice(TestData.DeviceAddedToMerchantEvent, CancellationToken.None);

            MerchantDevice merchantDevice = await context.MerchantDevices.SingleOrDefaultAsync(e => e.MerchantId == TestData.MerchantId && e.DeviceId == TestData.DeviceId && e.EstateId == TestData.EstateId);
            merchantDevice.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_AddMerchantSecurityUser_MerchantSecurityUserAdded(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            Mock<IDbContextFactory<EstateReportingContext>> dbContextFactory = new Mock<IDbContextFactory<EstateReportingContext>>();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.AddMerchantSecurityUser(TestData.MerchantSecurityUserAddedEvent, CancellationToken.None);

            MerchantSecurityUser merchantSecurityUser = await context.MerchantSecurityUsers.SingleOrDefaultAsync(e => e.MerchantId == TestData.MerchantId && e.SecurityUserId == TestData.MerchantSecurityUserId && e.EstateId == TestData.EstateId);
            merchantSecurityUser.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_AddMerchantOperator_MerchantOperatorAdded(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            Mock<IDbContextFactory<EstateReportingContext>> dbContextFactory = new Mock<IDbContextFactory<EstateReportingContext>>();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.AddMerchantOperator(TestData.OperatorAssignedToMerchantEvent, CancellationToken.None);

            MerchantOperator merchantOperator = await context.MerchantOperators.SingleOrDefaultAsync(e => e.MerchantId == TestData.MerchantId && e.OperatorId == TestData.OperatorId && e.EstateId == TestData.EstateId);
            merchantOperator.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_StartTransaction_TransactionAdded(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            Mock<IDbContextFactory<EstateReportingContext>> dbContextFactory = new Mock<IDbContextFactory<EstateReportingContext>>();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.StartTransaction(TestData.TransactionHasStartedEvent, CancellationToken.None);

            Transaction transaction = await context.Transactions.SingleOrDefaultAsync(e => e.MerchantId == TestData.MerchantId && e.TransactionId == TestData.TransactionId && e.EstateId == TestData.EstateId);
            transaction.ShouldNotBeNull();
        }
        
        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_RecordTransactionAdditionalRequestData_RequestDataRecorded(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);
            await context.Transactions.AddAsync(new Transaction
                                                {
                                                    TransactionId = TestData.TransactionId,
                                                    MerchantId = TestData.MerchantId,
                                                    EstateId = TestData.EstateId
            });
            await context.SaveChangesAsync();

            Mock<IDbContextFactory<EstateReportingContext>> dbContextFactory = new Mock<IDbContextFactory<EstateReportingContext>>();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.RecordTransactionAdditionalRequestData(TestData.AdditionalRequestDataRecordedEvent, CancellationToken.None);

            Transaction transaction = await context.Transactions.SingleOrDefaultAsync(e => e.MerchantId == TestData.MerchantId && e.TransactionId == TestData.TransactionId && e.EstateId == TestData.EstateId);
            transaction.ShouldNotBeNull();

            TransactionAdditionalRequestData transactionAdditionalRequestData = await context.TransactionsAdditionalRequestData.SingleOrDefaultAsync(e => e.MerchantId == TestData.MerchantId && e.TransactionId == TestData.TransactionId && e.EstateId == TestData.EstateId);
            transactionAdditionalRequestData.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_RecordTransactionAdditionalResponseData_ResponseDataRecorded(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);
            await context.Transactions.AddAsync(new Transaction
                                                {
                                                    TransactionId = TestData.TransactionId,
                                                    MerchantId = TestData.MerchantId,
                                                    EstateId = TestData.EstateId
                                                });
            await context.SaveChangesAsync();

            Mock<IDbContextFactory<EstateReportingContext>> dbContextFactory = new Mock<IDbContextFactory<EstateReportingContext>>();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.RecordTransactionAdditionalResponseData(TestData.AdditionalResponseDataRecordedEvent, CancellationToken.None);

            Transaction transaction = await context.Transactions.SingleOrDefaultAsync(e => e.MerchantId == TestData.MerchantId && e.TransactionId == TestData.TransactionId && e.EstateId == TestData.EstateId);
            transaction.ShouldNotBeNull();

            TransactionAdditionalResponseData transactionAdditionalResponseData = await context.TransactionsAdditionalResponseData.SingleOrDefaultAsync(e => e.MerchantId == TestData.MerchantId && e.TransactionId == TestData.TransactionId && e.EstateId == TestData.EstateId);
            transactionAdditionalResponseData.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_UpdateTransactionAuthorisation_LocallyAuthorised_TransactionUpdated(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);
            await context.Transactions.AddAsync(new Transaction
                                                {
                                                    TransactionId = TestData.TransactionId,
                                                    MerchantId = TestData.MerchantId,
                                                    EstateId = TestData.EstateId
            });
            await context.SaveChangesAsync();

            Mock<IDbContextFactory<EstateReportingContext>> dbContextFactory = new Mock<IDbContextFactory<EstateReportingContext>>();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.UpdateTransactionAuthorisation(TestData.TransactionHasBeenLocallyAuthorisedEvent, CancellationToken.None);

            Transaction transaction = await context.Transactions.SingleOrDefaultAsync(e => e.MerchantId == TestData.MerchantId && e.TransactionId == TestData.TransactionId && e.EstateId == TestData.EstateId);
            transaction.ShouldNotBeNull();
            transaction.ResponseMessage.ShouldBe(TestData.TransactionHasBeenLocallyAuthorisedEvent.ResponseMessage);
            transaction.ResponseCode.ShouldBe(TestData.TransactionHasBeenLocallyAuthorisedEvent.ResponseCode);
            transaction.IsAuthorised.ShouldBeTrue();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_UpdateTransactionAuthorisation_LocallyAuthorised_TransactionNotFound_ErrorThrown(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            Mock<IDbContextFactory<EstateReportingContext>> dbContextFactory = new Mock<IDbContextFactory<EstateReportingContext>>();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            Should.Throw<NotFoundException>(async () =>
                                            {
                                                await reportingRepository.UpdateTransactionAuthorisation(TestData.TransactionHasBeenLocallyAuthorisedEvent,
                                                                                                         CancellationToken.None);
                                            });
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_UpdateTransactionAuthorisation_LocallyDeclined_TransactionUpdated(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);
            await context.Transactions.AddAsync(new Transaction
                                                {
                                                    TransactionId = TestData.TransactionId,
                                                    MerchantId = TestData.MerchantId,
                                                    EstateId = TestData.EstateId
            });
            await context.SaveChangesAsync();

            Mock<IDbContextFactory<EstateReportingContext>> dbContextFactory = new Mock<IDbContextFactory<EstateReportingContext>>();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.UpdateTransactionAuthorisation(TestData.TransactionHasBeenLocallyDeclinedEvent, CancellationToken.None);

            Transaction transaction = await context.Transactions.SingleOrDefaultAsync(e => e.MerchantId == TestData.MerchantId && e.TransactionId == TestData.TransactionId && e.EstateId == TestData.EstateId);
            transaction.ShouldNotBeNull();
            transaction.ResponseMessage.ShouldBe(TestData.TransactionHasBeenLocallyDeclinedEvent.ResponseMessage);
            transaction.ResponseCode.ShouldBe(TestData.TransactionHasBeenLocallyDeclinedEvent.ResponseCode);
            transaction.IsAuthorised.ShouldBeFalse();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_UpdateTransactionAuthorisation_LocallyDeclined_TransactionNotFound_ErrorThrown(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            Mock<IDbContextFactory<EstateReportingContext>> dbContextFactory = new Mock<IDbContextFactory<EstateReportingContext>>();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            Should.Throw<NotFoundException>(async () => {
                                                await reportingRepository.UpdateTransactionAuthorisation(TestData.TransactionHasBeenLocallyDeclinedEvent, CancellationToken.None);

                                            });
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_UpdateTransactionAuthorisation_OperatorAuthorised_TransactionUpdated(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);
            await context.Transactions.AddAsync(new Transaction
                                                {
                                                    TransactionId = TestData.TransactionId,
                                                    MerchantId = TestData.MerchantId,
                                                    EstateId = TestData.EstateId
            });
            await context.SaveChangesAsync();

            Mock<IDbContextFactory<EstateReportingContext>> dbContextFactory = new Mock<IDbContextFactory<EstateReportingContext>>();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.UpdateTransactionAuthorisation(TestData.TransactionAuthorisedByOperatorEvent, CancellationToken.None);

            Transaction transaction = await context.Transactions.SingleOrDefaultAsync(e => e.MerchantId == TestData.MerchantId && e.TransactionId == TestData.TransactionId && e.EstateId == TestData.EstateId);
            transaction.ShouldNotBeNull();
            transaction.ResponseMessage.ShouldBe(TestData.TransactionAuthorisedByOperatorEvent.ResponseMessage);
            transaction.ResponseCode.ShouldBe(TestData.TransactionAuthorisedByOperatorEvent.ResponseCode);
            transaction.IsAuthorised.ShouldBeTrue();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_UpdateTransactionAuthorisation_OperatorAuthorised_TransactionNotFound_ErrorThrown(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            Mock<IDbContextFactory<EstateReportingContext>> dbContextFactory = new Mock<IDbContextFactory<EstateReportingContext>>();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            Should.Throw<NotFoundException>(async () =>
                                            {
                                                await reportingRepository.UpdateTransactionAuthorisation(TestData.TransactionAuthorisedByOperatorEvent,
                                                                                                         CancellationToken.None);
                                            });
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_UpdateTransactionAuthorisation_OperatorDeclined_TransactionUpdated(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);
            await context.Transactions.AddAsync(new Transaction
                                                {
                                                    TransactionId = TestData.TransactionId,
                                                    MerchantId = TestData.MerchantId,
                                                    EstateId = TestData.EstateId
            });
            await context.SaveChangesAsync();

            Mock<IDbContextFactory<EstateReportingContext>> dbContextFactory = new Mock<IDbContextFactory<EstateReportingContext>>();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.UpdateTransactionAuthorisation(TestData.TransactionDeclinedByOperatorEvent, CancellationToken.None);

            Transaction transaction = await context.Transactions.SingleOrDefaultAsync(e => e.MerchantId == TestData.MerchantId && e.TransactionId == TestData.TransactionId && e.EstateId == TestData.EstateId);
            transaction.ShouldNotBeNull();
            transaction.ResponseMessage.ShouldBe(TestData.TransactionDeclinedByOperatorEvent.ResponseMessage);
            transaction.ResponseCode.ShouldBe(TestData.TransactionDeclinedByOperatorEvent.ResponseCode);
            transaction.IsAuthorised.ShouldBeFalse();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_UpdateTransactionAuthorisation_OperatorDeclined_TransactionNotFound_ErrorThrown(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            Mock<IDbContextFactory<EstateReportingContext>> dbContextFactory = new Mock<IDbContextFactory<EstateReportingContext>>();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            Should.Throw<NotFoundException>(async () =>
                                            {
                                                await reportingRepository.UpdateTransactionAuthorisation(TestData.TransactionDeclinedByOperatorEvent,
                                                                                                         CancellationToken.None);
                                            });
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_CompleteTransaction_TransactionCompleted(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);
            await context.Transactions.AddAsync(new Transaction
                                                {
                                                    TransactionId = TestData.TransactionId,
                                                    MerchantId = TestData.MerchantId,
                                                    EstateId = TestData.EstateId
            });
            await context.SaveChangesAsync();

            Mock<IDbContextFactory<EstateReportingContext>> dbContextFactory = new Mock<IDbContextFactory<EstateReportingContext>>();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.CompleteTransaction(TestData.TransactionHasBeenCompletedEvent, CancellationToken.None);

            Transaction transaction = await context.Transactions.SingleOrDefaultAsync(e => e.MerchantId == TestData.MerchantId && e.TransactionId == TestData.TransactionId && e.EstateId == TestData.EstateId);
            transaction.ShouldNotBeNull();
            transaction.IsCompleted.ShouldBeTrue();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_CompleteTransaction_TransactionNotFound_ErrorThrown(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            Mock<IDbContextFactory<EstateReportingContext>> dbContextFactory = new Mock<IDbContextFactory<EstateReportingContext>>();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            Should.Throw<NotFoundException>(async () => { await reportingRepository.CompleteTransaction(TestData.TransactionHasBeenCompletedEvent, CancellationToken.None); });
        }
        private async Task<EstateReportingContext> GetContext(String databaseName, TestDatabaseType databaseType = TestDatabaseType.InMemory)
        {
            EstateReportingContext context = null;
            if (databaseType == TestDatabaseType.InMemory)
            {
                DbContextOptionsBuilder<EstateReportingContext> builder = new DbContextOptionsBuilder<EstateReportingContext>()
                                                                          .UseInMemoryDatabase(databaseName)
                                                                          .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                context = new EstateReportingContext(builder.Options);
            }
            else if (databaseType == TestDatabaseType.SqliteInMemory)
            {
                SqliteConnection inMemorySqlite = new SqliteConnection("Data Source=:memory:");
                inMemorySqlite.Open();

                DbContextOptionsBuilder<EstateReportingContext> builder = new DbContextOptionsBuilder<EstateReportingContext>().UseSqlite(inMemorySqlite);
                context = new EstateReportingContext(builder.Options);
                await context.Database.EnsureCreatedAsync();

            }
            else
            {
                throw new NotSupportedException($"Database type [{databaseType}] not supported");
            }
            

            

            return context;
        }

        public enum TestDatabaseType
        {
            InMemory = 0,
            SqliteInMemory = 1
        }
    }
}
