namespace EstateReporting.Database
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Shared.General;

    public class EstateReportingSqlServerContext : EstateReportingGenericContext
    {
        public EstateReportingSqlServerContext() : base("SqlServer",ConfigurationReader.GetConnectionString("EstateReportingReadModel"))
        {
        }

        public EstateReportingSqlServerContext(String connectionString) : base("SqlServer", connectionString) 
        {
        }

        public EstateReportingSqlServerContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!string.IsNullOrWhiteSpace(this.ConnectionString))
            {
                options.UseSqlServer(this.ConnectionString);
            }
        }

        protected override async Task SetIgnoreDuplicates(CancellationToken cancellationToken)
        {
            base.SetIgnoreDuplicates(cancellationToken);

            TablesToIgnoreDuplicates = TablesToIgnoreDuplicates.Select(x => $"ALTER TABLE [{x}]  REBUILD WITH (IGNORE_DUP_KEY = ON)").ToList();

            String sql = string.Join(";", TablesToIgnoreDuplicates);

            await this.Database.ExecuteSqlRawAsync(sql, cancellationToken);
        }
    }
}