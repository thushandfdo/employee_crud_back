using employee_crud.Models.Entities;

namespace employee_crud.Interfaces.Repositories;

public interface IDepartmentRepository
{
    Task<Department?> GetDepartmentById(int id);
    Task<Department?> GetDepartmentByName(string name);
    Task<IEnumerable<Department>> GetDepartments();
    void InsertDepartment(Department department);
    void UpdateDepartment(Department department);
    void DeleteDepartment(int id);
    Task Save();
}