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


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Content("Elo");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateActivityCommandModel model)
        {
            model.SetId(Guid.NewGuid()).SetCreatedAt(DateTime.Now).SetUserId(Guid.NewGuid());
            try
            {
                await busClient.PublishAsync(model);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
            
            return Accepted();
        }
    }
} 