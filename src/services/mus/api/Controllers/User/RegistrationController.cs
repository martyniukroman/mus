using domain.entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using application.account.commands.RegisterUser;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers.User;

[Route("api/[controller]/[action]")]
[ApiController]
public class RegistrationController : BaseController
{
    private UserManager<AppUser> _userManager;

    public RegistrationController(UserManager<AppUser> userManager)
	{
       this._userManager = userManager;
	}

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommandDto formData)
    {
        // holds errors related to registration process
        var errors = new List<string>();

        var user = new AppUser()
        {
            Email = formData.Email,
            UserName = formData.DisplayName,
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        var result = await _userManager.CreateAsync(user, formData.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Customer");
            return new OkObjectResult(result);
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
                errors.Add(error.Description);
            }

            return new BadRequestObjectResult(errors);
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> IsUserNameExist([FromQuery] string username)
    {
        return new OkObjectResult(await _userManager.FindByNameAsync(username) != null);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> IsUserEmailExist([FromQuery] string useremail)
    {
        return new OkObjectResult(await _userManager.FindByEmailAsync(useremail) != null);
    }

}
