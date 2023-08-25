using application.common.interfaces;
using domain.entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace application.account.commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
{

    private readonly IMusDbContext _context;
    private UserManager<AppUser> _userManager;

    public RegisterUserCommandHandler(IMusDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // holds errors related to registration process
        var errors = new List<string>();

        var user = new AppUser()
        {
            Email = request.Email,
            UserName = request.DisplayName,
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        var result = await _userManager.CreateAsync(user, request.Password ?? throw new Exception("password"));
        AppUser newUser = null;
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Customer");
            newUser = await _userManager.FindByEmailAsync(request?.Email);
        }
        return newUser?.Id ?? string.Empty;
    }
}
