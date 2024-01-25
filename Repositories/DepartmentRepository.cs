using employee_crud.Data;
using employee_crud.Interfaces.Repositories;
using employee_crud.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace employee_crud.Repositories
{
    public class DepartmentRepository(DataContext dataContext) : IDepartmentRepository
    {
        private DbSet<Department> Departments { get; set; } = dataContext.Departments;

        public async Task<Department?> GetDepartmentById(int id)
        {
            return await Departments.FindAsync(id);
        }

        public async Task<Department?> GetDepartmentByName(string name)
        {
            return await Departments.FirstOrDefaultAsync(department => department.Name == name);
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await Departments.ToListAsync();
        }

        public void InsertDepartment(Department department)
        {
            Departments.Add(department);
        }

        public void UpdateDepartment(Department department)
        {
            Departments.Update(department);
        }

        public async void DeleteDepartment(int id)
        {
            var department = await Departments.FindAsync(id);

            if (department != null)
            {
                Departments.Remove(department);
            }
        }

        public async Task Save()
        {
            await dataContext.SaveChangesAsync();
        }
    }
}
