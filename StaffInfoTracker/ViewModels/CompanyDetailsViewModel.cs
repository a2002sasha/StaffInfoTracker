using StaffInfoTracker.Models;

namespace StaffInfoTracker.ViewModels;

public class CompanyDetailsViewModel
{
    public string CompanyName { get; set; } = null!;

    public Address Address { get; set; } = null!;
}
