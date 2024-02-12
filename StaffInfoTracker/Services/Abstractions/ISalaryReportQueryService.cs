using StaffInfoTracker.ViewModels;

namespace StaffInfoTracker.Services.Abstractions;

public interface ISalaryReportQueryService
{
    Task<List<SalaryReportViewModel>> GetSalaryReportForDepartmentsAsync();
    Task<List<SalaryReportViewModel>> GetSalaryReportForPositionsAsync();
    Task<string> GenerateSalaryReportTextAsync(string filterOption);
}
