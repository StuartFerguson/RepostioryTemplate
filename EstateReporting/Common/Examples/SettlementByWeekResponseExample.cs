namespace EstateReporting.Common.Examples
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using DataTransferObjects;
    using Swashbuckle.AspNetCore.Filters;

    [ExcludeFromCodeCoverage]
    public class SettlementByWeekResponseExample : IExamplesProvider<SettlementByWeekResponse>
    {
        #region Methods

        /// <summary>
        /// Gets the examples.
        /// </summary>
        /// <returns></returns>
        public SettlementByWeekResponse GetExamples()
        {
            return new SettlementByWeekResponse()
                   {
                       SettlementWeekResponses = new List<SettlementWeekResponse>
                                                 {
                                                     new SettlementWeekResponse
                                                     {
                                                         NumberOfTransactionsSettled = ExampleData.SettlementWeekNumberOfTransactions,
                                                         ValueOfSettlement = ExampleData.SettlementWeekValueOfTransactions,
                                                         CurrencyCode = string.Empty,
                                                         WeekNumber = ExampleData.WeekNumber,
                                                         Year = ExampleData.YearNumber
                                                     }
                                                 }
                   };
        }

        #endregion
    }
}