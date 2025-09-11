using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RoleBasedAuthApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
