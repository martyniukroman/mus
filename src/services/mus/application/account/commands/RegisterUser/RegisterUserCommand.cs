using MediatR;
using System.ComponentModel.DataAnnotations;

namespace application.account.commands.RegisterUser;

public class RegisterUserCommand : IRequest<string>
{
    [Required]
    [EmailAddress]
    public string? Email { set; get; }

    [Required]
    public string? DisplayName { set; get; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { set; get; }
}
