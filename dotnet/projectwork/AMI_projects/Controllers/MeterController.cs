using AMI_projects.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace AMI_project.Web.Controllers
{
    [Authorize]
    public class MeterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _jsonOptions;

        public MeterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        // --- Helper: Gets the token and creates an authorized HttpClient ---
        private HttpClient GetAuthorizedClient()
        {
            var token = User.FindFirstValue("AuthToken");
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            return httpClient;
        }

        // GET: /Meter/Index
        // Returns the empty page, JavaScript will fetch the data.
        public IActionResult Index()
        {
            // Pass the token to the view so its JavaScript can use it.
            ViewBag.ApiToken = User.FindFirstValue("AuthToken");
            return View();
        }

        // GET: /Meter/GetMeterData/MTR1001
        // This is called by our "Edit" button's JavaScript
        [HttpGet]
        public async Task<IActionResult> GetMeterData(string id)
        {
            var httpClient = GetAuthorizedClient();
            var response = await httpClient.GetAsync($"meters/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }

            var meter = await response.Content.ReadFromJsonAsync<MeterViewModel>();
            return Ok(meter);
        }

        // POST: /Meter/Create
        // This is called by our "Add" modal's JavaScript
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MeterFormViewModel model)
        {
            var httpClient = GetAuthorizedClient();
            var response = await httpClient.PostAsJsonAsync("meters", model);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }

            var newMeter = await response.Content.ReadFromJsonAsync<MeterViewModel>();
            return Ok(newMeter);
        }

        // PUT: /Meter/Update/MTR1001
        // This is called by our "Edit" modal's JavaScript
        [HttpPut]
        public async Task<IActionResult> Update(string id, [FromBody] MeterFormViewModel model)
        {
            var httpClient = GetAuthorizedClient();
            var response = await httpClient.PutAsJsonAsync($"meters/{id}", model);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }

            var updatedMeter = await response.Content.ReadFromJsonAsync<MeterViewModel>();
            return Ok(updatedMeter);
        }

        // DELETE: /Meter/Delete/MTR1001
        // This is called by our "Delete" button's JavaScript
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var httpClient = GetAuthorizedClient();
            var response = await httpClient.DeleteAsync($"meters/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }

            return Ok();
        }
    }
}