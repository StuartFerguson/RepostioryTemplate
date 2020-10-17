namespace EstateReporting.DataTransferObjects
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class TransactionsByMonthResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets the transaction month responses.
        /// </summary>
        /// <value>
        /// The transaction week responses.
        /// </value>
        [JsonProperty("transaction_month_responses")]
        public List<TransactionMonthResponse> TransactionMonthResponses { get; set; }

        #endregion
    }
}