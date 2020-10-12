using System;
using System.Collections.Generic;
using System.Text;

namespace EstateReporting.Database.ViewEntities
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("uvwTransactionsView")]
    public class TransactionsView
    {
        public Guid TransactionId { get; set; }

        public DateTime TransactionDateTime { get; set; }

        public DateTime TransactionDate { get; set; }

        public Int32 DayNumber { get; set; }

        public String DayOfWeek { get; set; }

        public String MonthNumber { get; set; }

        public Int32 YearNumber { get; set; }

        public Guid EstateId { get; set; }
        public Guid MerchantId { get; set; }
        public Boolean IsAuthorised { get; set; }
        public Boolean IsCompleted { get; set; }
        public String TransactionType { get; set; }
        public String ResponseCode { get; set; }

        public Decimal Amount { get; set; }
        public String OperatorIdentifier { get; set; }
    }
}
