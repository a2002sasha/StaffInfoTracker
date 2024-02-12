namespace StaffInfoTracker.Models;

public class Department
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = null!;
    public List<Employee> Employees { get; set; } = new List<Employee>();
}
