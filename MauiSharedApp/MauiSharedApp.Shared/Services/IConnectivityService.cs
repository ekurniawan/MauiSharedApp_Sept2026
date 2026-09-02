using System;
using System.Collections.Generic;
using System.Text;

namespace MauiSharedApp.Shared.Services
{
    public interface IConnectivityService
    {
        //method for check internet connectivity
        bool IsConnected();

        //check supported platforms
        bool IsSupportedPlatform();
    }
}
