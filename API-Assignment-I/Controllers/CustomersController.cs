using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Assignment_I.Data;
using API_Assignment_I.Models;
using Azure;
//Static changes in master about feature.
namespace API_Assignment_I.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerDbContext _context;

        public CustomersController(CustomerDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Getcustomers()
        {
            if (_context.customers == null)
            {
                return NotFound();
            }
            return await _context.customers.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            if (_context.customers == null)
            {
                return NotFound();
            }
            var customer = await _context.customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }



        [HttpPut]
        [HttpPost]
        public async Task<ActionResult<Customer>> PostorUpdateCustomer([FromBody] Customer customer)
        {

            if (_context.customers == null)
            {
                return Problem("Entity set 'CustomerDbContext.customers'  is null.");
            }

            var res = CustomerExists(customer.Id);

            if (HttpContext.Request.Method == HttpMethods.Post)
            {
                if (res)
                {
                    return Conflict();
                }
                _context.customers.Add(customer);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);

            }
            else
            {
                _context.Entry(customer).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!res)
                    {
                        return NotFound("Id Not Found");
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();


            }



        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateEmployeePatchAsync(int id,String str)
        {
            var employeeQuery = await _context.customers.FindAsync(id);

            if (employeeQuery == null)
            {
                return NotFound();
            }

            employeeQuery.Name = str;
         

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (_context.customers == null)
            {
                return NotFound();
            }
            var customer = await _context.customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return (_context.customers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
