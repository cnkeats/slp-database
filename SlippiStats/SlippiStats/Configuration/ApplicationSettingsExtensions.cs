using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SlippiStats.Configuration
{
    public static class ApplicationSettingsExtensions
    {
        public static ApplicationSettings AddApplicationSettings(this IConfiguration configuration, IServiceCollection services)
        {
            ApplicationSettings settings = new ApplicationSettings();
            configuration.GetSection("ApplicationSettings").Bind(settings);
            settings.DatabaseConnectionString = configuration.GetConnectionString("ApplicationDatabase");

            services.AddTransient(provider => settings);

            return settings;
        }
    }
}