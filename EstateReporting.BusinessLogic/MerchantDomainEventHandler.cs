namespace EstateReporting.BusinessLogic
{
    using System.Threading;
    using System.Threading.Tasks;
    using EstateManagement.Merchant.DomainEvents;
    using Repository;
    using Shared.DomainDrivenDesign.EventSourcing;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="EstateReporting.BusinessLogic.IDomainEventHandler" />
    public class MerchantDomainEventHandler : IDomainEventHandler
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
        public MerchantDomainEventHandler(IEstateReportingRepository estateReportingRepository)
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
        private async Task HandleSpecificDomainEvent(MerchantCreatedEvent domainEvent,
                                                     CancellationToken cancellationToken)
        {
            await this.EstateReportingRepository.AddMerchant(domainEvent, cancellationToken);
        }

        /// <summary>
        /// Handles the specific domain event.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        private async Task HandleSpecificDomainEvent(AddressAddedEvent domainEvent,
                                                     CancellationToken cancellationToken)
        {
            await this.EstateReportingRepository.AddMerchantAddress(domainEvent, cancellationToken);
        }

        /// <summary>
        /// Handles the specific domain event.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        private async Task HandleSpecificDomainEvent(ContactAddedEvent domainEvent,
                                                     CancellationToken cancellationToken)
        {
            await this.EstateReportingRepository.AddMerchantContact(domainEvent, cancellationToken);
        }

        /// <summary>
        /// Handles the specific domain event.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        private async Task HandleSpecificDomainEvent(SecurityUserAddedEvent domainEvent,
                                                     CancellationToken cancellationToken)
        {
            await this.EstateReportingRepository.AddMerchantSecurityUser(domainEvent, cancellationToken);
        }

        /// <summary>
        /// Handles the specific domain event.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        private async Task HandleSpecificDomainEvent(DeviceAddedToMerchantEvent domainEvent,
                                                     CancellationToken cancellationToken)
        {
            await this.EstateReportingRepository.AddMerchantDevice(domainEvent, cancellationToken);
        }

        #endregion
    }
}