namespace StaffInfoTracker.ViewModels;

public class QueryStringViewModel(HttpContext context)
{
    public string? Position { get; set; } = context.Request.Query["position"];

    public string? Department { get; set; } = context.Request.Query["department"];

    public string? FullName { get; set; } = context.Request.Query["fullName"];
}
