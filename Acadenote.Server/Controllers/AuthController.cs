﻿using Acadenode.Core.Models;
using Acadenode.Core.Repositories;
using Acadenode.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Acadenote.Server.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public AuthController(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("auth/login")]
        public async Task<ActionResult> Login([FromBody] LoginModel model)
        {
            var (status, token) = await _authService.Login(model);

            if (status == 200)
            {
                var user = await _userRepository.GetUserByUsername(model.Username);
                user.Token = token;
                return Ok(user);
            }
            return StatusCode(status, token);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("auth/register")]
        public async Task<ActionResult> Register([FromBody] RegistrationModel model)
        {
            var (status, token) = await _authService.Registration(model);
            if (status == 200)
            {
                var user = await _userRepository.GetUserByUsername(model.Username);
                return Ok(user);
            }
            return StatusCode(status, token);
        }

        [Authorize]
        [HttpDelete]
        [Route("auth/delete/{username}")]
        public async Task<ActionResult> DeleteUser(string username)
        {

            // Check if is admin
            string token = Request.Headers["Authorization"];
            if (token == null)
            {
                return Unauthorized();
            }

            if (token.StartsWith("Bearer"))
            {
                token = token.Substring("Bearer ".Length).Trim();
            }

            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwt = handler.ReadJwtToken(token);
            if(Utils.TryGetJwtToken(Request, out string t))
            {
                var r = await Utils.GetRoleFromJwtToken(t, _userRepository);
                if(!r.HasFlag(Role.Admin))
                {
                    return Unauthorized();
                }
            }
            // Get the role integer
            var role = await Utils.GetRoleFromJwtToken(token, _userRepository);


            var user = await _userRepository.GetUserByUsername(username);
            if (user == null)
            {
                return NotFound();
            }
            await _userRepository.DeleteUser(username);
            return Ok(user);
        }

        [Authorize]
        [HttpGet]
        [Route("auth/test")]
        public async Task<ActionResult> Test()
        {
            string token = Request.Headers["Authorization"];
            if(token == null)
            {
                return Unauthorized();
            }
            if (token.StartsWith("Bearer"))
            {
                token = token.Substring("Bearer ".Length).Trim();
            }

            var handler = new JwtSecurityTokenHandler();

            JwtSecurityToken jwt = handler.ReadJwtToken(token);

            var claims = "List of Claims: \n\n";

            foreach (var claim in jwt.Claims)
            {
                claims += $"{claim.Type}: {claim.Value}\n";
            }

            claims += "\n\n";

            return Ok(claims);
        }

        [Authorize]
        [HttpGet]
        [Route("auth/testadmin")]
        public async Task<ActionResult> TestAdmin()
        {
            string token = Request.Headers["Authorization"];
            if(token == null)
            {
                return Unauthorized();
            }
            if (token.StartsWith("Bearer"))
            {
                token = token.Substring("Bearer ".Length).Trim();
            }

            // Gets "Admin" role from the token, if it exists return success, else return unauthorized
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwt = handler.ReadJwtToken(token);
            var role = await Utils.GetRoleFromJwtToken(token, _userRepository);
            if (role == null)
            {
                return Unauthorized();
            }
            
            if(role.HasFlag(Role.Admin))
            {
                return Ok("You are an Admin");
            }

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("auth/validate")]
        public async Task<ActionResult> Validate()
        {
            string token = Request.Headers["Authorization"];
            if (token == null)
            {
                return Unauthorized();
            }
            if (token.StartsWith("Bearer"))
            {
                token = token.Substring("Bearer ".Length).Trim();
            }

            var valid = await _authService.Validate(token);
            if (valid)
            {
                return Ok("Token is valid.");
            }
            return Unauthorized();
        }
    }
}