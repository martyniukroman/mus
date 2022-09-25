using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace persistence
{
    public static class PersistenceDI
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}