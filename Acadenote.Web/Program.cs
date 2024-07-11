using Acadenode.Core.Repositories;
using Acadenote.Web;
using Acadenote.API;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Globalization;
using Acadenode.Core.Services;
using Acadenote.Web.Services;
using Acadenote.Web.Repositories;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<AppTheme>();
builder.Services.AddSingleton<ColorUtility>();
builder.Services.AddSingleton<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<INoteRepository, LocalNoteRepository>();

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("pt-BR");

var app = builder.Build();

await app.RunAsync();

