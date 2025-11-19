using Microsoft.AspNetCore.Mvc;

namespace AMI_View.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult ConsumerLogin()
        {
            return View();
        }

    }
}
