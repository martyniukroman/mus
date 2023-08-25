using domain.entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var responce = await Mediator.Send(command);
        return Ok(responce);
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
