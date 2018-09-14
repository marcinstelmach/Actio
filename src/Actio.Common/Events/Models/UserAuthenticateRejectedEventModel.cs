using Actio.Common.Exceptions;

namespace Actio.Common.Events.Models
{
    public class UserAuthenticateRejectedEventModel : IRejectedEvent
    {
        public string Emial { get; }
        public string Reason { get; }
        public ErrorCode Code { get; }

        protected UserAuthenticateRejectedEventModel()
        {
        }

        public UserAuthenticateRejectedEventModel(string emial, string reason, ErrorCode code)
        {
            Emial = emial;
            Reason = reason;
            Code = code;
        }
    }
}
