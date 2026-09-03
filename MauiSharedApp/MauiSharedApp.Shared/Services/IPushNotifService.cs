using System;
using System.Collections.Generic;
using System.Text;

namespace MauiSharedApp.Shared.Services
{
    public interface IPushNotifService
    {
        //generate token for push notification
        Task<string> GenerateTokenAsync();
    }
}
