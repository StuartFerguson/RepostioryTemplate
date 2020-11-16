namespace EstateReporting.BusinessLogic.Events
{
    using System;
    using Newtonsoft.Json;
    using Shared.DomainDrivenDesign.EventSourcing;

    /*
    {
  "$type": "EstateReporting.BusinessLogic.Events.MerchantBalanceChangedEvent, EstateReporting.BusinessLogic",
  "merchantId": "cc4ec879-4961-4494-a34c-55eac43f9753",
  "estateId": "3bf2dab2-86d6-44e3-bcf8-51bec65cf8bc",
  "availableBalance": 402588.50499999983,
  "balance": 402588.50499999983,
  "changeAmount": 100,
  "eventId": "d2238a5e-a00c-4e37-adc7-28be324b0ec3",
  "eventTimestamp": "2020-11-16T19:01:30.4805948+00:00",
  "reference": "Transaction Completed"
}	
    */

    [JsonObject]
    public class MerchantBalanceChangedEvent : DomainEvent
    {
        [JsonProperty]
        public Guid MerchantId { get; set; }
        [JsonProperty]
        public Guid EstateId { get; set; }
        [JsonProperty]
        public Decimal AvailableBalance { get; set; }
        [JsonProperty]
        public Decimal Balance { get; set; }
        [JsonProperty]
        public Decimal ChangeAmount { get; set; }
        [JsonProperty]
        public String  Reference { get; set; }

    }
}
