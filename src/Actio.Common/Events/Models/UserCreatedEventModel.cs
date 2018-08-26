using System;

namespace Actio.Common.Events.Models
{
    public class UserCreatedEventModel : IEvent
    {
        public string Email { get; }
        public string Name { get; }

        protected UserCreatedEventModel() { }

        public UserCreatedEventModel(string email, string name)
        {
            Email = email;
            Name = name;
        }
    }
}
