namespace EstateReporting.Database
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Shared.General;
    using Shared.Logger;
    using ViewEntities;

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
        /// Initializes a new instance of the <see cref="EstateReportingContext" /> class.
        /// </summary>
        public EstateReportingContext()
        {
            // This is the migration connection string

            // Get connection string from configuration.
            this.ConnectionString = ConfigurationReader.GetConnectionString("EstateReportingReadModel");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EstateReportingContext" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public EstateReportingContext(String connectionString)
        {
            this.ConnectionString = connectionString;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EstateReportingContext" /> class.
        /// </summary>
        /// <param name="dbContextOptions">The database context options.</param>
        public EstateReportingContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        #endregion

        #region Properties

        public virtual DbSet<TransactionsView> TransactionsView { get; set; }

        /// <summary>
        /// Gets or sets the estate operators.
        /// </summary>
        /// <value>
        /// The estate operators.
        /// </value>
        public DbSet<EstateOperator> EstateOperators { get; set; }

        /// <summary>
        /// Gets or sets the estates.
        /// </summary>
        /// <value>
        /// The estates.
        /// </value>
        public DbSet<Estate> Estates { get; set; }

        /// <summary>
        /// Gets or sets the contracts.
        /// </summary>
        /// <value>
        /// The contracts.
        /// </value>
        public DbSet<Contract> Contracts { get; set; }

        /// <summary>
        /// Gets or sets the contract products.
        /// </summary>
        /// <value>
        /// The contract products.
        /// </value>
        public DbSet<ContractProduct> ContractProducts { get; set; }

        /// <summary>
        /// Gets or sets the contract product transaction fees.
        /// </summary>
        /// <value>
        /// The contract product transaction fees.
        /// </value>
        public DbSet<ContractProductTransactionFee> ContractProductTransactionFees { get; set; }

        /// <summary>
        /// Gets or sets the estate security users.
        /// </summary>
        /// <value>
        /// The estate security users.
        /// </value>
        public DbSet<EstateSecurityUser> EstateSecurityUsers { get; set; }

        /// <summary>
        /// Gets or sets the merchant addresses.
        /// </summary>
        /// <value>
        /// The merchant addresses.
        /// </value>
        public DbSet<MerchantAddress> MerchantAddresses { get; set; }

        /// <summary>
        /// Gets or sets the merchant contacts.
        /// </summary>
        /// <value>
        /// The merchant contacts.
        /// </value>
        public DbSet<MerchantContact> MerchantContacts { get; set; }

        /// <summary>
        /// Gets or sets the merchant devices.
        /// </summary>
        /// <value>
        /// The merchant devices.
        /// </value>
        public DbSet<MerchantDevice> MerchantDevices { get; set; }

        /// <summary>
        /// Gets or sets the merchant operators.
        /// </summary>
        /// <value>
        /// The merchant operators.
        /// </value>
        public DbSet<MerchantOperator> MerchantOperators { get; set; }

        /// <summary>
        /// Gets or sets the estate security users.
        /// </summary>
        /// <value>
        /// The estate security users.
        /// </value>
        public DbSet<Merchant> Merchants { get; set; }

        /// <summary>
        /// Gets or sets the merchant security users.
        /// </summary>
        /// <value>
        /// The merchant security users.
        /// </value>
        public DbSet<MerchantSecurityUser> MerchantSecurityUsers { get; set; }

        /// <summary>
        /// Gets or sets the transactions.
        /// </summary>
        /// <value>
        /// The transactions.
        /// </value>
        public DbSet<Transaction> Transactions { get; set; }

        /// <summary>
        /// Gets or sets the transaction fees.
        /// </summary>
        /// <value>
        /// The transaction fees.
        /// </value>
        public DbSet<TransactionFee> TransactionFees { get; set; }

        /// <summary>
        /// Gets or sets the transaction additional request data.
        /// </summary>
        /// <value>
        /// The transaction additional request data.
        /// </value>
        public DbSet<TransactionAdditionalRequestData> TransactionsAdditionalRequestData { get; set; }

        /// <summary>
        /// Gets or sets the transaction additional response data.
        /// </summary>
        /// <value>
        /// The transaction additional response data.
        /// </value>
        public DbSet<TransactionAdditionalResponseData> TransactionsAdditionalResponseData { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Migrates the asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task MigrateAsync(CancellationToken cancellationToken)
        {
            if (this.Database.IsSqlServer())
            {
                await this.Database.MigrateAsync(cancellationToken);
            }

            if (this.Database.IsSqlServer())
            {
                await this.SetIgnoreDuplicates(cancellationToken);
            }

            await this.CreateViews(cancellationToken);
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
            modelBuilder.Entity<Estate>().HasKey(t => new
                                                      {
                                                          t.EstateId
                                                      });

            modelBuilder.Entity<EstateSecurityUser>().HasKey(t => new
                                                                  {
                                                                      t.SecurityUserId,
                                                                      t.EstateId
                                                                  });

            modelBuilder.Entity<Merchant>().HasKey(t => new
                                                        {
                                                            t.EstateId,
                                                            t.MerchantId
                                                        });

            modelBuilder.Entity<MerchantAddress>().HasKey(t => new
                                                               {
                                                                   t.EstateId,
                                                                   t.MerchantId,
                                                                   t.AddressId
                                                               });

            modelBuilder.Entity<MerchantContact>().HasKey(t => new
                                                               {
                                                                   t.EstateId,
                                                                   t.MerchantId,
                                                                   t.ContactId
                                                               });

            modelBuilder.Entity<MerchantDevice>().HasKey(t => new
                                                              {
                                                                  t.EstateId,
                                                                  t.MerchantId,
                                                                  t.DeviceId
                                                              });

            modelBuilder.Entity<MerchantSecurityUser>().HasKey(t => new
                                                                    {
                                                                        t.EstateId,
                                                                        t.MerchantId,
                                                                        t.SecurityUserId
                                                                    });

            modelBuilder.Entity<EstateOperator>().HasKey(t => new
                                                              {
                                                                  t.EstateId,
                                                                  t.OperatorId
                                                              });

            modelBuilder.Entity<MerchantOperator>().HasKey(t => new
                                                                {
                                                                    t.EstateId,
                                                                    t.MerchantId,
                                                                    t.OperatorId
                                                                });

            modelBuilder.Entity<Transaction>().HasKey(t => new
                                                                {
                                                                    t.EstateId,
                                                                    t.MerchantId,
                                                                    t.TransactionId
                                                                });

            modelBuilder.Entity<TransactionFee>().HasKey(t => new
                                                           {
                                                               t.TransactionId,
                                                               t.FeeId
                                                           });

            modelBuilder.Entity<Contract>().HasKey(c => new
                                                        {
                                                            c.EstateId,
                                                            c.OperatorId,
                                                            c.ContractId
                                                        });

            modelBuilder.Entity<ContractProduct>().HasKey(c => new
                                                        {
                                                            c.EstateId,
                                                            c.ContractId,
                                                            c.ProductId
                                                        });

            modelBuilder.Entity<ContractProductTransactionFee>().HasKey(c => new
                                                               {
                                                                   c.EstateId,
                                                                   c.ContractId,
                                                                   c.ProductId,
                                                                   c.TransactionFeeId
                                                               });
            modelBuilder.Entity<ContractProductTransactionFee>().Property(p => p.Value).DecimalPrecision(18, 4);

            modelBuilder.Entity<TransactionAdditionalRequestData>().HasKey(t => new
                                                           {
                                                               t.EstateId,
                                                               t.MerchantId,
                                                               t.TransactionId
                                                           });

            modelBuilder.Entity<TransactionAdditionalRequestData>().HasKey(t => new
                                                           {
                                                               t.EstateId,
                                                               t.MerchantId,
                                                               t.TransactionId
                                                           });

            modelBuilder.Entity<TransactionsView>().HasNoKey().ToView("uvwTransactions");

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Sets the ignore duplicates.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        private async Task SetIgnoreDuplicates(CancellationToken cancellationToken)
        {
            String[] alterStatements =
            {
                nameof(Estate),
                nameof(EstateSecurityUser),
                nameof(EstateOperator),
                nameof(Merchant),
                nameof(MerchantAddress),
                nameof(MerchantContact),
                nameof(MerchantDevice),
                nameof(MerchantSecurityUser),
                nameof(MerchantOperator),
                nameof(Transaction),
                nameof(TransactionFee),
                nameof(TransactionAdditionalRequestData),
                nameof(TransactionAdditionalResponseData),
                nameof(Contract),
                nameof(ContractProduct),
                nameof(ContractProductTransactionFee)
            };

            alterStatements = alterStatements.Select(x => $"ALTER TABLE [{x}]  REBUILD WITH (IGNORE_DUP_KEY = ON)").ToArray();

            String sql = string.Join(";", alterStatements);

            await this.Database.ExecuteSqlRawAsync(sql, cancellationToken);
        }

        #endregion

        public async Task CreateViews(CancellationToken cancellationToken)
        {
            String executingAssemblyLocation = Assembly.GetExecutingAssembly().Location;
            String executingAssemblyFolder = Path.GetDirectoryName(executingAssemblyLocation);

            String scriptsFolder = $@"{executingAssemblyFolder}/Views";

            var directiories = Directory.GetDirectories(scriptsFolder);
            directiories = directiories.OrderBy(d => d).ToArray();

            foreach (String directiory in directiories)
            {
                String[] sqlFiles = Directory.GetFiles(directiory, "*View.sql");
                foreach (String sqlFile in sqlFiles.OrderBy(x => x))
                {
                    Logger.LogDebug($"About to create View [{sqlFile}]");
                    String sql = File.ReadAllText(sqlFile);

                    // Check here is we need to replace a Database Name
                    if (sql.Contains("{DatabaseName}"))
                    {
                        sql = sql.Replace("{DatabaseName}", this.Database.GetDbConnection().Database);
                    }

                    // Create the new view using the original sql from file
                    await this.Database.ExecuteSqlCommandAsync(sql, cancellationToken);

                    Logger.LogDebug($"Created View [{sqlFile}] successfully.");
                }
            }
        }
    }

    public static class Extensions
    {
        #region Methods

        /// <summary>
        /// Decimals the precision.
        /// </summary>
        /// <param name="propertyBuilder">The property builder.</param>
        /// <param name="precision">The precision.</param>
        /// <param name="scale">The scale.</param>
        /// <returns></returns>
        public static PropertyBuilder DecimalPrecision(this PropertyBuilder propertyBuilder,
                                                       Int32 precision,
                                                       Int32 scale)
        {
            return propertyBuilder.HasColumnType($"decimal({precision},{scale})");
        }

        #endregion
    }
}