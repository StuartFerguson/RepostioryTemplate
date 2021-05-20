namespace EstateReporting.Common.Examples
{
    using System.Diagnostics.CodeAnalysis;
    using DataTransferObjects;
    using Swashbuckle.AspNetCore.Filters;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Swashbuckle.AspNetCore.Filters.IExamplesProvider{EstateReporting.DataTransferObjects.TransactionMonthResponse}" />
    [ExcludeFromCodeCoverage]
    public class TransactionMonthResponseExample : IExamplesProvider<TransactionMonthResponse>
    {
        #region Methods

        /// <summary>
        /// Gets the examples.
        /// </summary>
        /// <returns></returns>
        public TransactionMonthResponse GetExamples()
        {
            return new TransactionMonthResponse
                   {
                       CurrencyCode = string.Empty,
                       NumberOfTransactions = ExampleData.TransactionMonthNumberOfTransactions,
                       ValueOfTransactions = ExampleData.TransactionMonthValueOfTransactions,
                       MonthNumber = ExampleData.MonthNumber,
                       Year = ExampleData.YearNumber
                   };
        }

        #endregion
    }
}