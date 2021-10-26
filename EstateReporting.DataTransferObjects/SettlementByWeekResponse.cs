namespace EstateReporting.DataTransferObjects
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class SettlementByWeekResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets the transaction week responses.
        /// </summary>
        /// <value>
        /// The transaction week responses.
        /// </value>
        [JsonProperty("settlement_week_responses")]
        public List<SettlementWeekResponse> SettlementWeekResponses { get; set; }

        #endregion
    }
}