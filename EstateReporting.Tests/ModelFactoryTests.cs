using System;
using Xunit;

namespace EstateReporting.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using DataTransferObjects;
    using Factories;
    using Models;
    using Shouldly;
    using Testing;

    public class ModelFactoryTests
    {
        [Fact]
        public void ModelFactory_ConvertFrom_SettlementFeeModel_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementFeeResponse response = factory.ConvertFrom(TestData.SettlementFeeModel);

            response.ShouldSatisfyAllConditions(r => r.SettlementDate.ShouldBe(TestData.SettlementFeeModel.SettlementDate),
                                                r => r.SettlementId.ShouldBe(TestData.SettlementFeeModel.SettlementId),
                                                r => r.CalculatedValue.ShouldBe(TestData.SettlementFeeModel.CalculatedValue),
                                                r => r.FeeDescription.ShouldBe(TestData.SettlementFeeModel.FeeDescription),
                                                r => r.IsSettled.ShouldBe(TestData.SettlementFeeModel.IsSettled),
                                                r => r.MerchantId.ShouldBe(TestData.SettlementFeeModel.MerchantId),
                                                r => r.MerchantName.ShouldBe(TestData.SettlementFeeModel.MerchantName),
                                                r => r.TransactionId.ShouldBe(TestData.SettlementFeeModel.TransactionId));
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementFeeModel_ModelIsNull_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementFeeModel model = null;
            SettlementFeeResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementFeeModelList_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            var response = factory.ConvertFrom(TestData.SettlementFeeModels);

            response.ShouldNotBeNull();
            response.ShouldNotBeEmpty();
            response.Count.ShouldBe(TestData.SettlementFeeModels.Count);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementFeeModelList_ListIsNull_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            List<SettlementFeeModel> modelList = null;
            List<SettlementFeeResponse> response = factory.ConvertFrom(modelList);

            response.ShouldNotBeNull();
            response.ShouldBeEmpty();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementFeeModelList_ListIsEmpty_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            List<SettlementFeeModel> modelList = new List<SettlementFeeModel>();
            List<SettlementFeeResponse> response = factory.ConvertFrom(modelList);

            response.ShouldNotBeNull();
            response.ShouldBeEmpty();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementModel_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementResponse response = factory.ConvertFrom(TestData.SettlementModel);

            response.ShouldSatisfyAllConditions(r => r.SettlementDate.ShouldBe(TestData.SettlementModel.SettlementDate),
                                                r => r.SettlementId.ShouldBe(TestData.SettlementModel.SettlementId),
                                                r => r.NumberOfFeesSettled.ShouldBe(TestData.SettlementModel.NumberOfFeesSettled),
                                                r => r.ValueOfFeesSettled.ShouldBe(TestData.SettlementModel.ValueOfFeesSettled),
                                                r => r.IsCompleted.ShouldBe(TestData.SettlementModel.IsCompleted),
                                                r => r.SettlementFees.Count.ShouldBe(TestData.SettlementModel.SettlementFees.Count));
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementModel_ModelIsNull_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementModel model = null;
            SettlementResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementModelList_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            List<SettlementResponse> response = factory.ConvertFrom(TestData.SettlementModels);

            response.ShouldNotBeNull();
            response.ShouldNotBeEmpty();
            response.Count.ShouldBe(TestData.SettlementModels.Count);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementModelList_ListIsNull_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            List<SettlementModel> settlementModeList = null;
            List<SettlementResponse> response = factory.ConvertFrom(settlementModeList);

            response.ShouldNotBeNull();
            response.ShouldBeEmpty();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementModelList_ListIsEmpty_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            List<SettlementModel> settlementModeList = new List<SettlementModel>();
            List<SettlementResponse> response = factory.ConvertFrom(settlementModeList);

            response.ShouldNotBeNull();
            response.ShouldBeEmpty();
        }
    }
}
