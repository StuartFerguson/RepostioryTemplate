namespace EstateReporting.Common.Examples
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using DataTransferObjects;
    using Swashbuckle.AspNetCore.Filters;

    [ExcludeFromCodeCoverage]
    public class SettlementByDayResponseExample : IExamplesProvider<SettlementByDayResponse>
    {
        #region Methods

        /// <summary>
        /// Gets the examples.
        /// </summary>
        /// <returns></returns>
        public SettlementByDayResponse GetExamples()
        {
            return new SettlementByDayResponse
                   {
                       SettlementDayResponses = new List<SettlementDayResponse>
                                                {
                                                    new SettlementDayResponse
                                                    {
                                                        CurrencyCode = string.Empty,
                                                        Date = ExampleData.SettlementDayResponseDate,
                                                        NumberOfTransactionsSettled = ExampleData.SettlementDayNumberOfTransactions,
                                                        ValueOfSettlement = ExampleData.SettlementDayValueOfTransactions
                                                    }
                                                }
                   };
        }

        #endregion
    }
}