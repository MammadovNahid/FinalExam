using Microsoft.AspNetCore.Mvc;

namespace FinalExam_Bilet7.Areas.Admin.Controllers
{
	public class DashboardController : Controller
	{
		[Area("Admin")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
