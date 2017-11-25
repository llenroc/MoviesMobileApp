using System;
using System.Net;

namespace MoviesMobileApp
{
    public sealed class Response<T>
    {
        internal Response(T result, HttpStatusCode statusCode, string errorMessage = null)
        {
            Result = result;
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }

        public T Result { get; }

        public HttpStatusCode StatusCode { get; }

        public string ErrorMessage { get; }
    }
}
