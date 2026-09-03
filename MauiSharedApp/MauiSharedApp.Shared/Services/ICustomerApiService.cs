using MauiSharedApp.Shared.Models;

namespace MauiSharedApp.Shared.Services
{
    public interface ICustomerApiService
    {
        Task<List<CustomerDto>> GetCustomersAsync();
        Task<CustomerDto?> GetCustomerAsync(int id);
        Task<CustomerDto?> CreateCustomerAsync(CustomerDto customer);
        Task<bool> UpdateCustomerAsync(int id, CustomerDto customer);
        Task<bool> DeleteCustomerAsync(int id);
        Task<List<CustomerWithDistanceDto>> GetNearbyCustomersAsync(double latitude, double longitude, double radiusKm);
        Task<List<SalesDto>> GetSalesAsync();
    }
}
