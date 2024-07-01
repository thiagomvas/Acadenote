using Acadenode.Core.Repositories;
using Acadenote.API.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Acadenote.API
{
    public static class DependencyInjection
    {
        public static void AddDatabaseAccess(this IServiceCollection services)
        {
            services.AddScoped<INoteRepository, NoteRepository>();   
        }
    }
}
