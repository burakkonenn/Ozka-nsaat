using Application.Abstractions.Wrappers;
using Application.DTOs.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Services.Cities
{
    public interface ICitiesService
    {
        Task<ServiceResponse> CreateAsync(CreateCitiesDto model);
        Task<ServiceResponse> UpdateAsync(UpdateCitiesDto model);
        Task<ServiceResponse> Remove(int id);
        Task<ServiceResponse<List<GetAllCitiesDto>>> GetAll();
        Task<ServiceResponse<GetByIdCitiesDto>> GetByIdAsync(int id);
    }
}
