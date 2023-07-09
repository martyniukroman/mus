using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.entities;

public class TokenResponseModel
{
    public string? Token { get; set; }
    public DateTime Expiration { get; set; }
    public string? Refresh_token { get; set; }
    public string? Roles { get; set; }
    public string? Username { get; set; }
    public string? DisplayName { get; set; }
    public string? UserId { get; set; }
}
