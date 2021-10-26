namespace EstateReporting.Common.Examples
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using DataTransferObjects;
    using Swashbuckle.AspNetCore.Filters;

    [ExcludeFromCodeCoverage]
    public class SettlementByOperatorResponseExample : IExamplesProvider<SettlementByOperatorResponse>
    {
        /// <summary>
        /// Gets the examples.
        /// </summary>
        /// <returns></returns>
        public SettlementByOperatorResponse GetExamples()
        {
            return new SettlementByOperatorResponse
                   {
                       SettlementOperatorResponses = new List<SettlementOperatorResponse>
                                                     {
                                                         new SettlementOperatorResponse
                                                         {
                                                             NumberOfTransactionsSettled = ExampleData.SettlementOperatorNumberOfTransactions,
                                                             ValueOfSettlement = ExampleData.SettlementOperatorValueOfTransactions,
                                                             CurrencyCode = String.Empty,
                                                             OperatorName = ExampleData.OperatorName
                                                         }
                                                     }
                   };
        }
    }
}