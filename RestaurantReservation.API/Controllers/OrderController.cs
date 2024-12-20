using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Repositories;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/orders")]
    [Authorize]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IRestaurantReservationRepository _repository;
        public OrderController(IRestaurantReservationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersAsync()
        {
            return Ok(await _repository.GetOrdersAsync());
        }
        [HttpGet("{orderId}")]
        public async Task<ActionResult<Order>> GetOrderAsync(int orderId)
        {
            var order = await _repository.GetOrderAsync(orderId);
            if (order is null)
            {
                return NotFound();
            }
            return Ok(order);
        }
        [HttpPost]
        public async Task<ActionResult<bool>> AddOrderAsync(Order order)
        {
            var result = await _repository.AddOrderAsync(order);
            if (!result)
            {
                return Conflict(new { Message = "The Order already exist." });
            }
            await _repository.SaveChangesAsync();
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateOrderAsync(Order order)
        {
            var result = await _repository.UpdateOrderAsync(order);
            if (!result)
            {
                return Conflict(new { Message = "The Order doesn't exist." });
            }
            await _repository.SaveChangesAsync();
            return Ok(result);
        }
        [HttpDelete("{orderId}")]
        public async Task<ActionResult<bool>> DeleteOrderAsync(int orderId)
        {
            var result = await _repository.DeleteOrderAsync(orderId);
            if (!result)
            {
                return Conflict(new { Message = "The Order doesn't exist." });
            }
            await _repository.SaveChangesAsync();
            return Ok(result);
        }
    }
}
