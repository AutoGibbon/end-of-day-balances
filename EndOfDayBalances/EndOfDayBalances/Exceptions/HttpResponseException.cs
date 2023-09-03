using System.Net;

namespace EndOfDayBalances.Exceptions
{
    public class HttpResponseException : Exception
    {
        public HttpStatusCode StatusCode { get; } = HttpStatusCode.InternalServerError;
        public HttpResponseException(HttpStatusCode statusCode, string message) : base (message) 
        {
            StatusCode = statusCode;
        }
    }
}
