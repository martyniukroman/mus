using domain.entities;
using Microsoft.EntityFrameworkCore;
using persistence;

namespace infrastructure.identity.Data;

public class DbInitializer
{
    private readonly MusDbContext _context;

    private const string AdministratorsRole = "Administrator";
    private const string UserRole = "User";
    private const string DefaultPassword = "123Admin!";

    public DbInitializer(
        MusDbContext context
    )
    {
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    //To be moved in a separate seeding services
    public async Task TrySeedAsync()
    {
        return;
    }
}
