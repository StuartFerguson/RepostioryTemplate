namespace EstateReporting.Common.Examples
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using DataTransferObjects;
    using Swashbuckle.AspNetCore.Filters;

    [ExcludeFromCodeCoverage]
    public class SettlementByMonthResponseExample : IExamplesProvider<SettlementByMonthResponse>
    {
        #region Methods

        /// <summary>
        /// Gets the examples.
        /// </summary>
        /// <returns></returns>
        public SettlementByMonthResponse GetExamples()
        {
            return new SettlementByMonthResponse
                   {
                       SettlementMonthResponses = new List<SettlementMonthResponse>
                                                  {
                                                      new SettlementMonthResponse
                                                      {
                                                          CurrencyCode = string.Empty,
                                                          NumberOfTransactionsSettled = ExampleData.SettlementMonthNumberOfTransactions,
                                                          ValueOfSettlement = ExampleData.SettlementMonthValueOfTransactions,
                                                          MonthNumber = ExampleData.MonthNumber,
                                                          Year = ExampleData.YearNumber
                                                      }
                                                  }
                   };
        }

        #endregion
    }
}