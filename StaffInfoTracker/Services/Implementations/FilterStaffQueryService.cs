using Dapper;
using Microsoft.Data.SqlClient;
using System.Text;
using StaffInfoTracker.Models;
using StaffInfoTracker.Services.Abstractions;

namespace StaffInfoTracker.Services.Implementations;

public class FilterStaffQueryService(IConfiguration configuration, IStaffQueryService appCrudService) : IFilterStaffQueryService
{
    private readonly string? _connectionString = configuration.GetConnectionString("DefaultConnection");

    public async Task<List<Employee>> GetFilteredEmployeesAsync(int positionId, int departmentId, string fullName)
    {
        if (positionId == 0 && departmentId == 0 && string.IsNullOrEmpty(fullName))
            return await appCrudService.GetAllEmployeesAsync();

        var query = BuildFilterQuery(positionId, departmentId, fullName);

        await using (var connection = new SqlConnection(_connectionString))
        {
            var filteredEmployees = await connection.QueryAsync<Employee, Position, Address, Department, Employee>(query,
                (employee, position, address, department) =>
                {
                    employee.Position = position;
                    employee.Address = address;
                    employee.Department = department;
                    return employee;
                },
                splitOn: "PositionId, AddressId, DepartmentId");

            return filteredEmployees.ToList();
        }
    }

    private string BuildFilterQuery(int positionId, int departmentId, string fullName)
    {
        var queryBuilder = new StringBuilder();

        queryBuilder.Append(@"SELECT e.*, p.*, a.*, d.*
                          FROM Employees AS e
                          JOIN Positions AS p 
                          ON e.PositionId = p.PositionId
                          JOIN Addresses AS a
                          ON e.AddressId = a.AddressId
                          JOIN Departments AS d 
                          ON e.DepartmentId = d.DepartmentId");

        var conditions = new List<string>();

        if (positionId != 0)
        {
            conditions.Add($"p.PositionId = {positionId}");
        }

        if (departmentId != 0)
        {
            conditions.Add($"d.DepartmentId = {departmentId}");
        }

        if (!string.IsNullOrEmpty(fullName))
        {
            var parsedFullName = ParseFullName(fullName);
            conditions.AddRange(parsedFullName);
        }

        var whereClause = string.Join(" AND ", conditions);;

        if (!string.IsNullOrEmpty(whereClause))
        {
            queryBuilder.Append(" WHERE ");
            queryBuilder.Append(whereClause);
        }

        return queryBuilder.ToString();
    }

    private List<string> ParseFullName(string fullName)
    {
        var nameParts = fullName?.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();

        var conditions = new List<string>();

        var properties = new string[] { "e.LastName", "e.FirstName", "e.MiddleName" };

        for (int i = 0; i < Math.Min(nameParts.Length, properties.Length); i++)
        {
            if (!string.IsNullOrEmpty(nameParts[i]))
            {
                conditions.Add($"{properties[i]} = '{nameParts[i]}'");
            }
        }

        return conditions;
    }
}
