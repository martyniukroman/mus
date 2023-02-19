using application.common.interfaces;
using application.users.Queries.GetUser;
using domain.entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers.Admin;

[Route("api/[controller]")]
[ApiController]
public class UsersController : BaseController
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IIdentityService _identityService;

    public UsersController(UserManager<ApplicationUser> userManager, IIdentityService identityService)
    {
        this._userManager = userManager;
        this._identityService = identityService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        return await _userManager.Users
            .OrderBy(u => u.UserName)
            .Select(u => new UserDto(u.Id, u.UserName ?? string.Empty, u.Email ?? string.Empty))
            .ToListAsync();
    }

    [HttpPost]
    public async Task<dynamic> register(string email, string password)
    {
        return Ok(await this._identityService.CreateUserAsync(email, password));
    }

    [HttpPut]
    public async Task<dynamic> auth(string userId, string policy)
    {
        try
        {
            return Ok(await this._identityService.AuthorizeAsync(userId, policy));
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest(ex);
        }

    }


}
