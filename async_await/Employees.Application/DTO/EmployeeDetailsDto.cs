namespace Employees.Application.DTO;

public record EmployeeDetailsDto(
    int Id,
    string Name,
    decimal SalaryAmount,
    string Currency
);
