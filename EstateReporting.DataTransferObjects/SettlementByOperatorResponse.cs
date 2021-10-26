namespace EstateReporting.DataTransferObjects
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class SettlementByOperatorResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets the transaction month responses.
        /// </summary>
        /// <value>
        /// The transaction week responses.
        /// </value>
        [JsonProperty("settlement_operator_responses")]
        public List<SettlementOperatorResponse> SettlementOperatorResponses { get; set; }

        #endregion
    }
}