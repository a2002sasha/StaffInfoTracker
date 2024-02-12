namespace StaffInfoTracker.ViewModels;

public class StaffInfoViewModel
{
	public List<EmployeeViewModel>? Employees { get; set; }
	public QueryStringViewModel? QueryString { get; set; }
	public SelectListViewModel SelectList { get; set; } = null!;
}
