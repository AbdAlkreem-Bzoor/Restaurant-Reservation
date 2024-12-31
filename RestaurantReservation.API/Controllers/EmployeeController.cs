using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Extensions;
using RestaurantReservation.API.Models.Employee;
using RestaurantReservation.API.Repositories;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/v{version:apiVersion}/employees")]
    [Authorize]
    [ApiController]
    [ApiVersion(1.0)]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        /// <summary>
        /// Gets employees partitioned into pages.
        /// </summary>
        /// <param name="pageNumber">The number of the needed page.</param>
        /// <param name="pageSize">The size of the needed page.</param>
        /// <response code="400">When pageNumber or pageSize is less than zero.</response>
        /// <response code="200">Returns the requested page of employees with pagination metadata in the headers.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EmployeeResponseDto>>> GetEmployees(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest($"page number and page size must be greater than 0.");
            }
            return Ok(_mapper.Map<IEnumerable<EmployeeResponseDto>>((await _repository.GetEmployeesAsync()).GetPage(pageNumber, pageSize)));
        }
        /// <summary>
        /// Returns an employee specified by ID.
        /// </summary>
        /// <param name="id">The ID of the employee to retrieve.</param>
        /// <response code="404">If the employee with the given id is not found.</response>
        /// <response code="200">Returns the requested employee.</response>
        [HttpGet("{id}", Name = "GetEmployee")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EmployeeResponseDto>> GetEmployee(int id)
        {
            var employee = await _repository.GetEmployeeAsync(id);

            if (employee is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<EmployeeResponseDto>(employee));
        }
        /// <summary>
        /// Creates a new employee.
        /// </summary>
        /// <param name="employee">The data of the new employee.</param>
        /// <returns>The newly created employee.</returns>
        /// <response code="400">If the employee data is invalid.</response>
        /// <response code="201">If the employee is created successfully.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<EmployeeResponseDto>> AddEmployee(EmployeeCreationDto employee)
        {
            var employeeToAdd = _mapper.Map<Employee>(employee);

            var result = await _repository.AddEmployeeAsync(employeeToAdd);

            if (!result)
            {
                return BadRequest(new { Message = "The Employee already exist." });
            }

            await _repository.SaveChangesAsync();

            return CreatedAtRoute("GetEmployee", new { id = employeeToAdd?.EmployeeId },
                                  _mapper.Map<EmployeeResponseDto>(employeeToAdd));
        }
        /// <summary>
        /// Updates an existing employee specified by ID.
        /// </summary>
        /// <param name="id">The ID of the employee to update.</param>
        /// <param name="employee">The data for updating the employee.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="404">If the employee with the specified ID is not found.</response>
        /// <response code="204">If successful.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeUpdateDto employee)
        {
            var employeeToUpdate = await _repository.GetEmployeeAsync(id);

            if (employeeToUpdate is null)
            {
                return NotFound();
            }

            _mapper.Map(employee, employeeToUpdate);

            await _repository.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Partially updates an existing employee specified by ID.
        /// </summary>
        /// <param name="id">The ID of the employee to update.</param>
        /// <param name="patchDocument">The JSON patch document with partial update operations.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="400">If the patch document or updated data is invalid.</response>
        /// <response code="404">If the employee with the specified ID is not found.</response>
        /// <response code="204">If the update is successful.</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PartiallyUpdateEmployee(int id,
            JsonPatchDocument<EmployeeUpdateDto> patchDocument)
        {
            var employee = await _repository.GetEmployeeAsync(id);

            if (employee is null)
            {
                return NotFound();
            }

            var employeeToPatch = _mapper.Map<EmployeeUpdateDto>(employee);

            patchDocument.ApplyTo(employeeToPatch, ModelState);

            if (!ModelState.IsValid || !TryValidateModel(employeeToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(employeeToPatch, employee);

            await _repository.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Deletes an existing employee specified by ID.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="404">if the employee with the specified ID is not found.</response>
        /// <response code="204">if the deletion is successful.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _repository.DeleteEmployeeAsync(id);

            if (!result)
            {
                return NotFound();
            }

            await _repository.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Gets employees who are managers partitioned into pages.
        /// </summary>
        /// <param name="pageNumber">The number of the needed page.</param>
        /// <param name="pageSize">The size of the needed page.</param>
        /// <response code="400">When pageNumber or pageSize is less than zero.</response>
        /// <response code="200">Returns all employees that are managers.</response>
        [HttpGet("managers")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EmployeeResponseDto>>> GetManagers(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest($"page number and page size must be greater than 0.");
            }
            return Ok(_mapper.Map<IEnumerable<EmployeeResponseDto>>((await _repository.ListManagersAsync()).GetPage(pageNumber, pageSize)));
        }
        /// <summary>
        /// Returns the average order amount for an employee.
        /// </summary>
        /// <param name="employeeId">The ID of the employee.</param>
        /// <response code="404">If the employee with the given ID is not found.</response>
        /// <response code="200">Returns the average order amount of the employee.</response>
        [HttpGet("{employeeId}/average-order-amount")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<decimal>> GetAverageOrderAmount(int employeeId)
        {
            var employee = _repository.GetEmployeeAsync(employeeId);
            if (employee is null)
            {
                return NotFound();
            }
            return Ok(await _repository.CalculateAverageOrderAmountAsync(employeeId));
        }
    }
}
