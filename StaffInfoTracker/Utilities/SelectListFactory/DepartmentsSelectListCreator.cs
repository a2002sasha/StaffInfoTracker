using Microsoft.AspNetCore.Mvc.Rendering;
using StaffInfoTracker.Models;

namespace StaffInfoTracker.Utilities.SelectListFactory;

public class DepartmentsSelectListCreator(IList<Department> departments, int selectedDepartmentId, bool isForFiltering) : SelectListCreator
{
    public override SelectList Create()
    {
        if (isForFiltering)
            departments.Insert(0, new Department() { DepartmentName = "Нічого", DepartmentId = 0 });

        return new SelectList(departments, nameof(Department.DepartmentId), nameof(Department.DepartmentName), selectedDepartmentId);
    }
}
