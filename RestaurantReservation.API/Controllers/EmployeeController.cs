using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Repositories;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/employees")]
    [Authorize]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IRestaurantReservationRepository _repository;
        public EmployeeController(IRestaurantReservationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesAsync()
        {
            return Ok(await _repository.GetEmployeesAsync());
        }
        [HttpGet("{employeeId}")]
        public async Task<ActionResult<Employee>> GetEmployeeAsync(int employeeId)
        {
            var employee = await _repository.GetEmployeeAsync(employeeId);
            if (employee is null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        [HttpGet("managers")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetManagersAsync()
        {
            return Ok(await _repository.ListManagersAsync());
        }
        [HttpGet("{employeeId}/average-order-amount")]
        public async Task<ActionResult<decimal>> GetAverageOrderAmount(int employeeId)
        {
            return Ok(await _repository.CalculateAverageOrderAmountAsync(employeeId));
        }
        [HttpPost]
        public async Task<ActionResult<bool>> AddEmployeeAsync(Employee employee)
        {
            var result = await _repository.AddEmployeeAsync(employee);
            if (!result)
            {
                return Conflict(new { Message = "The Employee already exist." });
            }
            await _repository.SaveChangesAsync();
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateEmployeeAsync(Employee employee)
        {
            var result = await _repository.UpdateEmployeeAsync(employee);
            if (!result)
            {
                return Conflict(new { Message = "The Employee doesn't exist." });
            }
            await _repository.SaveChangesAsync();
            return Ok(result);
        }
        [HttpDelete("{employeeId}")]
        public async Task<ActionResult<bool>> DeleteEmployeeAsync(int employeeId)
        {
            var result = await _repository.DeleteEmployeeAsync(employeeId);
            if (!result)
            {
                return Conflict(new { Message = "The Employee doesn't exist." });
            }
            await _repository.SaveChangesAsync();
            return Ok(result);
        }
    }
}
