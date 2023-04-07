using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using NuGet.Common;
using Ozkaya.Web.Areas.Cities.Models;
using Ozkaya.Web.Dtos.Cities;
using Ozkaya.Web.Models;
using System.Net;
using System.Net.Http.Headers;


namespace Ozkaya.Web.Areas.Cities.Controllers
{
    [Area("Cities")]
    public class CitiesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache memoryCache;
        public CitiesController(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
        {
            _httpClientFactory = httpClientFactory;
            this.memoryCache = memoryCache;
        }

        [Route("cities")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("burak");
            client.BaseAddress = new Uri("https://localhost:7144/");
            var token = this.memoryCache.Get("token");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var result = await client.GetStringAsync("api/cities/ozkaya/getall");
            var response = JsonConvert.DeserializeObject<CitiesResponse>(result);

            var model = new CityDetails()
            {
                CitiesResponse = response
            };

            return View(model);
        }

        [HttpPost]
        [Route("city/create")]
        public async Task<JsonResult> Create(CreateCityDto model)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("https://localhost:7144/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync("api/cities/ozkaya/addasync", model);
                var result = JsonConvert.DeserializeObject<CityCreateResponse>(response.Content.ReadAsStringAsync().Result);

                return Json(result);
            }

            return Json(model);
        }

        [HttpGet]
        [Route("city/edit/{id}")]
        public async Task<JsonResult> Edit(int id)
        {

            var client = _httpClientFactory.CreateClient("Özka.WEB");
            client.BaseAddress = new Uri("https://localhost:7144/");

            var uri = Path.Combine($"api/cities/ozkaya/getbyid/{id}");

            var result = await client.GetStringAsync(uri);
            var response = JsonConvert.DeserializeObject<GetCity>(result);

            return Json(response);
        }

        [HttpPut]
        [Route("city/update")]
        public async Task<PartialViewResult> Update(UpdateCityDto model)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7144/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.PutAsJsonAsync("api/cities/ozkaya/updateasync", model);
            var result = JsonConvert.DeserializeObject<CityUpdateResponse>(response.Content.ReadAsStringAsync().Result);

            return PartialView("UpdatePartial", result);
        }

        [HttpDelete]
        [Route("city/remove/{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7144/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.DeleteAsync($"api/cities/ozkaya/removeasync/{id}");
            var result = JsonConvert.DeserializeObject<CityDeleteResponse>(response.Content.ReadAsStringAsync().Result);

            return Json(result);
        }

    }
}
