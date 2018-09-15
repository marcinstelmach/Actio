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
        public static ErrorCode EmptyActivityName => new ErrorCode(nameof(EmptyActivityName), "Prodived name Activity Name is invalid");
        public static ErrorCode EmptyPassword => new ErrorCode(nameof(EmptyPassword), "Provided password is invalid or empty");
        public static ErrorCode MoreThanOneRecord => new ErrorCode(nameof(MoreThanOneRecord), "In collection exist more than one elements like provided");
        public static ErrorCode UserDoesNotExist => new ErrorCode(nameof(UserDoesNotExist));
        public static ErrorCode UserWithGivenEmailExist => new ErrorCode(nameof(UserDoesNotExist));
        public static ErrorCode InvalidUsernameOrPassword => new ErrorCode(nameof(InvalidUsernameOrPassword));
        public static ErrorCode ActivityDoesNotExist => new ErrorCode(nameof(ActivityDoesNotExist));
        public static ErrorCode CategoryDoesNotExist => new ErrorCode(nameof(CategoryDoesNotExist));
        public static ErrorCode InvalidGuid(string val) => new ErrorCode(nameof(InvalidGuid), $"Unable to parse '{val}' as a guid ");
    }
}
