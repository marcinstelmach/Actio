using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Commands.Models;
using Actio.Common.Events.Models;
using RawRabbit;

namespace Actio.Services.Identity.Handlers.Users
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommandModel>
    {
        private readonly IBusClient busClient;

        public CreateUserCommandHandler(IBusClient busClient)
        {
            this.busClient = busClient;
        }

        public async Task HandleAsync(CreateUserCommandModel command)
        {
            Console.WriteLine($"Creating activity: {command.Name}");
            await busClient.PublishAsync(new UserCreatedEventModel(command.Email, command.Name));
        }
    }
}
