namespace EstateReporting.Database.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// 
    /// </summary>
    [Table("merchantbalancehistory")]
    public class MerchantBalanceHistory
    {
        /// <summary>
        /// Gets or sets the event identifier.
        /// </summary>
        /// <value>
        /// The event identifier.
        /// </value>
        [Key]
        public Guid EventId { get; set; }

        /// <summary>
        /// Gets or sets the estate identifier.
        /// </summary>
        /// <value>
        /// The estate identifier.
        /// </value>
        public Guid EstateId { get; set; }

        /// <summary>
        /// Gets or sets the merchant identifier.
        /// </summary>
        /// <value>
        /// The merchant identifier.
        /// </value>
        public Guid MerchantId { get; set; }

        /// <summary>
        /// Gets or sets the available balance.
        /// </summary>
        /// <value>
        /// The available balance.
        /// </value>
        public Decimal AvailableBalance { get; set; }

        /// <summary>
        /// Gets or sets the balance.
        /// </summary>
        /// <value>
        /// The balance.
        /// </value>
        public Decimal Balance { get; set; }

        /// <summary>
        /// Gets or sets the change amount.
        /// </summary>
        /// <value>
        /// The change amount.
        /// </value>
        public Decimal ChangeAmount { get; set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        /// <value>
        /// The reference.
        /// </value>
        public String Reference { get; set; }

        /// <summary>
        /// Gets or sets the entry date time.
        /// </summary>
        /// <value>
        /// The entry date time.
        /// </value>
        public DateTime EntryDateTime { get; set; }
    }
}