using employee_crud.Exceptions;
using employee_crud.Interfaces.Services;
using employee_crud.Models.Entities;
using employee_crud.Services;
using Microsoft.AspNetCore.Mvc;

namespace employee_crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(IEmployeeService employeeService) : ControllerBase
    {
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employee = await employeeService.GetEmployeeById(id);
                return Ok(employee);
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
                var employees = await employeeService.GetEmployees();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertEmployee(Employee employee)
        {
            try
            {
                var insertedEmployee = await employeeService.InsertEmployee(employee);
                return Ok(insertedEmployee);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                //return StatusCode(500);
                throw;
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEmployee(Employee employee, int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Id must be greater than 0");
                }

                if (employee.Id != id)
                {
                    return BadRequest("Id in body must match id in route");
                }

                var updatedEmployee = await employeeService.UpdateEmployee(employee);
                return Ok(updatedEmployee);
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

                var deletedEmployee = await employeeService.DeleteEmployee(id);
                return Ok(deletedEmployee);
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
