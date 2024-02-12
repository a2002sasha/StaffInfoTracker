using StaffInfoTracker.Services.Abstractions;
using StaffInfoTracker.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICompanyQueryService, CompanyQueryService>();
builder.Services.AddScoped<IStaffQueryService, StaffQueryService>();
builder.Services.AddScoped<IFilterStaffQueryService, FilterStaffQueryService>();
builder.Services.AddScoped<ISalaryReportQueryService, SalaryReportQueryService>();

builder.Services.AddMvc();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapDefaultControllerRoute();

app.Run();