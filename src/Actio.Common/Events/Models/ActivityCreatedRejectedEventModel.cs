using Actio.Common.Exceptions;

namespace Actio.Common.Events.Models
{
    public class ActivityCreatedRejectedEventModel : IRejectedEvent
    {
        public string Reason { get; }
        public ErrorCode Code { get; }

        protected ActivityCreatedRejectedEventModel()
        {
        }

        public ActivityCreatedRejectedEventModel(string reason, ErrorCode code)
        {
            Reason = reason;
            Code = code;
        }
    }
}
