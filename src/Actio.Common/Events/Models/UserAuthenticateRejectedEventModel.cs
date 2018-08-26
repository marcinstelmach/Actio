namespace Actio.Common.Events.Models
{
    public class UserAuthenticateRejectedEventModel : IRejectedEvent
    {
        public string Emial { get; }
        public string Reason { get; }
        public string Code { get; }

        protected UserAuthenticateRejectedEventModel()
        {
        }

        public UserAuthenticateRejectedEventModel(string emial, string reason, string code)
        {
            Emial = emial;
            Reason = reason;
            Code = code;
        }
    }
}
