using Microsoft.AspNetCore.Mvc;
using System.Text;
using StaffInfoTracker.Services.Abstractions;
using StaffInfoTracker.Utilities.SelectListFactory;

namespace StaffInfoTracker.Controllers;

public class SalaryReportController(ISalaryReportQueryService salaryReportQueryService) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        SelectListCreator salaryReportSelectListCreator = new SalaryReportSelectListCreator("Відділ");

        ViewBag.FilterOptions = salaryReportSelectListCreator.Create();

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> GenerateReport(string filterOption)
    {
        SelectListCreator salaryReportSelectListCreator = new SalaryReportSelectListCreator(filterOption);

        ViewBag.FilterOptions = salaryReportSelectListCreator.Create();

        var salaryReport = filterOption == "Відділ" ? await salaryReportQueryService.GetSalaryReportForDepartmentsAsync()
            : await salaryReportQueryService.GetSalaryReportForPositionsAsync();

        return View("Index", salaryReport);
    }

    [HttpPost]
    public async Task<IActionResult> ExportToTxt(string filterOption)
    {
        var report = await salaryReportQueryService.GenerateSalaryReportTextAsync(filterOption);

        await System.IO.File.WriteAllTextAsync("Зарплатна звітність.txt", report);

        return File(Encoding.UTF8.GetBytes(report), "text/plain", "Зарплатна звітність.txt");
    }
}
