using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Data;
using RestaurantReservation.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Repositories
{
    public class EmployeeRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public EmployeeRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task CreateEmployeeAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(Employee updatedEmployee)
        {
            var employee = await _context.Employees.FindAsync(updatedEmployee.EmployeeId)
                                 ??
                                 throw new KeyNotFoundException("Employee not found");

            employee.FirstName = updatedEmployee.FirstName;
            employee.LastName = updatedEmployee.LastName;
            employee.Position = updatedEmployee.Position;

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int employeeId)
        {
            var employee = await _context.Employees.FindAsync(employeeId)
                                 ??
                                 throw new KeyNotFoundException("Employee not found");

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Employee>> ListManagersAsync()
        {
            return await _context.Employees.AsNoTracking()
                                           .Where(e => e.Position == "Manager")
                                           .ToListAsync();
        }
        public async Task<List<EmployeeRestaurantDetail>> 
            GetEmployeesWithRestaurantDetailsAsync()
        {
            return await _context.EmployeeRestaurantDetails.AsNoTracking().ToListAsync();
        }
    }
}
