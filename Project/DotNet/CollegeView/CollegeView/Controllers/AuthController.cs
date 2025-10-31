using Microsoft.AspNetCore.Mvc;

namespace CollegeView.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
