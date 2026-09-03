using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MauiSharedApp.API.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(150)]
        public string? Email { get; set; }

        [MaxLength(30)]
        public string? Phone { get; set; }

        [MaxLength(250)]
        public string? Address { get; set; }

        // Geolocation
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        // Relasi ke Sales (satu Sales bisa menangani banyak Customer)
        public int SalesId { get; set; }

        [ForeignKey(nameof(SalesId))]
        public Sales? Sales { get; set; }
    }
}
