using MauiSharedApp.Shared.Services;
using MauiSharedApp.Web.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the MauiSharedApp.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddSingleton<IConnectivityService, ConnectivityService>();
builder.Services.AddSingleton<IGeolocationService, GeolocationService>();

// HttpClient for calling MauiSharedApp.API
var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7293/";
builder.Services.AddHttpClient<ICustomerApiService, CustomerApiService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

await builder.Build().RunAsync();
