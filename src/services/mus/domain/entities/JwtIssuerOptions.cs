using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.entities;

public class JwtIssuerOptions
{
    public string? Issuer { get; set; }
    public string? Subject { get; set; }
    public string? Audience { get; set; }
    public string? Secret { get; set; }
    public string? ClientId { get; set; }
    public string? NotBefore => "1";
}
