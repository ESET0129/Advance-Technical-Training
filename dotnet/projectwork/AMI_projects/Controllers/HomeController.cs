using System.Diagnostics;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using AMI_projects.Models;
using AMI_projects.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;


namespace AMI_projects.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // ... (keep the constructor, Privacy, and Error actions) ...

        public IActionResult Index()
        {
            // We just pass the token for the global search bar
            ViewBag.ApiToken = User.FindFirstValue("AuthToken");
            return View();
        }
    }
}