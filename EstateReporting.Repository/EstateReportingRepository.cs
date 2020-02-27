namespace EstateReporting.Repository
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Database;
    using Database.Entities;
    using EstateManagement.Estate.DomainEvents;
    using EstateManagement.Merchant.DomainEvents;
    using Shared.EntityFramework;
    using Shared.Logger;
    using EstateSecurityUserAddedEvent = EstateManagement.Estate.DomainEvents.SecurityUserAddedEvent;
    using MerchantSecurityUserAddedEvent = EstateManagement.Merchant.DomainEvents.SecurityUserAddedEvent;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="EstateReporting.Repository.IEstateReportingRepository" />
    public class EstateReportingRepository : IEstateReportingRepository
    {
        #region Fields

        /// <summary>
        /// The database context factory
        /// </summary>
        private readonly IDbContextFactory<EstateReportingContext> DbContextFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EstateReportingRepository" /> class.
        /// </summary>
        /// <param name="dbContextFactory">The database context factory.</param>
        public EstateReportingRepository(IDbContextFactory<EstateReportingContext> dbContextFactory)
        {
            this.DbContextFactory = dbContextFactory;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the estate.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task AddEstate(EstateCreatedEvent domainEvent,
                                    CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            // Add the estate to the read model
            Estate estate = new Estate
                            {
                                CreatedDateTime = domainEvent.EventCreatedDateTime,
                                EstateId = estateId,
                                Name = domainEvent.EstateName
                            };
            await context.Estates.AddAsync(estate, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds the estate security user.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task AddEstateSecurityUser(EstateSecurityUserAddedEvent domainEvent,
                                                CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            EstateSecurityUser estateSecurityUser = new EstateSecurityUser
                                                    {
                                                        CreatedDateTime = domainEvent.EventCreatedDateTime,
                                                        EstateId = domainEvent.EstateId,
                                                        EmailAddress = domainEvent.EmailAddress,
                                                        SecurityUserId = domainEvent.SecurityUserId
                                                    };

            await context.EstateSecurityUsers.AddAsync(estateSecurityUser, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds the merchant.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task AddMerchant(MerchantCreatedEvent domainEvent,
                                      CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            Merchant merchant = new Merchant
                                {
                                    EstateId = domainEvent.EstateId,
                                    MerchantId = domainEvent.MerchantId,
                                    Name = domainEvent.MerchantName,
                                    CreatedDateTime = domainEvent.EventCreatedDateTime
                                };

            await context.Merchants.AddAsync(merchant, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds the merchant address.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task AddMerchantAddress(AddressAddedEvent domainEvent,
                                             CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            MerchantAddress merchantAddress = new MerchantAddress
                                              {
                                                  EstateId = domainEvent.EstateId,
                                                  MerchantId = domainEvent.MerchantId,
                                                  CreatedDateTime = domainEvent.EventCreatedDateTime,
                                                  AddressId = domainEvent.AddressId,
                                                  AddressLine1 = domainEvent.AddressLine1,
                                                  AddressLine2 = domainEvent.AddressLine2,
                                                  AddressLine3 = domainEvent.AddressLine3,
                                                  AddressLine4 = domainEvent.AddressLine4,
                                                  Country = domainEvent.Country,
                                                  PostalCode = domainEvent.PostalCode,
                                                  Region = domainEvent.Region,
                                                  Town = domainEvent.Town
                                              };

            await context.MerchantAddresses.AddAsync(merchantAddress, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds the merchant contact.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task AddMerchantContact(ContactAddedEvent domainEvent,
                                             CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            MerchantContact merchantContact = new MerchantContact
                                              {
                                                  EstateId = domainEvent.EstateId,
                                                  MerchantId = domainEvent.AggregateId,
                                                  Name = domainEvent.ContactName,
                                                  ContactId = domainEvent.ContactId,
                                                  EmailAddress = domainEvent.ContactEmailAddress,
                                                  PhoneNumber = domainEvent.ContactPhoneNumber,
                                                  CreatedDateTime = domainEvent.EventCreatedDateTime
                                              };

            await context.MerchantContacts.AddAsync(merchantContact, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds the merchant device.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task AddMerchantDevice(DeviceAddedToMerchantEvent domainEvent,
                                            CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            MerchantDevice merchantDevice = new MerchantDevice
                                            {
                                                EstateId = domainEvent.EstateId,
                                                MerchantId = domainEvent.MerchantId,
                                                DeviceId = domainEvent.DeviceId,
                                                DeviceIdentifier = domainEvent.DeviceIdentifier,
                                                CreatedDateTime = domainEvent.EventCreatedDateTime
                                            };

            await context.MerchantDevices.AddAsync(merchantDevice, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds the merchant security user.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task AddMerchantSecurityUser(MerchantSecurityUserAddedEvent domainEvent,
                                                  CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            MerchantSecurityUser merchantSecurityUser = new MerchantSecurityUser
                                                        {
                                                            EstateId = domainEvent.EstateId,
                                                            MerchantId = domainEvent.MerchantId,
                                                            EmailAddress = domainEvent.EmailAddress,
                                                            SecurityUserId = domainEvent.SecurityUserId,
                                                            CreatedDateTime = domainEvent.EventCreatedDateTime
                                                        };

            await context.MerchantSecurityUsers.AddAsync(merchantSecurityUser, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Creates the read model.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task CreateReadModel(EstateCreatedEvent domainEvent,
                                          CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.EstateId;

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            Logger.LogInformation($"About to run migrations on Read Model database for estate [{estateId}]");

            // Ensure the db is at the latest version
            await context.MigrateAsync(cancellationToken);

            Logger.LogInformation($"Read Model database for estate [{estateId}] migrated to latest version");
        }

        #endregion
    }
}