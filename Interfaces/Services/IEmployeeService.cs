using employee_crud.Models.Entities;

namespace employee_crud.Interfaces.Services;

public interface IEmployeeService
{
    Task<Employee> GetEmployeeById(int id);
    Task<IEnumerable<Employee>> GetEmployees();
    Task<Employee> InsertEmployee(Employee employee);
    Task<Employee> UpdateEmployee(Employee employee);
    Task<Employee> DeleteEmployee(int id);
}