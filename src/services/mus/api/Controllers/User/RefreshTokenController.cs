using domain.entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration;

namespace api.Controllers.User;

[Route("api/[controller]")]
[ApiController]
public class RefreshTokenController : BaseController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly MusDbContext _musDbContext;
    private readonly JwtIssuerOptions _jwtIssuerOptions;
    private readonly IConfiguration _configuration;

    public RefreshTokenController(UserManager<AppUser> userManager,
            MusDbContext musDbContext,
            IOptions<JwtIssuerOptions> options,
            IConfiguration configuration)
    {
        _userManager = userManager;
        _musDbContext = musDbContext;
        _jwtIssuerOptions = options.Value;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Post([FromBody] TokenRequestModel model)
    {
        if (model == null)
        {
            return BadRequest(ModelState);
        }

        switch (model.GrantType)
        {
            case "password": return await GenerateNewToken(model);
            case "refresh_token": return await RefreshToken(model);
            default: return Unauthorized();
        }
    }

    private async Task<IActionResult> RefreshToken(TokenRequestModel model)
    {
        try
        {
            var refreshToken = _musDbContext.Tokens.FirstOrDefault(x =>
                x.ClientId == _jwtIssuerOptions.ClientId && x.Value == model.RefreshToken);

            if (refreshToken == null)
                return new BadRequestObjectResult(new
                {
                    code = 400,
                    caption = "Invalid refresh token",
                    tag = "rTokenError",
                    afterAction = "relogin"
                });
            if (refreshToken.ExpiryTime < DateTime.UtcNow)
                return new BadRequestObjectResult(new
                {
                    code = 400,
                    caption = "Token lifetime is expired",
                    tag = "rTokenError",
                    afterAction = "relogin"
                });

            var user = await _userManager.FindByIdAsync(refreshToken.UserId);

            if (user == null)
                return new NotFoundObjectResult(new
                {
                    code = 404,
                    caption = "User not found",
                    tag = "notFoundError"
                });

            var newRefreshToken = CreateRefreshToken(refreshToken.ClientId, user.Id);
            _musDbContext.Tokens.Remove(refreshToken);
            _musDbContext.Tokens.Add(newRefreshToken);
            await _musDbContext.SaveChangesAsync();

            var response = await CreateAccessToken(user, newRefreshToken.Value);
            return new OkObjectResult(new { authToken = response });
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(new
            {
                code = 500,
                caption = e.Message + ' ' + e.InnerException?.Message,
                tag = "exceptionError"
            });
        }
    }

    private async Task<IActionResult> GenerateNewToken(TokenRequestModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.UserName);

        if (user == null)
            user = await _userManager.FindByNameAsync(model.UserName);


        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            //TODO: email validation isEmailConfirmed

            var newRefreshToken =
                CreateRefreshToken(_jwtIssuerOptions.ClientId, user.Id); // client id might be null

            var oldRefreshTokens = _musDbContext.Tokens.Where(x => x.UserId == user.Id);
            if (oldRefreshTokens != null)
            {
                _musDbContext.Tokens.RemoveRange(oldRefreshTokens);
            }

            _musDbContext.Tokens.Add(newRefreshToken);
            await _musDbContext.SaveChangesAsync();

            var accessToken = await CreateAccessToken(user, newRefreshToken.Value);
            return new OkObjectResult(new { authToken = accessToken });
        }

        return new BadRequestObjectResult(new
        {
            code = 404,
            caption = "Invalid Username/Password",
            tag = "notFoundError"
        });
    }

    private async Task<TokenResponseModel> CreateAccessToken(AppUser appUser, string rToken)
    {
        double tokenExpiryTime = Convert.ToDouble(_jwtIssuerOptions.NotBefore); // token lifetime
        var appKey = this._configuration.GetValue<string>("JwtIssuerOptions:Secret") ?? "TestSecret";
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appKey));
        var roles = await _userManager.GetRolesAsync(appUser);
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, appUser.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, appUser.Id),
                 // new Claim(ClaimTypes.Role, roles.FirstOrDefault()), // not sure about validating role
            }),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
            Issuer = _jwtIssuerOptions.Issuer,
            Audience = _jwtIssuerOptions.Audience,
            Expires = DateTime.UtcNow.AddMinutes(tokenExpiryTime),
        };

        var access_token = tokenHandler.CreateToken(tokenDescriptor);
        var encoded_token = tokenHandler.WriteToken(access_token);
        return new TokenResponseModel()
        {
            Token = encoded_token,
            Expiration = access_token.ValidTo,
            Refresh_token = rToken,
            Roles = roles.FirstOrDefault(),
            Username = appUser.UserName,
            UserId = appUser.Id,
        };
    }

    private RefreshTokenModel CreateRefreshToken(string clientId, string userId)
    {
        return new RefreshTokenModel()
        {
            ClientId = clientId,
            UserId = userId,
            Value = Guid.NewGuid().ToString("N"),
            CreatedDate = DateTime.UtcNow,
            ExpiryTime = DateTime.UtcNow.AddDays(1),
            Id = Guid.NewGuid().ToString("N"),
        };
    }
}
