using MauiSharedApp.Services;
using MauiSharedApp.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using Plugin.Firebase.CloudMessaging;
using Plugin.Firebase.CloudMessaging.EventArgs;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;


#if IOS
using Plugin.Firebase.Core.Platforms.iOS;
#elif ANDROID
using Plugin.Firebase.Core.Platforms.Android;
#endif

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
                .RegisterFirebaseServices()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Add device-specific services used by the MauiSharedApp.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();
            builder.Services.AddSingleton<IConnectivityService, ConnectivityService>();
            builder.Services.AddSingleton<IGeolocationService, GeolocationService>();
            builder.Services.AddSingleton<IPushNotifService, PushNotifService>();

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

        private static MauiAppBuilder RegisterFirebaseServices(this MauiAppBuilder builder)
        {
            builder.ConfigureLifecycleEvents(events => {
#if IOS
        events.AddiOS(iOS => iOS.WillFinishLaunching((_, __) => {
            CrossFirebase.Initialize();
            FirebaseCloudMessagingImplementation.Initialize();
            return false;
        }));
#elif ANDROID
                events.AddAndroid(android => android.OnCreate((activity, _) =>
                CrossFirebase.Initialize(activity, () => activity)));
#endif
            });

            return builder;
        }
    }
}
