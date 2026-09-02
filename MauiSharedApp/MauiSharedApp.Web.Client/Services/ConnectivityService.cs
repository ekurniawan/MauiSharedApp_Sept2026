using MauiSharedApp.Shared.Services;

namespace MauiSharedApp.Web.Client.Services
{
    public class ConnectivityService : IConnectivityService
    {
        public bool IsConnected()
        {
            //not implemented for web client, so we will return true for now
            return true;
        }

        public bool IsSupportedPlatform()
        {
            return false;
        }
    }
}
