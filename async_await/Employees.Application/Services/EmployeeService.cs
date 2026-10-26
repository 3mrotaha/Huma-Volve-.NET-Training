using Microsoft.EntityFrameworkCore;
using Employees.Domain.AppContext;
using Employees.Domain.Entities;
using Employees.Application.DTO;
using Employees.Application.ServiceContracts;

namespace Employees.Application.Services;

public class EmployeeService(AppDbContext db) : IEmployeeService
{
    private readonly AppDbContext _db = db;

    public async Task<EmployeeDetailsDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        // get the employee from database
        Employee? employee = await _db.Employees.FindAsync([id], ct);

        if(employee is null)
            return null;

        return employee.ToEmployeeDetails();
    }

    public async Task<List<EmployeeDetailsDto>> GetAllAsync(CancellationToken ct)
    {
        List<Employee> employees = await _db.Employees.AsNoTracking().ToListAsync(ct);

        return employees.Select(e => e.ToEmployeeDetails()).ToList();
    }

    public async Task<EmployeeDetailsDto> CreateAsync(EmployeeRequest request, CancellationToken ct)
    {
        var employee = new Employee() { Name = request.Name, Salary = request.Salary };

        await _db.Employees.AddAsync(employee, ct);
        await _db.SaveChangesAsync(ct);

        return employee.ToEmployeeDetails();
    }

    public async Task<EmployeeDetailsDto?> UpdateAsync(int id, EmployeeRequest request, CancellationToken ct)
    {
        Employee? employee = await _db.Employees.FindAsync([id], ct);

        if(employee is null)
            return null;

        employee.Name = request.Name;
        employee.Salary = request.Salary;

        await _db.SaveChangesAsync(ct);

        return employee.ToEmployeeDetails();
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        Employee? employee = await _db.Employees.FindAsync([id], ct);

        if(employee is null)
            return false;

        _db.Employees.Remove(employee);
        await _db.SaveChangesAsync(ct);

        return true;
    }
}
