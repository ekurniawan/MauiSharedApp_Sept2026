using MauiSharedApp.Shared.Models;
using MauiSharedApp.Shared.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MauiSharedApp.Services
{
    public class GeolocationService : IGeolocationService
    {
        public bool IsSupportedPlatform()
        {
            return true;
        }

        public bool IsGeocodingSupported()
        {
            return true;
        }

        public async Task<GeoLoc> GetCurrentLocationAsync()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                var location = await Geolocation.Default.GetLocationAsync(request);
                if (location != null)
                {
                    return new GeoLoc
                    {
                        Latitude = location.Latitude,
                        Longitude = location.Longitude
                    };
                }
                return new GeoLoc();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting current location", ex);
            }
        }

        public Task<string?> GetAddressFromCoordinatesAsync(double latitude, double longitude)
        {
            return Task.FromResult<string?>(null);
        }
    }
}
