using Dapper;
using Microsoft.Data.SqlClient;
using StaffInfoTracker.Models;
using StaffInfoTracker.Services.Abstractions;

namespace StaffInfoTracker.Services.Implementations;

public class CompanyQueryService(IConfiguration configuration) : ICompanyQueryService
{
    private readonly string? _connectionString = configuration.GetConnectionString("DefaultConnection");

    public async Task<CompanyDetails?> GetCompanyDetailsAsync()
    {
        await using (var connection = new SqlConnection(_connectionString))
        {
            var query = @"SELECT cd.*, a.*
	                        FROM CompanyDetails AS cd
	                        JOIN [Addresses] AS a
	                        ON cd.AddressId = a.AddressId;";

            var companyDetails = await connection.QueryAsync<CompanyDetails, Address, CompanyDetails>(
                query,
                (companyInfo, address) =>
                {
                    companyInfo.Address = address;
                    return companyInfo;
                },
                splitOn: "AddressId");

            return companyDetails.FirstOrDefault();
        }
    }
}
