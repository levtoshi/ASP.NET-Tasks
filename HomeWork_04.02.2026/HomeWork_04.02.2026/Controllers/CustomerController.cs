using HomeWork_04._02._2026.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Infrastructure;

namespace HomeWork_04._02._2026.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {

        private static readonly List<Customer> _list = new List<Customer>()
        {
            new Customer() { Id = 1, Name = "Alice", BirthdayYear = 1999 },
            new Customer() { Id = 2, Name = "Bob", BirthdayYear = 2001 },
            new Customer() { Id = 3, Name = "Charlie", BirthdayYear = 2000 }
        };


        [HttpGet]
        public IActionResult GetCustomers([FromQuery] int minYear)
        {
            return Ok(_list.OrderBy(c => c.Name).Where(c => c.BirthdayYear > minYear));
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer([FromRoute] int id)
        {
            var customer = _list.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpGet("names")]
        public IActionResult GetCustomerNames()
        {
            return Ok(_list.Select(c => c.Name).Distinct().Order());
        }

        [HttpPost]
        public IActionResult AddCustomer([FromBody] Customer customer)
        {
            _list.Add(customer);
            
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        [HttpPut]
        public IActionResult UpdateCustomer([FromBody] Customer customer)
        {
            var existingCustomer = _list.FirstOrDefault(c => c.Id == customer.Id);
            
            if (existingCustomer == null)
            {
                return NotFound();
            }

            existingCustomer.Name = customer.Name;
            existingCustomer.BirthdayYear = customer.BirthdayYear;
            
            return Ok(existingCustomer);
        }

        [HttpPatch("{id}/year")]
        
        public IActionResult UpdateCustomerName([FromRoute] int id, [FromBody] CustomerYearUpdateDto birthdayYearDTO)
        {
            var existingCustomer = _list.FirstOrDefault(c => c.Id == id);

            if (existingCustomer == null)
            {
                return NotFound();
            }
            existingCustomer.BirthdayYear = birthdayYearDTO.BirthdayYear;

            return Ok(existingCustomer);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer([FromRoute] int id)
        {
            var customer = _list.FirstOrDefault(c => c.Id == id);
            
            if (customer == null)
            {
                return NotFound();
            }
            _list.Remove(customer);
            
            return NoContent();
        }

        [HttpDelete("range")]
        public IActionResult DeleteRange([FromBody] CustomerRangeDeleteDto customerRangeDeleteDto)
        {
            foreach (int id in customerRangeDeleteDto.Ids)
            {
                var existingCustomer = _list.FirstOrDefault(c => c.Id == id);

                if (existingCustomer != null)
                {
                    _list.Remove(existingCustomer);
                }
            }

            return NoContent();
        }
    }
}