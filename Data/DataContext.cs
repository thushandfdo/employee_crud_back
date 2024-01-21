using employee_crud.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace employee_crud.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<Employee>? Employees { get; set; }

        public DbSet<Department>? Departments { get; set; }
    }
}
