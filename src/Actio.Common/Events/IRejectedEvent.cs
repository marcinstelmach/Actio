using Actio.Common.Exceptions;

namespace Actio.Common.Events
{
    public interface IRejectedEvent : IEvent
    {
        string Reason { get; }
        ErrorCode Code { get; }
    }
}
