using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Commands.Models;
using Actio.Common.Events.Models;
using Actio.Common.Exceptions;
using Actio.Services.Activities.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace Actio.Services.Activities.Handlers.Activities
{
    public class CreateActivityCommandHandler : ICommandHandler<CreateActivityCommandModel>
    {
        private readonly IBusClient busClient;
        private readonly IActivityService activityService;
        private readonly ILogger logger;

        public CreateActivityCommandHandler(IBusClient busClient, IActivityService activityService, ILogger<CreateActivityCommandHandler> logger)
        {
            this.busClient = busClient;
            this.activityService = activityService;
            this.logger = logger;
        }

        public async Task HandleAsync(CreateActivityCommandModel command)
        {
            logger.LogInformation($"Creating activity: {command.Id}: {command.Name}");
            try
            {
                await activityService.AddAsync(command.Id, command.UserId, command.Category, command.Name, command.Description,
                    command.CreatedAt);
                await busClient.PublishAsync(
                    new ActivityCreatedEventModel(command.Id, command.UserId, command.Category, command.Name, command.Description, command.CreatedAt));
            }
            catch (ActioException ex)
            {
                await busClient.PublishAsync(new ActivityCreatedRejectedEventModel(ex.Message, ex.ErrorCode));
                logger.LogError(ex, $"Activity creating rejected {command.Id}");
            }
            
        }
    }
}
