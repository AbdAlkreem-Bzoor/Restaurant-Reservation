using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Extensions;
using RestaurantReservation.API.Models.Customer;
using RestaurantReservation.API.Repositories;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/v{version:apiVersion}/customers")]
    [Authorize]
    [ApiController]
    [ApiVersion(1.0)]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;
        public CustomerController(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        /// <summary>
        /// Gets customers partitioned into pages.
        /// </summary>
        /// <param name="pageNumber">The number of the needed page.</param>
        /// <param name="pageSize">The size of the needed page.</param>
        /// <response code="400">When pageNumber or pageSize is less than zero.</response>
        /// <response code="200">Returns the requested page of customers.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CustomerResponseDto>>> GetCustomers(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest($"page number and page size must be greater than 0.");
            }
            return Ok(_mapper.Map<IEnumerable<CustomerResponseDto>>((await _repository.GetCustomersAsync()).GetPage(pageNumber, pageSize)));
        }
        /// <summary>
        /// Returns a customer specified by ID.
        /// </summary>
        /// <param name="id">The ID of the customer to retrieve.</param>
        /// <response code="404">If the customer with the given id is not found.</response>
        /// <response code="200">Returns the requested customer.</response>
        [HttpGet("{id}", Name = "GetCustomer")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CustomerResponseDto>> GetCustomer(int id)
        {
            var customer = await _repository.GetCustomerAsync(id);

            if (customer is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CustomerResponseDto>(customer));
        }
        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="customer">The data of the new customer.</param>
        /// <returns>The newly created customer.</returns>
        /// <response code="400">If the creation data is invalid.</response>
        /// <response code="201">If the customer is created successfully.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CustomerResponseDto>> AddCustomer(CustomerCreationDto customer)
        {
            var customerToAdd = _mapper.Map<Customer>(customer);

            var result = await _repository.AddCustomerAsync(customerToAdd);

            if (!result)
            {
                return BadRequest(new { Message = "The Customer already exist." });
            }

            await _repository.SaveChangesAsync();

            return CreatedAtRoute("GetCustomer", new { id = customerToAdd?.CustomerId },
                                  _mapper.Map<CustomerResponseDto>(customerToAdd));
        }
        /// <summary>
        /// Updates an existing customer specified by ID.
        /// </summary>
        /// <param name="id">The ID of the customer to update.</param>
        /// <param name="customer">The data for updating the customer.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="404">If the customer with the specified ID is not found.</response>
        /// <response code="204">If successful.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerUpdateDto customer)
        {
            var customerToUpdate = await _repository.GetCustomerAsync(id);

            if (customerToUpdate is null)
            {
                return NotFound();
            }

            _mapper.Map(customer, customerToUpdate);

            await _repository.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Partially updates an existing customer specified by ID.
        /// </summary>
        /// <param name="id">The ID of the customer to update.</param>
        /// <param name="patchDocument">The JSON patch document with partial update operations.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="400">If the patch document or updated data is invalid.</response>
        /// <response code="404">If the customer with the specified ID is not found.</response>
        /// <response code="204">If the update is successful.</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PartiallyUpdateCustomer(int id,
            JsonPatchDocument<CustomerUpdateDto> patchDocument)
        {
            var customer = await _repository.GetCustomerAsync(id);

            if (customer is null)
            {
                return NotFound();
            }

            var customerToPatch = _mapper.Map<CustomerUpdateDto>(customer);

            patchDocument.ApplyTo(customerToPatch, ModelState);

            if (!ModelState.IsValid || !TryValidateModel(customerToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(customerToPatch, customer);

            await _repository.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Deletes an existing customer specified by ID.
        /// </summary>
        /// <param name="id">The ID of the customer to delete.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="404">if the customer with the specified ID is not found.</response>
        /// <response code="204">if the deletion is successful.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result = await _repository.DeleteCustomerAsync(id);

            if (!result)
            {
                return NotFound();
            }

            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
