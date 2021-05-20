namespace EstateReporting.Common.Examples
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using DataTransferObjects;
    using Swashbuckle.AspNetCore.Filters;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Swashbuckle.AspNetCore.Filters.IExamplesProvider{EstateReporting.DataTransferObjects.TransactionsByOperatorResponse}" />
    [ExcludeFromCodeCoverage]
    public class TransactionsByOperatorResponseExample : IExamplesProvider<TransactionsByOperatorResponse>
    {
        /// <summary>
        /// Gets the examples.
        /// </summary>
        /// <returns></returns>
        public TransactionsByOperatorResponse GetExamples()
        {
            return new TransactionsByOperatorResponse
                   {
                       TransactionOperatorResponses = new List<TransactionOperatorResponse>
                                                      {
                                                          new TransactionOperatorResponse
                                                          {
                                                              NumberOfTransactions = ExampleData.TransactionOperatorNumberOfTransactions,
                                                              ValueOfTransactions = ExampleData.TransactionOperatorValueOfTransactions,
                                                              CurrencyCode = String.Empty,
                                                              OperatorName = ExampleData.OperatorName
                                                          }
                                                      }
                   };
        }
    }
}