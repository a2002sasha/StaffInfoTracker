namespace StaffInfoTracker.ViewModels;

public class SalaryReportViewModel
{
    public string Name { get; set; } = null!;

    public int EmployeeQty { get; set; }

    public decimal MinSalary { get; set; }

    public decimal MaxSalary { get; set; }

    public decimal AvgSalary { get; set; }

    public decimal TotalSalarySum { get; set; }
}
