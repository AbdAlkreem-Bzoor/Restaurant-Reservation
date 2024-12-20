using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Repositories;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/menu-items")]
    [Authorize]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IRestaurantReservationRepository _repository;
        public MenuItemController(IRestaurantReservationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItem>>> GetMenuItemsAsync()
        {
            return Ok(await _repository.GetMenuItemsAsync());
        }
        [HttpGet("{itemId}")]
        public async Task<ActionResult<MenuItem>> GetMenuItemAsync(int itemId)
        {
            var menuItem = await _repository.GetMenuItemAsync(itemId);
            if (menuItem is null)
            {
                return NotFound();
            }
            return Ok(menuItem);
        }
        [HttpPost]
        public async Task<ActionResult<bool>> AddMenuItemAsync(MenuItem menuItem)
        {
            var result = await _repository.AddMenuItemAsync(menuItem);
            if (!result)
            {
                return Conflict(new { Message = "The MenuItem already exist." });
            }
            await _repository.SaveChangesAsync();
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateMenuItemAsync(MenuItem menuItem)
        {
            var result = await _repository.UpdateMenuItemAsync(menuItem);
            if (!result)
            {
                return Conflict(new { Message = "The MenuItem doesn't exist." });
            }
            await _repository.SaveChangesAsync();
            return Ok(result);
        }
        [HttpDelete("{itemId}")]
        public async Task<ActionResult<bool>> DeleteMenuItemAsync(int itemId)
        {
            var result = await _repository.DeleteMenuItemAsync(itemId);
            if (!result)
            {
                return Conflict(new { Message = "The MenuItem doesn't exist." });
            }
            await _repository.SaveChangesAsync();
            return Ok(result);
        }
    }
}
