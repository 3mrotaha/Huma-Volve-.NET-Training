using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Employees.Application.DTO;
using Employees.Application.ServiceContracts;
using Employees.Application;
namespace EmployeesAPI.Controllers.V1;

[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class EmployeesController(IEmployeeService employeeService) : ControllerBase
{
    private readonly IEmployeeService _employeeService = employeeService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken ct)
    {
        var employees = await _employeeService.GetAllAsync(ct);

        var response = employees.Select(e => e.ToEmployeeResponseV1());

        return Ok(response);
    }

    [HttpGet("{id:int}", Name = "GetEmployeeV1")]
    public async Task<IActionResult> GetByIdAsync(int id, CancellationToken ct)
    {
        var employee = await _employeeService.GetByIdAsync(id, ct);

        if(employee is null)
            return NotFound();

        var response = employee.ToEmployeeResponseV1();

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(EmployeeRequest request, CancellationToken ct)
    {
        var employee = await _employeeService.CreateAsync(request, ct);

        var response = employee.ToEmployeeResponseV1();

        return CreatedAtRoute("GetEmployeeV1", new { id = response.Id, version = "1" }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, EmployeeRequest request, CancellationToken ct)
    {
        var employee = await _employeeService.UpdateAsync(id, request, ct);

        if(employee is null)
            return NotFound();

        var response = employee.ToEmployeeResponseV1();

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken ct)
    {
        var deleted = await _employeeService.DeleteAsync(id, ct);

        if(!deleted)
            return NotFound();

        return NoContent();
    }
}
