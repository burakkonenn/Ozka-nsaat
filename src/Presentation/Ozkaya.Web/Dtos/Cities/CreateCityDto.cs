using System.ComponentModel.DataAnnotations;

namespace Ozkaya.Web.Dtos.Cities
{
    public class CreateCityDto
    {
        [Required(ErrorMessage = "Lütfen zorunlu alanları boş geçmeyiniz")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Lütfen zorunlu alanları boş geçmeyiniz")]
        public string CountryName { get; set; }

    }
}
