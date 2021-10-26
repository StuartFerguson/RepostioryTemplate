namespace EstateReporting.DataTransferObjects
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class SettlementByMonthResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets the transaction month responses.
        /// </summary>
        /// <value>
        /// The transaction week responses.
        /// </value>
        [JsonProperty("settlement_month_responses")]
        public List<SettlementMonthResponse> SettlementMonthResponses { get; set; }

        #endregion
    }
}