using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Extensions;
using RestaurantReservation.API.Models.Table;
using RestaurantReservation.API.Repositories;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/v{version:apiVersion}/tables")]
    [Authorize]
    [ApiController]
    [ApiVersion(1.0)]
    public class TableController : ControllerBase
    {
        private readonly ITableRepository _repository;
        private readonly IMapper _mapper;
        public TableController(ITableRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        /// <summary>
        /// Gets tables partitioned into pages.
        /// </summary>
        /// <param name="pageNumber">The number of the needed page.</param>
        /// <param name="pageSize">The size of the needed page.</param>
        /// <response code="400">When pageNumber or pageSize is less than zero.</response>
        /// <response code="200">Returns the requested page of tables with pagination metadata in the headers.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TableResponseDto>>> GetTables(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest($"page number and page size must be greater than 0.");
            }
            return Ok(_mapper.Map<IEnumerable<TableResponseDto>>((await _repository.GetTablesAsync()).GetPage(pageNumber, pageSize)));
        }
        /// <summary>
        /// Returns a table specified by ID.
        /// </summary>
        /// <param name="id">The ID of the table to retrieve.</param>
        /// <response code="404">If the table with the given id is not found.</response>
        /// <response code="200">Returns the requested table.</response>
        [HttpGet("{id}", Name = "GetTable")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TableResponseDto>> GetTable(int id)
        {
            var table = await _repository.GetTableAsync(id);

            if (table is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TableResponseDto>(table));
        }
        /// <summary>
        /// Creates a new table.
        /// </summary>
        /// <param name="table">The data of the new table.</param>
        /// <returns>The newly created table.</returns>
        /// <response code="400">If the creation Data is invalid.</response>
        /// <response code="201">If the table is created successfully.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<TableResponseDto>> AddTable(TableCreationDto table)
        {
            var tableToAdd = _mapper.Map<Table>(table);

            var result = await _repository.AddTableAsync(tableToAdd);

            if (!result)
            {
                return BadRequest(new { Message = "The Table already exist." });
            }

            await _repository.SaveChangesAsync();

            return CreatedAtRoute("GetTable", new { id = tableToAdd?.TableId },
                                  _mapper.Map<TableResponseDto>(tableToAdd));
        }
        /// <summary>
        /// Updates an existing table specified by ID.
        /// </summary>
        /// <param name="id">The ID of the table to update.</param>
        /// <param name="table">The data for updating the table.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="404">If the table with the specified ID is not found.</response>
        /// <response code="204">If successful.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateTable(int id, TableUpdateDto table)
        {
            var tableToUpdate = await _repository.GetTableAsync(id);

            if (tableToUpdate is null)
            {
                return NotFound();
            }

            _mapper.Map(table, tableToUpdate);

            await _repository.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Partially updates an existing table specified by ID.
        /// </summary>
        /// <param name="id">The ID of the table to update.</param>
        /// <param name="patchDocument">The JSON patch document with partial update operations.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="400">If the patch document or updated data is invalid.</response>
        /// <response code="404">If the table with the specified ID is not found.</response>
        /// <response code="204">If the update is successful.</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PartiallyUpdateTable(int id,
            JsonPatchDocument<TableUpdateDto> patchDocument)
        {
            var table = await _repository.GetTableAsync(id);

            if (table is null)
            {
                return NotFound();
            }

            var tableToPatch = _mapper.Map<TableUpdateDto>(table);

            patchDocument.ApplyTo(tableToPatch, ModelState);

            if (!ModelState.IsValid || !TryValidateModel(tableToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(tableToPatch, table);

            await _repository.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Deletes an existing table specified by ID.
        /// </summary>
        /// <param name="id">The Id of the table to delete.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="404">if the table with the specified ID is not found.</response>
        /// <response code="204">if the deletion is successful.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var result = await _repository.DeleteTableAsync(id);

            if (!result)
            {
                return NotFound();
            }

            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
