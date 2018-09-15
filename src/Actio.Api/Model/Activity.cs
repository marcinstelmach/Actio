using System;

namespace Actio.Api.Model
{
    public class Activity
    {
        public Activity(Guid id, string name, string description, Guid userId, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Description = description;
            UserId = userId;
            CreatedAt = createdAt;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
