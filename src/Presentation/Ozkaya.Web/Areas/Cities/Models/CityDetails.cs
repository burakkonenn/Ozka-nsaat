using Ozkaya.Web.Dtos.Cities;
using Ozkaya.Web.Models;
using System.Reflection.Metadata.Ecma335;

namespace Ozkaya.Web.Areas.Cities.Models
{
    public class CityDetails
    {
        public CitiesResponse CitiesResponse { get; set; }
        public CreateCityDto CreateCity { get; set; }
        public UpdateCityDto UpdateCity { get; set; }
    }
}
