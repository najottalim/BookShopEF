using System.Net;

namespace BookStore.Service.Exceptions;

public class HttpStatusCodeException : Exception
{
    public HttpStatusCodeException(HttpStatusCode statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
    
    public HttpStatusCode StatusCode { get; set; }
    
}