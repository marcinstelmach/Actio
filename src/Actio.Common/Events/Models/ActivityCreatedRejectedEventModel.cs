namespace Actio.Common.Events.Models
{
    public class ActivityCreatedRejectedEventModel : IRejectedEvent
    {
        public string Reason { get; }
        public string Code { get; }

        protected ActivityCreatedRejectedEventModel()
        {
        }

        public ActivityCreatedRejectedEventModel(string reason, string code)
        {
            Reason = reason;
            Code = code;
        }
    }
}
