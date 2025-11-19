using AMI_projects.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace AMI_projects.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _jsonOptions;

        public UserController(IHttpClientFactory httpClientFactory)
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

        // GET: /User/Index (This action should already exist)
        // GET: /User/Index
        public IActionResult Index()
        {
            // Pass the token to the view for its JavaScript
            ViewBag.ApiToken = User.FindFirstValue("AuthToken");
            return View();
        }

        // --- ADD THESE ACTIONS ---

        // GET: /User/GetUserData/1
        [HttpGet]
        public async Task<IActionResult> GetUserData(int id)
        {
            var httpClient = GetAuthorizedClient();
            var response = await httpClient.GetAsync($"users/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }

            var user = await response.Content.ReadFromJsonAsync<UserViewModel>();
            return Ok(user); // Send the UserViewModel (it's safe, no hash)
        }

        // POST: /User/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var httpClient = GetAuthorizedClient();
            var response = await httpClient.PostAsJsonAsync("users", model);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }

            var newUser = await response.Content.ReadFromJsonAsync<UserViewModel>();
            return Ok(newUser);
        }

        // PUT: /User/Update/1
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var httpClient = GetAuthorizedClient();
            var response = await httpClient.PutAsJsonAsync($"users/{id}", model);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }

            var updatedUser = await response.Content.ReadFromJsonAsync<UserViewModel>();
            return Ok(updatedUser);
        }

        // DELETE: /User/Delete/1
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var httpClient = GetAuthorizedClient();
            var response = await httpClient.DeleteAsync($"users/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }

            return Ok(); // 200 OK on success
        }
    }
}