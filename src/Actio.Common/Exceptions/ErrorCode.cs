using System.Net;

namespace Actio.Common.Exceptions
{
    public class ErrorCode
    {
        public string Code { get; set; }
        public string Message { get; }
        public HttpStatusCode StatusCode { get; }

        public ErrorCode(string code, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            : this(code, code, statusCode)
        {
        }

        public ErrorCode(string code, string message,  HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            Code = code;
            Message = message;
            StatusCode = statusCode;
        }

        public static ErrorCode InvalidCommand => new ErrorCode(nameof(InvalidCommand), "The passed command is invalid");
        public static ErrorCode ActivityDoesntExist(string name) => new ErrorCode(nameof(ActivityDoesntExist), $"In database there is no category called {name}");
    }
}
