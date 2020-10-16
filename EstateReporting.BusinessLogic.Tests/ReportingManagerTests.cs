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

    public class ReportingManagerTests
    {
        [Fact]
        public async Task ReportingManager_GetTransactionsForEstateByDate_DataReturned()
        {
            Mock<IEstateReportingRepository> repository = new Mock<IEstateReportingRepository>();
            repository.Setup(r => r.GetTransactionsForEstateByDate(It.IsAny<Guid>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(TestData.TransactionsByDayModel);

            ReportingManager manager = new ReportingManager(repository.Object);

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
            repository.Setup(r => r.GetTransactionsForMerchantByDate(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(TestData.TransactionsByDayModel);

            ReportingManager manager = new ReportingManager(repository.Object);

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
            repository.Setup(r => r.GetTransactionsForEstateByWeek(It.IsAny<Guid>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(TestData.TransactionsByWeekModel);

            ReportingManager manager = new ReportingManager(repository.Object);

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
            repository.Setup(r => r.GetTransactionsForEstateByMonth(It.IsAny<Guid>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(TestData.TransactionsByMonthModel);

            ReportingManager manager = new ReportingManager(repository.Object);

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
            repository.Setup(r => r.GetTransactionsForMerchantByWeek(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(TestData.TransactionsByWeekModel);

            ReportingManager manager = new ReportingManager(repository.Object);

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
            repository.Setup(r => r.GetTransactionsForMerchantByMonth(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(TestData.TransactionsByMonthModel);

            ReportingManager manager = new ReportingManager(repository.Object);

            TransactionsByMonthModel model = await manager.GetTransactionsForMerchantByMonth(TestData.EstateId, TestData.MerchantId, TestData.StartDate, TestData.EndDate, CancellationToken.None);

            model.ShouldNotBeNull();
            model.TransactionMonthModels.ShouldNotBeNull();
            model.TransactionMonthModels.ShouldNotBeEmpty();
            model.TransactionMonthModels.Count.ShouldBe(TestData.TransactionsByMonthModel.TransactionMonthModels.Count);
        }
    }
}
