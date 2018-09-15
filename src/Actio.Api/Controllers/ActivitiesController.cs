using System;
using System.Threading.Tasks;
using Actio.Common.Commands.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;

namespace Actio.Api.Controllers
{
    [Route("api/Activities")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IBusClient busClient;

        public ActivitiesController(IBusClient busClient)
        {
            this.busClient = busClient;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateActivityCommandModel model)
        {
            await busClient.PublishAsync(model.SetId(Guid.NewGuid()).SetCreatedAt(DateTime.Now).SetUserId(Guid.NewGuid()));
            return Accepted();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Content("yes");
        }
    }
}
