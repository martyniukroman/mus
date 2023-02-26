using Microsoft.EntityFrameworkCore;
using persistence.contextInterceptors;
using System.Reflection;

namespace persistence;

public class MusDbContext : DbContext
{

    private readonly AuditableEntityInterceptor _auditableEntityInterceptor;

    public MusDbContext(DbContextOptions<MusDbContext> options,
        AuditableEntityInterceptor auditableEntityInterceptor) :
        base(options)
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
