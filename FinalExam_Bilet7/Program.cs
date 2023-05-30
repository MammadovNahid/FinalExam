using FinalExam_Bilet7.DAL;
using FinalExam_Bilet7.Models;
using FinalExam_Bilet7.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
	opt.Password.RequireNonAlphanumeric = false;
	opt.Password.RequiredUniqueChars = 1;
	opt.User.RequireUniqueEmail = true;
	opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
	opt.Lockout.MaxFailedAccessAttempts = 5;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<LayoutService>();
var app = builder.Build();


app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
	  name: "areas",
	  pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
	);
});
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
