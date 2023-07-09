using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.entities;

public class AppUserImage
{
    public int Id { set; get; }
    public string? Name { set; get; }

    [ForeignKey("AppUser")] public string? AppUserId { set; get; }
    public virtual AppUser? AppUser { set; get; }
}
