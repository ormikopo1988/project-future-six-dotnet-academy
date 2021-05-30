using DI.TinyCrm.Core.ApplicationInsights;
using DI.TinyCrm.Core.Interfaces;
using DI.TinyCrm.Core.Services;
using Microsoft.ApplicationInsights.AspNetCore.TelemetryInitializers;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace DI.TinyCrm.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }

        public static IServiceCollection AddApplicationInsights(this IServiceCollection services, IConfiguration configuration)
        {
            // The following line enables Application Insights telemetry collection.
            services.AddApplicationInsightsTelemetry();

            // Register the settings for "ApplicationInsights" section as a service for injection from DI container
            var applicationInsightsSettings = new ApplicationInsightsSettings();
            configuration.Bind(ApplicationInsightsSettings.ApplicationInsightsSectionKey, applicationInsightsSettings);
            services.AddSingleton(applicationInsightsSettings);

            // Use telemetry initializers when you want to enrich telemetry with additional information
            services.AddSingleton<ITelemetryInitializer, CloudRoleTelemetryInitializer>();

            // Remove a specific built-in telemetry initializer
            var telemetryInitializerToRemove = services.FirstOrDefault<ServiceDescriptor>
                                (t => t.ImplementationType == typeof(AspNetCoreEnvironmentTelemetryInitializer));

            if (telemetryInitializerToRemove != null)
            {
                services.Remove(telemetryInitializerToRemove);
            }

            // You can add custom telemetry processors to TelemetryConfiguration by using the extension method AddApplicationInsightsTelemetryProcessor on IServiceCollection. 
            // You use telemetry processors in advanced filtering scenarios
            services.AddApplicationInsightsTelemetryProcessor<StaticWebAssetsTelemetryProcessor>();

            return services;
        }
    }
}
