using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AMI_project.Web.Controllers
{
    [Authorize]
    public class OrgUnitController : Controller
    {
        // This controller no longer needs IHttpClientFactory

        // GET: /OrgUnit/Index
        public IActionResult Index()
        {
            // Pass the token to the view for its JavaScript
            ViewBag.ApiToken = User.FindFirstValue("AuthToken");
            return View();
        }

        // All other actions (GetChildren, GetHierarchy)
        // are now removed from this controller.
    }
}