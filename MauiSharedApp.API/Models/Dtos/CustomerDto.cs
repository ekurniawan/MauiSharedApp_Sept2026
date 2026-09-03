namespace MauiSharedApp.API.Models.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int SalesId { get; set; }
        public SalesDto? Sales { get; set; }
    }

    public class CustomerWithDistanceDto
    {
        public CustomerDto Customer { get; set; } = null!;
        public double DistanceKm { get; set; }
    }
}
