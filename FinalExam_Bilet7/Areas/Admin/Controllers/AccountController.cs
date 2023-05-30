using FinalExam_Bilet7.Models;
using FinalExam_Bilet7.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalExam_Bilet7.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM newuser)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = new AppUser()
            {
                Name = newuser.Name,
                Surname = newuser.Surname,
                Email = newuser.Email,
                UserName = newuser.Username
            };
            IdentityResult result = await _userManager.CreateAsync(user, newuser.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty,error.Description);
                }
                return View();
            }
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Dashboard", new { Areas = "" });
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM newuser,string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = await _userManager.FindByEmailAsync(newuser.UsernameorEmail);
            if(user == null)
            {
                user = await _userManager.FindByNameAsync(newuser.UsernameorEmail);
                if(user == null)
                {
                    ModelState.AddModelError(string.Empty, "Username, Email or password incorrect...");
                    return View();
                }
            }
            var result = await _signInManager.PasswordSignInAsync(user, newuser.Password, isPersistent: newuser.IsRemember, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Username, Email or password incorrect...");
                return View();
            }
            if(result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Blocked you please try again soon...");
                return View();
            }
            //if(ReturnUrl != null)
            //{
            //    return View(ReturnUrl);
            //}
            return RedirectToAction("Index", "Dashboard", new { Areas = "" });
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Dashboard", new { Areas = "" });
        }

    }
}
