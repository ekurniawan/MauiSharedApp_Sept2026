using MauiSharedApp.Shared.Models;
using MauiSharedApp.Shared.Services;

namespace MauiSharedApp.Web.Services
{
    public class GeolocationService : IGeolocationService
    {
        public Task<string?> GetAddressFromCoordinatesAsync(double latitude, double longitude)
        {
            throw new NotImplementedException();
        }

        public Task<GeoLoc> GetCurrentLocationAsync()
        {
            throw new NotImplementedException();
        }

        public bool IsGeocodingSupported()
        {
            return false;
        }

        public bool IsSupportedPlatform()
        {
            return false;
        }
    }
}
