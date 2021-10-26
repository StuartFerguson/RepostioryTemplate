namespace EstateReporting.Common.Examples
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using DataTransferObjects;
    using Swashbuckle.AspNetCore.Filters;

    [ExcludeFromCodeCoverage]
    public class SettlementWeekResponseExample : IExamplesProvider<SettlementWeekResponse>
    {
        /// <summary>
        /// Gets the examples.
        /// </summary>
        /// <returns></returns>
        public SettlementWeekResponse GetExamples()
        {
            return new SettlementWeekResponse
                   {
                       CurrencyCode = String.Empty,
                       NumberOfTransactionsSettled = ExampleData.SettlementWeekNumberOfTransactions,
                       ValueOfSettlement = ExampleData.SettlementWeekValueOfTransactions,
                       WeekNumber = ExampleData.WeekNumber,
                       Year = ExampleData.YearNumber
                   };
        }
    }
}