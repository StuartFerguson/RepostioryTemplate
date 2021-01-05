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
    [JsonObject]
    [ExcludeFromCodeCoverage]
    public class MerchantBalanceChangedEvent : DomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MerchantBalanceChangedEvent"/> class.
        /// </summary>
        public MerchantBalanceChangedEvent()
        {
            // Needed for serialisation    
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MerchantBalanceChangedEvent"/> class.
        /// </summary>
        /// <param name="aggregateId">The aggregate identifier.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="availableBalance">The available balance.</param>
        /// <param name="balance">The balance.</param>
        /// <param name="changeAmount">The change amount.</param>
        /// <param name="reference">The reference.</param>
        private MerchantBalanceChangedEvent(Guid aggregateId, Guid eventId,
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
        }

        /// <summary>
        /// Creates the specified aggregate identifier.
        /// </summary>
        /// <param name="aggregateId">The aggregate identifier.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="availableBalance">The available balance.</param>
        /// <param name="balance">The balance.</param>
        /// <param name="changeAmount">The change amount.</param>
        /// <param name="reference">The reference.</param>
        /// <returns></returns>
        public static MerchantBalanceChangedEvent Create(Guid aggregateId,
                                                         Guid eventId,
                                                  Guid estateId,
                                                  Guid merchantId,
                                                  Decimal availableBalance,
                                                  Decimal balance,
                                                  Decimal changeAmount,
                                                  String reference)
        {
            return new MerchantBalanceChangedEvent(aggregateId, eventId, estateId, merchantId, availableBalance, balance, changeAmount,reference);
        }

        #region Properties

        /// <summary>
        /// Gets or sets the available balance.
        /// </summary>
        /// <value>
        /// The available balance.
        /// </value>
        [JsonProperty]
        public Decimal AvailableBalance { get; private set; }

        /// <summary>
        /// Gets or sets the balance.
        /// </summary>
        /// <value>
        /// The balance.
        /// </value>
        [JsonProperty]
        public Decimal Balance { get; private set; }

        /// <summary>
        /// Gets or sets the change amount.
        /// </summary>
        /// <value>
        /// The change amount.
        /// </value>
        [JsonProperty]
        public Decimal ChangeAmount { get; private set; }

        /// <summary>
        /// Gets or sets the estate identifier.
        /// </summary>
        /// <value>
        /// The estate identifier.
        /// </value>
        [JsonProperty]
        public Guid EstateId { get; private set; }

        /// <summary>
        /// Gets or sets the merchant identifier.
        /// </summary>
        /// <value>
        /// The merchant identifier.
        /// </value>
        [JsonProperty]
        public Guid MerchantId { get; private set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        /// <value>
        /// The reference.
        /// </value>
        [JsonProperty]
        public String Reference { get; private set; }

        #endregion
    }
}