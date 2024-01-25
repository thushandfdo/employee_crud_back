using employee_crud.Data;
using employee_crud.Exceptions;
using employee_crud.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace employee_crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(DataContext dataContext) : ControllerBase
    {
        private DbSet<Employee> Employees { get; set; } = dataContext.Employees;

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new InvalidArgumentException("Id must be greater than 0");
                }

                var employee = await Employees.FindAsync(id);

                return Ok(employee) ?? throw new NotFoundException($"Employee with id {id} not found");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employees = await Employees.Include("Department").ToListAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertEmployee(Employee newEmployee)
        {
            try
            {
                Employees.Add(newEmployee);
                await dataContext.SaveChangesAsync();

                var insertedEmployee = await Employees.FirstOrDefaultAsync(employee => employee.FirstName == newEmployee.FirstName);

                return Ok(insertedEmployee) ??
                       throw new NotFoundException($"Employee with name {newEmployee.FirstName} not found");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEmployee(Employee newEmployee, int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Id must be greater than 0");
                }

                if (newEmployee.Id != id)
                {
                    return BadRequest("Id in body must match id in route");
                }

                var employeeToUpdate =
                    await Employees.FindAsync(newEmployee.Id) ??
                    throw new NotFoundException($"Employee with id {newEmployee.Id} not found");

                employeeToUpdate.FirstName = newEmployee.FirstName == "" ? employeeToUpdate.FirstName : newEmployee.FirstName;

                Employees.Update(employeeToUpdate);
                await dataContext.SaveChangesAsync();

                return Ok(employeeToUpdate);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Id must be greater than 0");
                }

                var employeeToDelete =
                    await Employees.FindAsync(id) ??
                    throw new NotFoundException($"Employee with id {id} not found");

                Employees.Remove(employeeToDelete);
                await dataContext.SaveChangesAsync();

                return Ok(employeeToDelete);
            }
            catch (InvalidArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500);
            }
        }
    }
}
