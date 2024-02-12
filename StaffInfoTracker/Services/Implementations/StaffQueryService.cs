using Dapper;
using Microsoft.Data.SqlClient;
using StaffInfoTracker.Models;
using StaffInfoTracker.Services.Abstractions;

namespace StaffInfoTracker.Services.Implementations;

public class StaffQueryService(IConfiguration configuration) : IStaffQueryService
{
    private readonly string? _connectionString = configuration.GetConnectionString("DefaultConnection");

    public async Task<List<Employee>> GetAllEmployeesAsync()
    {
        await using (var connection = new SqlConnection(_connectionString))
        {
            var query = @"SELECT e.*, p.*, a.*, d.*
							  FROM Employees AS e
							  JOIN Positions AS p
							  ON e.PositionId = p.PositionId
							  JOIN Addresses AS a
							  ON e.AddressId = a.AddressId
							  JOIN Departments AS d
							  ON e.DepartmentId = d.DepartmentId;";

            var employees = await connection.QueryAsync<Employee, Position, Address, Department, Employee>(
                query,
                (employee, position, address, department) =>
                {
                    employee.Position = position;
                    employee.Address = address;
                    employee.Department = department;
                    return employee;
                },
                splitOn: "PositionId, AddressId, DepartmentId"
            );

            return employees.ToList();
        }
    }

    public async Task<Employee?> GetEmployeeByIdAsync(int id)
    {
        await using (var connection = new SqlConnection(_connectionString))
        {
            var query = $@"SELECT e.*, p.*, a.*, d.*
							  FROM Employees AS e
							  JOIN Positions AS p
							  ON e.PositionId = p.PositionId
							  JOIN Addresses AS a
							  ON e.AddressId = a.AddressId
							  JOIN Departments AS d
							  ON e.DepartmentId = d.DepartmentId
							  WHERE e.EmployeeId = {id};";

            var employee = await connection.QueryAsync<Employee, Position, Address, Department, Employee>(
                query,
                (employee, position, address, department) =>
                {
                    employee.Position = position;
                    employee.Address = address;
                    employee.Department = department;
                    return employee;
                },
                splitOn: "PositionId, AddressId, DepartmentId"
            );

            return employee.FirstOrDefault();
        }
    }

    public async Task<List<Department>> GetDepartmentsAsync()
    {
        await using (var connection = new SqlConnection(_connectionString))
        {
            var departments = await connection.QueryAsync<Department>("SELECT * FROM Departments");

            return departments.ToList();
        }
    }

    public async Task<List<Position>> GetPositionsAsync()
    {
        await using (var connection = new SqlConnection(_connectionString))
        {
            var positions = await connection.QueryAsync<Position>("SELECT * FROM Positions");

            return positions.ToList();
        }
    }

    public async Task EditEmployeeInfoAsync(Employee updatedEmployee)
    {
        await using (var connection = new SqlConnection(_connectionString))
        {
            var updateAddressQuery = @"UPDATE Addresses
                                    SET Country = @Country,
                                        City = @City,
                                        Street = @Street,
                                        HouseNumber = @HouseNumber,
                                        ApartmentNumber = @ApartmentNumber,
                                        PostIndex = @PostIndex
                                    WHERE AddressId = @AddressId";

            var updateEmployeeQuery = @"UPDATE Employees
                                     SET PositionId = @PositionId,
                                         DepartmentId = @DepartmentId,
                                         FirstName = @FirstName,
                                         LastName = @LastName,
                                         MiddleName = @MiddleName,
                                         PhoneNumber = @PhoneNumber,
                                         BirthDate = @BirthDate,
                                         HireDate = @HireDate,
                                         Salary = @Salary
                                     WHERE EmployeeId = @EmployeeId";

            await connection.ExecuteAsync(updateAddressQuery, updatedEmployee.Address);

            await connection.ExecuteAsync(updateEmployeeQuery, updatedEmployee);
        }
    }

    public async Task<bool> IsEmployeeByPhoneNumberExistAsync(int employeeId, string phoneNumber)
    {
        await using (var connection = new SqlConnection(_connectionString))
        {
            var isExist = await connection.QueryFirstOrDefaultAsync<Employee>($@"SELECT * FROM Employees 
                                                                                WHERE PhoneNumber = '{phoneNumber}' 
                                                                                AND EmployeeId != {employeeId}");
            return isExist != null;
        }
    }
}