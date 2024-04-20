using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PustokMVC.Data;
using PustokMVC.Models;
using PustokMVC.ViewModels;

namespace PustokMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly PustokDbContext _context;

        public AccountController(UserManager<AppUser> userManager, PustokDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registeration(UserRegisterViewModel model)
        {
            if (!ModelState.IsValid) return View();
            AppUser member = new AppUser()
            {
                Fullname = model.Fullname,
                UserName = model.Username,
                Email = model.Email,
            };
            if(_context.Users.Any(x=>x.NormalizedUserName == model.Username.ToUpper()))
            {
                ModelState.AddModelError("UserName", "Username is already exist!");
                return View();
            }
            if (_context.Users.Any(x => x.NormalizedEmail == model.Email.ToUpper()))
            {
                ModelState.AddModelError("Email", "Email is already exist!");
                return View();
            }

            var result = await _userManager.CreateAsync(member,model.Password);

            if(!result.Succeeded) 
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                    return View();
                }
            }

            var Result = await _userManager.AddToRoleAsync(member, "Member");

            return RedirectToAction("login");
        }


    }
}
