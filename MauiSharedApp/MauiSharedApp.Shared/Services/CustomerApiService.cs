using System.Net.Http.Json;
using MauiSharedApp.Shared.Models;

namespace MauiSharedApp.Shared.Services
{
    public class CustomerApiService : ICustomerApiService
    {
        private readonly HttpClient _httpClient;

        public CustomerApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CustomerDto>> GetCustomersAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<CustomerDto>>("api/customers");
            return result ?? new List<CustomerDto>();
        }

        public async Task<CustomerDto?> GetCustomerAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<CustomerDto>($"api/customers/{id}");
        }

        public async Task<CustomerDto?> CreateCustomerAsync(CustomerDto customer)
        {
            var response = await _httpClient.PostAsJsonAsync("api/customers", customer);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CustomerDto>();
        }

        public async Task<bool> UpdateCustomerAsync(int id, CustomerDto customer)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/customers/{id}", customer);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/customers/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<CustomerWithDistanceDto>> GetNearbyCustomersAsync(double latitude, double longitude, double radiusKm)
        {
            var url = $"api/customers/nearby?lat={latitude}&lon={longitude}&radiusKm={radiusKm}";
            var result = await _httpClient.GetFromJsonAsync<List<CustomerWithDistanceDto>>(url);
            return result ?? new List<CustomerWithDistanceDto>();
        }

        public async Task<List<SalesDto>> GetSalesAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<SalesDto>>("api/sales");
            return result ?? new List<SalesDto>();
        }
    }
}
