namespace EstateReporting.Common.Examples
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using DataTransferObjects;
    using Swashbuckle.AspNetCore.Filters;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Swashbuckle.AspNetCore.Filters.IExamplesProvider{EstateReporting.DataTransferObjects.TransactionDayResponse}" />
    [ExcludeFromCodeCoverage]
    public class TransactionDayResponseExample : IExamplesProvider<TransactionDayResponse>
    {
        /// <summary>
        /// Gets the examples.
        /// </summary>
        /// <returns></returns>
        public TransactionDayResponse GetExamples()
        {
            return new TransactionDayResponse
                   {
                       CurrencyCode = String.Empty,
                       Date = ExampleData.TransactionDayResponseDate,
                       NumberOfTransactions = ExampleData.TransactionDayNumberOfTransactions,
                       ValueOfTransactions = ExampleData.TransactionDayValueOfTransactions
                   };
        }
    }
}