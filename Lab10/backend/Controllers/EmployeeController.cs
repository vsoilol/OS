using Lab10.API.DataAccess;
using Lab10.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab10.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly ApplicationContext _context;

    public EmployeeController(ApplicationContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Employee>>> GetAllEmployeesAsync()
    {
        var employees = await _context.Employees.ToListAsync();

        return employees;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Employee?>> GetEmployeeByIdAsync([FromRoute] int id)
    {
        var employees = await _context.Employees.FirstOrDefaultAsync(_ => _.Id == id);

        return employees;
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateEmployeeAsync([FromBody] Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return Ok();
    }
    
    [HttpPut]
    public async Task<ActionResult<Employee?>> UpdateEmployeeAsync([FromBody] Employee employee)
    {
        var updatedEmployee = await _context.Employees.FirstOrDefaultAsync(_ => _.Id == employee.Id);

        if (updatedEmployee is null)
        {
            return BadRequest();
        }

        updatedEmployee.FirstName = employee.FirstName;
        updatedEmployee.LastName = employee.LastName;
        updatedEmployee.Address = employee.Address;
        updatedEmployee.Age = employee.Age;

        await _context.SaveChangesAsync();

        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<Employee?>> DeleteEmployeeByIdAsync([FromRoute] int id)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(_ => _.Id == id);
        
        if (employee is null)
        {
            return BadRequest();
        }

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();

        return Ok();
    }
}