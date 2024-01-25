using employee_crud.Data;
using employee_crud.Exceptions;
using employee_crud.Interfaces.Services;
using employee_crud.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace employee_crud.Services
{
    public class DepartmentService(DataContext dataContext) : IDepartmentService
    {
        private DbSet<Department> Departments { get; set; } = dataContext.Departments;

        public async Task<Department> GetDepartmentById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidArgumentException("Id must be greater than 0");
            }

            var department = await Departments.FindAsync(id);

            return department ?? throw new NotFoundException($"Department with id {id} not found");
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await Departments.ToListAsync();
        }

        public async Task<Department> InsertDepartment(Department newDepartment)
        {
            Departments.Add(newDepartment);
            await dataContext.SaveChangesAsync();

            var insertedDepartment = await Departments.FirstOrDefaultAsync(department => department.Name == newDepartment.Name);

            return insertedDepartment ?? 
                   throw new NotFoundException($"Department with name {newDepartment.Name} not found");
        }

        public async Task<Department> UpdateDepartment(Department newDepartment)
        {
            var departmentToUpdate = 
                await Departments.FindAsync(newDepartment.Id) ?? 
                throw new NotFoundException($"Department with id {newDepartment.Id} not found");
            
            departmentToUpdate.Name = newDepartment.Name == "" ? departmentToUpdate.Name : newDepartment.Name;

            Departments.Update(departmentToUpdate);
            await dataContext.SaveChangesAsync();

            return departmentToUpdate;
        }

        public async Task<Department> DeleteDepartment(int id)
        {
            if (id <= 0)
            {
                throw new InvalidArgumentException("Id must be greater than 0");
            }

            var departmentToDelete = 
                await Departments.FindAsync(id) ?? 
                throw new NotFoundException($"Department with id {id} not found");

            Departments.Remove(departmentToDelete);
            await dataContext.SaveChangesAsync();

            return departmentToDelete;
        }
    }
}
