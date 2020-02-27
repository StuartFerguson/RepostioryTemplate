namespace EstateReporting.Database
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using Shared.General;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class EstateReportingContext : DbContext
    {
        #region Fields

        /// <summary>
        /// The connection string
        /// </summary>
        private readonly String ConnectionString;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EstateReportingContext"/> class.
        /// </summary>
        public EstateReportingContext()
        {
            // This is the migration connection string

            // Get connection string from configuration.
            this.ConnectionString = ConfigurationReader.GetConnectionString("EstateReportingReadModel");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EstateReportingContext"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public EstateReportingContext(String connectionString)
        {
            this.ConnectionString = connectionString;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EstateReportingContext"/> class.
        /// </summary>
        /// <param name="dbContextOptions">The database context options.</param>
        public EstateReportingContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the estates.
        /// </summary>
        /// <value>
        /// The estates.
        /// </value>
        public DbSet<Estate> Estates { get; set; }

        /// <summary>
        /// Gets or sets the estate security users.
        /// </summary>
        /// <value>
        /// The estate security users.
        /// </value>
        public DbSet<EstateSecurityUser> EstateSecurityUsers { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the ignore duplicates.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        private async Task SetIgnoreDuplicates(CancellationToken cancellationToken)
        {
            String[] alterStatements =
            {
                nameof(Estate),
                nameof(EstateSecurityUser)
            };

            alterStatements = alterStatements.Select(x => $"ALTER TABLE [{x}]  REBUILD WITH (IGNORE_DUP_KEY = ON)").ToArray();

            String sql = string.Join(";", alterStatements);

            await this.Database.ExecuteSqlRawAsync(sql, cancellationToken);
        }

        /// <summary>
        /// Migrates the asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task MigrateAsync(CancellationToken cancellationToken)
        {
            if (this.Database.IsSqlServer() || this.Database.IsSqlite())
            {
                await this.Database.MigrateAsync(cancellationToken);
            }

            if (this.Database.IsSqlServer())
            {
                await this.SetIgnoreDuplicates(cancellationToken);
            }
        }

        /// <summary>
        /// <para>
        /// Override this method to configure the database (and other options) to be used for this context.
        /// This method is called for each instance of the context that is created.
        /// The base implementation does nothing.
        /// </para>
        /// <para>
        /// In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
        /// to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
        /// the options have already been set, and skip some or all of the logic in
        /// <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
        /// </para>
        /// </summary>
        /// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions)
        /// typically define extension methods on this object that allow you to configure the context.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrWhiteSpace(this.ConnectionString))
            {
                optionsBuilder.UseSqlServer(this.ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}