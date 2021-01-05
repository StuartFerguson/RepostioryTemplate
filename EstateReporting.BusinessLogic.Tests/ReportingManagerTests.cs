using System;
using System.Collections.Generic;
using System.Text;

namespace EstateReporting.BusinessLogic.Tests
{
    using System.Threading;
    using System.Threading.Tasks;
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
    }
}
