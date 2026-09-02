using MauiSharedApp.Shared.Models;
using MauiSharedApp.Shared.Services;

using Microsoft.JSInterop;

namespace MauiSharedApp.Web.Services
{
    public class GeolocationService : IGeolocationService
    {
        private readonly IJSRuntime _jsRuntime;

        private IJSObjectReference? module;

        public GeolocationService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        //get module async
        private async Task<IJSObjectReference> GetModuleAsync()
        {
            if (module == null)
            {
                module = await _jsRuntime.InvokeAsync<IJSObjectReference>(
                    "import", "./_content/MauiSharedApp.Shared/js/geolocation.js");
            }
            return module;
        }

        public Task<string?> GetAddressFromCoordinatesAsync(double latitude, double longitude)
        {
            throw new NotImplementedException();
        }

        public async Task<GeoLoc> GetCurrentLocationAsync()
        {
            var module = await GetModuleAsync();
            var result = await module.InvokeAsync<GeoLoc>("getCurrentPosition");
            return result;
        }

        public bool IsGeocodingSupported()
        {
            return false;
        }

        public bool IsSupportedPlatform()
        {
            return true;
        }
    }
}
