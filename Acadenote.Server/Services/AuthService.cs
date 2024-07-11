using Acadenode.Core.Models;
using Acadenode.Core.Repositories;
using Acadenode.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Acadenote.Server.Services
{
    // Basic auth service to check jwt tokens
    public class AuthService : IAuthService
    {
        private readonly AcadenoteDbContext _context;
        private readonly IUserRepository _userRepository;

        public AuthService(AcadenoteDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<(int, string)> Login([FromBody] LoginModel model)
        {
            var user = await _userRepository.GetUserByUsername(model.Username);
            if (user == null)
            {
                return (404, "User not found.");
            }
            if(user.Password != GetHashString(model.Password))
            {
                return (401, "Invalid password.");
            }

            var authClaims = new List<Claim>
            {
                new Claim("name", user.UserName),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            string token = GenerateToken(authClaims);
            return (200, token);
        }

        public async Task<(int, string)> Registration([FromBody] RegistrationModel model)
        {
            User user = new User()
            {
                Name = model.Name,
                UserName = model.Username,
                Password = GetHashString(model.Password),
                Role = Roles.User,
            };

            var userExists = await _userRepository.GetUserByUsername(user.UserName);
            if (userExists != null)
            {
                return (400, "User already exists.");
            }

            var response = await _userRepository.CreateUser(user);
            if (response.Success)
            {
                var authClaims = new List<Claim>
                {
                    new Claim("name", user.UserName),
                };

                foreach (var r in user.Role.GetRoles())
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, r.ToString()));
                }

                string token = GenerateToken(authClaims);
                return (200, token);
            }
            return (400, response.Message);
        }

        private byte[] GetHash(string input)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
            {
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
            }
        }

        private string GetHashString(string input)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(input))
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secrets.JwtKey));

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = Secrets.JwtIssuer,
                Audience = Secrets.JwtAud,
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> Validate(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secrets.JwtKey);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = Secrets.JwtIssuer,
                ValidAudience = Secrets.JwtAud,
                ValidateLifetime = true
            }, out _);

            return true;
        }
    }
}
