using domain.entities;
using infrastructure.identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using persistence.contextInterceptors;

namespace persistence;

public static class PersistenceDI
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntityInterceptor>();

        //if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        //{
        //    services.AddDbContext<MusDbContext>(options =>
        //        options.UseInMemoryDatabase("MusConnectionString"));
        //}
        //else
        //{
            services.AddDbContext<MusDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MusConnectionString"),
                    builder => builder.MigrationsAssembly(typeof(MusDbContext).Assembly.FullName)));
        //}

        services.AddScoped<DbInitializer>();

        return services;
    }
}