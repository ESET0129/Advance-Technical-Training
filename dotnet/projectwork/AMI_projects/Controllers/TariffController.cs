using AMI_projects.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace AMI_projects.Controllers
{
    [Authorize]
    public class TariffController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _jsonOptions;

        private HttpClient GetAuthorizedClient()
        {
            var token = User.FindFirstValue("AuthToken");
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            return httpClient;
        }
        public TariffController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        //// GET: /Tariff/Index
        // GET: /Tariff/Index
        public IActionResult Index()
        {  // <-- Use a curly brace
           // Pass the token to the view for its JavaScript
            ViewBag.ApiToken = User.FindFirstValue("AuthToken");
            return View();
        }  // <-- Use a curly brace
        //public async Task<IActionResult> Index()
        //{
        //    var token = User.FindFirstValue("AuthToken");
        //    if (string.IsNullOrEmpty(token))
        //    {
        //        return RedirectToAction("Login", "Auth");
        //    }

        //    var httpClient = _httpClientFactory.CreateClient("ApiClient");
        //    httpClient.DefaultRequestHeaders.Authorization =
        //        new AuthenticationHeaderValue("Bearer", token);

        //    var response = await httpClient.GetAsync("tariffs");

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        //        {
        //            return RedirectToAction("Logout", "Auth");
        //        }
        //        ViewData["ErrorMessage"] = $"Error from API: {response.StatusCode}";
        //        return View(new List<TariffViewModel>());
        //    }

        //    var jsonString = await response.Content.ReadAsStringAsync();
        //    var tariffs = JsonSerializer.Deserialize<List<TariffViewModel>>(jsonString, _jsonOptions);

        //    return View(tariffs);
        //}
        // POST: /Tariff/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TariffFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var httpClient = GetAuthorizedClient();
            var response = await httpClient.PostAsJsonAsync("tariffs", model);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }

            var newTariff = await response.Content.ReadFromJsonAsync<TariffViewModel>();
            return Ok(newTariff);
        }

        // --- ADD THIS ACTION ---
        // DELETE: /Tariff/Delete/1
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var httpClient = GetAuthorizedClient();
            var response = await httpClient.DeleteAsync($"tariffs/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }

            return Ok(); // 200 OK on success
        }

    }
}
