using Actio.Common.Exceptions;

namespace Actio.Common.Events.Models
{
    public class UserCreatedRejectedEventModel : IRejectedEvent
    {
        public string Email { get; }
        public string Reason { get; }
        public ErrorCode Code { get; }

        protected UserCreatedRejectedEventModel()
        {
        }

        public UserCreatedRejectedEventModel(string email, string reason, ErrorCode code)
        {
            Email = email;
            Reason = reason;
            Code = code;
        }
    }
}
