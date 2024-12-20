using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Repositories;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/restaurants")]
    [Authorize]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantReservationRepository _repository;
        public RestaurantController(IRestaurantReservationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurantsAsync()
        {
            return Ok(await _repository.GetRestaurantsAsync());
        }
        [HttpGet("{restaurantId}")]
        public async Task<ActionResult<Restaurant>> GetRestaurantAsync(int restaurantId)
        {
            var restaurant = await _repository.GetRestaurantAsync(restaurantId);
            if (restaurant is null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }
        [HttpPost]
        public async Task<ActionResult<bool>> AddRestaurantAsync(Restaurant restaurant)
        {
            var result = await _repository.AddRestaurantAsync(restaurant);
            if (!result)
            {
                return Conflict(new { Message = "The Restaurant already exist." });
            }
            await _repository.SaveChangesAsync();
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateRestaurantAsync(Restaurant restaurant)
        {
            var result = await _repository.UpdateRestaurantAsync(restaurant);
            if (!result)
            {
                return Conflict(new { Message = "The Restaurant doesn't exist." });
            }
            await _repository.SaveChangesAsync();
            return Ok(result);
        }
        [HttpDelete("{restaurantId}")]
        public async Task<ActionResult<bool>> DeleteRestaurantAsync(int restaurantId)
        {
            var result = await _repository.DeleteRestaurantAsync(restaurantId);
            if (!result)
            {
                return Conflict(new { Message = "The Restaurant doesn't exist." });
            }
            await _repository.SaveChangesAsync();
            return Ok(result);
        }
    }
}
