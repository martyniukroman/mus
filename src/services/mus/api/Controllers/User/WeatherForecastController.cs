using domain.entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers.User;

public class WeatherForecastController : BaseController
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly UserManager<ApplicationUser> _userManager;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    [Authorize]
    [HttpGet]
    [Route("Auth")]
    public ActionResult<string> Get()
    {
        return Ok("responce");
    }

    [HttpGet]
    public ActionResult<string> GetNoAuth()
    {
        return Ok("responce");
    }

    [HttpGet]
    public ActionResult<dynamic> GetCaller()
    {
        string responce = string.Empty;

        return Ok(responce);
    }
}
