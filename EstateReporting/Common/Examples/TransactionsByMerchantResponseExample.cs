namespace EstateReporting.Common.Examples
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using DataTransferObjects;
    using Swashbuckle.AspNetCore.Filters;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Swashbuckle.AspNetCore.Filters.IExamplesProvider{EstateReporting.DataTransferObjects.TransactionsByMerchantResponse}" />
    [ExcludeFromCodeCoverage]
    public class TransactionsByMerchantResponseExample : IExamplesProvider<TransactionsByMerchantResponse>
    {
        #region Methods

        /// <summary>
        /// Gets the examples.
        /// </summary>
        /// <returns></returns>
        public TransactionsByMerchantResponse GetExamples()
        {
            return new TransactionsByMerchantResponse
                   {
                       TransactionMerchantResponses = new List<TransactionMerchantResponse>
                                                      {
                                                          new TransactionMerchantResponse
                                                          {
                                                              CurrencyCode = string.Empty,
                                                              NumberOfTransactions = ExampleData.TransactionMerchantNumberOfTransactions,
                                                              ValueOfTransactions = ExampleData.TransactionMerchantValueOfTransactions,
                                                              MerchantId = ExampleData.MerchantId,
                                                              MerchantName = ExampleData.MerchantName
                                                          }
                                                      }
                   };
        }

        #endregion
    }
}