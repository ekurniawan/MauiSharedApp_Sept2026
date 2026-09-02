using MauiSharedApp.Shared.Services;
using MauiSharedApp.Web.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the MauiSharedApp.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddSingleton<IConnectivityService, ConnectivityService>();
builder.Services.AddSingleton<IGeolocationService, GeolocationService>();

await builder.Build().RunAsync();
