using application.common.mappings;
using AutoMapper;
using domain.entities;

namespace application.users.Queries.GetUser;

public class UserDto : IMapFrom<AppUser>
{
    public UserDto() : this(string.Empty, string.Empty, string.Empty) { }

    public UserDto(string id, string userName, string email)
    {
        Id = id;
        UserName = userName;
        Email = email;
    }

    public string Id { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public List<string> Roles { get; set; } = new();


    public void Mapping(Profile profile)
    {
        profile.CreateMap<AppUser, UserDto>();
    }
}
