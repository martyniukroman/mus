using application.common.interfaces;
using domain.entities;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using infrastructure.identity;
using infrastructure.identity.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using persistence.contextInterceptors;
using System;
using System.IdentityModel.Tokens.Jwt;

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

        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<MusDbContext>();

        services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, MusDbContext>()
            .AddInMemoryClients(Clients.Get())
            .AddInMemoryIdentityResources(Resources.GetIdentityResources())
            .AddInMemoryApiResources(Resources.GetApiResources())
            .AddInMemoryApiScopes(Resources.GetApiScopes())
            .AddDeveloperSigningCredential();

        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Audience = "musApi";
                options.Authority = "https://localhost:44304";
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Administrator", policy => policy.RequireRole("Administrator"));
            options.AddPolicy("User", policy => policy.RequireRole("User"));
        });

        return services;
    }
}