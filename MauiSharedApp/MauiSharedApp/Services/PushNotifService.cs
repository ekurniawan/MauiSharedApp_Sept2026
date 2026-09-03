using MauiSharedApp.Shared.Services;
using Plugin.Firebase.CloudMessaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MauiSharedApp.Services
{
    public class PushNotifService : IPushNotifService
    {
        public async Task<string> GenerateTokenAsync()
        {
            await CrossFirebaseCloudMessaging.Current.CheckIfValidAsync();
            var token = await CrossFirebaseCloudMessaging.Current.GetTokenAsync();
            return token;
        }
    }
}
