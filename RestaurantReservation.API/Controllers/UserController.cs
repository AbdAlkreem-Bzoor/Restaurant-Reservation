using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Repositories;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRestaurantReservationRepository _repository;
        public UserController(IRestaurantReservationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersAsync()
        {
            return Ok(await _repository.GetUsersAsync());
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<Order>> GetOrderAsync(int userId)
        {
            var user = await _repository.GetUserAsync(userId);
            if (user is null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost]
        public async Task<ActionResult<bool>> AddUserAsync(User user)
        {
            var result = await _repository.AddUserAsync(user);
            if (!result)
            {
                return Conflict(new { Message = "The User already exist." });
            }
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateUserAsync(User user)
        {
            var result = await _repository.UpdateUserAsync(user);
            if (!result)
            {
                return Conflict(new { Message = "The User doesn't exist." });
            }
            return Ok(result);
        }
        [HttpDelete("{userId}")]
        public async Task<ActionResult<bool>> DeleteUserAsync(int userId)
        {
            var result = await _repository.DeleteUserAsync(userId);
            if (!result)
            {
                return Conflict(new { Message = "The User doesn't exist." });
            }
            return Ok(result);
        }
    }
}
