﻿using Acadenode.Core.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Acadenote.Server
{
    public static class Utils
    {
        public static Role GetRoleFromJwtToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwt = handler.ReadJwtToken(token);
            var role = jwt.Claims.Where(c => c.Type == "role").FirstOrDefault();
            if (role == null)
            {
                return Role.User;
            }
            return (Role)int.Parse(role.Value);
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
