using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EstateReporting
{
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using BusinessLogic.Events;
    using EstateManagement.Contract.DomainEvents;
    using EstateManagement.Estate.DomainEvents;
    using EstateManagement.Merchant.DomainEvents;
    using EventStore.Client;
    using FileProcessor.File.DomainEvents;
    using FileProcessor.FileImportLog.DomainEvents;
    using Microsoft.Extensions.DependencyInjection;
    using Shared.EventStore.Aggregate;
    using Shared.EventStore.EventHandling;
    using Shared.EventStore.Subscriptions;
    using Shared.Logger;
    using TransactionProcessor.Reconciliation.DomainEvents;
    using TransactionProcessor.Settlement.DomainEvents;
    using TransactionProcessor.Transaction.DomainEvents;
    using VoucherManagement.Voucher.DomainEvents;

    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            Program.CreateHostBuilder(args).Build().Run();
        }

        private static void Worker_TraceGenerated(string trace, LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    Logger.LogTrace(trace);
                    break;
                case LogLevel.Debug:
                    Logger.LogDebug(trace);
                    break;
                case LogLevel.Information:
                    Logger.LogInformation(trace);
                    break;
                case LogLevel.Warning:
                    Logger.LogWarning(trace);
                    break;
                case LogLevel.Error:
                    Logger.LogError(new Exception(trace));
                    break;
                case LogLevel.Critical:
                    Logger.LogCritical(new Exception(trace));
                    break;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            Console.Title = "Estate Reporting";

            //At this stage, we only need our hosting file for ip and ports
            IConfigurationRoot config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                                  .AddJsonFile("hosting.json", optional: true)
                                                                  .AddJsonFile("hosting.development.json", optional: true)
                                                                  .AddEnvironmentVariables().Build();

            IHostBuilder hostBuilder = Host.CreateDefaultBuilder(args);
            hostBuilder.ConfigureLogging(logging =>
                                         {
                                             logging.AddConsole();

                                         }).ConfigureWebHostDefaults(webBuilder =>
                                                                     {
                                                                         webBuilder.UseStartup<Startup>();
                                                                         webBuilder.UseConfiguration(config);
                                                                         webBuilder.UseKestrel();
                                                                     });
            //.ConfigureServices(services =>
            //                   {
            //                       VoucherIssuedEvent v = new VoucherIssuedEvent(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, "", "");

            //                       TransactionHasStartedEvent t = new TransactionHasStartedEvent(Guid.Parse("2AA2D43B-5E24-4327-8029-1135B20F35CE"), Guid.NewGuid(), Guid.NewGuid(),
            //                                                                                     DateTime.Now, "", "", "", "", null);

            //                       ReconciliationHasStartedEvent r =
            //                           new ReconciliationHasStartedEvent(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.Now);

            //                       EstateCreatedEvent e = new EstateCreatedEvent(Guid.NewGuid(), "");
            //                       MerchantCreatedEvent m = new MerchantCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), "", DateTime.Now);
            //                       ContractCreatedEvent c = new ContractCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "");
            //                       MerchantBalanceChangedEvent mb =
            //                           new MerchantBalanceChangedEvent(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, Guid.NewGuid(), Guid.NewGuid(), 0, 0, 0, "");
            //                       ImportLogCreatedEvent i = new ImportLogCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), DateTime.MinValue);
            //                       FileCreatedEvent f = new FileCreatedEvent(Guid.NewGuid(),
            //                                                                 Guid.NewGuid(),
            //                                                                 Guid.NewGuid(),
            //                                                                 Guid.NewGuid(),
            //                                                                 Guid.NewGuid(),
            //                                                                 Guid.NewGuid(),
            //                                                                 String.Empty,
            //                                                                 DateTime.MinValue);
            //                       SettlementCreatedForDateEvent s = new SettlementCreatedForDateEvent(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now);

            //                       TypeProvider.LoadDomainEventsTypeDynamically();
            //                       services.AddHostedService<SubscriptionWorker>(provider =>
            //                                                                     {
            //                                                                         IDomainEventHandlerResolver r =
            //                                                                             provider.GetRequiredService<IDomainEventHandlerResolver>();
            //                                                                         EventStorePersistentSubscriptionsClient p = provider.GetRequiredService<EventStorePersistentSubscriptionsClient>();
            //                                                                         HttpClient h = provider.GetRequiredService<HttpClient>();
            //                                                                         SubscriptionWorker worker = new SubscriptionWorker(r, p, h);
            //                                                                         worker.TraceGenerated += Worker_TraceGenerated;
            //                                                                         return worker;
            //                                                                     });
            //                   });
            
            return hostBuilder;
        }
    }
}
