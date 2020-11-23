using Microsoft.Extensions.DependencyInjection;

namespace SlippiStats.Configuration
{
    public static class ApplicationDatabaseExtensions
    {
        public static void AddApplicationDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddTransient(provider => new ApplicationDatabase(connectionString));
        }
    }
}