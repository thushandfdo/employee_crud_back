using employee_crud.Exceptions;
using employee_crud.Interfaces.Repositories;
using employee_crud.Interfaces.Services;
using employee_crud.Models.Entities;

namespace employee_crud.Services
{
    public class DepartmentService(IDepartmentRepository departments) : IDepartmentService
    {
        public async Task<Department> GetDepartmentById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidArgumentException("Id must be greater than 0");
            }

            var department = await departments.GetDepartmentById(id);

            return department ?? throw new NotFoundException($"Department with id {id} not found");
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await departments.GetDepartments();
        }

        public async Task<Department> InsertDepartment(Department newDepartment)
        {
            departments.InsertDepartment(newDepartment);
            await departments.Save();

            var insertedDepartment = await departments.GetDepartmentByName(newDepartment.Name);

            return insertedDepartment ?? 
                   throw new NotFoundException($"Department with name {newDepartment.Name} not found");
        }

        public async Task<Department> UpdateDepartment(Department newDepartment)
        {
            var departmentToUpdate = 
                await departments.GetDepartmentById(newDepartment.Id) ?? 
                throw new NotFoundException($"Department with id {newDepartment.Id} not found");
            
            departmentToUpdate.Name = newDepartment.Name == "" ? departmentToUpdate.Name : newDepartment.Name;

            departments.UpdateDepartment(departmentToUpdate);
            await departments.Save();

            return departmentToUpdate;
        }

        public async Task<Department> DeleteDepartment(int id)
        {
            if (id <= 0)
            {
                throw new InvalidArgumentException("Id must be greater than 0");
            }

            var departmentToDelete = 
                await departments.GetDepartmentById(id) ?? 
                throw new NotFoundException($"Department with id {id} not found");

            departments.DeleteDepartment(id);
            await departments.Save();

            return departmentToDelete;
        }
    }
}
