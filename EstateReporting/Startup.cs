using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EstateReporting
{
    using System.Data.Common;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Net.Http;
    using System.Reflection;
    using System.Threading;
    using System.Xml.Serialization;
    using BusinessLogic;
    using Common;
    using Database;
    using EventStore.Client;
    using Factories;
    using HealthChecks.UI.Client;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.OpenApi.Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using NLog.Extensions.Logging;
    using Repository;
    using Shared.EntityFramework;
    using Shared.EntityFramework.ConnectionStringConfiguration;
    using Shared.EventStore.EventHandling;
    using Shared.EventStore.EventStore;
    using Shared.Extensions;
    using Shared.General;
    using Shared.Logger;
    using Shared.Repositories;
    using Swashbuckle.AspNetCore.Filters;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using ConnectionStringType = Shared.Repositories.ConnectionStringType;
    using ILogger = Microsoft.Extensions.Logging.ILogger;

    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public static IConfigurationRoot Configuration { get; set; }

        public static IWebHostEnvironment WebHostEnvironment { get; set; }

        public Startup(IWebHostEnvironment webHostEnvironment)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(webHostEnvironment.ContentRootPath)
                                                                      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                                                      .AddJsonFile($"appsettings.{webHostEnvironment.EnvironmentName}.json", optional: true).AddEnvironmentVariables();

            Startup.Configuration = builder.Build();
            Startup.WebHostEnvironment = webHostEnvironment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigurationReader.Initialise(Startup.Configuration);

            this.ConfigureMiddlewareServices(services);
        }
        
        public void ConfigureContainer(IServiceCollection services)
        {
            Boolean useConnectionStringConfig = Boolean.Parse(ConfigurationReader.GetValue("AppSettings", "UseConnectionStringConfig"));
            
            if (useConnectionStringConfig)
            {
                String connectionStringConfigurationConnString = ConfigurationReader.GetConnectionString("ConnectionStringConfiguration");
                services.AddSingleton<IConnectionStringConfigurationRepository, ConnectionStringConfigurationRepository>();
                services.AddTransient<ConnectionStringConfigurationContext>(c =>
                                                                            {
                                                                                return new ConnectionStringConfigurationContext(connectionStringConfigurationConnString);
                                                                            });
            }
            else
            {
                services.AddSingleton<IConnectionStringConfigurationRepository, ConfigurationReaderConnectionStringRepository>();
            }

            Dictionary<String, String[]> eventHandlersConfiguration = new Dictionary<String, String[]>();

            if (Startup.Configuration != null)
            {
                IConfigurationSection section = Startup.Configuration.GetSection("AppSettings:EventHandlerConfiguration");

                if (section != null)
                {
                    Startup.Configuration.GetSection("AppSettings:EventHandlerConfiguration").Bind(eventHandlersConfiguration);
                }
            }

            services.AddSingleton<Dictionary<String, String[]>>(eventHandlersConfiguration);

            services.AddSingleton<IEstateReportingRepository, EstateReportingRepository>();
            services.AddSingleton<IEstateReportingRepositoryForReports, EstateReportingRepositoryForReports>();
            services.AddSingleton<IDbContextFactory<EstateReportingContext>, DbContextFactory<EstateReportingContext>>();
            
            services.AddSingleton<Func<Type, IDomainEventHandler>>(container => (type) =>
                                                                                {
                                                                                    IDomainEventHandler handler = container.GetService(type) as IDomainEventHandler;
                                                                                    return handler;
                                                                                });

            services.AddSingleton<Func<String, EstateReportingContext>>(cont => (connectionString) => { return new EstateReportingContext(connectionString); });
            
            services.AddSingleton<EstateDomainEventHandler>();
            services.AddSingleton<MerchantDomainEventHandler>();
            services.AddSingleton<TransactionDomainEventHandler>();
            services.AddSingleton<ContractDomainEventHandler>();
            services.AddSingleton<IDomainEventHandlerResolver, DomainEventHandlerResolver>();
            services.AddSingleton<IReportingManager, ReportingManager>();
            services.AddSingleton<IModelFactory,ModelFactory>();

            services.AddEventStorePersistentSubscriptionsClient(Startup.ConfigureEventStoreSettings);

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
            services.AddSingleton(httpClient);
        }

        private static void ConfigureEventStoreSettings(EventStoreClientSettings settings = null)
        {
            if (settings == null)
            {
                settings = new EventStoreClientSettings();
            }

            settings.CreateHttpMessageHandler = () => new SocketsHttpHandler
                                                      {
                                                          SslOptions =
                                                          {
                                                              RemoteCertificateValidationCallback = (sender,
                                                                                                     certificate,
                                                                                                     chain,
                                                                                                     errors) => true,
                                                          }
                                                      };
            settings.ConnectionName = Startup.Configuration.GetValue<String>("EventStoreSettings:ConnectionName");
            settings.ConnectivitySettings = new EventStoreClientConnectivitySettings
                                            {
                                                Address = new Uri(Startup.Configuration.GetValue<String>("EventStoreSettings:ConnectionString")),
                                            };

            settings.DefaultCredentials = new UserCredentials(Startup.Configuration.GetValue<String>("EventStoreSettings:UserName"),
                                                              Startup.Configuration.GetValue<String>("EventStoreSettings:Password"));
            Startup.EventStoreClientSettings = settings;
        }

        private static EventStoreClientSettings EventStoreClientSettings;

        private void ConfigureMiddlewareServices(IServiceCollection services)
        {
            services.AddHealthChecks()
                    .AddSqlServer(connectionString: ConfigurationReader.GetConnectionString("HealthCheck"),
                                  healthQuery: "SELECT 1;",
                                  name: "Read Model Server",
                                  failureStatus: HealthStatus.Degraded,
                                  tags: new string[] { "db", "sql", "sqlserver" })
                    .AddUrlGroup(new Uri($"{ConfigurationReader.GetValue("SecurityConfiguration", "Authority")}/health"),
                                 name: "Security Service",
                                 httpMethod: HttpMethod.Get,
                                 failureStatus: HealthStatus.Unhealthy,
                                 tags: new string[] { "security", "authorisation" });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                                   {
                                       Title = "Estate Reporting API",
                                       Version = "1.0",
                                       Description = "A REST Api to manage reporting aspects of an estate (merchants, operators and contracts).",
                                       Contact = new OpenApiContact
                                                 {
                                                     Name = "Stuart Ferguson",
                                                     Email = "golfhandicapping@btinternet.com"
                                                 }
                                   });

                // add a custom operation filter which sets default values
                c.OperationFilter<SwaggerDefaultValues>();
                c.ExampleFilters();

                //Locate the XML files being generated by ASP.NET...
                var directory = new DirectoryInfo(AppContext.BaseDirectory);
                var xmlFiles = directory.GetFiles("*.xml");

                //... and tell Swagger to use those XML comments.
                foreach (FileInfo fileInfo in xmlFiles)
                {
                    c.IncludeXmlComments(fileInfo.FullName);
                }
            });

            services.AddSwaggerExamplesFromAssemblyOf<SwaggerJsonConverter>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    //options.SaveToken = true;
                    options.Authority = ConfigurationReader.GetValue("SecurityConfiguration", "Authority");
                    options.Audience = ConfigurationReader.GetValue("SecurityConfiguration", "ApiName");
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = ConfigurationReader.GetValue("SecurityConfiguration", "ApiName"),
                        ValidIssuer = ConfigurationReader.GetValue("SecurityConfiguration", "Authority"),
                    };
                    options.IncludeErrorDetails = true;
                });

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
                options.SerializerSettings.Formatting = Formatting.Indented;
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            Assembly assembly = this.GetType().GetTypeInfo().Assembly;
            services.AddMvcCore().AddApplicationPart(assembly).AddControllersAsServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            String nlogConfigFilename = "nlog.config";

            if (env.IsDevelopment())
            {
                nlogConfigFilename = $"nlog.{env.EnvironmentName}.config";
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.ConfigureNLog(Path.Combine(env.ContentRootPath, nlogConfigFilename));
            loggerFactory.AddNLog();

            ILogger logger = loggerFactory.CreateLogger("EstateReporting");

            Logger.Initialise(logger);

            ConfigurationReader.Initialise(Startup.Configuration);

            app.AddRequestLogging();
            app.AddResponseLogging();
            app.AddExceptionHandler();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                             {
                                 endpoints.MapControllers();
                                 endpoints.MapHealthChecks("health", new HealthCheckOptions()
                                                                     {
                                                                         Predicate = _ => true,
                                                                         ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                                                                     });
                             });
            app.UseSwagger();

            app.UseSwaggerUI();
        }
    }

    
}
