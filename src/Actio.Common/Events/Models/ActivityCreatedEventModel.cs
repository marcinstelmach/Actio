using System;

namespace Actio.Common.Events.Models
{
    public class ActivityCreatedEventModel : IAuthenticatedEvent
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        
        protected ActivityCreatedEventModel() { }

        public ActivityCreatedEventModel(Guid userId, Guid id, string category, string name, string description, DateTime createdAt)
        {
            UserId = userId;
            Id = id;
            Category = category;
            Name = name;
            Description = description;
            CreatedAt = createdAt;
        }
    }
}
