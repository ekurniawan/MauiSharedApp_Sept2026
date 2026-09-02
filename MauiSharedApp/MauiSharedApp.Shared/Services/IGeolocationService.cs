using MauiSharedApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MauiSharedApp.Shared.Services
{
    public interface IGeolocationService
    {
        bool IsSupportedPlatform();
        bool IsGeocodingSupported();
        Task<GeoLoc> GetCurrentLocationAsync();
        Task<string?> GetAddressFromCoordinatesAsync(double latitude, double longitude);
    }
}
