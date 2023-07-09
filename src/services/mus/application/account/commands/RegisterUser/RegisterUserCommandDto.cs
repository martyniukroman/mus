using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.account.commands.RegisterUser;

public class RegisterUserCommandDto
{
    [Required]
    [EmailAddress] 
    public string Email { set; get; }

    [Required] 
    public string? DisplayName { set; get; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { set; get; }
}
