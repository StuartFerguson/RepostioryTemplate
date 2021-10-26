namespace EstateReporting.DataTransferObjects
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class SettlementByMerchantResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets the transaction month responses.
        /// </summary>
        /// <value>
        /// The transaction week responses.
        /// </value>
        [JsonProperty("settlement_merchant_responses")]
        public List<SettlementMerchantResponse> SettlementMerchantResponses { get; set; }

        #endregion
    }
}