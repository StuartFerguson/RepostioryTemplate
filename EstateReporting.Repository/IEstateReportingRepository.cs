namespace EstateReporting.Repository
{
    using System.Threading;
    using System.Threading.Tasks;
    using EstateManagement.Estate.DomainEvents;
    using EstateManagement.Merchant.DomainEvents;
    using EstateSecurityUserAddedEvent = EstateManagement.Estate.DomainEvents.SecurityUserAddedEvent;
    using MerchantSecurityUserAddedEvent = EstateManagement.Merchant.DomainEvents.SecurityUserAddedEvent;

    /// <summary>
    /// 
    /// </summary>
    public interface IEstateReportingRepository
    {
        #region Methods

        /// <summary>
        /// Adds the estate.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddEstate(EstateCreatedEvent domainEvent,
                       CancellationToken cancellationToken);

        /// <summary>
        /// Adds the estate security user.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddEstateSecurityUser(EstateSecurityUserAddedEvent domainEvent,
                                   CancellationToken cancellationToken);

        /// <summary>
        /// Adds the merchant.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddMerchant(MerchantCreatedEvent domainEvent,
                         CancellationToken cancellationToken);

        /// <summary>
        /// Adds the merchant address.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddMerchantAddress(AddressAddedEvent domainEvent,
                                CancellationToken cancellationToken);

        /// <summary>
        /// Adds the merchant contact.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddMerchantContact(ContactAddedEvent domainEvent,
                                CancellationToken cancellationToken);

        /// <summary>
        /// Adds the merchant device.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddMerchantDevice(DeviceAddedToMerchantEvent domainEvent,
                               CancellationToken cancellationToken);

        /// <summary>
        /// Adds the merchant security user.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task AddMerchantSecurityUser(MerchantSecurityUserAddedEvent domainEvent,
                                     CancellationToken cancellationToken);

        /// <summary>
        /// Creates the read model.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task CreateReadModel(EstateCreatedEvent domainEvent,
                             CancellationToken cancellationToken);

        #endregion
    }
}