using System;
using Xunit;

namespace EstateReporting.Tests
{
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
        public void ModelFactory_ConvertFrom_TransactionsByMerchantModel_NullTransactionMerchantModelList_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionsByMerchantModel model = TestData.TransactionsByMerchantModelNullTransactionMerchantModelList;

            TransactionsByMerchantResponse response = factory.ConvertFrom(model);

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
        public void ModelFactory_ConvertFrom_TransactionMerchantModelNull_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionMerchantModel model = null;

            TransactionMerchantResponse response = factory.ConvertFrom(model);

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
        public void ModelFactory_ConvertFrom_TransactionsByDayModel_NullTransactionDayModelList_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionsByDayModel model = TestData.TransactionsByDayModelNullTransactionDayModelList;

            TransactionsByDayResponse response = factory.ConvertFrom(model);

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
        public void ModelFactory_ConvertFrom_TransactionDayModelNull_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionDayModel model = null;

            TransactionDayResponse response = factory.ConvertFrom(model);

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
        public void ModelFactory_ConvertFrom_TransactionsByWeekModel_NullTransactionWeekModelList_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionsByWeekModel model = TestData.TransactionsByWeekModelNullTransactionWeekModelList;

            TransactionsByWeekResponse response = factory.ConvertFrom(model);

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
        public void ModelFactory_ConvertFrom_TransactionWeekModelNull_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionWeekModel model = null;

            TransactionWeekResponse response = factory.ConvertFrom(model);

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
        public void ModelFactory_ConvertFrom_TransactionMonthModelNull_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionMonthModel model = null;

            TransactionMonthResponse response = factory.ConvertFrom(model);

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
        public void ModelFactory_ConvertFrom_TransactionsByMonthModel_NullTransactionMonthModelList_ModelConverted()
        {
            ModelFactory factory = new ModelFactory();
            TransactionsByMonthModel model = TestData.TransactionsByMonthModelNullTransactionMonthModelList;

            TransactionsByMonthResponse response = factory.ConvertFrom(model);

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
    }
}
