using AMI_projects.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace AMI_projects.Controllers
{
    [Authorize]
    public class TariffSlabsController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _jsonOptions;

        public TariffSlabsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        private HttpClient GetAuthorizedClient()
        {
            var token = User.FindFirstValue("AuthToken");
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            return httpClient;
        }
        // --- Helper: Reusable method to get data
        //private async Task<T> GetApiData<T>(string apiUrl) where T : class
        //{
        //    var httpClient = GetAuthorizedClient();
        //    var response = await httpClient.GetAsync(apiUrl);

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        return null;
        //    }

        //    var jsonString = await response.Content.ReadAsStringAsync();
        //    return JsonSerializer.Deserialize<T>(jsonString, _jsonOptions);
        //}

        // 1. This action loads the main page
        // GET: /TariffSlabs/Index
        // GET: /TariffSlabs/Index
        public IActionResult Index()
        {
            // Pass the token to the view so its JavaScript can call the API
            ViewBag.ApiToken = User.FindFirstValue("AuthToken");
            return View();
        }

        // 2. This action will be called by our JavaScript
        // GET: /TariffSlabs/GetSlabsForTariff?tariffId=1
        [HttpGet]
        public async Task<IActionResult> GetSlabsForTariff(int tariffId)
        {
            if (tariffId <= 0)
            {
                return Json(new List<TariffSlabViewModel>()); // Return empty list
            }

            var slabs = await GetApiData<List<TariffSlabViewModel>>($"tariffslabs/by-tariff/{tariffId}");
            return Json(slabs); // Return the data as JSON
        }

        // POST: /TariffSlabs/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TariffSlabFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var httpClient = GetAuthorizedClient();
            var response = await httpClient.PostAsJsonAsync("tariffslabs", model);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }

            var newSlab = await response.Content.ReadFromJsonAsync<TariffSlabViewModel>();
            return Ok(newSlab);
        }

        // --- ADD THIS ACTION ---
        // DELETE: /TariffSlabs/Delete/1
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var httpClient = GetAuthorizedClient();
            var response = await httpClient.DeleteAsync($"tariffslabs/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }

            return Ok(); // 200 OK on success
        }
    

        // --- Reusable Helper Method ---
        private async Task<T> GetApiData<T>(string apiUrl) where T : class
        {
            var token = User.FindFirstValue("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(jsonString, _jsonOptions);
        }
    }
}
