namespace EstateReporting.Common.Examples
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using DataTransferObjects;
    using Swashbuckle.AspNetCore.Filters;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Swashbuckle.AspNetCore.Filters.IExamplesProvider{EstateReporting.DataTransferObjects.TransactionsByMonthResponse}" />
    [ExcludeFromCodeCoverage]
    public class TransactionsByMonthResponseExample : IExamplesProvider<TransactionsByMonthResponse>
    {
        #region Methods

        /// <summary>
        /// Gets the examples.
        /// </summary>
        /// <returns></returns>
        public TransactionsByMonthResponse GetExamples()
        {
            return new TransactionsByMonthResponse
                   {
                       TransactionMonthResponses = new List<TransactionMonthResponse>
                                                   {
                                                       new TransactionMonthResponse
                                                       {
                                                           CurrencyCode = string.Empty,
                                                           NumberOfTransactions = ExampleData.TransactionMonthNumberOfTransactions,
                                                           ValueOfTransactions = ExampleData.TransactionMonthValueOfTransactions,
                                                           MonthNumber = ExampleData.MonthNumber,
                                                           Year = ExampleData.YearNumber
                                                       }
                                                   }
                   };
        }

        #endregion
    }
}