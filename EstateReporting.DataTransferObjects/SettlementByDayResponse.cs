namespace EstateReporting.DataTransferObjects
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class SettlementByDayResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets the transaction day models.
        /// </summary>
        /// <value>
        /// The transaction day models.
        /// </value>
        [JsonProperty("settlement_day_responses")]
        public List<SettlementDayResponse> SettlementDayResponses { get; set; }

        #endregion
    }
}