using Any.Core;
using Any.Core.Configuration;
using Any.Core.Http;
using Any.Core.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Any.Api.Framework.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static (IEngine, AnyConfig) ConfigureApplicationServices(this IServiceCollection services,
             IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            //most of API providers require TLS 1.2 nowadays
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //add NopConfig configuration parameters
            var nopConfig = services.ConfigureStartupConfig<AnyConfig>(configuration.GetSection("Any"));

            //add hosting configuration parameters
            //services.ConfigureStartupConfig<HostingConfig>(configuration.GetSection("Hosting"));

            //add accessor to HttpContext
            services.AddHttpContextAccessor();

            services.AddControllers();

            //create default file provider
            CommonHelper.DefaultFileProvider = new AnyFileProvider(webHostEnvironment);

            //initialize plugins
            //var mvcCoreBuilder = services.AddMvcCore();
            //mvcCoreBuilder.PartManager.InitializePlugins(nopConfig);

            //create engine and configure service provider
            var engine = EngineContext.Create();

            engine.ConfigureServices(services, configuration, nopConfig);

            return (engine, nopConfig);
        }
        /// <summary>
        /// Create, bind and register as service the specified configuration parameters 
        /// </summary>
        /// <typeparam name="TConfig">Configuration parameters</typeparam>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Set of key/value application configuration properties</param>
        /// <returns>Instance of configuration parameters</returns>
        public static TConfig ConfigureStartupConfig<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            //create instance of config
            var config = new TConfig();

            //bind it to the appropriate section of configuration
            configuration.Bind(config);

            //and register it as a service
            services.AddSingleton(config);

            return config;
        }

        /// <summary>
        /// Register HttpContextAccessor
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

    }
}
