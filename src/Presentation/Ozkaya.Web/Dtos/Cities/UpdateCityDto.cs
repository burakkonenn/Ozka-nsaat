using System.ComponentModel.DataAnnotations;

namespace Ozkaya.Web.Dtos.Cities
{
    public class UpdateCityDto
    {
        public int CityId { get; set; }

        [Required(ErrorMessage = "Lütfen zorunlu alanları boş geçmeyiniz")]
        public string CityName { get; set; }

        [Required(ErrorMessage = "Lütfen zorunlu alanları boş geçmeyiniz")]
        public string CountryName { get; set; }
    }
}
