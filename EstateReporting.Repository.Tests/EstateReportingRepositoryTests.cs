using System;
using Xunit;

namespace EstateReporting.Repository.Tests
{
    using System.Threading;
    using System.Threading.Tasks;
    using Database;
    using Database.Entities;
    using Database.ViewEntities;
    using EstateManagement.Estate.DomainEvents;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Moq;
    using Shared.EntityFramework;
    using Shared.Exceptions;
    using Shared.Logger;
    using Shouldly;
    using Testing;

    public class EstateReportingRepositoryTests
    {
        public EstateReportingRepositoryTests()
        {
            Logger.Initialise(NullLogger.Instance);
        }

        private Mock<Shared.EntityFramework.IDbContextFactory<EstateReportingContext>> CreateMockContextFactory()
        {
            return new Mock<Shared.EntityFramework.IDbContextFactory<EstateReportingContext>>();
        }

        [Fact]
        public void EstateReportingRepository_CanBeCreated_IsCreated()
        {
            var dbContextFactory = this.CreateMockContextFactory();

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            reportingRepository.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_CreateReadModel_ReadModelCreated(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            var dbContextFactory = this.CreateMockContextFactory();
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

            var dbContextFactory = this.CreateMockContextFactory();
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

            var dbContextFactory = this.CreateMockContextFactory();
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

            var dbContextFactory = this.CreateMockContextFactory();
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

            var dbContextFactory = this.CreateMockContextFactory();
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

            var dbContextFactory = this.CreateMockContextFactory();
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

            var dbContextFactory = this.CreateMockContextFactory();
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

            var dbContextFactory = this.CreateMockContextFactory();
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

            var dbContextFactory = this.CreateMockContextFactory();
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

            var dbContextFactory = this.CreateMockContextFactory();
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

            var dbContextFactory = this.CreateMockContextFactory();
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

            var dbContextFactory = this.CreateMockContextFactory();
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

            var dbContextFactory = this.CreateMockContextFactory();
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

            var dbContextFactory = this.CreateMockContextFactory();
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

            var dbContextFactory = this.CreateMockContextFactory();
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

            var dbContextFactory = this.CreateMockContextFactory();
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

            var dbContextFactory = this.CreateMockContextFactory();
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

            var dbContextFactory = this.CreateMockContextFactory();
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

            var dbContextFactory = this.CreateMockContextFactory();
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

            var dbContextFactory = this.CreateMockContextFactory();
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

            var dbContextFactory = this.CreateMockContextFactory();
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

            var dbContextFactory = this.CreateMockContextFactory();
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

            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            Should.Throw<NotFoundException>(async () => { await reportingRepository.CompleteTransaction(TestData.TransactionHasBeenCompletedEvent, CancellationToken.None); });
        }


        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_AddContract_ContractAdded(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.AddContract(TestData.ContractCreatedEvent, CancellationToken.None);

            Contract contract = await context.Contracts.SingleOrDefaultAsync(e => e.ContractId== TestData.ContractId);
            contract.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_AddContractProduct_FixedValue_ContractProductAdded(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.AddContractProduct(TestData.FixedValueProductAddedToContractEvent, CancellationToken.None);

            ContractProduct contractProduct = await context.ContractProducts.SingleOrDefaultAsync(e => e.ContractId == TestData.ContractId &&
                                                                                                e.ProductId == TestData.ProductId);
            contractProduct.ShouldNotBeNull();
            contractProduct.Value.ShouldBe(TestData.ProductFixedValue);
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_AddContractProduct_VariableValue_ContractProductAdded(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.AddContractProduct(TestData.VariableValueProductAddedToContractEvent, CancellationToken.None);

            ContractProduct contractProduct = await context.ContractProducts.SingleOrDefaultAsync(e => e.ContractId == TestData.ContractId &&
                                                                                                       e.ProductId == TestData.ProductId);
            contractProduct.ShouldNotBeNull();
            contractProduct.Value.ShouldBeNull();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_AddContractProductTransactionFee_ContractProductTransactionFee(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.AddContractProductTransactionFee(TestData.TransactionFeeForProductAddedToContractEvent, CancellationToken.None);

            ContractProductTransactionFee contractProductTransactionFee = await context.ContractProductTransactionFees.SingleOrDefaultAsync(e => e.ContractId == TestData.ContractId &&
                                                                                                       e.ProductId == TestData.ProductId &&
                                                                                                       e.TransactionFeeId == TestData.TransactionFeeId);
            contractProductTransactionFee.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_AddProductDetailsToTransaction_ProductDetailsAdded(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);
            await context.Transactions.AddAsync(new Transaction
            {
                TransactionId = TestData.TransactionId,
                MerchantId = TestData.MerchantId,
                EstateId = TestData.EstateId
            });
            await context.SaveChangesAsync();

            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.AddProductDetailsToTransaction(TestData.ProductDetailsAddedToTransactionEvent, CancellationToken.None);

            Transaction transaction = await context.Transactions.SingleOrDefaultAsync(e => e.MerchantId == TestData.MerchantId && e.TransactionId == TestData.TransactionId && e.EstateId == TestData.EstateId);
            transaction.ShouldNotBeNull();
            transaction.ContractId.ShouldBe(TestData.ProductDetailsAddedToTransactionEvent.ContractId);
            transaction.ProductId.ShouldBe(TestData.ProductDetailsAddedToTransactionEvent.ProductId);
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_AddProductDetailsToTransaction_TransactionNotFound_ErrorThroen(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);
            Should.Throw<NotFoundException>(async () =>
                                            {
                                                await reportingRepository.AddProductDetailsToTransaction(TestData.ProductDetailsAddedToTransactionEvent,
                                                                                                         CancellationToken.None);
                                            });
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_AddFeeDetailsToTransaction_MerchantFeeAddedToTransactionEvent_FeeAdded(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);
            await context.Transactions.AddAsync(new Transaction
            {
                TransactionId = TestData.TransactionId,
                MerchantId = TestData.MerchantId,
                EstateId = TestData.EstateId
            });
            await context.SaveChangesAsync();

            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.AddFeeDetailsToTransaction(TestData.MerchantFeeAddedToTransactionEvent, CancellationToken.None);

            TransactionFee transactionFee = await context.TransactionFees.SingleOrDefaultAsync(e => e.TransactionId == TestData.TransactionId && e.FeeId == TestData.TransactionFeeId);
            transactionFee.ShouldNotBeNull();
            transactionFee.FeeId.ShouldBe(TestData.MerchantFeeAddedToTransactionEvent.FeeId);
            transactionFee.CalculatedValue.ShouldBe(TestData.MerchantFeeAddedToTransactionEvent.CalculatedValue);
            transactionFee.CalculationType.ShouldBe(TestData.MerchantFeeAddedToTransactionEvent.FeeCalculationType);
            transactionFee.EventId.ShouldBe(TestData.MerchantFeeAddedToTransactionEvent.EventId);
            transactionFee.FeeType.ShouldBe(0);
            transactionFee.FeeValue.ShouldBe(TestData.MerchantFeeAddedToTransactionEvent.FeeValue);
            transactionFee.TransactionId.ShouldBe(TestData.MerchantFeeAddedToTransactionEvent.TransactionId);
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_AddFeeDetailsToTransaction_MerchantFeeAddedToTransactionEvent_TransactionNotFound_ErrorThrown(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);
            Should.Throw<NotFoundException>(async () =>
            {
                await reportingRepository.AddFeeDetailsToTransaction(TestData.MerchantFeeAddedToTransactionEvent, CancellationToken.None);
            });
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_AddFeeDetailsToTransaction_ServiceProviderFeeAddedToTransactionEvent_FeeAdded(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);
            await context.Transactions.AddAsync(new Transaction
            {
                TransactionId = TestData.TransactionId,
                MerchantId = TestData.MerchantId,
                EstateId = TestData.EstateId
            });
            await context.SaveChangesAsync();

            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.AddFeeDetailsToTransaction(TestData.ServiceProviderFeeAddedToTransactionEvent, CancellationToken.None);

            TransactionFee transactionFee = await context.TransactionFees.SingleOrDefaultAsync(e => e.TransactionId == TestData.TransactionId && e.FeeId == TestData.TransactionFeeId);
            transactionFee.ShouldNotBeNull();
            transactionFee.FeeId.ShouldBe(TestData.ServiceProviderFeeAddedToTransactionEvent.FeeId);
            transactionFee.CalculatedValue.ShouldBe(TestData.ServiceProviderFeeAddedToTransactionEvent.CalculatedValue);
            transactionFee.CalculationType.ShouldBe(TestData.ServiceProviderFeeAddedToTransactionEvent.FeeCalculationType);
            transactionFee.EventId.ShouldBe(TestData.ServiceProviderFeeAddedToTransactionEvent.EventId);
            transactionFee.FeeType.ShouldBe(1);
            transactionFee.FeeValue.ShouldBe(TestData.ServiceProviderFeeAddedToTransactionEvent.FeeValue);
            transactionFee.TransactionId.ShouldBe(TestData.ServiceProviderFeeAddedToTransactionEvent.TransactionId);
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_AddFeeDetailsToTransaction_ServiceProviderFeeAddedToTransactionEvent_TransactionNotFound_ErrorThrown(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);
            Should.Throw<NotFoundException>(async () =>
            {
                await reportingRepository.AddFeeDetailsToTransaction(TestData.ServiceProviderFeeAddedToTransactionEvent, CancellationToken.None);
            });
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_DisableContractProductTransactionFee_TransactionFeeDisabled(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);
            await context.ContractProductTransactionFees.AddAsync(new ContractProductTransactionFee
                                                                  {
                                                                      EstateId = TestData.EstateId,
                                                                      ProductId = TestData.ProductId,
                                                                      TransactionFeeId = TestData.TransactionFeeId,
                                                                      Value = TestData.FeeValue,
                                                                      FeeType = TestData.FeeType,
                                                                      Description = TestData.TransactionFeeDescription,
                                                                      CalculationType = TestData.FeeCalculationType,
                                                                      IsEnabled = true,
                                                                      ContractId = TestData.ContractId
                                                                  });
            await context.SaveChangesAsync();

            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);
            
            await reportingRepository.DisableContractProductTransactionFee(TestData.TransactionFeeForProductDisabledEvent, CancellationToken.None);

            ContractProductTransactionFee transactionFee = await context.ContractProductTransactionFees.SingleAsync(t => t.TransactionFeeId == TestData.TransactionFeeId);

            transactionFee.IsEnabled.ShouldBeFalse();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_DisableContractProductTransactionFee_TransactionFeeNotFound_ErrorThrown(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            Should.Throw<NotFoundException>(async () =>
                                            {
                                                await reportingRepository.DisableContractProductTransactionFee(TestData.TransactionFeeForProductDisabledEvent,
                                                                                                               CancellationToken.None);
                                            });
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_StartReconciliation_ReconciliationAdded(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.StartReconciliation(TestData.ReconciliationHasStartedEvent, CancellationToken.None);

            Reconciliation reconciliation = await context.Reconciliations.SingleOrDefaultAsync(e => e.MerchantId == TestData.MerchantId && e.TransactionId == TestData.TransactionId && e.EstateId == TestData.EstateId);
            reconciliation.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_UpdateReconciliationOverallTotals_ReconciliationUpdated(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);
            await context.Reconciliations.AddAsync(new Reconciliation
                                                {
                                                    TransactionId = TestData.TransactionId,
                                                    MerchantId = TestData.MerchantId,
                                                    EstateId = TestData.EstateId,
                                                    TransactionDate = TestData.TransactionDateTime.Date,
                                                    TransactionDateTime = TestData.TransactionDateTime,
                                                    TransactionTime = TestData.TransactionDateTime.TimeOfDay
                                                });
            await context.SaveChangesAsync();

            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.UpdateReconciliationOverallTotals(TestData.OverallTotalsRecordedEvent, CancellationToken.None);

            Reconciliation reconciliation = await context.Reconciliations.SingleOrDefaultAsync(e => e.MerchantId == TestData.MerchantId && e.TransactionId == TestData.TransactionId && e.EstateId == TestData.EstateId);
            reconciliation.TransactionCount.ShouldBe(TestData.ReconcilationTransactionCount);
            reconciliation.TransactionValue.ShouldBe(TestData.ReconcilationTransactionValue);
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_UpdateReconciliationOverallTotals_ReconciliationNotFound_ErrorThrown(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            Should.Throw<NotFoundException>(async () =>
                                            {
                                                await reportingRepository.UpdateReconciliationOverallTotals(TestData.OverallTotalsRecordedEvent,
                                                                                              CancellationToken.None);
                                            });
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_UpdateReconciliationStatus_Authorised_ReconciliationUpdated(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);
            await context.Reconciliations.AddAsync(new Reconciliation
                                                   {
                                                       TransactionId = TestData.TransactionId,
                                                       MerchantId = TestData.MerchantId,
                                                       EstateId = TestData.EstateId,
                                                       TransactionDate = TestData.TransactionDateTime.Date,
                                                       TransactionDateTime = TestData.TransactionDateTime,
                                                       TransactionTime = TestData.TransactionDateTime.TimeOfDay
                                                   });
            await context.SaveChangesAsync();

            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.UpdateReconciliationStatus(TestData.ReconciliationHasBeenLocallyAuthorisedEvent, CancellationToken.None);

            Reconciliation reconciliation = await context.Reconciliations.SingleOrDefaultAsync(e => e.MerchantId == TestData.MerchantId && e.TransactionId == TestData.TransactionId && e.EstateId == TestData.EstateId);
            reconciliation.IsAuthorised.ShouldBeTrue();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_UpdateReconciliationStatus_Authorised_ReconciliationNotFound_ErrorThrown(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            Should.Throw<NotFoundException>(async () =>
                                            {
                                                await reportingRepository.UpdateReconciliationStatus(TestData.ReconciliationHasBeenLocallyAuthorisedEvent,
                                                                                                            CancellationToken.None);
                                            });
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_UpdateReconciliationStatus_Declined_ReconciliationUpdated(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);
            await context.Reconciliations.AddAsync(new Reconciliation
            {
                TransactionId = TestData.TransactionId,
                MerchantId = TestData.MerchantId,
                EstateId = TestData.EstateId,
                TransactionDate = TestData.TransactionDateTime.Date,
                TransactionDateTime = TestData.TransactionDateTime,
                TransactionTime = TestData.TransactionDateTime.TimeOfDay
            });
            await context.SaveChangesAsync();

            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.UpdateReconciliationStatus(TestData.ReconciliationHasBeenLocallyDeclinedEvent, CancellationToken.None);

            Reconciliation reconciliation = await context.Reconciliations.SingleOrDefaultAsync(e => e.MerchantId == TestData.MerchantId && e.TransactionId == TestData.TransactionId && e.EstateId == TestData.EstateId);
            reconciliation.IsAuthorised.ShouldBeFalse();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_UpdateReconciliationStatus_Declined_ReconciliationNotFound_ErrorThrown(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            Should.Throw<NotFoundException>(async () =>
                                            {
                                                await reportingRepository.UpdateReconciliationStatus(TestData.ReconciliationHasBeenLocallyDeclinedEvent,
                                                                                                     CancellationToken.None);
                                            });
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_CompleteReconciliation_ReconciliationUpdated(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);
            await context.Reconciliations.AddAsync(new Reconciliation
                                                   {
                                                       TransactionId = TestData.TransactionId,
                                                       MerchantId = TestData.MerchantId,
                                                       EstateId = TestData.EstateId,
                                                       TransactionDate = TestData.TransactionDateTime.Date,
                                                       TransactionDateTime = TestData.TransactionDateTime,
                                                       TransactionTime = TestData.TransactionDateTime.TimeOfDay
                                                   });
            await context.SaveChangesAsync();

            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.CompleteReconciliation(TestData.ReconciliationHasCompletedEvent, CancellationToken.None);

            Reconciliation reconciliation = await context.Reconciliations.SingleOrDefaultAsync(e => e.MerchantId == TestData.MerchantId && e.TransactionId == TestData.TransactionId && e.EstateId == TestData.EstateId);
            reconciliation.IsCompleted.ShouldBeTrue();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_CompleteReconciliation_ReconciliationNotFound_ErrorThrown(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            Should.Throw<NotFoundException>(async () =>
                                            {
                                                await reportingRepository.CompleteReconciliation(TestData.ReconciliationHasCompletedEvent,
                                                                                                     CancellationToken.None);
                                            });
        }


        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_InsertMerchantBalanceRecord_BalanceRecordAdded(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.InsertMerchantBalanceRecord(TestData.MerchantBalanceChangedEvent, CancellationToken.None);

            MerchantBalanceHistory balanceHistory= await context.MerchantBalanceHistories.SingleOrDefaultAsync(e => e.EstateId == TestData.EstateId);
            balanceHistory.ShouldNotBeNull();
            balanceHistory.AvailableBalance.ShouldBe(TestData.AvailableBalance);
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_InsertMerchantBalanceRecord_Replay_BalanceRecordUpdated(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.InsertMerchantBalanceRecord(TestData.MerchantBalanceChangedEvent, CancellationToken.None);

            MerchantBalanceHistory balanceHistory = null;
            balanceHistory = await context.MerchantBalanceHistories.SingleOrDefaultAsync(e => e.EventId == TestData.MerchantBalanceChangedEvent.EventId);
            balanceHistory.ShouldNotBeNull();
            balanceHistory.AvailableBalance.ShouldBe(TestData.AvailableBalance);

            await reportingRepository.InsertMerchantBalanceRecord(TestData.MerchantBalanceChangedEvent2, CancellationToken.None);

            balanceHistory = await context.MerchantBalanceHistories.SingleOrDefaultAsync(e => e.EventId == TestData.MerchantBalanceChangedEvent.EventId);
            balanceHistory.ShouldNotBeNull();
            balanceHistory.AvailableBalance.ShouldBe(TestData.AvailableBalance2);
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_AddGeneratedVoucher_VoucherAdded(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.AddGeneratedVoucher(TestData.VoucherGeneratedEvent, CancellationToken.None);

            Voucher voucher = await context.Vouchers.SingleOrDefaultAsync(e => e.VoucherId == TestData.VoucherId && e.EstateId == TestData.EstateId);
            voucher.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_UpdateVoucherIssueDetails_VoucherUpdated(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);
            await context.Vouchers.AddAsync(new Voucher
                                 {
                                     EstateId = TestData.EstateId,
                                     VoucherId = TestData.VoucherId
                                 }, CancellationToken.None);
            await context.SaveChangesAsync(CancellationToken.None);
            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.UpdateVoucherIssueDetails(TestData.VoucherIssuedEvent, CancellationToken.None);

            Voucher voucher = await context.Vouchers.SingleOrDefaultAsync(e => e.VoucherId == TestData.VoucherId && e.EstateId == TestData.EstateId);
            voucher.IsIssued.ShouldBeTrue();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_UpdateVoucherIssueDetails_VouchernNotFound_ErrorThrown(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);
            
            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            Should.Throw<NotFoundException>(async () =>
                                            {
                                                await reportingRepository.UpdateVoucherIssueDetails(TestData.VoucherIssuedEvent, CancellationToken.None);
                                            });
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_UpdateVoucherRedemptionDetails_VoucherUpdated(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);
            await context.Vouchers.AddAsync(new Voucher
            {
                EstateId = TestData.EstateId,
                VoucherId = TestData.VoucherId
            }, CancellationToken.None);
            await context.SaveChangesAsync(CancellationToken.None);
            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            await reportingRepository.UpdateVoucherRedemptionDetails(TestData.VoucherFullyRedeemedEvent, CancellationToken.None);

            Voucher voucher = await context.Vouchers.SingleOrDefaultAsync(e => e.VoucherId == TestData.VoucherId && e.EstateId == TestData.EstateId);
            voucher.IsRedeemed.ShouldBeTrue();
        }

        [Theory]
        [InlineData(TestDatabaseType.InMemory)]
        [InlineData(TestDatabaseType.SqliteInMemory)]
        public async Task EstateReportingRepository_UpdateVoucherRedemptionDetails_VoucherNotFound_ErrorThrown(TestDatabaseType testDatabaseType)
        {
            EstateReportingContext context = await this.GetContext(Guid.NewGuid().ToString("N"), testDatabaseType);

            var dbContextFactory = this.CreateMockContextFactory();
            dbContextFactory.Setup(d => d.GetContext(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(context);

            EstateReportingRepository reportingRepository = new EstateReportingRepository(dbContextFactory.Object);

            Should.Throw<NotFoundException>(async () =>
            {
                await reportingRepository.UpdateVoucherRedemptionDetails(TestData.VoucherFullyRedeemedEvent, CancellationToken.None);
            });
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
