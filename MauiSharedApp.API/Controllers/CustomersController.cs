using MauiSharedApp.API.Data;
using MauiSharedApp.API.Models;
using MauiSharedApp.API.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MauiSharedApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CustomersController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            var customers = await _db.Customers.Include(c => c.Sales).ToListAsync();
            return customers.Select(ToDto).ToList();
        }

        // GET: api/customers/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            var customer = await _db.Customers.Include(c => c.Sales).FirstOrDefaultAsync(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return ToDto(customer);
        }

        // POST: api/customers
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer(CustomerDto customerDto)
        {
            var customer = ToEntity(customerDto);
            _db.Customers.Add(customer);
            await _db.SaveChangesAsync();

            await _db.Entry(customer).Reference(c => c.Sales).LoadAsync();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, ToDto(customer));
        }

        // PUT: api/customers/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (id != customerDto.Id)
            {
                return BadRequest();
            }

            var customer = await _db.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            customer.Name = customerDto.Name;
            customer.Email = customerDto.Email;
            customer.Phone = customerDto.Phone;
            customer.Address = customerDto.Address;
            customer.Latitude = customerDto.Latitude;
            customer.Longitude = customerDto.Longitude;
            customer.SalesId = customerDto.SalesId;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _db.Customers.AnyAsync(c => c.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/customers/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _db.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _db.Customers.Remove(customer);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        // GET: api/customers/nearby?lat=&lon=&radiusKm=
        [HttpGet("nearby")]
        public async Task<ActionResult<IEnumerable<CustomerWithDistanceDto>>> GetNearbyCustomers(
            [FromQuery] double lat, [FromQuery] double lon, [FromQuery] double radiusKm = 5.0)
        {
            // 1. Hitung bounding box (kotak lat/lon) berdasarkan radius - pre-filter murah di database
            var (minLat, maxLat, minLon, maxLon) = GetBoundingBox(lat, lon, radiusKm);

            var candidates = await _db.Customers
                .Include(c => c.Sales)
                .Where(c => c.Latitude >= minLat && c.Latitude <= maxLat &&
                            c.Longitude >= minLon && c.Longitude <= maxLon)
                .ToListAsync();

            // 2. Hitung jarak Haversine presisi hanya untuk kandidat yang lolos bounding box
            var result = candidates
                .Select(c => new CustomerWithDistanceDto
                {
                    Customer = ToDto(c),
                    DistanceKm = CalculateDistanceKm(lat, lon, c.Latitude, c.Longitude)
                })
                .Where(x => x.DistanceKm <= radiusKm)
                .OrderBy(x => x.DistanceKm)
                .ToList();

            return result;
        }

        private static CustomerDto ToDto(Customer customer) => new()
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Phone = customer.Phone,
            Address = customer.Address,
            Latitude = customer.Latitude,
            Longitude = customer.Longitude,
            SalesId = customer.SalesId,
            Sales = customer.Sales == null ? null : new SalesDto
            {
                Id = customer.Sales.Id,
                Name = customer.Sales.Name,
                Email = customer.Sales.Email,
                Phone = customer.Sales.Phone
            }
        };

        private static Customer ToEntity(CustomerDto dto) => new()
        {
            Id = dto.Id,
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            Address = dto.Address,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            SalesId = dto.SalesId
        };

        private static (double MinLat, double MaxLat, double MinLon, double MaxLon) GetBoundingBox(
            double lat, double lon, double radiusKm)
        {
            const double earthRadiusKm = 6371.0;

            double deltaLat = radiusKm / (earthRadiusKm * Math.PI / 180);
            double deltaLon = radiusKm / (earthRadiusKm * Math.PI / 180 * Math.Cos(DegreesToRadians(lat)));

            return (
                MinLat: lat - deltaLat,
                MaxLat: lat + deltaLat,
                MinLon: lon - deltaLon,
                MaxLon: lon + deltaLon
            );
        }

        private static double CalculateDistanceKm(double lat1, double lon1, double lat2, double lon2)
        {
            const double earthRadiusKm = 6371.0;
            var dLat = DegreesToRadians(lat2 - lat1);
            var dLon = DegreesToRadians(lon2 - lon1);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return earthRadiusKm * c;
        }

        private static double DegreesToRadians(double degrees) => degrees * Math.PI / 180;
    }
}
