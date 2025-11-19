using Microsoft.AspNetCore.Mvc;

namespace AMI_View.Controllers
{
    public class ConsumerController : Controller
    {
        

        // Consumer Dashboard (after login)
        public IActionResult Dashboard()
        {
            return View();
        }

        // Page to show all meters
        public IActionResult Meters()
        {
            return View();
        }

        // Page to show all bills
        public IActionResult Bills()
        {
            return View();
        }
        
        public IActionResult Tariffs()
        {
            return View();
        }
        public IActionResult TariffSlabs()
        {
            return View();
        }
    }
}
