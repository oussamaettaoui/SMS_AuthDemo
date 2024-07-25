using System.Net;

namespace SMS_Auth.Domain.Entities
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            ErrorMessages = new List<string>();
        }
        public HttpStatusCode HttpStatus { get; set; }
        public bool IsSuccess { get; set; }
        public List<string>? ErrorMessages { get; set; }
        public Object? Result { get; set; }
    }
}
