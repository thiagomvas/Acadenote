using Acadenode.Core.Repositories;
using Acadenode.Core.Services;
using Acadenote.API.Repositories;
using Acadenote.API.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Acadenote.API
{
    public static class DependencyInjection
    {
        public static void AddDatabaseAccess(this IServiceCollection services)
        {
            services.AddScoped<INoteRepository, NoteRepository>();   
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
