﻿//using application.common.interfaces;
//using domain.entities;
//using Microsoft.EntityFrameworkCore;

//namespace infrastructure.identity;

//public class IdentityService { 


//    public IdentityService()
//    {
//    }

//    public async Task<bool> AuthorizeAsync(string userId, string policyName)
//    {
//        var user = await this._userManager.Users.SingleOrDefaultAsync(x => x.Id == userId);

//        if (user == null)
//        {
//            return false;
//        }

//        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

//        var result = await _authorizationService.AuthorizeAsync(principal, policyName); ;

//        return result.Succeeded;
//    }

//    public async Task<(IdentityResult Result, string UserId)> CreateUserAsync(string userName, string password)
//    {
//        var user = new ApplicationUser
//        {
//            UserName = userName,
//            Email = userName,
//        };

//        var result = await _userManager.CreateAsync(user, password);

//        return (result, user.Id);
//    }

//    public async Task<IdentityResult> DeleteUserAsync(string userId)
//    {
//        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
//        return user != null ? await _userManager.DeleteAsync(user) : throw new Exception($"user not found. userId: {userId}");
//    }

//    public async Task<string?> GetUserNameAsync(string userId)
//    {
//        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);
//        return user.UserName;
//    }

//    public async Task<bool> IsInRoleAsync(string userId, string role)
//    {
//        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
//        return user != null && await _userManager.IsInRoleAsync(user, role);
//    }

//}
