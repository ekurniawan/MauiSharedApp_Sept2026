using MauiSharedApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MauiSharedApp.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Sales> Sales => Set<Sales>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sales>()
                .HasMany(s => s.Customers)
                .WithOne(c => c.Sales)
                .HasForeignKey(c => c.SalesId)
                .OnDelete(DeleteBehavior.Restrict);

            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sales>().HasData(
                new Sales { Id = 1, Name = "Budi Santoso", Email = "budi.santoso@example.com", Phone = "081100000001" },
                new Sales { Id = 2, Name = "Siti Rahayu", Email = "siti.rahayu@example.com", Phone = "081100000002" }
            );

            // Catatan: HasData tidak mendukung navigation property, jadi relasi diset lewat SalesId (FK) langsung.
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "Toko Tugu Jaya", Address = "Tugu Yogyakarta", Latitude = -7.7830, Longitude = 110.3671, SalesId = 1 },
                new Customer { Id = 2, Name = "Batik Keraton", Address = "Keraton Yogyakarta", Latitude = -7.8052, Longitude = 110.3641, SalesId = 1 },
                new Customer { Id = 3, Name = "Toko Oleh-Oleh Malioboro", Address = "Malioboro Street", Latitude = -7.7925, Longitude = 110.3654, SalesId = 1 },
                new Customer { Id = 4, Name = "Warung Prambanan", Address = "Candi Prambanan", Latitude = -7.7520, Longitude = 110.4915, SalesId = 2 },
                new Customer { Id = 5, Name = "Kios Borobudur", Address = "Candi Borobudur", Latitude = -7.6079, Longitude = 110.2038, SalesId = 2 },
                new Customer { Id = 6, Name = "Cafe Parangtritis", Address = "Pantai Parangtritis", Latitude = -8.0253, Longitude = 110.3316, SalesId = 2 }
            );
        }
    }
}
