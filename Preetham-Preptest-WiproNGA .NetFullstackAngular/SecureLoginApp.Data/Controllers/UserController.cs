using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RoleBasedAuthApp.Controllers
{
    [Authorize(Roles = "User")]
    public class UserController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }
    }
}
