namespace EstateReporting.Database
{
    using System;
    using System.Reflection.Metadata.Ecma335;
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using Shared.General;

    public class EstateReportingMySqlContext : EstateReportingGenericContext
    {
        public EstateReportingMySqlContext() : base("MySql", ConfigurationReader.GetConnectionString("EstateReportingReadModel"))
        {
        }

        public EstateReportingMySqlContext(String connectionString) : base("MySql", connectionString)
        {
        }

        public EstateReportingMySqlContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!string.IsNullOrWhiteSpace(this.ConnectionString))
            {
                options.UseMySql(this.ConnectionString, ServerVersion.Parse("8.0.27")).AddInterceptors(new MySqlIgnoreDuplicatesOnInsertInterceptor());
            }
        }
    }
}