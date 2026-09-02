using MauiSharedApp.Shared.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MauiSharedApp.Services
{
    public class ConnectivityService : IConnectivityService
    {
        public bool IsConnected()
        {
            //check internet connectivity
            var connectivity = Connectivity.Current;
            return connectivity.NetworkAccess == NetworkAccess.Internet;
        }

        public bool IsSupportedPlatform()
        {
            return true;
        }
    }
}
