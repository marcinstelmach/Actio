using System;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Commands.Models;
using Actio.Common.Events.Models;
using RawRabbit;

namespace Actio.Services.Activities.Handlers.Activities
{
    public class CreateActivityCommandHandler : ICommandHandler<CreateActivityCommandModel>
    {
        private readonly IBusClient busClient;

        public CreateActivityCommandHandler(IBusClient busClient)
        {
            this.busClient = busClient;
        }

        public async Task HandleAsync(CreateActivityCommandModel command)
        {
            Console.WriteLine($"Creating activity: {command.Name}");
            await busClient.PublishAsync(new ActivityCreatedEventModel(command.Id, command.UserId, command.Category, command.Name, command.Description, command.CreatedAt));
        }
    }
}
