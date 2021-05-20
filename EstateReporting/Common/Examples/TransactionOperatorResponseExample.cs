namespace EstateReporting.Common.Examples
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using DataTransferObjects;
    using Swashbuckle.AspNetCore.Filters;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Swashbuckle.AspNetCore.Filters.IExamplesProvider{EstateReporting.DataTransferObjects.TransactionOperatorResponse}" />
    [ExcludeFromCodeCoverage]
    public class TransactionOperatorResponseExample : IExamplesProvider<TransactionOperatorResponse>
    {
        /// <summary>
        /// Gets the examples.
        /// </summary>
        /// <returns></returns>
        public TransactionOperatorResponse GetExamples()
        {
            return new TransactionOperatorResponse
                   {
                       NumberOfTransactions = ExampleData.TransactionOperatorNumberOfTransactions,
                       ValueOfTransactions = ExampleData.TransactionOperatorValueOfTransactions,
                       CurrencyCode = String.Empty,
                       OperatorName = ExampleData.OperatorName
                   };
        }
    }
}