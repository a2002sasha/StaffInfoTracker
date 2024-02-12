using Mapster;
using Microsoft.AspNetCore.Mvc;
using StaffInfoTracker.Models;
using StaffInfoTracker.Utilities.SelectListFactory;
using StaffInfoTracker.Services.Abstractions;
using StaffInfoTracker.ViewModels;

namespace StaffInfoTracker.Controllers;

public class StaffController(IStaffQueryService staffQueryService, IFilterStaffQueryService filterStaffQueryService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(int position, int department, string fullName)
    {
        List<Employee> employees;

        if (position == 0 && department == 0 && fullName == null)
            employees = await staffQueryService.GetAllEmployeesAsync();
        else
            employees = await filterStaffQueryService.GetFilteredEmployeesAsync(position, department, fullName);

        var employeeViewModel = employees.Adapt<List<EmployeeViewModel>>();

        var queryStringViewModel = new QueryStringViewModel(HttpContext);

        var selectListViewModel = await GetSelectListViewModel(Convert.ToInt32(queryStringViewModel.Position),
                                                               Convert.ToInt32(queryStringViewModel.Department), true);

        var staffInfoViewModel = new StaffInfoViewModel()
        {
            Employees = employeeViewModel,
            QueryString = queryStringViewModel,
            SelectList = selectListViewModel
        };

        return View(staffInfoViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> FilterEmployees(int position, int department, string fullName)
    {
        var queryStringViewModel = new QueryStringViewModel(HttpContext);

        if (position != 0)
            queryStringViewModel.Position = position.ToString();

        if (department != 0)
            queryStringViewModel.Department = department.ToString();

        if (!String.IsNullOrEmpty(fullName))
            queryStringViewModel.FullName = fullName;

        var filteredEmployees = await filterStaffQueryService.GetFilteredEmployeesAsync(position, department, fullName);

        var employeeViewModel = filteredEmployees.Adapt<List<EmployeeViewModel>>();

        var selectListViewModel = await GetSelectListViewModel(position, department, true);

        var staffInfoViewModel = new StaffInfoViewModel()
        {
            Employees = employeeViewModel,
            QueryString = queryStringViewModel,
            SelectList = selectListViewModel
        };

        return View("index", staffInfoViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> EditEmployee(int id)
    {
        var employee = await staffQueryService.GetEmployeeByIdAsync(id);

        if (employee == null)
            return NotFound();

        var employeeViewModel = employee.Adapt<EmployeeViewModel>();

        employeeViewModel.SelectedPositionId = employee.PositionId;
        employeeViewModel.SelectedDepartmentId = employee.DepartmentId;

        employeeViewModel.QueryString = new QueryStringViewModel(HttpContext);

        employeeViewModel.SelectList = await GetSelectListViewModel(employeeViewModel.SelectedPositionId,
                                                                    employeeViewModel.SelectedDepartmentId, false);

        return View(employeeViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> EditEmployee(EmployeeViewModel model)
    {
        model.QueryString = new QueryStringViewModel(HttpContext);

        var isExist = await staffQueryService.IsEmployeeByPhoneNumberExistAsync(model.EmployeeId, model.PhoneNumber);

        if (isExist)
            ModelState.AddModelError(nameof(EmployeeViewModel.PhoneNumber), "Співробітник з таким номером телефону вже існує");

        if (ModelState.IsValid)
        {
            var updatedEmployee = model.Adapt<Employee>();

            updatedEmployee.PositionId = model.SelectedPositionId;
            updatedEmployee.DepartmentId = model.SelectedDepartmentId;

            await staffQueryService.EditEmployeeInfoAsync(updatedEmployee);

            return RedirectToAction("index", new
            {
                position = model.QueryString.Position,
                department = model.QueryString.Department,
                fullName = model.QueryString.FullName
            });
        }

        model.SelectList = await GetSelectListViewModel(model.SelectedPositionId, model.SelectedDepartmentId, false);

        return View(model);
    }

    private async Task<SelectListViewModel> GetSelectListViewModel(int selectedPositionId, int selectedDepartmentId, bool isForFiltering)
    {
        var positions = await staffQueryService.GetPositionsAsync();
        var departments = await staffQueryService.GetDepartmentsAsync();

        SelectListCreator positionsSelectListCreator = new PositionsSelectListCreator(positions, selectedPositionId, isForFiltering);
        SelectListCreator departmentsSelectListCreator = new DepartmentsSelectListCreator(departments, selectedDepartmentId, isForFiltering);

        return new SelectListViewModel
        {
            Positions = positionsSelectListCreator.Create(),
            Departments = departmentsSelectListCreator.Create()
        };
    }
}