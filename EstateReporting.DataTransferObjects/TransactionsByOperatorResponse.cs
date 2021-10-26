namespace EstateReporting.DataTransferObjects
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class TransactionsByOperatorResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets the transaction month responses.
        /// </summary>
        /// <value>
        /// The transaction week responses.
        /// </value>
        [JsonProperty("transaction_operator_responses")]
        public List<TransactionOperatorResponse> TransactionOperatorResponses { get; set; }

        #endregion
    }
}