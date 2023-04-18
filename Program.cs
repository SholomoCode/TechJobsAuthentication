using System;
using Microsoft.EntityFrameworkCore;
using TechJobsAuthentication.Data;
using Microsoft.AspNetCore.Identity;
//using TechJobsAuthentication.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
/*var connectionString = builder.Configuration.GetConnectionString("JobDbContextConnection");builder.Services.AddDbContext<JobDbContext>(options =>
    options.UseSqlServer(connectionString));builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<JobDbContext>();
// Add services to the container.*/
builder.Services.AddControllersWithViews();

//--- MySql connection

//pomelo connection syntax: https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql
// working with the .NET 6 (specifically the lack of a Startup.cs)
//https://learn.microsoft.com/en-us/aspnet/core/migration/50-to-60-samples?view=aspnetcore-6.0#add-configuration-providers

var connectionString = "server=localhost;user=techjobs_auth;password=techjobs_auth;database=techjobs_auth";
var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));

builder.Services.AddDbContext<JobDbContext>(dbContextOptions => dbContextOptions.UseMySql(connectionString, serverVersion));
//--- end of connection syntax

builder.Services.AddRazorPages();

builder.Services.AddDefaultIdentity<IdentityUser>
(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<JobDbContext>();





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
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

