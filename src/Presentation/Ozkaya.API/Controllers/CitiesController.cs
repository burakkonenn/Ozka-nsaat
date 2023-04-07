using Application.Abstractions.Services.Cities;
using Application.Abstractions.Wrappers;
using Application.DTOs.Cities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Ozkaya.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICitiesService _citiesService;
        public CitiesController(ICitiesService citiesService)
        {
            _citiesService = citiesService; 
        }

        [HttpGet]
        [Route("ozkaya/getall")]
        public async Task<ServiceResponse<List<GetAllCitiesDto>>> GetAll()
        {
             return await _citiesService.GetAll();
        }

        [HttpGet]
        [Route("ozkaya/getbyid/{id}")]
        public async Task<ServiceResponse<GetByIdCitiesDto>> GetById(int id)
        {
            var data = await _citiesService.GetByIdAsync(id);
            return data;
        }

        [HttpPost]
        [Route("ozkaya/addasync")]
        public async Task<ServiceResponse> Post(CreateCitiesDto model)
        {
            var data = await _citiesService.CreateAsync(model);
            return data;
        }

        [HttpPut]
        [Route("ozkaya/updateasync")]
        public async Task<ServiceResponse> Put(UpdateCitiesDto model)
        {
            var data = await _citiesService.UpdateAsync(model);
            return data;
        }

        [HttpDelete]
        [Route("ozkaya/removeasync/{id}")]
        public async Task<ServiceResponse> Delete(int id)
        {
            var data = await _citiesService.Remove(id);
            return data;    
        }
    }
}
