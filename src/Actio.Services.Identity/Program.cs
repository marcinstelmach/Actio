using System.Threading.Tasks;
using Actio.Common.Commands.Models;
using Actio.Common.Services;

namespace Actio.Services.Identity
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await ServiceHost.Create<Startup>(args)
                .UseRabbitMq()
                .SubscribeToCommand<CreateUserCommandModel>()
                .Build()
                .Run();
        }
    }
}
