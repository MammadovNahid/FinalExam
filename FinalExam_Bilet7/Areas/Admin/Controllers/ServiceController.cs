using FinalExam_Bilet7.DAL;
using FinalExam_Bilet7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;

namespace FinalExam_Bilet7.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Setting> settings = await _context.Settings.ToListAsync();
            return View(settings);
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
            var setting = await _context.Settings.FirstOrDefaultAsync(x=>x.Id==id);
            if (setting == null)
            {
                return NotFound();
            }
            return View(setting);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Setting setting)
        {
            Setting exists=await _context.Settings.FirstOrDefaultAsync(x=>x.Id==setting.Id);
            if(exists==null)
            {
                return NotFound();
            }
            if(!ModelState.IsValid)
            {
                return View();
            }
            exists.Value=setting.Value;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
