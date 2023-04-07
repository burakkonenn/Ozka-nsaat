using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cities
{
    public class GetAllCitiesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }

    }
}
