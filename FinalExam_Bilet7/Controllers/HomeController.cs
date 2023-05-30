using FinalExam_Bilet7.DAL;
using FinalExam_Bilet7.Models;
using FinalExam_Bilet7.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FinalExam_Bilet7.Controllers
{
	public class HomeController : Controller
	{
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
		{
            var team = await _context.Employees.ToListAsync();
            HomeVM homevm = new HomeVM()
            {
                Teams = team
            };
			return View(homevm);
		}

	}
}