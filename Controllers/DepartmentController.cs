using employee_crud.Exceptions;
using employee_crud.Interfaces.Services;
using employee_crud.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace employee_crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController(IDepartmentService departmentService) : ControllerBase
    {
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                var department = await departmentService.GetDepartmentById(id);
                return Ok(department);
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
                var departments = await departmentService.GetDepartments();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertDepartment(Department department)
        {
            try
            {
                var insertedDepartment = await departmentService.InsertDepartment(department);
                return Ok(insertedDepartment);
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
        public async Task<IActionResult> UpdateDepartment(Department department, int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Id must be greater than 0");
                }

                if (department.Id != id)
                {
                    return BadRequest("Id in body must match id in route");
                }

                var updatedDepartment = await departmentService.UpdateDepartment(department);
                return Ok(updatedDepartment);
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

                var deletedDepartment = await departmentService.DeleteDepartment(id);
                return Ok(deletedDepartment);
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
