using System.Threading.Tasks;
using Actio.Common.Commands.Models;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Actio.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IBusClient busClient;

        public UsersController(IBusClient busClient)
        {
            this.busClient = busClient;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommandModel model)
        {
            await busClient.PublishAsync(model);
            return Accepted();
        }
    }
}