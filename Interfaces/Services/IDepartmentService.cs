using employee_crud.Models.Entities;

namespace employee_crud.Interfaces.Services;

public interface IDepartmentService
{
    Task<Department> GetDepartmentById(int id);
    Task<IEnumerable<Department>> GetDepartments();
    Task<Department> InsertDepartment(Department department);
    Task<Department> UpdateDepartment(Department department);
    Task<Department> DeleteDepartment(int id);
}