using AMI_projects.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace AMI_projects.Controllers
{
    [Authorize]   
    public class BillingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BillingController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient GetAuthorizedClient()
        {
            var token = User.FindFirstValue("AuthToken");
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            return httpClient;
        }

        // GET: /Billing/Index
        public IActionResult Index()
        {
            ViewBag.ApiToken = User.FindFirstValue("AuthToken");
            return View();
        }

        // POST: /Billing/MarkAsPaid/5
        [HttpPost]
        public async Task<IActionResult> MarkAsPaid(long id)
        {
            var httpClient = GetAuthorizedClient();
            var payload = new { status = "Paid" };

            var response = await httpClient.PutAsJsonAsync($"bills/{id}/status", payload);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
            return Ok();
        }

        // POST: /Billing/GenerateBills
        [HttpPost]
        public async Task<IActionResult> GenerateBills([FromBody] GenerateBillsRequestDto model)
        {
            var httpClient = GetAuthorizedClient();
            var response = await httpClient.PostAsJsonAsync("bills/generate", model);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
            var result = await response.Content.ReadFromJsonAsync<GenerateBillsResponseDto>();
            return Ok(result);
        }
    }
}
