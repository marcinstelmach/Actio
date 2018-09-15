using System;
using System.Threading.Tasks;
using Actio.Api.Repositories;
using Actio.Common.Events;
using Actio.Common.Events.Models;

namespace Actio.Api.Handlers.Activity
{
    public class ActivityCreatedEventHandler : IEventHandler<ActivityCreatedEventModel>
    {
        private readonly IActivityRepository activityRepository;

        public ActivityCreatedEventHandler(IActivityRepository activityRepository)
        {
            this.activityRepository = activityRepository;
        }

        public async Task HandleAsync(ActivityCreatedEventModel @event)
        {
            // if activity created successfully, add the same to the api database, for quicker fetch - Event Sourceing
            await activityRepository.AddAsync(new Model.Activity(
                @event.Id,
                @event.Name,
                @event.Description,
                @event.UserId,
                @event.CreatedAt));
            Console.WriteLine($"Activity Created {@event.Name}");
        }
    }
}
