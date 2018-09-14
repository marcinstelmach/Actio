using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Commands.Models;
using Actio.Common.Events.Models;
using Actio.Common.Exceptions;
using Actio.Services.Identity.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace Actio.Services.Identity.Handlers.Users
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommandModel>
    {
        private readonly IBusClient busClient;
        private readonly IUserService userService;
        private readonly ILogger logger;

        public CreateUserCommandHandler(IBusClient busClient, IUserService userService, ILogger<CreateUserCommandHandler> logger)
        {
            this.busClient = busClient;
            this.userService = userService;
            this.logger = logger;
        }

        public async Task HandleAsync(CreateUserCommandModel command)
        {
            logger.LogInformation($"Creating user: {command.Email}");
            try
            {
                await userService.RegisterAsync(command.Email, command.Password, command.Name);
                await busClient.PublishAsync(new UserCreatedEventModel(command.Email, command.Name));
            }
            catch (ActioException e)
            {
                await busClient.PublishAsync(new UserCreatedRejectedEventModel(command.Email, "Bo tak", e.ErrorCode));
                logger.LogError(e, $"Registering rejected: {command.Email}");
            }
        }
    }
}
