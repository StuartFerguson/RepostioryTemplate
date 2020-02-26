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
