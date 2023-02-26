using application.common.models;

namespace application.common.interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<dynamic> CreateUserAsync(string userName, string password);

    Task<dynamic> DeleteUserAsync(string userId);
}
