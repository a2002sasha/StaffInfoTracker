using Microsoft.AspNetCore.Mvc.Rendering;

namespace StaffInfoTracker.ViewModels;

public class SelectListViewModel()
{
    public SelectList Positions { get; set; } = null!;
    public SelectList Departments { get; set; } = null!;
}