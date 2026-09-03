using System.ComponentModel.DataAnnotations;

namespace MauiSharedApp.API.Models
{
    public class Sales
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(150)]
        public string? Email { get; set; }

        [MaxLength(30)]
        public string? Phone { get; set; }

        public List<Customer> Customers { get; set; } = new();
    }
}
