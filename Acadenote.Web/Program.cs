using Acadenote.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Globalization;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<AppTheme>();
builder.Services.AddSingleton<ColorUtility>();

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("pt-BR");

var app = builder.Build();

await app.RunAsync();

