using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Extensions;
using RestaurantReservation.API.Models.MenuItem;
using RestaurantReservation.API.Models.Order;
using RestaurantReservation.API.Models.Reservation;
using RestaurantReservation.API.Repositories;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/v{version:apiVersion}/reservations")]
    [Authorize]
    [ApiController]
    [ApiVersion(1.0)]
    public class ReservationController : ControllerBase
    {
        private readonly IApplicationRepository _repository;
        private readonly IMapper _mapper;
        public ReservationController(IApplicationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        /// <summary>
        /// Gets reservations partitioned into pages.
        /// </summary>
        /// <param name="pageNumber">The number of the needed page.</param>
        /// <param name="pageSize">The size of the needed page.</param>
        /// <response code="400">When pageNumber or pageSize is less than zero.</response>
        /// <response code="200">Returns the requested page of employees with pagination metadata in the headers.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ReservationResponseDto>>> GetReservations(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest($"page number and page size must be greater than 0.");
            }
            return Ok(_mapper.Map<IEnumerable<ReservationResponseDto>>((await _repository.GetReservationsAsync()).GetPage(pageNumber, pageSize)));
        }
        /// <summary>
        /// Returns a reservation specified by ID.
        /// </summary>
        /// <param name="id">The ID of the reservation to retrieve.</param>
        /// <response code="404">If the reservation with the given id is not found.</response>
        /// <response code="200">Returns the requested reservation.</response>
        [HttpGet("{id}", Name = "GetReservation")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ReservationResponseDto>> GetReservation(int id)
        {
            var reservation = await _repository.GetReservationAsync(id);

            if (reservation is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ReservationResponseDto>(reservation));
        }
        /// <summary>
        /// Creates a new reservation.
        /// </summary>
        /// <param name="reservation">The data of the new reservation.</param>
        /// <returns>The newly created reservation.</returns>
        /// <response code="400">If the creation data is invalid.</response>
        /// <response code="201">If the reservation is created successfully.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ReservationResponseDto>> AddReservation(ReservationCreationDto reservation)
        {
            var reservationToAdd = _mapper.Map<Reservation>(reservation);

            var result = await _repository.AddReservationAsync(reservationToAdd);

            if (!result)
            {
                return Conflict(new { Message = "The Reservation already exist." });
            }

            await _repository.SaveChangesAsync();

            return CreatedAtRoute("GetReservation", new { id = reservationToAdd?.ReservationId },
                                  _mapper.Map<ReservationResponseDto>(reservationToAdd));
        }
        /// <summary>
        /// Updates an existing reservation specified by ID.
        /// </summary>
        /// <param name="id">The ID of the reservation to update.</param>
        /// <param name="reservation">The data for updating the reservation.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="404">If the reservation with the specified ID is not found.</response>
        /// <response code="204">If successful.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateReservation(int id, ReservationUpdateDto reservation)
        {
            var reservationToUpdate = await _repository.GetReservationAsync(id);

            if (reservationToUpdate is null)
            {
                return NotFound();
            }

            _mapper.Map(reservation, reservationToUpdate);

            await _repository.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Partially updates an existing reservation specified by ID.
        /// </summary>
        /// <param name="id">The ID of the reservation to update.</param>
        /// <param name="patchDocument">The JSON patch document with partial update operations.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="400">If the patch document or updated data is invalid.</response>
        /// <response code="404">If the restaurant with the specified ID is not found.</response>
        /// <response code="204">If the update is successful.</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PartiallyUpdateReservation(int id,
            JsonPatchDocument<ReservationUpdateDto> patchDocument)
        {
            var reservation = await _repository.GetReservationAsync(id);

            if (reservation is null)
            {
                return NotFound();
            }

            var reservationToPatch = _mapper.Map<ReservationUpdateDto>(reservation);

            patchDocument.ApplyTo(reservationToPatch, ModelState);

            if (!ModelState.IsValid || !TryValidateModel(reservationToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(reservationToPatch, reservation);

            await _repository.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Deletes an existing reservation specified by ID.
        /// </summary>
        /// <param name="id">The ID of the reservation to delete.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="404">if the reservation with the specified ID is not found.</response>
        /// <response code="204">if the deletion is successful.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<IActionResult> DeleteReservation(int id)
        {
            var result = await _repository.DeleteReservationAsync(id);

            if (!result)
            {
                return NotFound();
            }

            await _repository.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Gets reservation for a customer specified by ID partitioned into pages.
        /// </summary>
        /// <param name="customerId">The ID of the customer to get reservations for.</param>
        /// <param name="pageNumber">The number of the needed page.</param>
        /// <param name="pageSize">The size of the needed page.</param>
        /// <response code="400">When pageNumber or pageSize is less than zero.</response>
        /// <response code="404">If the reservation with the given id is not found.</response>
        /// <response code="200">Returns the requested page of reservations with pagination metadata in the headers.</response>
        [HttpGet("customer/{customerId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ReservationResponseDto>>> GetReservationsForCustomer(int customerId, int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest($"page number and page size must be greater than 0.");
            }

            var customer = await _repository.GetCustomerAsync(customerId);

            if (customer is null)
            {
                return NotFound();
            }

            var reservations = await _repository.GetReservationsForCustomerAsync(customerId);

            return Ok(_mapper.Map<IEnumerable<ReservationResponseDto>>(reservations).GetPage(pageNumber, pageSize));
        }
        /// <summary>
        /// Gets orders for a reservation specified by ID.
        /// </summary>
        /// <param name="reservationId">The ID of the reservation.</param>
        /// <response code="404">If the reservation with the given id is not found.</response>
        /// <response code="200">Returns the requested orders.</response>
        [HttpGet("{reservationId}/orders")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetOrdersForReservationAsync(int reservationId)
        {
            var reservation = await _repository.GetReservationAsync(reservationId);

            if (reservation is null)
            {
                return NotFound();
            }

            var orders = await _repository.GetOrdersForReservationAsync(reservationId);

            return Ok(_mapper.Map<IEnumerable<OrderResponseDto>>(orders));
        }
        /// <summary>
        /// Retrieves ordered menu items for a reservation. 
        /// </summary>
        /// <param name="reservationId">The ID of the reservation.</param>
        /// <response code="404">If the reservation with the given id is not found.</response>
        /// <response code="200">Returns the requested menu items.</response>
        [HttpGet("{reservationId}/menu-items")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MenuItemResponseDto>>> GetMenuItemsForReservationAsync(int reservationId)
        {
            var reservation = await _repository.GetReservationAsync(reservationId);

            if (reservation is null)
            {
                return NotFound();
            }

            var menuItems = await _repository.GetMenuItemsForReservationAsync(reservationId);

            return Ok(_mapper.Map<IEnumerable<MenuItemResponseDto>>(menuItems));
        }
    }
}
