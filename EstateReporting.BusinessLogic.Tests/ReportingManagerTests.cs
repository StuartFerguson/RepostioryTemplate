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
        public async Task ReportingManager_GetTransactionsForEstate_ByDate_DataReturned()
        {
            Mock<IEstateReportingRepository> repository = new Mock<IEstateReportingRepository>();
            repository.Setup(r => r.GetTransactionsForEstateByDate(It.IsAny<Guid>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(TestData.TransactionsByDayModel);

            ReportingManager manager = new ReportingManager(repository.Object);

            TransactionsByDayModel model = await manager.GetTransactionsForEstate(TestData.EstateId, TestData.StartDate, TestData.EndDate, GroupingType.ByDate, CancellationToken.None);

            model.ShouldNotBeNull();
            model.TransactionDayModels.ShouldNotBeNull();
            model.TransactionDayModels.ShouldNotBeEmpty();
            model.TransactionDayModels.Count.ShouldBe(TestData.TransactionsByDayModel.TransactionDayModels.Count);
        }

        [Fact]
        public async Task ReportingManager_GetTransactionsForMerchant_ByDate_DataReturned()
        {
            Mock<IEstateReportingRepository> repository = new Mock<IEstateReportingRepository>();
            repository.Setup(r => r.GetTransactionsForMerchantByDate(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(TestData.TransactionsByDayModel);

            ReportingManager manager = new ReportingManager(repository.Object);

            TransactionsByDayModel model = await manager.GetTransactionsForMerchant(TestData.EstateId, TestData.MerchantId, TestData.StartDate, TestData.EndDate, GroupingType.ByDate, CancellationToken.None);

            model.ShouldNotBeNull();
            model.TransactionDayModels.ShouldNotBeNull();
            model.TransactionDayModels.ShouldNotBeEmpty();
            model.TransactionDayModels.Count.ShouldBe(TestData.TransactionsByDayModel.TransactionDayModels.Count);
        }
    }
}
