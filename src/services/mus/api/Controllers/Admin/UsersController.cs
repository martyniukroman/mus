using application.users.Queries.GetUser;
using domain.entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers.Admin;

[Route("api/[controller]")]
[ApiController]
public class UsersController : BaseController
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UsersController(UserManager<ApplicationUser> userManager)
    {
        this._userManager = userManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        return await _userManager.Users
            .OrderBy(u => u.UserName)
            .Select(u => new UserDto(u.Id, u.UserName ?? string.Empty, u.Email ?? string.Empty))
            .ToListAsync();
    }
}
