using application.common.interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace persistence
{
    public static class PersistenceDI
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MusDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MusConnectionString")));

            services.AddScoped<IMusDbContext>(provider => provider.GetService<MusDbContext>());

            return services;
        }
    }
}