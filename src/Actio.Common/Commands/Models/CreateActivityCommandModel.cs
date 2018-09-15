using System;

namespace Actio.Common.Commands.Models
{
    public class CreateActivityCommandModel : IAuthenticatedCommand
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; private set; }

        
        public CreateActivityCommandModel SetId(Guid id)
        {
            if (Id == Guid.Empty)
            {
                Id = id;
            }
            
            return this;
        }

        public CreateActivityCommandModel SetCreatedAt(DateTime dateTime)
        {
            CreatedAt = dateTime;
            return this;
        }

        public CreateActivityCommandModel SetUserId(Guid userId)
        {
            UserId = userId;
            return this;
        }
    }
}
