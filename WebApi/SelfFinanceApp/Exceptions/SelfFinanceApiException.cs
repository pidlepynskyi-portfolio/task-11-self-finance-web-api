using System.Net;

namespace SelfFinanceApp.Exceptions
{
    public class SelfFinanceApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public SelfFinanceApiException(string message, HttpStatusCode statusCode)
           : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
