namespace EstateReporting.Repository
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Database;
    using Database.Entities;
    using EstateManagement.Estate.DomainEvents;
    using Microsoft.EntityFrameworkCore;
    using Shared.EntityFramework;
    using Shared.Logger;

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
        /// <returns></returns>
        public async Task AddEstate(EstateCreatedEvent domainEvent,
                                    CancellationToken cancellationToken)
        {
            Guid estateId = domainEvent.AggregateId;

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
        /// Creates the read model.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
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