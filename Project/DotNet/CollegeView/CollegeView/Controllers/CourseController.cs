using Microsoft.AspNetCore.Mvc;

namespace CollegeView.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult CourseView()
        {
            return View();
        }
    }
}
