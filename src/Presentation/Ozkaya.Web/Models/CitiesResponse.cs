namespace Ozkaya.Web.Models
{
    public class CitiesResponse
    {
        public string message { get; set; }
        public bool isSuccess { get; set; }

        public List<Cities> data { get; set; }
    }
}
