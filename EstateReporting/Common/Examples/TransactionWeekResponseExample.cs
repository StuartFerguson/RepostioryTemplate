namespace EstateReporting.Common.Examples
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using DataTransferObjects;
    using Swashbuckle.AspNetCore.Filters;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Swashbuckle.AspNetCore.Filters.IExamplesProvider{EstateReporting.DataTransferObjects.TransactionWeekResponse}" />
    [ExcludeFromCodeCoverage]
    public class TransactionWeekResponseExample : IExamplesProvider<TransactionWeekResponse>
    {
        /// <summary>
        /// Gets the examples.
        /// </summary>
        /// <returns></returns>
        public TransactionWeekResponse GetExamples()
        {
            return new TransactionWeekResponse
                   {
                       CurrencyCode = String.Empty,
                       NumberOfTransactions = ExampleData.TransactionWeekNumberOfTransactions,
                       ValueOfTransactions = ExampleData.TransactionWeekValueOfTransactions,
                       WeekNumber = ExampleData.WeekNumber,
                       Year = ExampleData.YearNumber
                   };
        }
    }
}