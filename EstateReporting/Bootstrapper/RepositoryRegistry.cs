namespace EstateReporting.Bootstrapper
{
    using System;
    using BusinessLogic;
    using Database;
    using Lamar;
    using Microsoft.Extensions.DependencyInjection;
    using Repository;
    using Shared.EntityFramework;
    using Shared.EntityFramework.ConnectionStringConfiguration;
    using Shared.General;
    using Shared.Repositories;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Lamar.ServiceRegistry" />
    public class RepositoryRegistry : ServiceRegistry
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryRegistry"/> class.
        /// </summary>
        public RepositoryRegistry()
        {
            Boolean useConnectionStringConfig = bool.Parse(ConfigurationReader.GetValue("AppSettings", "UseConnectionStringConfig"));

            if (useConnectionStringConfig)
            {
                String connectionStringConfigurationConnString = ConfigurationReader.GetConnectionString("ConnectionStringConfiguration");
                this.AddSingleton<IConnectionStringConfigurationRepository, ConnectionStringConfigurationRepository>();
                this.AddTransient(c => { return new ConnectionStringConfigurationContext(connectionStringConfigurationConnString); });
            }
            else
            {
                this.AddSingleton<IConnectionStringConfigurationRepository, ConfigurationReaderConnectionStringRepository>();
            }

            this.AddSingleton<IEstateReportingRepository, EstateReportingRepository>();
            this.AddSingleton<IEstateReportingRepositoryForReports, EstateReportingRepositoryForReports>();
            this.AddSingleton<IDbContextFactory<EstateReportingGenericContext>, DbContextFactory<EstateReportingGenericContext>>();

            this.AddSingleton<Func<String, EstateReportingGenericContext>>(cont => connectionString =>
                                                                                   {
                                                                                       String databaseEngine =
                                                                                           ConfigurationReader.GetValue("AppSettings", "DatabaseEngine");

                                                                                       return databaseEngine switch
                                                                                       {
                                                                                           "MySql" => new EstateReportingMySqlContext(connectionString),
                                                                                           "SqlServer" => new EstateReportingSqlServerContext(connectionString),
                                                                                           _ => throw new
                                                                                               NotSupportedException($"Unsupported Database Engine {databaseEngine}")
                                                                                       };
                                                                                   });
        }

        #endregion
    }
}