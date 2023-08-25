using application.common.interfaces;
using domain.applicationExceptions;
using domain.entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using persistence.contextInterceptors;
using System;
using System.Reflection;

namespace persistence;

public class MusDbContext : IdentityDbContext<AppUser> , IMusDbContext
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

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = null;
                    entry.Entity.CreatedOn = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedBy = null;
                    entry.Entity.UpdatedOn = DateTime.Now;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<AppUserImage> AppUserImages { set; get; }
    public DbSet<RefreshTokenModel> Tokens { set; get; }

}
