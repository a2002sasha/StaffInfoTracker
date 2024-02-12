using Microsoft.AspNetCore.Mvc.Rendering;

namespace StaffInfoTracker.Utilities.SelectListFactory;

public class SalaryReportSelectListCreator(string selectedOption) : SelectListCreator
{
    public override SelectList Create()
    {
        return new SelectList(new List<string> { "Відділ", "Посада" }, selectedOption);
    }
}
