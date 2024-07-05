using Acadenode.Core.Models;
using Acadenode.Core.Repositories;
using System.IdentityModel.Tokens.Jwt;

namespace Acadenote.Server
{
    public static class Utils
    {
        public static async Task<Role> GetRoleFromJwtToken(string token, IUserRepository repo)
        {
            if(token.StartsWith("Bearer"))
            {
                token = token.Substring("Bearer ".Length).Trim();
            }
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwt = handler.ReadJwtToken(token);

            // Get user id and find role 
            var username = jwt.Claims.First(claim => claim.Type.ToLower() == "name").Value;

            var user = await repo.GetUserByUsername(username);
            if(user == null)
            {
                return Role.Guest;
            }
            return user.Role;
        }

        public static bool TryGetJwtToken(HttpRequest request, out string token)
        {
            token = request.Headers["Authorization"];
            if (token == null)
            {
                return false;
            }
            if (token.StartsWith("Bearer"))
            {
                token = token.Substring("Bearer ".Length).Trim();
            }

            return true;
        }
    }
}
