using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Repositories;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/customers")]
    [Authorize]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IRestaurantReservationRepository _repository;
        public CustomerController(IRestaurantReservationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomersAsync()
        {
            return Ok(await _repository.GetCustomersAsync());
        }
        [HttpGet("{customerId}")]
        public async Task<ActionResult<Customer>> GetCustomerAsync(int customerId)
        {
            var customer = await _repository.GetCustomerAsync(customerId);
            if (customer is null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
        [HttpPost]
        public async Task<ActionResult<bool>> AddCustomerAsync(Customer customer)
        {
            var result = await _repository.AddCustomerAsync(customer);
            if (!result)
            {
                return Conflict(new { Message = "The Customer already exist." });
            }
            await _repository.SaveChangesAsync();
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateCustomerAsync(Customer customer)
        {
            var result = await _repository.UpdateCustomerAsync(customer);
            if (!result)
            {
                return Conflict(new { Message = "The Customer doesn't exist." });
            }
            await _repository.SaveChangesAsync();
            return Ok(result);
        }
        [HttpDelete("{customerId}")]
        public async Task<ActionResult<bool>> DeleteCustomerAsync(int customerId)
        {
            var result = await _repository.DeleteCustomerAsync(customerId);
            if (!result)
            {
                return Conflict(new { Message = "The Customer doesn't exist." });
            }
            await _repository.SaveChangesAsync();
            return Ok(result);
        }
    }
}
