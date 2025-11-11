using AMI_projects.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace AMI_projects.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: /Auth/Login
        [HttpGet]
        public IActionResult Login()
        {
            // We'll pass both models to the view
            var viewModel = new LoginMasterViewModel
            {
                AdminLogin = new LoginViewModel(),
                ConsumerLogin = new ConsumerLoginViewModel()
            };
            return View(viewModel);
        }

        // POST: /Auth/AdminLogin
        [HttpPost]
        public async Task<IActionResult> AdminLogin(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // If admin login fails, return to the main view with both models
                var masterModel = new LoginMasterViewModel { AdminLogin = model, ConsumerLogin = new ConsumerLoginViewModel() };
                ViewBag.ActiveTab = "admin"; // Keep the admin tab active
                return View("Login", masterModel);
            }

            var httpClient = _httpClientFactory.CreateClient("ApiClient");

            var response = await httpClient.PostAsJsonAsync("Auth/login", model);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                var masterModel = new LoginMasterViewModel { AdminLogin = model, ConsumerLogin = new ConsumerLoginViewModel() };
                ViewBag.ActiveTab = "admin";
                return View("Login", masterModel);
            }

            var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
            if (authResponse == null || string.IsNullOrEmpty(authResponse.Token))
            {
                ModelState.AddModelError(string.Empty, "Failed to get auth token from API.");
                var masterModel = new LoginMasterViewModel { AdminLogin = model, ConsumerLogin = new ConsumerLoginViewModel() };
                ViewBag.ActiveTab = "admin";
                return View("Login", masterModel);
            }

            // Sign in the user with the token
            await SignInUser(authResponse.Token);

            return RedirectToAction("Index", "Home");
        }

        // --- ADD THIS NEW ACTION ---
        // POST: /Auth/ConsumerLogin
        [HttpPost]
        public async Task<IActionResult> ConsumerLogin(ConsumerLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // If consumer login fails, return to the main view with both models
                var masterModel = new LoginMasterViewModel { AdminLogin = new LoginViewModel(), ConsumerLogin = model };
                ViewBag.ActiveTab = "consumer"; // Keep the consumer tab active
                return View("Login", masterModel);
            }

            var httpClient = _httpClientFactory.CreateClient("ApiClient");

            // Call our new API endpoint
            var response = await httpClient.PostAsJsonAsync("Auth/consumer-login", model);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Invalid Consumer ID or Name.");
                var masterModel = new LoginMasterViewModel { AdminLogin = new LoginViewModel(), ConsumerLogin = model };
                ViewBag.ActiveTab = "consumer";
                return View("Login", masterModel);
            }

            var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
            if (authResponse == null || string.IsNullOrEmpty(authResponse.Token))
            {
                ModelState.AddModelError(string.Empty, "Failed to get auth token from API.");
                var masterModel = new LoginMasterViewModel { AdminLogin = new LoginViewModel(), ConsumerLogin = model };
                ViewBag.ActiveTab = "consumer";
                return View("Login", masterModel);
            }

            // Sign in the user with the token
            await SignInUser(authResponse.Token);

            return RedirectToAction("Index", "Home"); // Both logins go to Home/Index
        }

        // --- ADD THIS HELPER METHOD ---
        private async Task SignInUser(string token)
        {
            // Read the token to get the claims
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Create the claims principal
            var claims = new List<Claim>
            {
                // This claim holds the token for API calls
                new Claim("AuthToken", token)
            };

            // Add all claims from the JWT (like Name, Role, Sub)
            claims.AddRange(jwtToken.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
            };

            // Sign the user in (creates the cookie)
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        // GET: /Auth/Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // Clear the login cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }

        //// GET: /Auth/Login
        //[HttpGet]
        //public IActionResult Login()
        //{
        //    // This view will be our login form
        //    return View();
        //}

        //// POST: /Auth/Login
        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    var httpClient = _httpClientFactory.CreateClient("ApiClient");

        //    // 1. Call the backend API
        //    var response = await httpClient.PostAsJsonAsync("Auth/login", model);

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        // API returned an error (e.g., 401 Unauthorized)
        //        ModelState.AddModelError(string.Empty, "Invalid username or password.");
        //        return View(model);
        //    }

        //    // 2. Read and deserialize the API's response
        //    var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        //    if (authResponse == null || string.IsNullOrEmpty(authResponse.Token))
        //    {
        //        ModelState.AddModelError(string.Empty, "Failed to get auth token from API.");
        //        return View(model);
        //    }

        //    // 3. Create the user's "identity" (claims)
        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, authResponse.Username),
        //        new Claim(ClaimTypes.Email, authResponse.Email),
        //        // We store the token to use it for future API calls
        //        new Claim("AuthToken", authResponse.Token)
        //    };

        //    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        //    var authProperties = new AuthenticationProperties
        //    {
        //        IsPersistent = true, // Remember the user
        //        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
        //    };

        //    // 4. Sign the user in (creates the cookie)
        //    await HttpContext.SignInAsync(
        //        CookieAuthenticationDefaults.AuthenticationScheme,
        //        new ClaimsPrincipal(claimsIdentity),
        //        authProperties);

        //    // 5. Redirect to the Home page
        //    return RedirectToAction("Index", "Home");
        //}

        //// GET: /Auth/Logout
        //[HttpGet]
        //public async Task<IActionResult> Logout()
        //{
        //    // Clear the login cookie
        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    return RedirectToAction("Login", "Auth");
        //}
        ////public IActionResult Index()
        ////{
        ////    return View();
        ////}
    }
}
