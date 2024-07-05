using Acadenode.Core.Models;
using Acadenode.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Acadenote.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository repo;

        public UserController(IUserRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await repo.GetUserIds();
            return Ok(users);
        }

        [HttpGet("setadmin/{id}")]
        public async Task<ActionResult<User>> SetAdmin(string id)
        {
            var user = await repo.GetUserByUsername(id);
            if (user == null)
            {
                return NotFound();
            }
            user.Role = Roles.SuperAdmin;
            await repo.UpdateUser(user);
            return Ok(user);
        }
    }
}
