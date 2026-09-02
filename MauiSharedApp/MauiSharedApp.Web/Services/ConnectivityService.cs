using MauiSharedApp.Shared.Services;

namespace MauiSharedApp.Web.Services
{
    public class ConnectivityService : IConnectivityService
    {
        public bool IsConnected()
        {
            //not implemented for web, so we will return true for now
            return true;
        }

        public bool IsSupportedPlatform()
        {
            return false;
        }
    }
}
