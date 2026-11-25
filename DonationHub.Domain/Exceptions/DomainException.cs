using System.Net;

namespace DonationHub.Domain.Exceptions
{
    public abstract class DomainException(string msg, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : Exception(msg)
    {
        public HttpStatusCode StatusCode { get; }

    }
}
