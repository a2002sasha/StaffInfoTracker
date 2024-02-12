using StaffInfoTracker.Models;

namespace StaffInfoTracker.Services.Abstractions;

public interface ICompanyQueryService
{
    Task<CompanyDetails?> GetCompanyDetailsAsync();
}
