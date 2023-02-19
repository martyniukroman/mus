using domain.entities;
using infrastructure.identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace api.Controllers.User;

public class WeatherForecastController : BaseController
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly DbInitializer _dbInitializer;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, UserManager<ApplicationUser> userManager, DbInitializer dbInitializer)
    {
        _logger = logger;
        _userManager = userManager;
        _dbInitializer = dbInitializer;
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

    [HttpPost]
    public async Task init()
    {
        await this._dbInitializer.InitialiseAsync();
        await this._dbInitializer.TrySeedAsync();
    }
}


