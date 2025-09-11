using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecureLoginMVC.Models.ViewModels;

namespace SecureLoginMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register() => View(new RegisterViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            if (!await _roleManager.RoleExistsAsync(model.Role))
            {
                ModelState.AddModelError("", "Selected role does not exist.");
                return View(model);
            }

            var user = new IdentityUser { UserName = model.Username };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.Role);
                TempData["Success"] = $"User \"{model.Username}\" registered as {model.Role}. Please login.";
                return RedirectToAction(nameof(Login));
            }
            foreach (var e in result.Errors) ModelState.AddModelError("", e.Description);
            return View(model);
        }

        [HttpGet]
        public IActionResult Login() => View(new LoginViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null) { ModelState.AddModelError("", "Invalid login attempt."); return View(model); }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded) return RedirectToAction("Index", "Product");

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult AccessDenied() => View();
    }
}
