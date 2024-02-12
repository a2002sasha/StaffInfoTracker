namespace StaffInfoTracker.Models;

public class Address
{
    public int AddressId { get; set; }
    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string HouseNumber { get; set; } = null!;
    public short? ApartmentNumber { get; set; }
    public string? PostIndex { get; set; }
    public CompanyDetails Company { get; set; } = null!;
    public Employee Employee { get; set; } = null!;
}
