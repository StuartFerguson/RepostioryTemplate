using System;
using System.Collections.Generic;
using System.Text;

namespace EstateReporting.BusinessLogic.Tests
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Castle.Components.DictionaryAdapter;
    using Models;
    using Moq;
    using Repository;
    using Shouldly;
    using Testing;
    using Xunit;
    using SortDirection = BusinessLogic.SortDirection;
    using SortField = BusinessLogic.SortField;

    public class ReportingManagerTests
    {
        [Fact]
        public async Task ReportingManager_GetSettlement_DataReturned()
        {
            Mock<IEstateReportingRepository> repository = new Mock<IEstateReportingRepository>();
            Mock<IEstateReportingRepositoryForReports> repositoryForReports = new Mock<IEstateReportingRepositoryForReports>();
            repositoryForReports.Setup(r => r.GetSettlement(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                .ReturnsAsync(TestData.SettlementModel);

            ReportingManager manager = new ReportingManager(repository.Object, repositoryForReports.Object);

            SettlementModel model = await manager.GetSettlement(TestData.EstateId, TestData.MerchantId, TestData.SettlementId, CancellationToken.None);

            model.ShouldNotBeNull();
            model.ShouldSatisfyAllConditions(p => p.SettlementDate.ShouldBe(TestData.SettlementModel.SettlementDate),
                                             p => p.IsCompleted.ShouldBe(TestData.SettlementModel.IsCompleted),
                                             p => p.NumberOfFeesSettled.ShouldBe(TestData.SettlementModel.NumberOfFeesSettled),
                                             p => p.SettlementId.ShouldBe(TestData.SettlementModel.SettlementId),
                                             p => p.ValueOfFeesSettled.ShouldBe(TestData.SettlementModel.ValueOfFeesSettled));
        }

        [Fact]
        public async Task ReportingManager_GetSettlements_DataReturned()
        {
            Mock<IEstateReportingRepository> repository = new Mock<IEstateReportingRepository>();
            Mock<IEstateReportingRepositoryForReports> repositoryForReports = new Mock<IEstateReportingRepositoryForReports>();
            repositoryForReports.Setup(r => r.GetSettlements(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<CancellationToken>()))
                                .ReturnsAsync(TestData.SettlementModels);

            ReportingManager manager = new ReportingManager(repository.Object, repositoryForReports.Object);

            List<SettlementModel> model = await manager.GetSettlements(TestData.EstateId, TestData.MerchantId, TestData.StartDate, TestData.EndDate, CancellationToken.None);

            model.ShouldNotBeNull();
            model.ShouldNotBeEmpty();
            model.ShouldNotBeEmpty();
            model.Count.ShouldBe(TestData.SettlementModels.Count);
        }

        [Fact]
        public async Task ReportingManager_GetTransactionsForEstateByDate_DataReturned()
        {
            Mock<IEstateReportingRepository> repository = new Mock<IEstateReportingRepository>();
            Mock<IEstateReportingRepositoryForReports> repositoryForReports = new Mock<IEstateReportingRepositoryForReports>();
            repositoryForReports.Setup(r => r.GetTransactionsForEstateByDate(It.IsAny<Guid>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(TestData.TransactionsByDayModel);

            ReportingManager manager = new ReportingManager(repository.Object, repositoryForReports.Object);

            TransactionsByDayModel model = await manager.GetTransactionsForEstateByDate(TestData.EstateId, TestData.StartDate, TestData.EndDate, CancellationToken.None);

            model.ShouldNotBeNull();
            model.TransactionDayModels.ShouldNotBeNull();
            model.TransactionDayModels.ShouldNotBeEmpty();
            model.TransactionDayModels.Count.ShouldBe(TestData.TransactionsByDayModel.TransactionDayModels.Count);
        }

        [Fact]
        public async Task ReportingManager_GetSettlementForEstateByDate_DataReturned()
        {
            Mock<IEstateReportingRepository> repository = new Mock<IEstateReportingRepository>();
            Mock<IEstateReportingRepositoryForReports> repositoryForReports = new Mock<IEstateReportingRepositoryForReports>();
            repositoryForReports.Setup(r => r.GetSettlementForEstateByDate(It.IsAny<Guid>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<CancellationToken>()))
                                .ReturnsAsync(TestData.SettlementByDayModel);

            ReportingManager manager = new ReportingManager(repository.Object, repositoryForReports.Object);

            var model = await manager.GetSettlementForEstateByDate(TestData.EstateId, TestData.StartDate, TestData.EndDate, CancellationToken.None);

            model.ShouldNotBeNull();
            model.SettlementDayModels.ShouldNotBeNull();
            model.SettlementDayModels.ShouldNotBeEmpty();
            model.SettlementDayModels.Count.ShouldBe(TestData.SettlementByDayModel.SettlementDayModels.Count);
        }

        [Fact]
        public async Task ReportingManager_GetTransactionsForMerchantByDate_DataReturned()
        {
            Mock<IEstateReportingRepository> repository = new Mock<IEstateReportingRepository>();
            Mock<IEstateReportingRepositoryForReports> repositoryForReports = new Mock<IEstateReportingRepositoryForReports>();
            repositoryForReports.Setup(r => r.GetTransactionsForMerchantByDate(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(TestData.TransactionsByDayModel);

            ReportingManager manager = new ReportingManager(repository.Object, repositoryForReports.Object);

            TransactionsByDayModel model = await manager.GetTransactionsForMerchantByDate(TestData.EstateId, TestData.MerchantId, TestData.StartDate, TestData.EndDate, CancellationToken.None);

            model.ShouldNotBeNull();
            model.TransactionDayModels.ShouldNotBeNull();
            model.TransactionDayModels.ShouldNotBeEmpty();
            model.TransactionDayModels.Count.ShouldBe(TestData.TransactionsByDayModel.TransactionDayModels.Count);
        }

        [Fact]
        public async Task ReportingManager_GetSettlementForMerchantByDate_DataReturned()
        {
            Mock<IEstateReportingRepository> repository = new Mock<IEstateReportingRepository>();
            Mock<IEstateReportingRepositoryForReports> repositoryForReports = new Mock<IEstateReportingRepositoryForReports>();
            repositoryForReports.Setup(r => r.GetSettlementForMerchantByDate(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<CancellationToken>()))
                                .ReturnsAsync(TestData.SettlementByDayModel);

            ReportingManager manager = new ReportingManager(repository.Object, repositoryForReports.Object);

            SettlementByDayModel model = await manager.GetSettlementForMerchantByDate(TestData.EstateId, TestData.MerchantId, TestData.StartDate, TestData.EndDate, CancellationToken.None);

            model.ShouldNotBeNull();
            model.SettlementDayModels.ShouldNotBeNull();
            model.SettlementDayModels.ShouldNotBeEmpty();
            model.SettlementDayModels.Count.ShouldBe(TestData.SettlementByDayModel.SettlementDayModels.Count);
        }

        [Fact]
        public async Task ReportingManager_GetTransactionsForEstateByWeek_DataReturned()
        {
            Mock<IEstateReportingRepository> repository = new Mock<IEstateReportingRepository>();
            Mock<IEstateReportingRepositoryForReports> repositoryForReports = new Mock<IEstateReportingRepositoryForReports>();
            repositoryForReports.Setup(r => r.GetTransactionsForEstateByWeek(It.IsAny<Guid>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(TestData.TransactionsByWeekModel);

            ReportingManager manager = new ReportingManager(repository.Object, repositoryForReports.Object);

            TransactionsByWeekModel model = await manager.GetTransactionsForEstateByWeek(TestData.EstateId, TestData.StartDate, TestData.EndDate, CancellationToken.None);

            model.ShouldNotBeNull();
            model.TransactionWeekModels.ShouldNotBeNull();
            model.TransactionWeekModels.ShouldNotBeEmpty();
            model.TransactionWeekModels.Count.ShouldBe(TestData.TransactionsByWeekModel.TransactionWeekModels.Count);
        }

        [Fact]
        public async Task ReportingManager_GetSettlementForEstateByWeek_DataReturned()
        {
            Mock<IEstateReportingRepository> repository = new Mock<IEstateReportingRepository>();
            Mock<IEstateReportingRepositoryForReports> repositoryForReports = new Mock<IEstateReportingRepositoryForReports>();
            repositoryForReports.Setup(r => r.GetSettlementForEstateByWeek(It.IsAny<Guid>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<CancellationToken>()))
                                .ReturnsAsync(TestData.SettlementByWeekModel);

            ReportingManager manager = new ReportingManager(repository.Object, repositoryForReports.Object);

            SettlementByWeekModel model = await manager.GetSettlementForEstateByWeek(TestData.EstateId, TestData.StartDate, TestData.EndDate, CancellationToken.None);

            model.ShouldNotBeNull();
            model.SettlementWeekModels.ShouldNotBeNull();
            model.SettlementWeekModels.ShouldNotBeEmpty();
            model.SettlementWeekModels.Count.ShouldBe(TestData.SettlementByWeekModel.SettlementWeekModels.Count);
        }

        [Fact]
        public async Task ReportingManager_GetTransactionsForEstateByMonth_DataReturned()
        {
            Mock<IEstateReportingRepository> repository = new Mock<IEstateReportingRepository>();
            Mock<IEstateReportingRepositoryForReports> repositoryForReports = new Mock<IEstateReportingRepositoryForReports>();
            repositoryForReports.Setup(r => r.GetTransactionsForEstateByMonth(It.IsAny<Guid>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(TestData.TransactionsByMonthModel);

            ReportingManager manager = new ReportingManager(repository.Object, repositoryForReports.Object);

            TransactionsByMonthModel model = await manager.GetTransactionsForEstateByMonth(TestData.EstateId, TestData.StartDate, TestData.EndDate, CancellationToken.None);

            model.ShouldNotBeNull();
            model.TransactionMonthModels.ShouldNotBeNull();
            model.TransactionMonthModels.ShouldNotBeEmpty();
            model.TransactionMonthModels.Count.ShouldBe(TestData.TransactionsByMonthModel.TransactionMonthModels.Count);
        }

        [Fact]
        public async Task ReportingManager_GetSettlementForEstateByMonth_DataReturned()
        {
            Mock<IEstateReportingRepository> repository = new Mock<IEstateReportingRepository>();
            Mock<IEstateReportingRepositoryForReports> repositoryForReports = new Mock<IEstateReportingRepositoryForReports>();
            repositoryForReports.Setup(r => r.GetSettlementForEstateByMonth(It.IsAny<Guid>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<CancellationToken>()))
                                .ReturnsAsync(TestData.SettlementByMonthModel);

            ReportingManager manager = new ReportingManager(repository.Object, repositoryForReports.Object);

            SettlementByMonthModel model = await manager.GetSettlementForEstateByMonth(TestData.EstateId, TestData.StartDate, TestData.EndDate, CancellationToken.None);

            model.ShouldNotBeNull();
            model.SettlementMonthModels.ShouldNotBeNull();
            model.SettlementMonthModels.ShouldNotBeEmpty();
            model.SettlementMonthModels.Count.ShouldBe(TestData.SettlementByMonthModel.SettlementMonthModels.Count);
        }

        [Fact]
        public async Task ReportingManager_GetTransactionsForMerchantByWeek_DataReturned()
        {
            Mock<IEstateReportingRepository> repository = new Mock<IEstateReportingRepository>();
            Mock<IEstateReportingRepositoryForReports> repositoryForReports = new Mock<IEstateReportingRepositoryForReports>();
            repositoryForReports.Setup(r => r.GetTransactionsForMerchantByWeek(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(TestData.TransactionsByWeekModel);

            ReportingManager manager = new ReportingManager(repository.Object, repositoryForReports.Object);

            TransactionsByWeekModel model = await manager.GetTransactionsForMerchantByWeek(TestData.EstateId, TestData.MerchantId, TestData.StartDate, TestData.EndDate, CancellationToken.None);

            model.ShouldNotBeNull();
            model.TransactionWeekModels.ShouldNotBeNull();
            model.TransactionWeekModels.ShouldNotBeEmpty();
            model.TransactionWeekModels.Count.ShouldBe(TestData.TransactionsByWeekModel.TransactionWeekModels.Count);
        }

        [Fact]
        public async Task ReportingManager_GetSettlementForMerchantByWeek_DataReturned()
        {
            Mock<IEstateReportingRepository> repository = new Mock<IEstateReportingRepository>();
            Mock<IEstateReportingRepositoryForReports> repositoryForReports = new Mock<IEstateReportingRepositoryForReports>();
            repositoryForReports.Setup(r => r.GetSettlementForMerchantByWeek(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<CancellationToken>()))
                                .ReturnsAsync(TestData.SettlementByWeekModel);

            ReportingManager manager = new ReportingManager(repository.Object, repositoryForReports.Object);

            SettlementByWeekModel model = await manager.GetSettlementForMerchantByWeek(TestData.EstateId, TestData.MerchantId, TestData.StartDate, TestData.EndDate, CancellationToken.None);

            model.ShouldNotBeNull();
            model.SettlementWeekModels.ShouldNotBeNull();
            model.SettlementWeekModels.ShouldNotBeEmpty();
            model.SettlementWeekModels.Count.ShouldBe(TestData.SettlementByWeekModel.SettlementWeekModels.Count);
        }

        [Fact]
        public async Task ReportingManager_GetTransactionsForMerchantByMonth_DataReturned()
        {
            Mock<IEstateReportingRepository> repository = new Mock<IEstateReportingRepository>();
            Mock<IEstateReportingRepositoryForReports> repositoryForReports = new Mock<IEstateReportingRepositoryForReports>();
            repositoryForReports.Setup(r => r.GetTransactionsForMerchantByMonth(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(TestData.TransactionsByMonthModel);

            ReportingManager manager = new ReportingManager(repository.Object, repositoryForReports.Object);

            TransactionsByMonthModel model = await manager.GetTransactionsForMerchantByMonth(TestData.EstateId, TestData.MerchantId, TestData.StartDate, TestData.EndDate, CancellationToken.None);

            model.ShouldNotBeNull();
            model.TransactionMonthModels.ShouldNotBeNull();
            model.TransactionMonthModels.ShouldNotBeEmpty();
            model.TransactionMonthModels.Count.ShouldBe(TestData.TransactionsByMonthModel.TransactionMonthModels.Count);
        }

        [Fact]
        public async Task ReportingManager_GetSettlementForMerchantByMonth_DataReturned()
        {
            Mock<IEstateReportingRepository> repository = new Mock<IEstateReportingRepository>();
            Mock<IEstateReportingRepositoryForReports> repositoryForReports = new Mock<IEstateReportingRepositoryForReports>();
            repositoryForReports.Setup(r => r.GetSettlementForMerchantByMonth(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<CancellationToken>()))
                                .ReturnsAsync(TestData.SettlementByMonthModel);

            ReportingManager manager = new ReportingManager(repository.Object, repositoryForReports.Object);

            SettlementByMonthModel model = await manager.GetSettlementForMerchantByMonth(TestData.EstateId, TestData.MerchantId, TestData.StartDate, TestData.EndDate, CancellationToken.None);

            model.ShouldNotBeNull();
            model.SettlementMonthModels.ShouldNotBeNull();
            model.SettlementMonthModels.ShouldNotBeEmpty();
            model.SettlementMonthModels.Count.ShouldBe(TestData.SettlementByMonthModel.SettlementMonthModels.Count);
        }

        [Theory]
        [InlineData(SortDirection.Ascending, SortField.Count)]
        [InlineData(SortDirection.Descending, SortField.Count)]
        [InlineData(SortDirection.Ascending, SortField.Value)]
        [InlineData(SortDirection.Descending, SortField.Value)]
        public async Task ReportingManager_GetTransactionsForEstateByMerchant_DataReturned(SortDirection sortDirection, SortField sortField)
        {
            Mock<IEstateReportingRepository> repository = new Mock<IEstateReportingRepository>();
            Mock<IEstateReportingRepositoryForReports> repositoryForReports = new Mock<IEstateReportingRepositoryForReports>();
            repositoryForReports.Setup(r => r.GetTransactionsForEstateByMerchant(It.IsAny<Guid>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<Int32>(), It.IsAny<EstateReporting.Repository.SortField>(),It.IsAny<EstateReporting.Repository.SortDirection>() , It.IsAny<CancellationToken>()))
                      .ReturnsAsync(TestData.TransactionsByMerchantModel);

            ReportingManager manager = new ReportingManager(repository.Object, repositoryForReports.Object);

            TransactionsByMerchantModel model = await manager.GetTransactionsForEstateByMerchant(TestData.EstateId, TestData.StartDate, TestData.EndDate, 5, sortField, sortDirection, CancellationToken.None);

            model.ShouldNotBeNull();
            model.TransactionMerchantModels.ShouldNotBeNull();
            model.TransactionMerchantModels.ShouldNotBeEmpty();
            model.TransactionMerchantModels.Count.ShouldBe(TestData.TransactionsByMerchantModel.TransactionMerchantModels.Count);
        }

        [Theory]
        [InlineData(SortDirection.Ascending, SortField.Count)]
        [InlineData(SortDirection.Descending, SortField.Count)]
        [InlineData(SortDirection.Ascending, SortField.Value)]
        [InlineData(SortDirection.Descending, SortField.Value)]
        public async Task ReportingManager_GetSettlementForEstateByMerchant_DataReturned(SortDirection sortDirection, SortField sortField)
        {
            Mock<IEstateReportingRepository> repository = new Mock<IEstateReportingRepository>();
            Mock<IEstateReportingRepositoryForReports> repositoryForReports = new Mock<IEstateReportingRepositoryForReports>();
            repositoryForReports.Setup(r => r.GetSettlementForEstateByMerchant(It.IsAny<Guid>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<Int32>(), It.IsAny<EstateReporting.Repository.SortField>(), It.IsAny<EstateReporting.Repository.SortDirection>(), It.IsAny<CancellationToken>()))
                                .ReturnsAsync(TestData.SettlementByMerchantModel);

            ReportingManager manager = new ReportingManager(repository.Object, repositoryForReports.Object);

            SettlementByMerchantModel model = await manager.GetSettlementForEstateByMerchant(TestData.EstateId, TestData.StartDate, TestData.EndDate, 5, sortField, sortDirection, CancellationToken.None);

            model.ShouldNotBeNull();
            model.SettlementMerchantModels.ShouldNotBeNull();
            model.SettlementMerchantModels.ShouldNotBeEmpty();
            model.SettlementMerchantModels.Count.ShouldBe(TestData.SettlementByMerchantModel.SettlementMerchantModels.Count);
        }

        [Theory]
        [InlineData(SortDirection.Ascending, SortField.Count)]
        [InlineData(SortDirection.Descending, SortField.Count)]
        [InlineData(SortDirection.Ascending, SortField.Value)]
        [InlineData(SortDirection.Descending, SortField.Value)]
        public async Task ReportingManager_GetTransactionsForEstateByOperator_DataReturned(SortDirection sortDirection, SortField sortField)
        {
            Mock<IEstateReportingRepository> repository = new Mock<IEstateReportingRepository>();
            Mock<IEstateReportingRepositoryForReports> repositoryForReports = new Mock<IEstateReportingRepositoryForReports>();
            repositoryForReports.Setup(r => r.GetTransactionsForEstateByOperator(It.IsAny<Guid>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<Int32>(), It.IsAny<EstateReporting.Repository.SortField>(), It.IsAny<EstateReporting.Repository.SortDirection>(), It.IsAny<CancellationToken>()))
                                .ReturnsAsync(TestData.TransactionsByOperatorModel);

            ReportingManager manager = new ReportingManager(repository.Object, repositoryForReports.Object);

            TransactionsByOperatorModel model = await manager.GetTransactionsForEstateByOperator(TestData.EstateId, TestData.StartDate, TestData.EndDate, 5, sortField, sortDirection, CancellationToken.None);

            model.ShouldNotBeNull();
            model.TransactionOperatorModels.ShouldNotBeNull();
            model.TransactionOperatorModels.ShouldNotBeEmpty();
            model.TransactionOperatorModels.Count.ShouldBe(TestData.TransactionsByOperatorModel.TransactionOperatorModels.Count);
        }

        [Theory]
        [InlineData(SortDirection.Ascending, SortField.Count)]
        [InlineData(SortDirection.Descending, SortField.Count)]
        [InlineData(SortDirection.Ascending, SortField.Value)]
        [InlineData(SortDirection.Descending, SortField.Value)]
        public async Task ReportingManager_GetSettlementForEstateByOperator_DataReturned(SortDirection sortDirection, SortField sortField)
        {
            Mock<IEstateReportingRepository> repository = new Mock<IEstateReportingRepository>();
            Mock<IEstateReportingRepositoryForReports> repositoryForReports = new Mock<IEstateReportingRepositoryForReports>();
            repositoryForReports.Setup(r => r.GetSettlementForEstateByOperator(It.IsAny<Guid>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<Int32>(), It.IsAny<EstateReporting.Repository.SortField>(), It.IsAny<EstateReporting.Repository.SortDirection>(), It.IsAny<CancellationToken>()))
                                .ReturnsAsync(TestData.SettlementByOperatorModel);

            ReportingManager manager = new ReportingManager(repository.Object, repositoryForReports.Object);

            SettlementByOperatorModel model = await manager.GetSettlementForEstateByOperator(TestData.EstateId, TestData.StartDate, TestData.EndDate, 5, sortField, sortDirection, CancellationToken.None);

            model.ShouldNotBeNull();
            model.SettlementOperatorModels.ShouldNotBeNull();
            model.SettlementOperatorModels.ShouldNotBeEmpty();
            model.SettlementOperatorModels.Count.ShouldBe(TestData.SettlementByOperatorModel.SettlementOperatorModels.Count);
        }
    }
}
