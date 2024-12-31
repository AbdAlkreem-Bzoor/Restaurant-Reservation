using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Extensions;
using RestaurantReservation.API.Models.OrderItem;
using RestaurantReservation.API.Repositories;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/v{version:apiVersion}/order-items")]
    [Authorize]
    [ApiController]
    [ApiVersion(1.0)]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemRepository _repository;
        private readonly IMapper _mapper;
        public OrderItemController(IOrderItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        /// <summary>
        /// Gets order items for an order partitioned into pages.
        /// </summary>
        /// <param name="pageNumber">The number of the needed page.</param>
        /// <param name="pageSize">The size of the needed page.</param>
        /// <response code="400">When pageNumber or pageSize is less than zero.</response>
        /// <response code="200">Returns the requested page of order items with pagination metadata in the headers.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OrderItemResponseDto>>> GetOrderItems(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest($"page number and page size must be greater than 0.");
            }
            return Ok(_mapper.Map<IEnumerable<OrderItemResponseDto>>((await _repository.GetOrderItemsAsync()).GetPage(pageNumber, pageSize)));
        }
        /// <summary>
        /// Returns an order item specified by ID for an order.
        /// </summary>
        /// <param name="id">The ID of the order item to retrieve.</param>
        /// <response code="404">If the order with the given id or an order item with the given ID for the order is not found.</response>
        /// <response code="200">Returns the requested table.</response>
        [HttpGet("{id}", Name = "GetOrderItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderItemResponseDto>> GetOrderItem(int id)
        {
            var orderItem = await _repository.GetOrderItemAsync(id);

            if (orderItem is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<OrderItemResponseDto>(orderItem));
        }
        /// <summary>
        /// Creates a new order item for an order specified by ID.
        /// </summary>
        /// <param name="orderItem">The data of the new table.</param>
        /// <returns>The newly created table.</returns>
        /// <response code="400">If the creation Data is invalid.</response>
        /// <response code="201">If the table is created successfully.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderItemResponseDto>> AddOrderItem(OrderItemCreationDto orderItem)
        {
            var orderToAdd = _mapper.Map<OrderItem>(orderItem);

            var result = await _repository.AddOrderItemAsync(orderToAdd);

            if (!result)
            {
                return BadRequest(new { Message = "The OrderItem already exist." });
            }

            await _repository.SaveChangesAsync();

            return CreatedAtRoute("GetOrderItem", new { id = orderToAdd?.OrderItemId },
                                  _mapper.Map<OrderItemResponseDto>(orderToAdd));
        }
        /// <summary>
        /// Updates an existing order item specified by ID for an order specified by ID.
        /// </summary>
        /// <param name="id">The ID of the order item to update.</param>
        /// <param name="orderItem">The data for updating the order item.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="404">If the order with the given id or an order item with the given ID for the order is not found.</response>
        /// <response code="204">If successful.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOrderItem(int id, OrderItemUpdateDto orderItem)
        {
            var orderItemToUpdate = await _repository.GetOrderItemAsync(id);

            if (orderItemToUpdate is null)
            {
                return NotFound();
            }

            _mapper.Map(orderItem, orderItemToUpdate);

            await _repository.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Partially updates an existing order item specified by ID for an order specified by ID.
        /// </summary>
        /// <param name="id">The ID of the table to update.</param>
        /// <param name="patchDocument">The JSON patch document with partial update operations.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="400">If the patch document or updated data is invalid.</response>
        /// <response code="404">If the order with the given id or an order item with the given ID for the order is not found.</response>
        /// <response code="204">If successful.</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PartiallyUpdateOrderItem(int id,
            JsonPatchDocument<OrderItemUpdateDto> patchDocument)
        {
            var orderItem = await _repository.GetOrderItemAsync(id);

            if (orderItem is null)
            {
                return NotFound();
            }

            var orderItemToPatch = _mapper.Map<OrderItemUpdateDto>(orderItem);

            patchDocument.ApplyTo(orderItemToPatch, ModelState);

            if (!ModelState.IsValid || !TryValidateModel(orderItemToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(orderItemToPatch, orderItem);

            await _repository.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Deletes an existing order item specified by ID for an order specified by ID.
        /// </summary>
        /// <param name="id">The ID of the order item to delete.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="404">if the table with the specified ID is not found.</response>
        /// <response code="204">if the deletion is successful.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var result = await _repository.DeleteOrderItemAsync(id);

            if (!result)
            {
                return NotFound();
            }

            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
