namespace EstateReporting.BusinessLogic
{
    using System.Threading;
    using System.Threading.Tasks;
    using EstateManagement.Estate.DomainEvents;
    using EstateReporting.Repository;
    using Shared.DomainDrivenDesign.EventSourcing;
    using Shared.Logger;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="EstateReporting.BusinessLogic.IDomainEventHandler" />
    public class EstateDomainEventHandler : IDomainEventHandler
    {
        private readonly IEstateReportingRepository EstateReportingRepository;

        #region Constructors

        public EstateDomainEventHandler(IEstateReportingRepository estateReportingRepository)
        {
            this.EstateReportingRepository = estateReportingRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the specified domain event.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task Handle(DomainEvent domainEvent,
                                 CancellationToken cancellationToken)
        {
            await this.HandleSpecificDomainEvent((dynamic)domainEvent, cancellationToken);
        }

        /// <summary>
        /// Handles the specific domain event.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        private async Task HandleSpecificDomainEvent(EstateCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            await this.EstateReportingRepository.CreateReadModel(domainEvent, cancellationToken);

            await this.EstateReportingRepository.AddEstate(domainEvent, cancellationToken);
        }

        #endregion
    }
}