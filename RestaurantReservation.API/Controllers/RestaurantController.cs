using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Extensions;
using RestaurantReservation.API.Models.Restaurant;
using RestaurantReservation.API.Repositories;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/v{version:apiVersion}/restaurants")]
    [Authorize]
    [ApiController]
    [ApiVersion(1.0)]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantRepository _repository;
        private readonly IMapper _mapper;
        public RestaurantController(IRestaurantRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        /// <summary>
        /// Gets restaurants partitioned into pages.
        /// </summary>
        /// <param name="pageNumber">The number of the needed page.</param>
        /// <param name="pageSize">The size of the needed page.</param>
        /// <response code="400">When pageNumber or pageSize is less than zero.</response>
        /// <response code="200">Returns the requested page of restaurants with pagination metadata in the headers.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<RestaurantResponseDto>>> GetRestaurants(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest($"page number and page size must be greater than 0.");
            }
            return Ok(_mapper.Map<IEnumerable<RestaurantResponseDto>>((await _repository.GetRestaurantsAsync()).GetPage(pageNumber, pageSize)));
        }
        /// <summary>
        /// Returns a restaurant specified by ID.
        /// </summary>
        /// <param name="id">The ID of the restaurant to retrieve.</param>
        /// <response code="404">If the restaurant with the given id is not found.</response>
        /// <response code="200">Returns the requested restaurant.</response>
        [HttpGet("{id}", Name = "GetRestaurant")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<RestaurantResponseDto>> GetRestaurant(int id)
        {
            var restaurant = await _repository.GetRestaurantAsync(id);

            if (restaurant is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RestaurantResponseDto>(restaurant));
        }
        /// <summary>
        /// Creates a new restaurant.
        /// </summary>
        /// <param name="restaurant">The data of the new restaurant.</param>
        /// <returns>The newly created restaurant.</returns>
        /// <response code="400">If the restaurant data is invalid.</response>
        /// <response code="201">If the restaurant is created successfully.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<RestaurantResponseDto>> AddRestaurant(RestaurantCreationDto restaurant)
        {
            var restaurantToAdd = _mapper.Map<Restaurant>(restaurant);

            var result = await _repository.AddRestaurantAsync(restaurantToAdd);

            if (!result)
            {
                return BadRequest(new { Message = "The Restaurant already exist." });
            }

            await _repository.SaveChangesAsync();

            return CreatedAtRoute("GetRestaurant", new { id = restaurantToAdd?.RestaurantId },
                                  _mapper.Map<RestaurantResponseDto>(restaurantToAdd));
        }
        /// <summary>
        /// Updates an existing restaurant specified by ID.
        /// </summary>
        /// <param name="id">The ID of the restaurant to update.</param>
        /// <param name="restaurant">The data for updating the restaurant.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="404">If the restaurant with the specified ID is not found.</response>
        /// <response code="204">If successful.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateRestaurant(int id, RestaurantUpdateDto restaurant)
        {
            var restaurantToUpdate = await _repository.GetRestaurantAsync(id);

            if (restaurantToUpdate is null)
            {
                return NotFound();
            }

            _mapper.Map(restaurant, restaurantToUpdate);

            await _repository.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Partially updates an existing restaurant specified by ID.
        /// </summary>
        /// <param name="id">The ID of the restaurant to update.</param>
        /// <param name="patchDocument">The JSON patch document with partial update operations.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="400">If the patch document or updated data is invalid.</response>
        /// <response code="404">If the restaurant with the specified ID is not found.</response>
        /// <response code="204">If the update is successful.</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PartiallyUpdateRestaurant(int id,
            JsonPatchDocument<RestaurantUpdateDto> patchDocument)
        {
            var restaurant = await _repository.GetRestaurantAsync(id);

            if (restaurant is null)
            {
                return NotFound();
            }

            var restaurantToPatch = _mapper.Map<RestaurantUpdateDto>(restaurant);

            patchDocument.ApplyTo(restaurantToPatch, ModelState);

            if (!ModelState.IsValid || !TryValidateModel(restaurantToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(restaurantToPatch, restaurant);

            await _repository.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Deletes an existing restaurant specified by ID.
        /// </summary>
        /// <param name="id">The ID of the restaurant to delete.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="404">if the restaurant with the specified ID is not found.</response>
        /// <response code="204">if the deletion is successful.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var result = await _repository.DeleteRestaurantAsync(id);

            if (!result)
            {
                return NotFound();
            }

            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
