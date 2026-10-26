using Employees.Domain.Entities;
using Employees.Application.DTO;
using Employees.Application.DTO.V1;

namespace Employees.Application;

public static class EmployeeExtensions
{
    public static EmployeeDetailsDto ToEmployeeDetails(this Employee e)
    {
        return new EmployeeDetailsDto(e.Id, e.Name, e.Salary, "EGP");
    }
    public static EmployeeDTO ToEmployeeResponseV1(this EmployeeDetailsDto ed)
    {
        return new EmployeeDTO(ed.Id, ed.Name, ed.SalaryAmount);
    }

    public static DTO.V2.EmployeeDTO ToEmployeeResponseV2(this EmployeeDetailsDto ed)
    {
        return new DTO.V2.EmployeeDTO(ed.Id, ed.Name,
                    new DTO.V2.SalaryResponse(ed.SalaryAmount, ed.Currency));
    }
}
