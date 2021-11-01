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
    using DTOSortDirection = DataTransferObjects.SortDirection;
    using ModelSortDirection = BusinessLogic.SortDirection;
    using DTOSortField = DataTransferObjects.SortField;
    using ModelSortField = BusinessLogic.SortField;

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
        
        [Theory]
        [InlineData(DTOSortDirection.Descending, ModelSortDirection.Descending)]
        [InlineData(DTOSortDirection.Ascending, ModelSortDirection.Ascending)]
        public void ModelFactory_ConvertFrom_SortDirection_ModelConverted(DTOSortDirection input, ModelSortDirection expectedOutput)
        {
            ModelFactory factory = new ModelFactory();
            ModelSortDirection output = factory.ConvertFrom(input);
            output.ShouldBe(expectedOutput);
        }

        [Theory]
        [InlineData(DTOSortField.Count, ModelSortField.Count)]
        [InlineData(DTOSortField.Value, ModelSortField.Value)]
        public void ModelFactory_ConvertFrom_SortField_ModelConverted(DTOSortField input, ModelSortField expectedOutput)
        {
            ModelFactory factory = new ModelFactory();
            ModelSortField output = factory.ConvertFrom(input);
            output.ShouldBe(expectedOutput);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TransactionsByMerchantModel_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionsByMerchantModel model = TestData.TransactionsByMerchantModel;

            TransactionsByMerchantResponse response = factory.ConvertFrom(model);

            response.TransactionMerchantResponses.ShouldNotBeNull();
            response.TransactionMerchantResponses.ShouldNotBeEmpty();

            foreach (TransactionMerchantResponse translated in response.TransactionMerchantResponses)
            {
                // Find the original record
                TransactionMerchantModel original = model.TransactionMerchantModels.SingleOrDefault(m => m.MerchantId == translated.MerchantId);
                original.ShouldNotBeNull();

                translated.ValueOfTransactions.ShouldBe(original.ValueOfTransactions);
                translated.CurrencyCode.ShouldBe(original.CurrencyCode);
                translated.NumberOfTransactions.ShouldBe(original.NumberOfTransactions);
            }
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementByMerchantModel_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementByMerchantModel model = TestData.SettlementByMerchantModel;

            SettlementByMerchantResponse response = factory.ConvertFrom(model);

            response.SettlementMerchantResponses.ShouldNotBeNull();
            response.SettlementMerchantResponses.ShouldNotBeEmpty();

            foreach (SettlementMerchantResponse translated in response.SettlementMerchantResponses)
            {
                // Find the original record
                SettlementMerchantModel original = model.SettlementMerchantModels.SingleOrDefault(m => m.MerchantId == translated.MerchantId);
                original.ShouldNotBeNull();

                translated.ValueOfSettlement.ShouldBe(original.ValueOfSettlement);
                translated.CurrencyCode.ShouldBe(original.CurrencyCode);
                translated.NumberOfTransactionsSettled.ShouldBe(original.NumberOfTransactionsSettled);
            }
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TransactionsByMerchantModel_NullTransactionMerchantModelList_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionsByMerchantModel model = TestData.TransactionsByMerchantModelNullTransactionMerchantModelList;

            TransactionsByMerchantResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementByMerchantModel_NullSettlementMerchantModelList_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementByMerchantModel model = TestData.SettlementByMerchantModelNullSettlementMerchantModelList;

            SettlementByMerchantResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TransactionsByMerchantModel_EmptyTransactionMerchantModelList_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionsByMerchantModel model = TestData.TransactionsByMerchantModelEmptyTransactionMerchantModelList;

            TransactionsByMerchantResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementByMerchantModel_EmptySettlementMerchantModelList_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementByMerchantModel model = TestData.SettlementByMerchantModelEmptySettlementMerchantModelList;

            SettlementByMerchantResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TransactionMerchantModel_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionMerchantModel model = TestData.TransactionMerchantModel;

            TransactionMerchantResponse response = factory.ConvertFrom(model);

            response.ShouldNotBeNull();

            response.MerchantId.ShouldBe(model.MerchantId);
            response.MerchantName.ShouldBe(model.MerchantName);
            response.ValueOfTransactions.ShouldBe(model.ValueOfTransactions);
            response.CurrencyCode.ShouldBe(model.CurrencyCode);
            response.NumberOfTransactions.ShouldBe(model.NumberOfTransactions);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementMerchantModel_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementMerchantModel model = TestData.SettlementMerchantModel;

            SettlementMerchantResponse response = factory.ConvertFrom(model);

            response.ShouldNotBeNull();

            response.MerchantId.ShouldBe(model.MerchantId);
            response.MerchantName.ShouldBe(model.MerchantName);
            response.ValueOfSettlement.ShouldBe(model.ValueOfSettlement);
            response.CurrencyCode.ShouldBe(model.CurrencyCode);
            response.NumberOfTransactionsSettled.ShouldBe(model.NumberOfTransactionsSettled);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TransactionMerchantModelNull_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionMerchantModel model = null;

            TransactionMerchantResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementMerchantModelNull_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementMerchantModel model = null;

            SettlementMerchantResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TransactionsByDayModel_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionsByDayModel model = TestData.TransactionsByDayModel;

            TransactionsByDayResponse response = factory.ConvertFrom(model);

            response.TransactionDayResponses.ShouldNotBeNull();
            response.TransactionDayResponses.ShouldNotBeEmpty();

            foreach (TransactionDayResponse translated in response.TransactionDayResponses)
            {
                // Find the original record
                TransactionDayModel original = model.TransactionDayModels.SingleOrDefault(m => m.Date == translated.Date);
                original.ShouldNotBeNull();
                
                translated.ValueOfTransactions.ShouldBe(original.ValueOfTransactions);
                translated.CurrencyCode.ShouldBe(original.CurrencyCode);
                translated.NumberOfTransactions.ShouldBe(original.NumberOfTransactions);
            }
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementByDayModel_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementByDayModel model = TestData.SettlementByDayModel;

            SettlementByDayResponse response = factory.ConvertFrom(model);

            response.SettlementDayResponses.ShouldNotBeNull();
            response.SettlementDayResponses.ShouldNotBeEmpty();

            foreach (SettlementDayResponse translated in response.SettlementDayResponses)
            {
                // Find the original record
                SettlementDayModel original = model.SettlementDayModels.SingleOrDefault(m => m.Date == translated.Date);
                original.ShouldNotBeNull();

                translated.ValueOfSettlement.ShouldBe(original.ValueOfSettlement);
                translated.CurrencyCode.ShouldBe(original.CurrencyCode);
                translated.NumberOfTransactionsSettled.ShouldBe(original.NumberOfTransactionsSettled);
            }
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TransactionsByDayModel_NullTransactionDayModelList_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionsByDayModel model = TestData.TransactionsByDayModelNullTransactionDayModelList;

            TransactionsByDayResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementByDayModel_NullSettlementDayModelList_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementByDayModel model = TestData.SettlementByDayModelNullSettlementDayModelList;

            SettlementByDayResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TransactionsByDayModel_EmptyTransactionDayModelList_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionsByDayModel model = TestData.TransactionsByDayModelEmptyTransactionDayModelList;

            TransactionsByDayResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementByDayModel_EmptySettlemnentDayModelList_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementByDayModel model = TestData.SettlementByDayModelEmptySettlementDayModelList;

            SettlementByDayResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TransactionDayModel_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionDayModel model = TestData.TransactionDayModel;

            TransactionDayResponse response = factory.ConvertFrom(model);

            response.ShouldNotBeNull();

            response.Date.ShouldBe(model.Date);
            response.ValueOfTransactions.ShouldBe(model.ValueOfTransactions);
            response.CurrencyCode.ShouldBe(model.CurrencyCode);
            response.NumberOfTransactions.ShouldBe(model.NumberOfTransactions);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementDayModel_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementDayModel model = TestData.SettlementDayModel;

            SettlementDayResponse response = factory.ConvertFrom(model);

            response.ShouldNotBeNull();

            response.Date.ShouldBe(model.Date);
            response.ValueOfSettlement.ShouldBe(model.ValueOfSettlement);
            response.CurrencyCode.ShouldBe(model.CurrencyCode);
            response.NumberOfTransactionsSettled.ShouldBe(model.NumberOfTransactionsSettled);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TransactionDayModelNull_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionDayModel model = null;

            TransactionDayResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementDayModelNull_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementDayModel model = null;

            SettlementDayResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TransactionsByWeekModel_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionsByWeekModel model = TestData.TransactionsByWeekModel;

            TransactionsByWeekResponse response = factory.ConvertFrom(model);

            response.TransactionWeekResponses.ShouldNotBeNull();
            response.TransactionWeekResponses.ShouldNotBeEmpty();

            foreach (TransactionWeekResponse translated in response.TransactionWeekResponses)
            {
                // Find the original record
                TransactionWeekModel original = model.TransactionWeekModels.SingleOrDefault(m => m.WeekNumber == translated.WeekNumber && m.Year == translated.Year);
                original.ShouldNotBeNull();

                translated.ValueOfTransactions.ShouldBe(original.ValueOfTransactions);
                translated.CurrencyCode.ShouldBe(original.CurrencyCode);
                translated.NumberOfTransactions.ShouldBe(original.NumberOfTransactions);
            }
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementByWeekModel_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementByWeekModel model = TestData.SettlementByWeekModel;

            SettlementByWeekResponse response = factory.ConvertFrom(model);

            response.SettlementWeekResponses.ShouldNotBeNull();
            response.SettlementWeekResponses.ShouldNotBeEmpty();

            foreach (SettlementWeekResponse translated in response.SettlementWeekResponses)
            {
                // Find the original record
                SettlementWeekModel original = model.SettlementWeekModels.SingleOrDefault(m => m.WeekNumber == translated.WeekNumber && m.Year == translated.Year);
                original.ShouldNotBeNull();

                translated.ValueOfSettlement.ShouldBe(original.ValueOfSettlement);
                translated.CurrencyCode.ShouldBe(original.CurrencyCode);
                translated.NumberOfTransactionsSettled.ShouldBe(original.NumberOfTransactionsSettled);
            }
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TransactionsByWeekModel_NullTransactionWeekModelList_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionsByWeekModel model = TestData.TransactionsByWeekModelNullTransactionWeekModelList;

            TransactionsByWeekResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementByWeekModel_NullSettlementWeekModelList_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementByWeekModel model = TestData.SettlementByWeekModelNullSettlementWeekModelList;

            SettlementByWeekResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TransactionsByWeekModel_EmptyTransactionWeekModelList_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionsByWeekModel model = TestData.TransactionsByWeekModelEmptyTransactionWeekModelList;

            TransactionsByWeekResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementByWeekModel_EmptySettlementWeekModelList_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementByWeekModel model = TestData.SettlementByWeekModelEmptySettlementWeekModelList;

            SettlementByWeekResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TransactionWeekModel_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionWeekModel model = TestData.TransactionWeekModel;

            TransactionWeekResponse response = factory.ConvertFrom(model);

            response.ShouldNotBeNull();

            response.WeekNumber.ShouldBe(model.WeekNumber);
            response.Year.ShouldBe(model.Year);
            response.ValueOfTransactions.ShouldBe(model.ValueOfTransactions);
            response.CurrencyCode.ShouldBe(model.CurrencyCode);
            response.NumberOfTransactions.ShouldBe(model.NumberOfTransactions);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementWeekModel_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementWeekModel model = TestData.SettlementWeekModel;

            SettlementWeekResponse response = factory.ConvertFrom(model);

            response.ShouldNotBeNull();

            response.WeekNumber.ShouldBe(model.WeekNumber);
            response.Year.ShouldBe(model.Year);
            response.ValueOfSettlement.ShouldBe(model.ValueOfSettlement);
            response.CurrencyCode.ShouldBe(model.CurrencyCode);
            response.NumberOfTransactionsSettled.ShouldBe(model.NumberOfTransactionsSettled);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TransactionWeekModelNull_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionWeekModel model = null;

            TransactionWeekResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementWeekModelNull_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementWeekModel model = null;

            SettlementWeekResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TransactionMonthModel_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionMonthModel model = TestData.TransactionMonthModel;

            TransactionMonthResponse response = factory.ConvertFrom(model);

            response.ShouldNotBeNull();

            response.MonthNumber.ShouldBe(model.MonthNumber);
            response.Year.ShouldBe(model.Year);
            response.ValueOfTransactions.ShouldBe(model.ValueOfTransactions);
            response.CurrencyCode.ShouldBe(model.CurrencyCode);
            response.NumberOfTransactions.ShouldBe(model.NumberOfTransactions);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementMonthModel_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementMonthModel model = TestData.SettlementMonthModel;

            SettlementMonthResponse response = factory.ConvertFrom(model);

            response.ShouldNotBeNull();

            response.MonthNumber.ShouldBe(model.MonthNumber);
            response.Year.ShouldBe(model.Year);
            response.ValueOfSettlement.ShouldBe(model.ValueOfSettlement);
            response.CurrencyCode.ShouldBe(model.CurrencyCode);
            response.NumberOfTransactionsSettled.ShouldBe(model.NumberOfTransactionsSettled);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TransactionMonthModelNull_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionMonthModel model = null;

            TransactionMonthResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementMonthModelNull_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementMonthModel model = null;

            SettlementMonthResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TransactionsByMonthModel_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionsByMonthModel model = TestData.TransactionsByMonthModel;

            TransactionsByMonthResponse response = factory.ConvertFrom(model);

            response.TransactionMonthResponses.ShouldNotBeNull();
            response.TransactionMonthResponses.ShouldNotBeEmpty();

            foreach (TransactionMonthResponse translated in response.TransactionMonthResponses)
            {
                // Find the original record
                TransactionMonthModel original = model.TransactionMonthModels.SingleOrDefault(m => m.MonthNumber == translated.MonthNumber && m.Year == translated.Year);
                original.ShouldNotBeNull();

                translated.ValueOfTransactions.ShouldBe(original.ValueOfTransactions);
                translated.CurrencyCode.ShouldBe(original.CurrencyCode);
                translated.NumberOfTransactions.ShouldBe(original.NumberOfTransactions);
            }
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementByMonthModel_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementByMonthModel model = TestData.SettlementByMonthModel;

            SettlementByMonthResponse response = factory.ConvertFrom(model);

            response.SettlementMonthResponses.ShouldNotBeNull();
            response.SettlementMonthResponses.ShouldNotBeEmpty();

            foreach (SettlementMonthResponse translated in response.SettlementMonthResponses)
            {
                // Find the original record
                SettlementMonthModel original = model.SettlementMonthModels.SingleOrDefault(m => m.MonthNumber == translated.MonthNumber && m.Year == translated.Year);
                original.ShouldNotBeNull();

                translated.ValueOfSettlement.ShouldBe(original.ValueOfSettlement);
                translated.CurrencyCode.ShouldBe(original.CurrencyCode);
                translated.NumberOfTransactionsSettled.ShouldBe(original.NumberOfTransactionsSettled);
            }
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TransactionsByMonthModel_NullTransactionMonthModelList_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionsByMonthModel model = TestData.TransactionsByMonthModelNullTransactionMonthModelList;

            TransactionsByMonthResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementByMonthModel_NullSettlementMonthModelList_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementByMonthModel model = TestData.SettlementByMonthModelNullSettlementMonthModelList;

            SettlementByMonthResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TransactionsByMonthModel_EmptyTransactionMonthModelList_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionsByMonthModel model = TestData.TransactionsByMonthModelEmptyTransactionMonthModelList;

            TransactionsByMonthResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementByMonthModel_EmptySettlementMonthModelList_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementByMonthModel model = TestData.SettlementByMonthModelEmptySettlementMonthModelList;

            SettlementByMonthResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TransactionOperatorModel_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionOperatorModel model = TestData.TransactionOperatorModel;

            TransactionOperatorResponse response = factory.ConvertFrom(model);

            response.ShouldNotBeNull();

            response.OperatorName.ShouldBe(model.OperatorName);
            response.ValueOfTransactions.ShouldBe(model.ValueOfTransactions);
            response.CurrencyCode.ShouldBe(model.CurrencyCode);
            response.NumberOfTransactions.ShouldBe(model.NumberOfTransactions);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementOperatorModel_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementOperatorModel model = TestData.SettlementOperatorModel;

            SettlementOperatorResponse response = factory.ConvertFrom(model);

            response.ShouldNotBeNull();

            response.OperatorName.ShouldBe(model.OperatorName);
            response.ValueOfSettlement.ShouldBe(model.ValueOfSettlement);
            response.CurrencyCode.ShouldBe(model.CurrencyCode);
            response.NumberOfTransactionsSettled.ShouldBe(model.NumberOfTransactionsSettled);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TransactionOperatorModelNull_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionOperatorModel model = null;

            TransactionOperatorResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementOperatorModelNull_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementOperatorModel model = null;

            SettlementOperatorResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TransactionsByOperatorModel_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionsByOperatorModel model = TestData.TransactionsByOperatorModel;

            TransactionsByOperatorResponse response = factory.ConvertFrom(model);

            response.TransactionOperatorResponses.ShouldNotBeNull();
            response.TransactionOperatorResponses.ShouldNotBeEmpty();

            foreach (TransactionOperatorResponse translated in response.TransactionOperatorResponses)
            {
                // Find the original record
                TransactionOperatorModel original = model.TransactionOperatorModels.SingleOrDefault(m => m.OperatorName == translated.OperatorName);
                original.ShouldNotBeNull();

                translated.ValueOfTransactions.ShouldBe(original.ValueOfTransactions);
                translated.CurrencyCode.ShouldBe(original.CurrencyCode);
                translated.NumberOfTransactions.ShouldBe(original.NumberOfTransactions);
            }
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementByOperatorModel_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementByOperatorModel model = TestData.SettlementByOperatorModel;

            SettlementByOperatorResponse response = factory.ConvertFrom(model);

            response.SettlementOperatorResponses.ShouldNotBeNull();
            response.SettlementOperatorResponses.ShouldNotBeEmpty();

            foreach (SettlementOperatorResponse translated in response.SettlementOperatorResponses)
            {
                // Find the original record
                SettlementOperatorModel original = model.SettlementOperatorModels.SingleOrDefault(m => m.OperatorName == translated.OperatorName);
                original.ShouldNotBeNull();

                translated.ValueOfSettlement.ShouldBe(original.ValueOfSettlement);
                translated.CurrencyCode.ShouldBe(original.CurrencyCode);
                translated.NumberOfTransactionsSettled.ShouldBe(original.NumberOfTransactionsSettled);
            }
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TransactionsByOperatorModel_NullTransactionOperatorModelList_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionsByOperatorModel model = TestData.TransactionsByOperatorModelNullTransactionOperatorModelList;

            TransactionsByOperatorResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementByOperatorModel_NullSettlementOperatorModelList_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementByOperatorModel model = TestData.SettlementByOperatorModelNullSettlementOperatorModelList;

            SettlementByOperatorResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_TransactionsByOperatorModel_EmptyTransactionOperatorModelList_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionsByOperatorModel model = TestData.TransactionsByOperatorModelEmptyTransactionOperatorModelList;

            TransactionsByOperatorResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_SettlementByOperatorModel_EmptySettlementOperatorModelList_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            SettlementByOperatorModel model = TestData.SettlementByOperatorModelEmptySettlementOperatorModelList;

            SettlementByOperatorResponse response = factory.ConvertFrom(model);

            response.ShouldBeNull();
        }

    }
}
