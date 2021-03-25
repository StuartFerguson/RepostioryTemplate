namespace EstateReporting.BusinessLogic.Events
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Newtonsoft.Json;
    using Shared.DomainDrivenDesign.EventSourcing;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Shared.DomainDrivenDesign.EventSourcing.DomainEvent" />
    [ExcludeFromCodeCoverage]
    public record MerchantBalanceChangedEvent : DomainEventRecord.DomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MerchantBalanceChangedEvent" /> class.
        /// </summary>
        /// <param name="aggregateId">The aggregate identifier.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="eventCreatedDateTime">The event created date time.</param>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="availableBalance">The available balance.</param>
        /// <param name="balance">The balance.</param>
        /// <param name="changeAmount">The change amount.</param>
        /// <param name="reference">The reference.</param>
        public MerchantBalanceChangedEvent(Guid aggregateId, Guid eventId,
                                           DateTime eventCreatedDateTime,
                                           Guid estateId,
                                           Guid merchantId,
                                           Decimal availableBalance,
                                           Decimal balance,
                                           Decimal changeAmount,
                                           String reference) : base(aggregateId, eventId)
        {
            this.EstateId = estateId;
            this.MerchantId = merchantId;
            this.AvailableBalance = availableBalance;
            this.Balance = balance;
            this.ChangeAmount = changeAmount;
            this.Reference = reference;
            this.EventId = eventId;
            this.AggregateId = aggregateId;
            this.EventCreatedDateTime = eventCreatedDateTime;
        }
        
        #region Properties

        /// <summary>
        /// Gets or sets the available balance.
        /// </summary>
        /// <value>
        /// The available balance.
        /// </value>
        public Decimal AvailableBalance { get; init; }

        public new Guid EventId { get; init; }
        public DateTime EventCreatedDateTime { get; init; }
        public new Guid AggregateId { get; init; }

        /// <summary>
        /// Gets or sets the balance.
        /// </summary>
        /// <value>
        /// The balance.
        /// </value>
        public Decimal Balance { get; init; }

        /// <summary>
        /// Gets or sets the change amount.
        /// </summary>
        /// <value>
        /// The change amount.
        /// </value>
        public Decimal ChangeAmount { get; init; }

        /// <summary>
        /// Gets or sets the estate identifier.
        /// </summary>
        /// <value>
        /// The estate identifier.
        /// </value>
        public Guid EstateId { get; init; }

        /// <summary>
        /// Gets or sets the merchant identifier.
        /// </summary>
        /// <value>
        /// The merchant identifier.
        /// </value>
        public Guid MerchantId { get; init; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        /// <value>
        /// The reference.
        /// </value>
        public String Reference { get; init; }

        #endregion
    }
}