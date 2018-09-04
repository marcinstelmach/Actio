using System;
using System.Threading.Tasks;
using Actio.Common.Events;
using Actio.Common.Events.Models;

namespace Actio.Api.Handlers.Activity
{
    public class ActivityCreatedEventHandler : IEventHandler<ActivityCreatedEventModel>
    {
        public async Task HandleAsync(ActivityCreatedEventModel @event)
        {
            await Task.CompletedTask;
            Console.WriteLine($"Activity Created {@event.Name}");
        }
    }
}
