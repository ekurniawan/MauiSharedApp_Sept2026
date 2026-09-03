using MauiSharedApp.API.Data;
using MauiSharedApp.API.Models;
using MauiSharedApp.API.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MauiSharedApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public SalesController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesDto>>> GetSales()
        {
            var sales = await _db.Sales.ToListAsync();
            return sales.Select(ToDto).ToList();
        }

        // GET: api/sales/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SalesDto>> GetSalesById(int id)
        {
            var sales = await _db.Sales.FindAsync(id);
            if (sales == null)
            {
                return NotFound();
            }
            return ToDto(sales);
        }

        // GET: api/sales/5/customers
        [HttpGet("{id:int}/customers")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomersBySales(int id)
        {
            if (!await _db.Sales.AnyAsync(s => s.Id == id))
            {
                return NotFound();
            }

            var customers = await _db.Customers.Where(c => c.SalesId == id).ToListAsync();
            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address,
                Latitude = c.Latitude,
                Longitude = c.Longitude,
                SalesId = c.SalesId
            }).ToList();
        }

        // POST: api/sales
        [HttpPost]
        public async Task<ActionResult<SalesDto>> CreateSales(SalesDto salesDto)
        {
            var sales = ToEntity(salesDto);
            _db.Sales.Add(sales);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSalesById), new { id = sales.Id }, ToDto(sales));
        }

        // PUT: api/sales/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateSales(int id, SalesDto salesDto)
        {
            if (id != salesDto.Id)
            {
                return BadRequest();
            }

            var sales = await _db.Sales.FindAsync(id);
            if (sales == null)
            {
                return NotFound();
            }

            sales.Name = salesDto.Name;
            sales.Email = salesDto.Email;
            sales.Phone = salesDto.Phone;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _db.Sales.AnyAsync(s => s.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/sales/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSales(int id)
        {
            var sales = await _db.Sales.FindAsync(id);
            if (sales == null)
            {
                return NotFound();
            }

            var hasCustomers = await _db.Customers.AnyAsync(c => c.SalesId == id);
            if (hasCustomers)
            {
                return Conflict("Cannot delete sales that still has assigned customers.");
            }

            _db.Sales.Remove(sales);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        private static SalesDto ToDto(Sales sales) => new()
        {
            Id = sales.Id,
            Name = sales.Name,
            Email = sales.Email,
            Phone = sales.Phone
        };

        private static Sales ToEntity(SalesDto dto) => new()
        {
            Id = dto.Id,
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone
        };
    }
}
