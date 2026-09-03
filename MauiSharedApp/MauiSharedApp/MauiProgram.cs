using MauiSharedApp.Services;
using MauiSharedApp.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MauiSharedApp
{
    public static class MauiProgram
    {
        // Note: Android emulator uses 10.0.2.2 to reach the host machine's localhost.
        // Adjust this base URL according to where MauiSharedApp.API is running.
        private const string ApiBaseUrl = "https://localhost:7293/";

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Add device-specific services used by the MauiSharedApp.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();
            builder.Services.AddSingleton<IConnectivityService, ConnectivityService>();
            builder.Services.AddSingleton<IGeolocationService, GeolocationService>();

            // HttpClient for calling MauiSharedApp.API
            builder.Services.AddHttpClient<ICustomerApiService, CustomerApiService>(client =>
            {
                client.BaseAddress = new Uri(ApiBaseUrl);
            });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
