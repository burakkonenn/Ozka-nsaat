using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Ozkaya.Web.Models;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Ozkaya.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache memoryCache;
        readonly IHttpContextAccessor _httpContextAccessor;
        public AuthController(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            this.memoryCache = memoryCache;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {


            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("https://localhost:7144/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("api/auth/Authenticate", model);
                var result = JsonConvert.DeserializeObject<UserLoginResponse>(response.Content.ReadAsStringAsync().Result);
                memoryCache.Set("token", result.Token);

                if (result != null)
                {
                    if (result.Token != null)
                    {
                        var userClaims = new List<Claim>();
                        userClaims.Add(new(ClaimTypes.Name, model.UserName));
                        var userClaimIdentity = new ClaimsIdentity(userClaims, "userClaimIdentity");
                        var userPrincipal = new ClaimsPrincipal(new[] { userClaimIdentity });
                        _httpContextAccessor.HttpContext.SignInAsync(userPrincipal);
                        return RedirectToAction("Index", "Cities");
                        
                    }
                    else
                        return NoContent();

                }
                else
                {
                    return NoContent();
                }

            }
            return Ok("bad reques");
        }


        public ActionResult LogOut()
        {
            _httpContextAccessor.HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
