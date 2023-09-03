using System.Net;

namespace EndOfDayBalances.Exceptions
{
    public class NotFoundException : HttpResponseException
    {
        const string DefaultMessage = "Entity Not Found";
        public NotFoundException() : this (DefaultMessage) {}
        public NotFoundException(string message) : base(HttpStatusCode.NotFound, message) {}
    }
}
