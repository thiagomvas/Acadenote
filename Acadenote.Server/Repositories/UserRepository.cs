using Acadenode.Core.Models;
using Acadenode.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Acadenote.Server.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AcadenoteDbContext _context;

        public UserRepository(AcadenoteDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse> CreateUser(User user)
        {
            var response = new ServiceResponse();
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse> DeleteUser(string username)
        {
            var response = new ServiceResponse();
            try
            {
                var user = await _context.Users.FindAsync(username);
                if (user == null)
                {
                    response.Success = false;
                    response.Data = "User not found.";
                    return response;
                }
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = ex.Message;
            }
            return response;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await _context.Users.FindAsync(username);
            return user;
        }

        public async Task<User> GetUserByUsernameAndPassword(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
            return user;
        }

        public async Task<ServiceResponse> UpdateUser(User user)
        {
            var response = new ServiceResponse();
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = ex.Message;
            }
            return response;
        }
    }
}
