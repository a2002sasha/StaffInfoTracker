using StaffInfoTracker.Models;

namespace StaffInfoTracker.Services.Abstractions;

public interface IFilterStaffQueryService
{
    Task<List<Employee>> GetFilteredEmployeesAsync(int positionId, int departmentId, string fullName);
}
