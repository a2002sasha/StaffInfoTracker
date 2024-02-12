namespace StaffInfoTracker.Models;

public class CompanyDetails
{
    public int CompanyId { get; set; }
    public string CompanyName { get; set; } = null!;
    public int AddressId { get; set; }
    public Address Address { get; set; } = null!;
}
