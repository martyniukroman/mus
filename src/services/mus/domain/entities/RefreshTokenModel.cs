using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace domain.entities;

[Table("AspNetRefreshTokens")]
public class RefreshTokenModel
{
    [Key]
    public string? Id { get; set; }
    public string? Value { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ExpiryTime { get; set; }
    public string? UserId { get; set; }
    public string? ClientId { get; set; }

    public virtual AppUser? AppUser { get; set; }
}
