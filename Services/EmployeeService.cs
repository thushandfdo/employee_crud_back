using employee_crud.Exceptions;
using employee_crud.Interfaces.Repositories;
using employee_crud.Interfaces.Services;
using employee_crud.Models.Entities;

namespace employee_crud.Services
{
    public class EmployeeService(IEmployeeRepository employees) : IEmployeeService
    {
        public async Task<Employee> GetEmployeeById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidArgumentException("Id must be greater than 0");
            }

            var employee = await employees.GetEmployeeById(id);

            return employee ?? throw new NotFoundException($"Employee with id {id} not found");
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await employees.GetEmployees();
        }

        public async Task<Employee> InsertEmployee(Employee newEmployee)
        {
            employees.InsertEmployee(newEmployee);
            await employees.Save();

            var insertedEmployee = await employees.GetEmployeeByFirstName(newEmployee.FirstName);

            return insertedEmployee ??
                   throw new NotFoundException($"Employee with name {newEmployee.FirstName} not found");
        }

        public async Task<Employee> UpdateEmployee(Employee newEmployee)
        {
            var employeeToUpdate =
                await employees.GetEmployeeById(newEmployee.Id) ??
                throw new NotFoundException($"Employee with id {newEmployee.Id} not found");

            employeeToUpdate.FirstName = newEmployee.FirstName == "" ? employeeToUpdate.FirstName : newEmployee.FirstName;

            employees.UpdateEmployee(employeeToUpdate);
            await employees.Save();

            return employeeToUpdate;
        }

        public async Task<Employee> DeleteEmployee(int id)
        {
            if (id <= 0)
            {
                throw new InvalidArgumentException("Id must be greater than 0");
            }

            var employeeToDelete =
                await employees.GetEmployeeById(id) ??
                throw new NotFoundException($"Employee with id {id} not found");

            employees.DeleteEmployee(id);
            await employees.Save();

            return employeeToDelete;
        }
    }
}
