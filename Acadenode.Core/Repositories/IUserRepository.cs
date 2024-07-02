using Acadenode.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acadenode.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsername(string username);
        Task<User> GetUserByUsernameAndPassword(string username, string password);
        Task<ServiceResponse> CreateUser(User user);
        Task<ServiceResponse> UpdateUser(User user);
        Task<ServiceResponse> DeleteUser(string username);
    }
}
