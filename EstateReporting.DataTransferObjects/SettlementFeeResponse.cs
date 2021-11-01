namespace EstateReporting.DataTransferObjects
{
    using System;
    using Newtonsoft.Json;

    public class SettlementFeeResponse
    {
        #region Properties

        [JsonProperty("calculated_value")]
        public Decimal CalculatedValue { get; set; }

        [JsonProperty("fee_description")]
        public String FeeDescription { get; set; }

        [JsonProperty("is_settled")]
        public Boolean IsSettled { get; set; }

        [JsonProperty("merchant_id")]
        public Guid MerchantId { get; set; }

        [JsonProperty("merchant_name")]
        public String MerchantName { get; set; }

        [JsonProperty("settlement_date")]
        public DateTime SettlementDate { get; set; }

        [JsonProperty("settlement_id")]
        public Guid SettlementId { get; set; }

        [JsonProperty("transaction_id")]
        public Guid TransactionId { get; set; }

        #endregion
    }
}