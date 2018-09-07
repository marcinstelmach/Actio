using System;

namespace Actio.Common.Exceptions
{
    public class ActioException : Exception
    {
        public ErrorCode ErrorCode { get; }

        public ActioException(ErrorCode errorCode)
            : this(errorCode, string.Empty)
        {
        }

        public ActioException(ErrorCode errorCode, string message)
            : this(errorCode, message, null)
        {
        }

        public ActioException(ErrorCode errorCode, string message, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }


    }
}
