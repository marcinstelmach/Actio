namespace Actio.Common.Events.Models
{
    public class UserCreatedRejectedEventModel : IRejectedEvent
    {
        public string Email { get; }
        public string Reason { get; }
        public string Code { get; }

        protected UserCreatedRejectedEventModel()
        {
        }

        public UserCreatedRejectedEventModel(string email, string reason, string code)
        {
            Email = email;
            Reason = reason;
            Code = code;
        }
    }
}
