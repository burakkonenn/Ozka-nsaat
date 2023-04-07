namespace Ozkaya.Web.Wrappers
{
    public partial class ServiceResponse
    {
        public string message { get; set; }
        public bool  IsSuccess { get; set; }
    }


    public partial class ServiceResponse<T>
    {
        public T Data { get; set; }
    }
}
