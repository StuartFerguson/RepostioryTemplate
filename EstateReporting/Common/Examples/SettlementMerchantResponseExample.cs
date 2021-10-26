namespace EstateReporting.Common.Examples
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using DataTransferObjects;
    using Swashbuckle.AspNetCore.Filters;

    [ExcludeFromCodeCoverage]
    public class SettlementMerchantResponseExample : IExamplesProvider<SettlementMerchantResponse>
    {
        /// <summary>
        /// Gets the examples.
        /// </summary>
        /// <returns></returns>
        public SettlementMerchantResponse GetExamples()
        {
            return new SettlementMerchantResponse
                   {
                       CurrencyCode = String.Empty,
                       NumberOfTransactionsSettled = ExampleData.SettlementMerchantNumberOfTransactions,
                       ValueOfSettlement = ExampleData.SettlementMerchantValueOfTransactions,
                       MerchantId = ExampleData.MerchantId,
                       MerchantName = ExampleData.MerchantName
                   };
        }
    }
}