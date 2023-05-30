using FinalExam_Bilet7.DAL;
using FinalExam_Bilet7.Models;
using FinalExam_Bilet7.Utilities.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalExam_Bilet7.Areas.Admin.Controllers
{
	[Area("Admin")]
	//[Authorize]
	public class TeamController : Controller
	{
		private readonly AppDbContext _context;
		private readonly IWebHostEnvironment _env;

		public TeamController(AppDbContext context,IWebHostEnvironment env)
        {
			_context = context;
			_env = env;
		}
		public async Task<IActionResult> Index()
		{
			var employee = await _context.Employees.ToListAsync();
			return View(employee);
		}
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(OurTeam employee)
		{
			if(!ModelState.IsValid)
			{
				return View();
			}
			if (employee.Photo == null)
			{
				ModelState.AddModelError("Photo", "Shekil yerlesdirilmedi...");
				return View();
			}
			if (!employee.Photo.CheckFileType("image/"))
			{
				ModelState.AddModelError("Photo", "Shekil type uygun deyil...");
				return View();
			}
			if (!employee.Photo.CheckFileSize(200))
			{
				ModelState.AddModelError("Photo", "Shekil olcusu 200-den chox ola bilmez");
				return View();
			}
			employee.Image = await employee.Photo.CreateFileAsync(_env.WebRootPath, "assets/img");
			await _context.AddAsync(employee);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Delete(int? id)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			if(id== null || id < 1)
			{
				return BadRequest();
			}
			var team = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
			if(team == null)
			{
				return NotFound();
			}
			_context.Employees.Remove(team);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Update(int? id)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			if (id == null || id < 1)
			{
				return BadRequest();
			}
			var team = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
			if (team == null)
			{
				return NotFound();
			}
			return View(team);
		}

		[HttpPost]
		public async Task<IActionResult> Update(int? id,OurTeam employee)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			if (id == null || id < 1)
			{
				return BadRequest();
			}
			var existed = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
			if (existed == null)
			{
				return NotFound();
			}

			if (employee.Photo != null)
			{
				if (!employee.Photo.CheckFileType("image/"))
				{
					ModelState.AddModelError("Photo", "Shekil type uygun deyil...");
					return View();
				}
				if (!employee.Photo.CheckFileSize(200))
				{
					ModelState.AddModelError("Photo", "Shekil olcusu 200-den chox ola bilmez");
					return View();
				}
			}
			existed.Image.Delete(_env.WebRootPath, "assets/img");
			existed.Image = await employee.Photo.CreateFileAsync(_env.WebRootPath, "assets/img");
			existed.Description= employee.Description;
			existed.Name = employee.Name;
			existed.Surname=employee.Surname;
			existed.Profession=employee.Profession;
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

	}
}
