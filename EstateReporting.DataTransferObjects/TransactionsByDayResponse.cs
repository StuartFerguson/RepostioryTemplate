namespace EstateReporting.DataTransferObjects
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// 
    /// </summary>
    public class TransactionsByDayResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets the transaction day models.
        /// </summary>
        /// <value>
        /// The transaction day models.
        /// </value>
        [JsonProperty("transaction_day_responses")]
        public List<TransactionDayResponse> TransactionDayResponses { get; set; }

        #endregion
    }
}