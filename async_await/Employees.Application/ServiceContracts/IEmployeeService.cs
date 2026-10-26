using Employees.Application.DTO;

namespace Employees.Application.ServiceContracts;

public interface IEmployeeService
{
    public Task<EmployeeDetailsDto?> GetByIdAsync(int id, CancellationToken ct);
    public Task<List<EmployeeDetailsDto>> GetAllAsync(CancellationToken ct);
    public Task<EmployeeDetailsDto> CreateAsync(EmployeeRequest request, CancellationToken ct);
    public Task<EmployeeDetailsDto?> UpdateAsync(int id, EmployeeRequest request, CancellationToken ct);
    public Task<bool> DeleteAsync(int id, CancellationToken ct);
}
