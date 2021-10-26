namespace EstateReporting.Common.Examples
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using DataTransferObjects;
    using Swashbuckle.AspNetCore.Filters;

    [ExcludeFromCodeCoverage]
    public class SettlementDayResponseExample : IExamplesProvider<SettlementDayResponse>
    {
        /// <summary>
        /// Gets the examples.
        /// </summary>
        /// <returns></returns>
        public SettlementDayResponse GetExamples()
        {
            return new SettlementDayResponse
                   {
                       CurrencyCode = String.Empty,
                       Date = ExampleData.SettlementDayResponseDate,
                       NumberOfTransactionsSettled = ExampleData.SettlementDayNumberOfTransactions,
                       ValueOfSettlement = ExampleData.SettlementDayValueOfTransactions
                   };
        }
    }
}