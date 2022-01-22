namespace EstateReporting.Bootstrapper
{
    using System.Net.Http;
    using BusinessLogic;
    using Factories;
    using Lamar;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Lamar.ServiceRegistry" />
    public class MiscRegistry : ServiceRegistry
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MiscRegistry"/> class.
        /// </summary>
        public MiscRegistry()
        {
            this.AddSingleton<IReportingManager, ReportingManager>();
            this.AddSingleton<IModelFactory, ModelFactory>();
            this.AddEventStorePersistentSubscriptionsClient(Startup.ConfigureEventStoreSettings);

            var httpMessageHandler = new SocketsHttpHandler
                                     {
                                         SslOptions =
                                         {
                                             RemoteCertificateValidationCallback = (sender,
                                                                                    certificate,
                                                                                    chain,
                                                                                    errors) => true,
                                         }
                                     };
            HttpClient httpClient = new HttpClient(httpMessageHandler);
            this.AddSingleton(httpClient);
        }

        #endregion
    }
}