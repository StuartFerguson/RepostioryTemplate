namespace EstateReporting.BusinessLogic.EventHandling
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Repository;
    using Shared.DomainDrivenDesign.EventSourcing;
    using Shared.EventStore.EventHandling;
    using TransactionProcessor.Settlement.DomainEvents;
    using TransactionProcessor.Transaction.DomainEvents;

    public class SettlementDomainEventHandler : IDomainEventHandler
    {
        #region Fields

        /// <summary>
        /// The estate reporting repository
        /// </summary>
        private readonly IEstateReportingRepository EstateReportingRepository;

        #endregion
        
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MerchantDomainEventHandler"/> class.
        /// </summary>
        /// <param name="estateReportingRepository">The estate reporting repository.</param>
        public SettlementDomainEventHandler(IEstateReportingRepository estateReportingRepository)
        {
            this.EstateReportingRepository = estateReportingRepository;
        }

        #endregion

        /// <summary>
        /// Handles the specified domain event.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task Handle(IDomainEvent domainEvent,
                                 CancellationToken cancellationToken)
        {
            await this.HandleSpecificDomainEvent((dynamic)domainEvent, cancellationToken);
        }

        private async Task HandleSpecificDomainEvent(SettlementCreatedForDateEvent domainEvent,
                                                     CancellationToken cancellationToken)
        {
            await this.EstateReportingRepository.CreateSettlement(domainEvent, cancellationToken);
        }

        private async Task HandleSpecificDomainEvent(MerchantFeeAddedToTransactionEvent domainEvent,
                                                     CancellationToken cancellationToken)
        {
            // Generate the settlement id from the date
            Guid settlementId = SettlementDomainEventHandler.GetSettlementId(domainEvent.SettlementDueDate);

            await this.EstateReportingRepository.AddSettledMerchantFeeToSettlement(settlementId, domainEvent, cancellationToken);
        }

        private async Task HandleSpecificDomainEvent(MerchantFeeAddedPendingSettlementEvent domainEvent,
                                                     CancellationToken cancellationToken)
        {
            await this.EstateReportingRepository.AddPendingMerchantFeeToSettlement(domainEvent, cancellationToken);
        }

        private async Task HandleSpecificDomainEvent(MerchantFeeSettledEvent domainEvent,
                                                     CancellationToken cancellationToken)
        {
            await this.EstateReportingRepository.MarkMerchantFeeAsSettled(domainEvent, cancellationToken);
        }

        private async Task HandleSpecificDomainEvent(SettlementCompletedEvent domainEvent,
                                                     CancellationToken cancellationToken)
        {
            await this.EstateReportingRepository.MarkSettlementAsCompleted(domainEvent, cancellationToken);
        }

        public static Guid GetSettlementId(DateTime dt)
        {
            Byte[] bytes = BitConverter.GetBytes(dt.Ticks);

            Array.Resize(ref bytes, 16);

            return new Guid(bytes);
        }
    }
}
