using application.common.interfaces;
using domain.entities;
using infrastructure.identity;
using infrastructure.identity.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using persistence.contextInterceptors;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace persistence
{
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

            services.AddScoped<MusDbContext>();
            services.AddScoped<DbInitializer>();

            services
                .AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<MusDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, MusDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddAuthorization(options =>
                options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

            return services;
        }
    }
}