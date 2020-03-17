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
                await context.Database.MigrateAsync();

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
