using Microsoft.AspNetCore.Mvc.Rendering;

namespace StaffInfoTracker.Utilities.SelectListFactory;

public abstract class SelectListCreator()
{
    public abstract SelectList Create();
}
