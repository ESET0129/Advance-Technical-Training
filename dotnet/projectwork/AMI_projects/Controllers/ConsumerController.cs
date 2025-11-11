using AMI_projects.ViewModels;
using AMI_projects.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace AMI_projects.Controllers
{
    [Authorize] // This page requires login
    public class ConsumerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _jsonOptions;

        public ConsumerController(IHttpClientFactory httpClientFactory)
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

        // --- Helper: Gets the current username ---
        private string GetCurrentUsername()
        {
            return User.FindFirstValue(ClaimTypes.Name);
        }

        // GET: /Consumer/Index
        public IActionResult Index()
        {
            // We just return the empty page.
            // We pass the auth token to the view so our JavaScript can use it.
            ViewBag.ApiToken = User.FindFirstValue("AuthToken");
            return View();
        }

        // GET: /Consumer/GetConsumerData/1005
        [HttpGet]
        public async Task<IActionResult> GetConsumerData(long id)
        {
            var httpClient = GetAuthorizedClient();
            var response = await httpClient.GetAsync($"consumers/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }

            var consumer = await response.Content.ReadFromJsonAsync<ConsumerViewModel>();
            return Ok(consumer);
        }

        // POST: /Consumer/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ConsumerFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var httpClient = GetAuthorizedClient();

            // Add the 'CreatedBy' username
            var backendModel = new
            {
                model.Name,
                model.Address,
                model.Phone,
                model.Email,
                model.Status,
                CreatedBy = GetCurrentUsername()
            };

            var response = await httpClient.PostAsJsonAsync("consumers", backendModel);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }

            var newConsumer = await response.Content.ReadFromJsonAsync<ConsumerViewModel>();
            return Ok(newConsumer);
        }

        // PUT: /Consumer/Update/1005
        [HttpPut]
        public async Task<IActionResult> Update(long id, [FromBody] ConsumerFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var httpClient = GetAuthorizedClient();

            // Add the 'UpdatedBy' username
            var backendModel = new
            {
                model.Name,
                model.Address,
                model.Phone,
                model.Email,
                model.Status,
                UpdatedBy = GetCurrentUsername()
            };

            var response = await httpClient.PutAsJsonAsync($"consumers/{id}", backendModel);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }

            var updatedConsumer = await response.Content.ReadFromJsonAsync<ConsumerViewModel>();
            return Ok(updatedConsumer);
        }

        // DELETE: /Consumer/Delete/1005
        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var httpClient = GetAuthorizedClient();
            var response = await httpClient.DeleteAsync($"consumers/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }

            return Ok(); // 200 OK on success
        }
    }
}