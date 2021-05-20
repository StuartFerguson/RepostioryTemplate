namespace EstateReporting.Common.Examples
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using DataTransferObjects;
    using Swashbuckle.AspNetCore.Filters;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Swashbuckle.AspNetCore.Filters.IExamplesProvider{EstateReporting.DataTransferObjects.TransactionsByDayResponse}" />
    [ExcludeFromCodeCoverage]
    public class TransactionsByDayResponseExample : IExamplesProvider<TransactionsByDayResponse>
    {
        #region Methods

        /// <summary>
        /// Gets the examples.
        /// </summary>
        /// <returns></returns>
        public TransactionsByDayResponse GetExamples()
        {
            return new TransactionsByDayResponse
                   {
                       TransactionDayResponses = new List<TransactionDayResponse>
                                                 {
                                                     new TransactionDayResponse
                                                     {
                                                         CurrencyCode = string.Empty,
                                                         Date = ExampleData.TransactionDayResponseDate,
                                                         NumberOfTransactions = ExampleData.TransactionDayNumberOfTransactions,
                                                         ValueOfTransactions = ExampleData.TransactionDayValueOfTransactions
                                                     }
                                                 }
                   };
        }

        #endregion
    }
}