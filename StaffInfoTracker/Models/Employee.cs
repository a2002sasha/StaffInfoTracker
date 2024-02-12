namespace StaffInfoTracker.Models;

public class Employee
{
    public int EmployeeId { get; set; }
    public int PositionId { get; set; }
    public int DepartmentId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string MiddleName { get; set; } = null!;
    public int AddressId { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public DateTime HireDate { get; set; }
    public decimal Salary { get; set; }
    public Address Address { get; set; } = null!;
    public Position Position { get; set; } = null!;
    public Department Department { get; set; } = null!;
}
