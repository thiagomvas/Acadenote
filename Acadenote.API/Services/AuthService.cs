﻿using Acadenode.Core.Models;
using Acadenode.Core.Repositories;
using Acadenode.Core.Services;
using System.Net.Http.Json;

namespace Acadenote.API.Services
{
    internal class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<(int, string)> Login(LoginModel model)
        {
            var response = await _httpClient.PostAsJsonAsync(Path.Combine(Config.AuthEndpoint, "login"), model);
            return ((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<(int, string)> Registeration(RegistrationModel model)
        {
            var response = await _httpClient.PostAsJsonAsync(Path.Combine(Config.AuthEndpoint, "register"), model);
            return ((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}
