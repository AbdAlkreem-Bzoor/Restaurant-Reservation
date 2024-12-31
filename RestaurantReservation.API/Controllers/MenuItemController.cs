using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Extensions;
using RestaurantReservation.API.Models.MenuItem;
using RestaurantReservation.API.Repositories;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/v{version:apiVersion}/menu-items")]
    [Authorize]
    [ApiController]
    [ApiVersion(1.0)]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemRepository _repository;
        private readonly IMapper _mapper;
        public MenuItemController(IMenuItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        /// <summary>
        /// Gets menu items partitioned into pages.
        /// </summary>
        /// <param name="pageNumber">The number of the needed page.</param>
        /// <param name="pageSize">The size of the needed page.</param>
        /// <response code="400">When pageNumber or pageSize is less than zero.</response>
        /// <response code="200">Returns the requested page of menu items with pagination metadata in the headers.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MenuItemResponseDto>>> GetMenuItems(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest($"page number and page size must be greater than 0.");
            }
            return Ok(_mapper.Map<IEnumerable<MenuItemResponseDto>>((await _repository.GetMenuItemsAsync()).GetPage(pageNumber, pageSize)));
        }
        /// <summary>
        /// Returns a menu item specified by ID.
        /// </summary>
        /// <param name="id">The ID of the menu item to retrieve.</param>
        /// <response code="404">If the menu item with the given id is not found.</response>
        /// <response code="200">Returns the requested order.</response>
        [HttpGet("{id}", Name = "GetMenuItem")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<MenuItemResponseDto>> GetMenuItem(int id)
        {
            var menuItem = await _repository.GetMenuItemAsync(id);

            if (menuItem is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MenuItemResponseDto>(menuItem));
        }
        /// <summary>
        /// Creates a new menu item.
        /// </summary>
        /// <param name="menuItem">The data of the new menu item.</param>
        /// <returns>The newly created menu item.</returns>
        /// <response code="400">If the creation data is invalid.</response>
        /// <response code="201">If the menu item is created successfully.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<MenuItemResponseDto>> AddMenuItem(MenuItemCreationDto menuItem)
        {
            var menuItemToAdd = _mapper.Map<MenuItem>(menuItem);

            var result = await _repository.AddMenuItemAsync(menuItemToAdd);

            if (!result)
            {
                return Conflict(new { Message = "The MenuItem already exist." });
            }

            await _repository.SaveChangesAsync();

            return CreatedAtRoute("GetMenuItem", new { id = menuItemToAdd?.ItemId },
                                  _mapper.Map<MenuItemResponseDto>(menuItemToAdd));
        }
        /// <summary>
        /// Updates an existing menu item specified by ID.
        /// </summary>
        /// <param name="id">The ID of the menu item to update.</param>
        /// <param name="menuItem">The data for updating the menu item.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="404">If the menu item with the specified ID is not found.</response>
        /// <response code="204">If successful.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateMenuItem(int id, MenuItemUpdateDto menuItem)
        {
            var menuItemToUpdate = await _repository.GetMenuItemAsync(id);

            if (menuItemToUpdate is null)
            {
                return NotFound();
            }

            _mapper.Map(menuItem, menuItemToUpdate);

            await _repository.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Partially updates an existing menu item specified by ID.
        /// </summary>
        /// <param name="id">The ID of the menu item to update.</param>
        /// <param name="patchDocument">The JSON patch document with partial update operations.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="400">If the patch document or updated data is invalid.</response>
        /// <response code="404">If the menu item with the specified ID is not found.</response>
        /// <response code="204">If the update is successful.</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PartiallyUpdateMenuItem(int id,
            JsonPatchDocument<MenuItemUpdateDto> patchDocument)
        {
            var menuItem = await _repository.GetMenuItemAsync(id);

            if (menuItem is null)
            {
                return NotFound();
            }

            var menuItemToPatch = _mapper.Map<MenuItemUpdateDto>(menuItem);

            patchDocument.ApplyTo(menuItemToPatch, ModelState);

            if (!ModelState.IsValid || !TryValidateModel(menuItemToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(menuItemToPatch, menuItem);

            await _repository.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Deletes an existing menu item specified by ID.
        /// </summary>
        /// <param name="id">The Id of the menu item to delete.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="404">if the menu item with the specified ID is not found.</response>
        /// <response code="204">if the deletion is successful.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var result = await _repository.DeleteMenuItemAsync(id);

            if (!result)
            {
                return NotFound();
            }

            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
