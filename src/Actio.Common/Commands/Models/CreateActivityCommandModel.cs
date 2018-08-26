using System;

namespace Actio.Common.Commands.Models
{
    public class CreateActivityCommandModel : IAuthenticatedCommand
    {
        public Guid UserId { get; set; }
        public Guid Id { get; private set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; private set; }

        
        public CreateActivityCommandModel SetId(Guid id)
        {
            Id = id;
            return this;
        }

        public CreateActivityCommandModel SetCreatedAt(DateTime dateTime)
        {
            CreatedAt = dateTime;
            return this;
        }
    }
}
