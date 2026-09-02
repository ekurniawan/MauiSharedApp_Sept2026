using MauiSharedApp.Services;
using MauiSharedApp.Shared.Services;
using Microsoft.Extensions.Logging;

namespace MauiSharedApp
{
    public static class MauiProgram
    {
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


            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
