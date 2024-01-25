using employee_crud.Models.Entities;

namespace employee_crud.Interfaces.Repositories;

public interface IEmployeeRepository
{
    Task<Employee?> GetEmployeeById(int id);
    Task<Employee?> GetEmployeeByFirstName(string firstName);
    Task<IEnumerable<Employee>> GetEmployees();
    void InsertEmployee(Employee employee);
    void UpdateEmployee(Employee employee);
    void DeleteEmployee(int id);
    Task Save();
}