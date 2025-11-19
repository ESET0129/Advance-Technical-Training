using System.Diagnostics;
using AMI_View.Models;
using Microsoft.AspNetCore.Mvc;

namespace AMI_View.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Meter()

        {
            return View();
        }

        public IActionResult Consumer()
        {
            return View();
        }

        public IActionResult Orgunit()
        {
            return View();
        }
        public IActionResult Tariff()
        {
            return View();
        }

        public IActionResult TariffSlab()
        {
            return View();
        }

        public IActionResult ConsumerBill()
        {
            return View();
        }

        public IActionResult UploadBill()
        {
            return View();
        }
        public IActionResult Users()
        {
            return View();
        }
    }

}
