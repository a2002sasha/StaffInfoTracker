using StaffInfoTracker.Models;

namespace StaffInfoTracker.Services.Abstractions;

public interface IStaffQueryService
{
    Task<List<Employee>> GetAllEmployeesAsync();
    Task<Employee?> GetEmployeeByIdAsync(int id);
    Task<List<Department>> GetDepartmentsAsync();
    Task<List<Position>> GetPositionsAsync();
    Task EditEmployeeInfoAsync(Employee employee);
    Task<bool> IsEmployeeByPhoneNumberExistAsync(int employeeId, string phoneNumber);
}
