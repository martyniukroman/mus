using Microsoft.AspNetCore.Identity;

namespace domain.entities;

public class AppUser : IdentityUser
{
    public string? PictureUrl { get; set; }

    public List<RefreshTokenModel>? Tokens { get; set; }

    public virtual AppUserImage? AppUserImage { get; set; }
}
