using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Repositories;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/tables")]
    [Authorize]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly IRestaurantReservationRepository _repository;
        public TableController(IRestaurantReservationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Table>>> GetTablesAsync()
        {
            return Ok(await _repository.GetTablesAsync());
        }
        [HttpGet("{tableId}")]
        public async Task<ActionResult<Table>> GetTableAsync(int tableId)
        {
            var table = await _repository.GetTableAsync(tableId);
            if (table is null)
            {
                return NotFound();
            }
            return Ok(table);
        }
        [HttpPost]
        public async Task<ActionResult<bool>> AddTableAsync(Table table)
        {
            var result = await _repository.AddTableAsync(table);
            if (!result)
            {
                return Conflict(new { Message = "The Table already exist." });
            }
            await _repository.SaveChangesAsync();
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateTableAsync(Table table)
        {
            var result = await _repository.UpdateTableAsync(table);
            if (!result)
            {
                return Conflict(new { Message = "The Table doesn't exist." });
            }
            await _repository.SaveChangesAsync();
            return Ok(result);
        }
        [HttpDelete("{tableId}")]
        public async Task<ActionResult<bool>> DeleteTableAsync(int tableId)
        {
            var result = await _repository.DeleteTableAsync(tableId);
            if (!result)
            {
                return Conflict(new { Message = "The Table doesn't exist." });
            }
            await _repository.SaveChangesAsync();
            return Ok(result);
        }
    }
}
