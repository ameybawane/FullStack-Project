using FullStack.API.Data;
using FullStack.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        // _fullStackDbContext private property
        private readonly FullStackDbContext _fullStackDbContext;

        // use dbcontext to use ef to talk to our sql server db
        public EmployeesController(FullStackDbContext fullStackDbContext)
        {
            _fullStackDbContext = fullStackDbContext;
        }

        // GET: api/Employees
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var employees = _fullStackDbContext.Employees.ToList();
            return Ok(employees);
        }

        // GET: api/Employees/3
        [HttpGet("{id}")]
        public IActionResult GetEmployee(Guid id)
        {
            // get a employee from dbcontext
            var employee = _fullStackDbContext.Employees.Where(x => x.Id == id).FirstOrDefault();

            if (employee == null)
            {
                return NotFound("Employee not found!");
            }
            return Ok(employee);
        }

        // POST: api/Employees
        [HttpPost]
        public IActionResult AddEmployee([FromBody] Employee employeeRequest)
        {
            try
            {
                // create always new id for employee
                employeeRequest.Id = Guid.NewGuid();
                // add new employee to dbcontext
                var employees = _fullStackDbContext.Employees.Add(employeeRequest);
                // to save changes
                _fullStackDbContext.SaveChanges();

                return Ok(employeeRequest);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
                // 500 internal server error
            }
        }

        // PUT: api/Employees/3
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee([FromRoute] Guid id, Employee updateEmployeeRequest)
        {
            try
            {
                // get a employee from dbcontext
                var employee = _fullStackDbContext.Employees.Find(id);

                if (employee == null)
                {
                    return NotFound();
                    // not found 404
                }
                employee.Name = updateEmployeeRequest.Name;
                employee.Email = updateEmployeeRequest.Email;
                employee.Phone = updateEmployeeRequest.Phone;
                employee.Salary = updateEmployeeRequest.Salary;
                employee.Department = updateEmployeeRequest.Department;
                // to save changes
                _fullStackDbContext.SaveChanges();

                return Ok(employee);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
                // 500 internal server error
            }
        }

        // DELETE: api/Employees/3
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee([FromRoute] Guid id)
        {
            // get a employee from dbcontext
            var employee = _fullStackDbContext.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            // to delete found employee
            _fullStackDbContext.Employees.Remove(employee);
            // to save changes
            _fullStackDbContext.SaveChanges();

            return Ok(employee);
        }
    }
}
