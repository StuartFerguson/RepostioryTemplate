namespace EstateReporting.DataTransferObjects
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class TransactionsByWeekResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets the transaction week responses.
        /// </summary>
        /// <value>
        /// The transaction week responses.
        /// </value>
        [JsonProperty("transaction_week_responses")]
        public List<TransactionWeekResponse> TransactionWeekResponses { get; set; }

        #endregion
    }
}