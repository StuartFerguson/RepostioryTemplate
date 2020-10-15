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

    public class ModelFactoryTests
    {
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
    }
}
