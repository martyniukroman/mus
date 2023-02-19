using domain.entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using persistence;

namespace infrastructure.identity.Data;

public class DbInitializer
{
    private readonly MusDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    private const string AdministratorsRole = "Administrator";
    private const string UserRole = "User";
    private const string DefaultPassword = "123Admin!";

    public DbInitializer(
        MusDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
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
        // Default roles
        var administratorRole = new IdentityRole();
        administratorRole.Name = AdministratorsRole;

        var userRole = new IdentityRole();
        userRole.Name = UserRole;        

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await this._roleManager.CreateAsync(administratorRole);
        }

        if (_roleManager.Roles.All(r => r.Name != userRole.Name))
        {
            await this._roleManager.CreateAsync(administratorRole);
        }

        //await _context.SaveChangesAsync();

        // Default users
        var administrator = new ApplicationUser { UserName = "admin", Email = "admin@gmail.com" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, DefaultPassword);
            await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
        }

        var user = new ApplicationUser { UserName = "user", Email = "user@gmail.com" };

        if (_userManager.Users.All(u => u.UserName != user.UserName))
        {
            await _userManager.CreateAsync(user, DefaultPassword);
            await _userManager.AddToRolesAsync(user, new[] { UserRole });
        }

        await _context.SaveChangesAsync();


        // Default data
        // Seed, if necessary
        //if (!_context.somedata.any)
        //{
        //    _context.somedata.Add(new somedata
        //    {

        //    });

        //    await _context.SaveChangesAsync();
        //}
    }
}
