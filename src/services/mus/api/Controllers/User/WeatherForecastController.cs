using domain.entities;
using infrastructure.identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace api.Controllers.User;

public class WeatherForecastController : BaseController
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [Authorize]
    [HttpGet]
    public ActionResult<string> GetAuth()
    {
        return Ok(new { weatherData = Summaries[RandomNumberGenerator.GetInt32(Summaries.Count() - 1)] });
    }

    [HttpGet]
    public ActionResult<string> GetNoAuth()
    {
        return Ok(new { weatherData = Summaries[RandomNumberGenerator.GetInt32(Summaries.Count() - 1)] });
    }

    [HttpGet]
    public ActionResult<dynamic> GetCaller()
    {
        string responce = string.Empty;

        return Ok(responce);
    }
}


