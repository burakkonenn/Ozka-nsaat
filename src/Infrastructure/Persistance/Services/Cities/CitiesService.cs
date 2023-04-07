using Application.Abstractions.Repositories;
using Application.Abstractions.Services.Cities;
using Application.Abstractions.Wrappers;
using Application.DTOs.Cities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Persistance.Services
{
    public class CitiesService : ICitiesService
    {
        private readonly ICitiesReadRepository _citiesReadRepository;
        private readonly ICitiesWriteRepository _citiesWriteRepository;
        public CitiesService(ICitiesReadRepository citiesReadRepository, ICitiesWriteRepository citiesWriteRepository)
        {
            _citiesReadRepository = citiesReadRepository;
            _citiesWriteRepository = citiesWriteRepository;
        }
        
        public async Task<ServiceResponse> CreateAsync(CreateCitiesDto model)
        {
            var response = new ServiceResponse<Cities>();
            var cities = new Cities();
            cities.Name = model.Name;
            cities.CountryName = model.CountryName;
            //cities.CreatedDate = DateTime.Now;
            //cities.IsDeleted = false;

            await _citiesWriteRepository.AddAsync(cities);
            response.IsSuccess = true;
            response.Message = "Kayıt ekleme başarılı";
            response.Data = cities;
            return response;

        }

        public async Task<ServiceResponse<List<GetAllCitiesDto>>> GetAll()
        {
            var response = new ServiceResponse<List<GetAllCitiesDto>>();
            var cities =  _citiesReadRepository.GetAll().Where(a => a.IsDeleted == false);

            response.Data = await cities.Select(a => new GetAllCitiesDto()
            {
                Id = a.Id,
                Name = a.Name,
                CountryName = a.CountryName
            }).ToListAsync();
            response.Message = "OK";
            response.IsSuccess =true;
            return response;
        }

        public async Task<ServiceResponse<GetByIdCitiesDto>> GetByIdAsync(int id)
        {
            var response = new ServiceResponse<GetByIdCitiesDto>();
            var city = await _citiesReadRepository.GetByIdAsync(id);
            if(city == null)
            {
                response.Message = "Kayıt bulunamadı";
                response.IsSuccess = false;
                return response;
            }

            var data = new GetByIdCitiesDto();
            data.Id = city.Id;
            data.Name = city.Name;
            data.CountryName = city.CountryName;

            response.IsSuccess = true;
            response.Data = data;
            return response;
        }

        public async Task<ServiceResponse> Remove(int id)
        {
            ServiceResponse response = new ServiceResponse();
            var city = await _citiesReadRepository.GetByIdAsync(id);
            
            if(city == null) {
                response.IsSuccess = false;
                response.Message = "Silinecek kayıt bulunamadı";
                return response;
            }

            await _citiesWriteRepository.RemoveAsync(city);
            response.IsSuccess = true;
            response.Message = "Silme işlemi başarılı";
            return response;
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateCitiesDto model)
        {
            var response = new ServiceResponse<Cities>();
            var city = await _citiesReadRepository.GetByIdAsync(model.CityId);
            if (city == null)
            {
                response.IsSuccess = false;
                response.Message = "güncellenecek kayıt bulunamadı";
                return response;
            }
            city.Name = model.CityName;
            city.CountryName = model.CountryName;
            //city.UpdatedDate = DateTime.Now;
            await _citiesWriteRepository.UpdateAsync(city);

            response.IsSuccess = true;
            response.Message = "Güncelleme işlemi başarılı";
            response.Data = city;

            return response;
        }
    }
}
