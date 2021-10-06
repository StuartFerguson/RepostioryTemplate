using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateReporting.Database.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNetCore.Mvc;

    [Table("settlement")]
    public class Settlement
    {
        public Guid EstateId { get; set; }

        public Guid SettlementId { get; set; }

        public DateTime SettlementDate { get; set; }

        public Boolean IsCompleted { get; set; }
    }

    [Table("merchantsettlementfees")]
    public class MerchantSettlementFee
    {
        public Decimal CalculatedValue { get; set; }

        public DateTime FeeCalculatedDateTime { get; set; }

        public Guid EstateId { get; set; }

        public Guid FeeId { get; set; }

        public Decimal FeeValue { get; set; }

        public Guid MerchantId { get; set; }

        public Guid TransactionId { get; set; }

        public Guid SettlementId { get; set; }

        public Boolean IsSettled { get; set; }
    }
}
