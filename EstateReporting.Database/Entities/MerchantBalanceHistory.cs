using System;
using System.Collections.Generic;
using System.Text;

namespace EstateReporting.Database.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /*{
    "$type": "EstateReporting.BusinessLogic.Events.MerchantBalanceChangedEvent, EstateReporting.BusinessLogic",
    "aggregateId": "c4c33d75-f011-40e4-9d97-1f428ab563d8",
    "merchantId": "c4c33d75-f011-40e4-9d97-1f428ab563d8",
    "estateId": "3bf2dab2-86d6-44e3-bcf8-51bec65cf8bc",
    "availableBalance": 404000.2300000001,
    "balance": 404000.2300000001,
    "changeAmount": 200,
    "eventId": "670ea920-60b0-4b91-b9fb-c8858a07cf53",
    "eventCreatedDateTime": "2020-11-16T20:00:42.5879722+00:00",
    "reference": "Transaction Completed"
    }*/

    [Table("merchantbalancehistory")]
    public class MerchantBalanceHistory
    {
        [Key]
        public Guid EventId { get; set; }
        public Guid EstateId { get; set; }
        public Guid MerchantId { get; set; }
        public Decimal AvailableBalance { get; set; }
        public Decimal Balance { get; set; }
        public Decimal ChangeAmount { get; set; }
        public String Reference { get; set; }

    }
}
