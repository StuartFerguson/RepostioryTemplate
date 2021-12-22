using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateReporting.Database.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("statementheader")]
    public class StatementHeader
    {
        public Guid StatementId { get; set; }

        public Guid EstateId { get; set; }

        public Guid MerchantId { get; set; }

        public DateTime StatementCreatedDate { get; set; }

        public DateTime StatementGeneratedDate { get; set; }
    }

    [Table("statementline")]
    public class StatementLine
    {
        public Guid StatementId { get; set; }

        public Guid EstateId { get; set; }

        public Guid MerchantId { get; set; }

        public DateTime ActivityDateTime { get; set; }

        public Int32 ActivityType { get; set; }

        public String ActivityDescription { get; set; }

        public Decimal InAmount { get; set; }
        public Decimal OutAmount { get; set; }

        public Guid TransactionId { get; set; }
    }
}
