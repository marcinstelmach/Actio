namespace Actio.Common.Events.Models
{
    public class UserAuthenticatedEventModel : IEvent
    {
        public string Email { get; }

        protected UserAuthenticatedEventModel()
        {
        }

        public UserAuthenticatedEventModel(string email)
        {
            Email = email;
        }
    }
}
