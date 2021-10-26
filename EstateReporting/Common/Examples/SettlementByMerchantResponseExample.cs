namespace EstateReporting.Common.Examples
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using DataTransferObjects;
    using Swashbuckle.AspNetCore.Filters;

    [ExcludeFromCodeCoverage]
    public class SettlementByMerchantResponseExample : IExamplesProvider<SettlementByMerchantResponse>
    {
        #region Methods

        /// <summary>
        /// Gets the examples.
        /// </summary>
        /// <returns></returns>
        public SettlementByMerchantResponse GetExamples()
        {
            return new SettlementByMerchantResponse
                   {
                       SettlementMerchantResponses = new List<SettlementMerchantResponse>
                                                     {
                                                         new SettlementMerchantResponse
                                                         {
                                                             CurrencyCode = string.Empty,
                                                             NumberOfTransactionsSettled = ExampleData.SettlementMerchantNumberOfTransactions,
                                                             ValueOfSettlement = ExampleData.SettlementMerchantValueOfTransactions,
                                                             MerchantId = ExampleData.MerchantId,
                                                             MerchantName = ExampleData.MerchantName
                                                         }
                                                     }
                   };
        }

        #endregion
    }
}