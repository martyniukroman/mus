using MediatR;

namespace application.users.Queries.GetUser;

public class GetUserQuery : IRequest<UserDto>
{
    public string Id { get; set; }
}
