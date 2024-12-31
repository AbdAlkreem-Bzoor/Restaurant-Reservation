using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Extensions;
using RestaurantReservation.API.Models.Order;
using RestaurantReservation.API.Repositories;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/v{version:apiVersion}/orders")]
    [Authorize]
    [ApiController]
    [ApiVersion(1.0)]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;
        public OrderController(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        /// <summary>
        /// Gets orders partitioned into pages.
        /// </summary>
        /// <param name="pageNumber">The number of the needed page.</param>
        /// <param name="pageSize">The size of the needed page.</param>
        /// <response code="400">When pageNumber or pageSize is less than zero.</response>
        /// <response code="200">Returns the requested page of orders with pagination metadata in the headers.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetOrders(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest($"page number and page size must be greater than 0.");
            }
            return Ok(_mapper.Map<IEnumerable<OrderResponseDto>>((await _repository.GetOrdersAsync()).GetPage(pageNumber, pageSize)));
        }
        /// <summary>
        /// Returns an order specified by ID.
        /// </summary>
        /// <param name="id">The ID of the order to retrieve.</param>
        /// <response code="404">If the order with the given id is not found.</response>
        /// <response code="200">Returns the requested order.</response>
        [HttpGet("{id}", Name = "GetOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderResponseDto>> GetOrder(int id)
        {
            var order = await _repository.GetOrderAsync(id);

            if (order is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<OrderResponseDto>(order));
        }
        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="order">The data of the new order.</param>
        /// <returns>The newly created order.</returns>
        /// <response code="400">If the creation data is invalid.</response>
        /// <response code="201">If the order is created successfully.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderResponseDto>> AddOrder(OrderCreationDto order)
        {
            var orderToAdd = _mapper.Map<Order>(order);

            var result = await _repository.AddOrderAsync(orderToAdd);

            if (!result)
            {
                return BadRequest(new { Message = "The Order already exist." });
            }

            await _repository.SaveChangesAsync();

            return CreatedAtRoute("GetOrder", new { id = orderToAdd?.OrderId },
                                  _mapper.Map<OrderResponseDto>(orderToAdd));
        }
        /// <summary>
        /// Updates an existing order specified by ID.
        /// </summary>
        /// <param name="id">The ID of the order to update.</param>
        /// <param name="order">The data for updating the order.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="404">If the order with the specified ID is not found.</response>
        /// <response code="204">If successful.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOrder(int id, OrderUpdateDto order)
        {
            var orderToUpdate = await _repository.GetOrderAsync(id);

            if (orderToUpdate is null)
            {
                return NotFound();
            }

            _mapper.Map(order, orderToUpdate);

            await _repository.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Partially updates an existing order specified by ID.
        /// </summary>
        /// <param name="id">The ID of the order to update.</param>
        /// <param name="patchDocument">The JSON patch document with partial update operations.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="400">If the patch document or updated data is invalid.</response>
        /// <response code="404">If the order with the specified ID is not found.</response>
        /// <response code="204">If the update is successful.</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PartiallyUpdateOrder(int id,
            JsonPatchDocument<OrderUpdateDto> patchDocument)
        {
            var order = await _repository.GetOrderAsync(id);

            if (order is null)
            {
                return NotFound();
            }

            var orderToPatch = _mapper.Map<OrderUpdateDto>(order);

            patchDocument.ApplyTo(orderToPatch, ModelState);

            if (!ModelState.IsValid || !TryValidateModel(orderToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(orderToPatch, order);

            await _repository.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Deletes an existing order specified by ID.
        /// </summary>
        /// <param name="id">The Id of the order to delete.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="404">if the order with the specified ID is not found.</response>
        /// <response code="204">if the deletion is successful.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _repository.DeleteOrderAsync(id);

            if (!result)
            {
                return NotFound();
            }

            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
