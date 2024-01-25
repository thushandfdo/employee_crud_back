using employee_crud.Data;
using employee_crud.Exceptions;
using employee_crud.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace employee_crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController(DataContext dataContext) : ControllerBase
    {
        private DbSet<Department> Departments { get; set; } = dataContext.Departments;

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new InvalidArgumentException("Id must be greater than 0");
                }

                var department = await Departments.FindAsync(id);

                return Ok(department) ?? throw new NotFoundException($"Department with id {id} not found");
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
        public async Task<IActionResult> GetDepartments()
        {
            try
            {
                var departments = await Departments.ToListAsync();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertDepartment(Department newDepartment)
        {
            try
            {
                Departments.Add(newDepartment);
                await dataContext.SaveChangesAsync();

                var insertedDepartment = await Departments.FirstOrDefaultAsync(department => department.Name == newDepartment.Name);

                return Ok(insertedDepartment) ??
                       throw new NotFoundException($"Department with name {newDepartment.Name} not found");
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
        public async Task<IActionResult> UpdateDepartment(Department newDepartment, int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Id must be greater than 0");
                }

                if (newDepartment.Id != id)
                {
                    return BadRequest("Id in body must match id in route");
                }

                var departmentToUpdate =
                    await Departments.FindAsync(newDepartment.Id) ??
                    throw new NotFoundException($"Department with id {newDepartment.Id} not found");

                departmentToUpdate.Name = newDepartment.Name == "" ? departmentToUpdate.Name : newDepartment.Name;

                Departments.Update(departmentToUpdate);
                await dataContext.SaveChangesAsync();

                return Ok(departmentToUpdate);
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
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Id must be greater than 0");
                }

                var departmentToDelete =
                    await Departments.FindAsync(id) ??
                    throw new NotFoundException($"Department with id {id} not found");

                Departments.Remove(departmentToDelete);
                await dataContext.SaveChangesAsync();

                return Ok(departmentToDelete);
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
