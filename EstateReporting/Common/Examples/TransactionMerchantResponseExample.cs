namespace EstateReporting.Common.Examples
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using DataTransferObjects;
    using Swashbuckle.AspNetCore.Filters;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Swashbuckle.AspNetCore.Filters.IExamplesProvider{EstateReporting.DataTransferObjects.TransactionMerchantResponse}" />
    [ExcludeFromCodeCoverage]
    public class TransactionMerchantResponseExample : IExamplesProvider<TransactionMerchantResponse>
    {
        /// <summary>
        /// Gets the examples.
        /// </summary>
        /// <returns></returns>
        public TransactionMerchantResponse GetExamples()
        {
            return new TransactionMerchantResponse
                   {
                       CurrencyCode = String.Empty,
                       NumberOfTransactions = ExampleData.TransactionMerchantNumberOfTransactions,
                       ValueOfTransactions = ExampleData.TransactionMerchantValueOfTransactions,
                       MerchantId = ExampleData.MerchantId,
                       MerchantName = ExampleData.MerchantName
                   };
        }
    }
}