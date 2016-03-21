using System.Collections.Generic;
using System.Security.Claims;
using KrakmApp.Entities;

namespace KrakmApp.Core.Repositories.Base
{
    public interface IMembershipService
    {
        MembershipContext ValidateUser(string username, string password);
        User CreateUser(string username, string email, string password, int[] roles);
        User GetUser(int userId);
        List<Role> GetUserRoles(string username);
        User GetUserByPrinciples(ClaimsPrincipal claims);
    }
}
