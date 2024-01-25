using employee_crud.Data;
using employee_crud.Exceptions;
using employee_crud.Interfaces.Services;
using employee_crud.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace employee_crud.Services
{
    public class EmployeeService(DataContext dataContext) : IEmployeeService
    {
        private DbSet<Employee> Employees { get; set; } = dataContext.Employees;

        public async Task<Employee> GetEmployeeById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidArgumentException("Id must be greater than 0");
            }

            var employee = await Employees.FindAsync(id);

            return employee ?? throw new NotFoundException($"Employee with id {id} not found");
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await Employees.Include("Department").ToListAsync();
        }

        public async Task<Employee> InsertEmployee(Employee newEmployee)
        {
            Employees.Add(newEmployee);
            await dataContext.SaveChangesAsync();

            var insertedEmployee = await Employees.FirstOrDefaultAsync(employee => employee.FirstName == newEmployee.FirstName);

            return insertedEmployee ??
                   throw new NotFoundException($"Employee with name {newEmployee.FirstName} not found");
        }

        public async Task<Employee> UpdateEmployee(Employee newEmployee)
        {
            var employeeToUpdate =
                await Employees.FindAsync(newEmployee.Id) ??
                throw new NotFoundException($"Employee with id {newEmployee.Id} not found");

            employeeToUpdate.FirstName = newEmployee.FirstName == "" ? employeeToUpdate.FirstName : newEmployee.FirstName;

            Employees.Update(employeeToUpdate);
            await dataContext.SaveChangesAsync();

            return employeeToUpdate;
        }

        public async Task<Employee> DeleteEmployee(int id)
        {
            if (id <= 0)
            {
                throw new InvalidArgumentException("Id must be greater than 0");
            }

            var employeeToDelete =
                await Employees.FindAsync(id) ??
                throw new NotFoundException($"Employee with id {id} not found");

            Employees.Remove(employeeToDelete);
            await dataContext.SaveChangesAsync();

            return employeeToDelete;
        }
    }
}
