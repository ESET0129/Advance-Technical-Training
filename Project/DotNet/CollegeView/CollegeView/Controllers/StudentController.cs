using Microsoft.AspNetCore.Mvc;

namespace CollegeView.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult StudentView()
        {
            return View();
        }
    }
}
