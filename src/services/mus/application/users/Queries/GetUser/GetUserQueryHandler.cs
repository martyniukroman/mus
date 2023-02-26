//using application.common.interfaces;
//using AutoMapper;
//using domain.entities;
//using MediatR;
//using Microsoft.AspNetCore.Identity;

//namespace application.users.Queries.GetUser;

//public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
//{

//    private readonly IMapper _mapper;
//    private readonly UserManager<ApplicationUser> _userManager;

//    public GetUserQueryHandler(
//        IMapper mapper,
//        UserManager<ApplicationUser> userManager
//    )
//    {
//        this._mapper = mapper;
//        this._userManager = userManager;
//    }

//    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
//    {
//        var user = await _userManager.FindByIdAsync(request.Id);

//        if (user == null)
//        {
//            return null;
//        }

//        var dto = new UserDto(user.Id, user.UserName ?? string.Empty, user.Email ?? string.Empty);

//        var roles = await _userManager.GetRolesAsync(user);

//        dto.Roles.AddRange(roles);

//        return dto;
//    }
//}
