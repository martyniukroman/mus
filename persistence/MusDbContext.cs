using application.common.interfaces;
using common;
using domain.applicationExceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence
{
    public class MusDbContext : DbContext, IMusDbContext
    {

        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public MusDbContext(DbContextOptions<MusDbContext> options)
            : base(options)
        {
        }

        public MusDbContext(
           DbContextOptions<MusDbContext> options,
           ICurrentUserService currentUserService,
           IDateTime dateTime)
           : base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService?.UserId;
                        entry.Entity.CreatedOn = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = _currentUserService?.UserId;
                        entry.Entity.UpdatedOn = _dateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MusDbContext).Assembly);
        }
    }
}
