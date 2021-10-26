namespace EstateReporting.Common.Examples
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using DataTransferObjects;
    using Swashbuckle.AspNetCore.Filters;

    [ExcludeFromCodeCoverage]
    public class SettlementOperatorResponseExample : IExamplesProvider<SettlementOperatorResponse>
    {
        /// <summary>
        /// Gets the examples.
        /// </summary>
        /// <returns></returns>
        public SettlementOperatorResponse GetExamples()
        {
            return new SettlementOperatorResponse
                   {
                       NumberOfTransactionsSettled = ExampleData.SettlementOperatorNumberOfTransactions,
                       ValueOfSettlement = ExampleData.SettlementOperatorValueOfTransactions,
                       CurrencyCode = String.Empty,
                       OperatorName = ExampleData.OperatorName
                   };
        }
    }
}