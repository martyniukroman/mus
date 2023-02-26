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
    public UsersController()
    {
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        //return await _userManager.Users
        //    .OrderBy(u => u)
        //    .Select(u => new UserDto(u.Id, u.UserName ?? string.Empty, u.Email ?? string.Empty))
        //    .ToListAsync();
        return Ok();
    }

    [HttpPost]
    public async Task<dynamic> Register(string email, string password)
    {
        //return Ok(await this._identityService.CreateUserAsync(email, password))
        return Ok();
    }

    [HttpPut]
    public async Task<dynamic> Auth(string userId, string policy)
    {

        return Ok();

        //try
        //{
        //    return Ok(await this._identityService.AuthorizeAsync(userId, policy));
        //}
        //catch (System.Exception ex)
        //{
        //    Console.WriteLine(ex);
        //    return BadRequest(ex);
        //}

    }


}
