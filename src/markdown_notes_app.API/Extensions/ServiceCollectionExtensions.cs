using markdown_notes_app.API.Controllers;
using markdown_notes_app.Core.Interfaces.Common;
using markdown_notes_app.Infrastructure.Data;
using markdown_notes_app.Infrastructure.Logging;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace markdown_notes_app.API.Extensions
{
    /// <summary>
    /// Provides extension methods to register and configure framework-level services
    /// used by the API project (CORS, IIS integrations, etc.).
    /// </summary>
    /// <remarks>
    /// These extensions centralize service registration so they can be called from a single
    /// location (for example in `Program.cs` or the application's startup sequence).
    /// </remarks>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers a CORS policy named "CORSPolicy".
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the CORS policy to.</param>
        /// <remarks>
        /// The registered policy is permissive: it allows any origin, any HTTP method and any header.
        /// This is convenient for development and cross-origin testing but should be restricted
        /// for production scenarios to specific origins, methods, and headers as appropriate.
        /// </remarks>
        public static void ConfigureCORS(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CORSPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        /// <summary>
        /// Configures IIS integration options.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to configure.</param>
        /// <remarks>
        /// The configuration is intentionally left empty to provide a single location where
        /// IIS-specific options can be adjusted if needed (for example when hosting behind IIS).
        /// </remarks>
        public static void ConfigureIISIntegrations(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options => { });
        }

        /// <summary>
        /// Configures Logging Using NLog
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to configure.</param>
        /// <remarks>
        /// Plan to improve per layer and class for more robustness
        /// </remarks>
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager<WeatherForecastController>, LoggerManager<WeatherForecastController>>();
        }

        /// <summary>
        /// Configures the mysql db for the application
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to configure.</param>
        /// <param name="configuration">The <see cref="IConfiguration"/> to configure.</param>
        public static void ConfigureMySQLContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(
                options => { 
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)); 
                });
        }
    }
}
