using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AMI_project.Web.Controllers
{
    [Authorize]
    public class UploadMeterController : Controller
    {
        // GET: /UploadMeter/Index
        [HttpGet]
        public IActionResult Index()
        {
            // Pass the token to the view for its JavaScript
            ViewBag.ApiToken = User.FindFirstValue("AuthToken");
            return View();
        }

        // The [HttpPost] Index action is no longer needed here.
        // The JavaScript will post directly to the API.
    }
}