using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Repositories;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/order-items")]
    [Authorize]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IRestaurantReservationRepository _repository;
        public OrderItemController(IRestaurantReservationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItemsAsync()
        {
            return Ok(await _repository.GetOrderItemsAsync());
        }
        [HttpGet("{orderItemId}")]
        public async Task<ActionResult<OrderItem>> GetOrderItemAsync(int orderItemId)
        {
            var orderItem = await _repository.GetOrderItemAsync(orderItemId);
            if (orderItem is null)
            {
                return NotFound();
            }
            return Ok(orderItem);
        }
        [HttpPost]
        public async Task<ActionResult<bool>> AddOrderItemAsync(OrderItem orderItem)
        {
            var result = await _repository.AddOrderItemAsync(orderItem);
            if (!result)
            {
                return Conflict(new { Message = "The OrderItem already exist." });
            }
            await _repository.SaveChangesAsync();
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateOrderItemAsync(OrderItem orderItem)
        {
            var result = await _repository.UpdateOrderItemAsync(orderItem);
            if (!result)
            {
                return Conflict(new { Message = "The OrderItem doesn't exist." });
            }
            await _repository.SaveChangesAsync();
            return Ok(result);
        }
        [HttpDelete("{orderItemId}")]
        public async Task<ActionResult<bool>> DeleteOrderItemAsync(int orderItemId)
        {
            var result = await _repository.DeleteOrderItemAsync(orderItemId);
            if (!result)
            {
                return Conflict(new { Message = "The OrderItem doesn't exist." });
            }
            await _repository.SaveChangesAsync();
            return Ok(result);
        }
    }
}
