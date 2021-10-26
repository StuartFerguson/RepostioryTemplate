namespace EstateReporting.Common.Examples
{
    using System.Diagnostics.CodeAnalysis;
    using DataTransferObjects;
    using Swashbuckle.AspNetCore.Filters;

    [ExcludeFromCodeCoverage]
    public class SettlementMonthResponseExample : IExamplesProvider<SettlementMonthResponse>
    {
        #region Methods

        /// <summary>
        /// Gets the examples.
        /// </summary>
        /// <returns></returns>
        public SettlementMonthResponse GetExamples()
        {
            return new SettlementMonthResponse
                   {
                       CurrencyCode = string.Empty,
                       NumberOfTransactionsSettled = ExampleData.SettlementMonthNumberOfTransactions,
                       ValueOfSettlement = ExampleData.SettlementMonthValueOfTransactions,
                       MonthNumber = ExampleData.MonthNumber,
                       Year = ExampleData.YearNumber
                   };
        }

        #endregion
    }

}