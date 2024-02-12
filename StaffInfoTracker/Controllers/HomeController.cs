using Mapster;
using Microsoft.AspNetCore.Mvc;
using StaffInfoTracker.Services.Abstractions;
using StaffInfoTracker.ViewModels;

namespace StaffInfoTracker.Controllers;

public class HomeController(ICompanyQueryService companyQueryService) : Controller
{
	[HttpGet]
	public async Task<IActionResult> Index()
	{
		var companyDetails = await companyQueryService.GetCompanyDetailsAsync();

		if (companyDetails == null)
			return NotFound();

		var model = companyDetails.Adapt<CompanyDetailsViewModel>();

		return View(model);
	}
}
