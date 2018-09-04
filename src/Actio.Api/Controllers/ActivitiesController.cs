using System;
using System.Threading.Tasks;
using Actio.Common.Commands.Models;
using Microsoft.AspNetCore.Mvc;
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

//        [HttpGet("{id}", Name = "Get")]
//        public async Task<IActionResult> Get(Guid id)
//        {
//            var command = new Activit
//            busClient.SubscribeAsync()
//        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateActivityCommandModel model)
        {
            await busClient.PublishAsync(model.SetId(Guid.NewGuid()).SetCreatedAt(DateTime.Now));
            return AcceptedAtRoute("Get", new {id = model.Id});
        }
    }
} 