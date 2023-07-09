using domain.entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using persistence.contextInterceptors;
using System.Reflection;

namespace persistence;

public class MusDbContext : IdentityDbContext<AppUser>
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
        modelBuilder.Entity<IdentityRole>().HasData(
            new { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
            new { Id = "2", Name = "Customer", NormalizedName = "CUSTOMER" },
            new { Id = "3", Name = "Moderator", NormalizedName = "MODERATOR" }
        );
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntityInterceptor);
    }

    public DbSet<AppUserImage> AppUserImages { set; get; }
    public DbSet<RefreshTokenModel> Tokens { set; get; }

}
