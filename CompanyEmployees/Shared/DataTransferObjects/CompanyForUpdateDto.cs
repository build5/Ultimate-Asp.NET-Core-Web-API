namespace Shared.DataTransferObjects;

public record CompanyForUpdateDto(string Name, string Adderss, string Country, IEnumerable<EmployeeForCreationDto> Employees);

