using domain.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.common.interfaces;

public interface IMusDbContext
{
    public DbSet<AppUserImage> AppUserImages { set; get; }
    public DbSet<RefreshTokenModel> Tokens { set; get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
