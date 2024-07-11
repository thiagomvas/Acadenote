using Acadenode.Core.Models;
using Acadenode.Core.Repositories;
using System.Net.Http.Json;

namespace Acadenote.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HttpClient _httpClient;

        public UserRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ServiceResponse> CreateUser(User user)
        {
            var model = new RegistrationModel()
            {
                Email = user.Email,
                Password = user.Password,
                Username = user.UserName,
                Name = user.Name
            };
            var response = await _httpClient.PostAsJsonAsync(Path.Combine(Config.AuthEndpoint, "register"), user);
            if (response.IsSuccessStatusCode)
            {
                return new ServiceResponse { Success = true, Message = await response.Content.ReadAsStringAsync() };
            }
            return new ServiceResponse { Success = false, Message = await response.Content.ReadAsStringAsync() };
        }

        public Task<ServiceResponse> DeleteUser(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByUsername(string username)
        { 
            var response = await _httpClient.GetAsync(Path.Combine(Config.AuthEndpoint, "user", username));
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<User>();
            }
            else
            {
                return null;
            }
        }

        public Task<User> GetUserByUsernameAndPassword(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<string[]> GetUserIds()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
