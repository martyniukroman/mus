using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence
{
    public class MusDbContextFactory : DesignTimeDbContextFactoryBase<MusDbContext>
    {
        protected override MusDbContext CreateNewInstance(DbContextOptions<MusDbContext> options)
        {
            return new MusDbContext(options);
        }
    }
}
