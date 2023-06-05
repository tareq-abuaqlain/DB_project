using ProjectDB.Controllers;
using ProjectDB.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ProjectDBContextConnection") ?? throw new InvalidOperationException("Connection string 'ProjectDBContextConnection' not found.");



// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped<IManageRegestrationService, ManageRegestrationService>();
builder.Services.AddScoped<IManageDerpartmentService, ManageDerpartmentService>();
builder.Services.AddScoped<IManageInstructorService, ManageInstructorService>();
builder.Services.AddScoped<IManageUserServices, ManageUserServices>();
builder.Services.AddScoped<IManageRolesService, ManageRolesService>();
builder.Services.AddScoped<IManageUserRoleService, ManageUserRoleService>();
builder.Services.AddScoped<IManageCourseService, ManageCourseService>();
builder.Services.AddScoped<IManageCourseTeachService, ManageCourseTeachService>();
builder.Services.AddScoped<IManageDivService, ManageDivService>();
builder.Services.AddScoped<IManageStudentsService, ManageStudentsService>();
builder.Services.AddScoped<IManageLecturesService, ManageLecturesService>();
builder.Services.AddScoped<IManageAttendService, ManageAttendService>();



builder.Services.AddRazorPages();
builder.Services.AddAuthentication().AddCookie("Identity.External");
builder.Services.AddMvc();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.UseSession();

app.Run();
