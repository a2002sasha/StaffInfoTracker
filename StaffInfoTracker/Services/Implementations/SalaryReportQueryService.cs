using Dapper;
using Microsoft.Data.SqlClient;
using System.Text;
using StaffInfoTracker.Services.Abstractions;
using StaffInfoTracker.ViewModels;

namespace StaffInfoTracker.Services.Implementations;

public class SalaryReportQueryService(IConfiguration configuration) : ISalaryReportQueryService
{
    private readonly string? _connectionString = configuration.GetConnectionString("DefaultConnection");

    public async Task<List<SalaryReportViewModel>> GetSalaryReportForDepartmentsAsync()
    {
        await using (var connection = new SqlConnection(_connectionString))
        {
            var query = @"SELECT 
                            d.DepartmentName AS Name,
                            COUNT(e.EmployeeId) AS EmployeeQty,
                            MIN(e.Salary) AS MinSalary,
                            MAX(e.Salary) AS MaxSalary,
                            AVG(e.Salary) AS AvgSalary,
                            SUM(e.Salary) AS TotalSalarySum
                        FROM Employees e
						JOIN Departments d ON e.DepartmentId = d.DepartmentId
                        GROUP BY d.DepartmentName";

            var salaryReports = await connection.QueryAsync<SalaryReportViewModel>(query);

            return salaryReports.ToList();
        }
    }

    public async Task<List<SalaryReportViewModel>> GetSalaryReportForPositionsAsync()
    {
        await using (var connection = new SqlConnection(_connectionString))
        {
            var query = @"SELECT 
                            p.PositionName AS Name,
                            COUNT(e.EmployeeId) AS EmployeeQty,
                            MIN(e.Salary) AS MinSalary,
                            MAX(e.Salary) AS MaxSalary,
                            AVG(e.Salary) AS AvgSalary,
                            SUM(e.Salary) AS TotalSalarySum
                        FROM Employees e
						JOIN Positions p ON e.PositionId = p.PositionId
                        GROUP BY p.PositionName";

            var salaryReports = await connection.QueryAsync<SalaryReportViewModel>(query);

            return salaryReports.ToList();
        }
    }

    public async Task<string> GenerateSalaryReportTextAsync(string filterOption)
    {
        var salaryReport = filterOption == "Відділ"
            ? await GetSalaryReportForDepartmentsAsync()
            : await GetSalaryReportForPositionsAsync();

        var stringBuilder = new StringBuilder();

        foreach (var reportItem in salaryReport)
        {
            stringBuilder.AppendLine($"Назва: {reportItem.Name}");
            stringBuilder.AppendLine($"Кількість працівників: {reportItem.EmployeeQty}");
            stringBuilder.AppendLine($"Мінімальний оклад: {reportItem.MinSalary.ToString("0.00")}");
            stringBuilder.AppendLine($"Максимальний оклад: {reportItem.MaxSalary.ToString("0.00")}");
            stringBuilder.AppendLine($"Середній оклад: {reportItem.AvgSalary.ToString("0.00")}");
            stringBuilder.AppendLine($"Загальна сума окладів: {reportItem.TotalSalarySum.ToString("0.00")}");
            stringBuilder.AppendLine();
        }

        return stringBuilder.ToString();
    }
}
