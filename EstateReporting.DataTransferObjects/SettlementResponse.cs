﻿namespace EstateReporting.DataTransferObjects
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class SettlementResponse
    {
        #region Constructors

        public SettlementResponse()
        {
            this.SettlementFees = new List<SettlementFeeResponse>();
        }

        #endregion

        #region Properties

        [JsonProperty("is_completed")]
        public Boolean IsCompleted { get; set; }

        [JsonProperty("number_of_fees_settled")]
        public Int32 NumberOfFeesSettled { get; set; }

        [JsonProperty("settlement_date")]
        public DateTime SettlementDate { get; set; }

        [JsonProperty("settlement_fees")]
        public List<SettlementFeeResponse> SettlementFees { get; set; }

        [JsonProperty("settlement_id")]
        public Guid SettlementId { get; set; }

        [JsonProperty("value_of_fees_settled")]
        public Decimal ValueOfFeesSettled { get; set; }

        #endregion
    }
}