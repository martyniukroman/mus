using application.common.interfaces;
using domain.applicationExceptions;
using domain.entities;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using persistence.contextInterceptors;
using System.Reflection;

namespace persistence;

public class MusDbContext : ApiAuthorizationDbContext<ApplicationUser>
{

    private readonly AuditableEntityInterceptor _auditableEntityInterceptor;

    public MusDbContext(DbContextOptions<MusDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions,
        AuditableEntityInterceptor auditableEntityInterceptor) :
        base(options, operationalStoreOptions)
    {
        _auditableEntityInterceptor = auditableEntityInterceptor;
    }       

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntityInterceptor);
    }

}
