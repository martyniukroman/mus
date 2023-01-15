using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.common.interfaces
{
    public interface IMusDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
