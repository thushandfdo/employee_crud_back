using employee_crud.Data;
using employee_crud.Interfaces.Repositories;
using employee_crud.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace employee_crud.Repositories
{
    public class EmployeeRepository(DataContext dataContext) : IEmployeeRepository
    {
        private DbSet<Employee> Employees { get; set; } = dataContext.Employees;

        public async Task<Employee?> GetEmployeeById(int id)
        {
            return await Employees.FindAsync(id);
        }

        public async Task<Employee?> GetEmployeeByFirstName(string firstName)
        {
            return await Employees.FirstOrDefaultAsync(employee => employee.FirstName == firstName);
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await Employees.Include("Department").ToListAsync();
        }

        public void InsertEmployee(Employee employee)
        {
            Employees.Add(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            Employees.Update(employee);
        }

        public async void DeleteEmployee(int id)
        {
            var employee = await Employees.FindAsync(id);

            if (employee != null)
            {
                Employees.Remove(employee);
            }
        }

        public async Task Save()
        {
            await dataContext.SaveChangesAsync();
        }
    }
}
