using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cities
{
    public class UpdateCitiesDto
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
    }
}
