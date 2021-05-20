namespace EstateReporting.Common.Examples
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using DataTransferObjects;
    using Swashbuckle.AspNetCore.Filters;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Swashbuckle.AspNetCore.Filters.IExamplesProvider{EstateReporting.DataTransferObjects.TransactionsByWeekResponse}" />
    [ExcludeFromCodeCoverage]
    public class TransactionsByWeekResponseExample : IExamplesProvider<TransactionsByWeekResponse>
    {
        #region Methods

        /// <summary>
        /// Gets the examples.
        /// </summary>
        /// <returns></returns>
        public TransactionsByWeekResponse GetExamples()
        {
            return new TransactionsByWeekResponse
                   {
                       TransactionWeekResponses = new List<TransactionWeekResponse>
                                                  {
                                                      new TransactionWeekResponse
                                                      {
                                                          NumberOfTransactions = ExampleData.TransactionWeekNumberOfTransactions,
                                                          ValueOfTransactions = ExampleData.TransactionWeekValueOfTransactions,
                                                          CurrencyCode = string.Empty,
                                                          WeekNumber = ExampleData.WeekNumber,
                                                          Year = ExampleData.YearNumber
                                                      }
                                                  }
                   };
        }

        #endregion
    }
}