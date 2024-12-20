using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Repositories;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/reservations")]
    [Authorize]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IRestaurantReservationRepository _repository;
        public ReservationController(IRestaurantReservationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservationsAsync()
        {
            return Ok(await _repository.GetReservationsAsync());
        }
        [HttpGet("{reservationId}")]
        public async Task<ActionResult<Reservation>> GetReservationAsync(int reservationId)
        {
            var reservation = await _repository.GetReservationAsync(reservationId);
            if (reservation is null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservationsForCustomerAsync(int customerId)
        {
            var result = await _repository.GetReservationsForCustomerAsync(customerId);
            return result.Any() ? Ok(result) : NotFound();
        }
        [HttpGet("{reservationId}/orders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersForReservationAsync(int reservationId)
        {
            return Ok(await _repository.GetOrdersForReservationAsync(reservationId));
        }
        [HttpGet("{reservationId}/menu-items")]
        public async Task<ActionResult<IEnumerable<Order>>> GetMenuItemsForReservationAsync(int reservationId)
        {
            return Ok(await _repository.GetMenuItemsForReservationAsync(reservationId));
        }
        [HttpPost]
        public async Task<ActionResult<bool>> AddReservationAsync(Reservation reservation)
        {
            var result = await _repository.AddReservationAsync(reservation);
            if (!result)
            {
                return Conflict(new { Message = "The Reservation already exist." });
            }
            await _repository.SaveChangesAsync();
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateReservationAsync(Reservation reservation)
        {
            var result = await _repository.UpdateReservationAsync(reservation);
            if (!result)
            {
                return Conflict(new { Message = "The Reservation doesn't exist." });
            }
            await _repository.SaveChangesAsync();
            return Ok(result);
        }
        [HttpDelete("{reservationId}")]
        public async Task<ActionResult<bool>> DeleteReservationAsync(int reservationId)
        {
            var result = await _repository.DeleteReservationAsync(reservationId);
            if (!result)
            {
                return Conflict(new { Message = "The Reservation doesn't exist." });
            }
            await _repository.SaveChangesAsync();
            return Ok(result);
        }
    }
}
