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
    using System.IO;
    using System.Net.Http;
    using System.Reflection;
    using System.Threading;
    using System.Xml.Serialization;
    using BusinessLogic;
    using Common;
    using Database;
    using EventStore.ClientAPI;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.AspNetCore.Mvc.Versioning;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using NLog.Extensions.Logging;
    using Repository;
    using Shared.DomainDrivenDesign.EventStore;
    using Shared.EntityFramework;
    using Shared.EntityFramework.ConnectionStringConfiguration;
    using Shared.EventStore.EventStore;
    using Shared.Extensions;
    using Shared.General;
    using Shared.Logger;
    using Shared.Repositories;
    using Swashbuckle.AspNetCore.Filters;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using ConnectionStringType = Shared.Repositories.ConnectionStringType;
    using ILogger = Microsoft.Extensions.Logging.ILogger;

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
            this.ConfigureMiddlewareServices(services);


        }
        
        public void ConfigureContainer(IServiceCollection services)
        {
            ConfigurationReader.Initialise(Startup.Configuration);
            
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
            services.AddSingleton<IDbContextFactory<EstateReportingContext>, DbContextFactory<EstateReportingContext>>();
            
            services.AddSingleton<Func<Type, IDomainEventHandler>>(container => (type) =>
                                                                                {
                                                                                    IDomainEventHandler handler = container.GetService(type) as IDomainEventHandler;
                                                                                    return handler;
                                                                                });

            services.AddSingleton<Func<String, EstateReportingContext>>(cont => (connectionString) => { return new EstateReportingContext(connectionString); });
            
            services.AddSingleton<EstateDomainEventHandler>();
            services.AddSingleton<MerchantDomainEventHandler>();
            services.AddSingleton<IDomainEventHandlerResolver, DomainEventHandlerResolver>();

            //    //builder.RegisterType<ModelFactory>().As<IModelFactory>().SingleInstance();
            //    //builder.RegisterType<Factories.ModelFactory>().As<Factories.IModelFactory>().SingleInstance();
            //    //builder.RegisterType<SecurityServiceClient>().As<ISecurityServiceClient>().SingleInstance();

            //Func <String, String> apiAddressResolver = (serviceName) => { return ConfigurationReader.GetBaseServerUri(serviceName).OriginalString; };

            //    builder.RegisterInstance<Func<String, String>>(apiAddressResolver);
            //    builder.RegisterType<HttpClient>().SingleInstance();
        }

        private void ConfigureMiddlewareServices(IServiceCollection services)
        {
            services.AddApiVersioning(
                                      options =>
                                      {
                                          // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                                          options.ReportApiVersions = true;
                                          options.DefaultApiVersion = new ApiVersion(1, 0);
                                          options.AssumeDefaultVersionWhenUnspecified = true;
                                          options.ApiVersionReader = new HeaderApiVersionReader("api-version");
                                      });

            services.AddVersionedApiExplorer(
                                             options =>
                                             {
                                                 // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                                                 // note: the specified format code will format the version as "'v'major[.minor][-status]"
                                                 options.GroupNameFormat = "'v'VVV";

                                                 // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                                                 // can also be used to control the format of the API version in route templates
                                                 options.SubstituteApiVersionInUrl = true;
                                             });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen(c =>
            {
                // add a custom operation filter which sets default values
                c.OperationFilter<SwaggerDefaultValues>();
                c.ExampleFilters();
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory,
                              IApiVersionDescriptionProvider provider)
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
                             });
            app.UseSwagger();

            app.UseSwaggerUI(
                             options =>
                             {
                                 // build a swagger endpoint for each discovered API version
                                 foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
                                 {
                                     options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                                 }
                             });
        }
    }

    
}
