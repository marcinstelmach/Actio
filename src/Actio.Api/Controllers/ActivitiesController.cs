using System;
using System.Threading.Tasks;
using Actio.Api.Repositories;
using Actio.Common.Commands.Models;
using Actio.Common.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Actio.Api.Controllers
{
    [Route("api/Activities")]
    [ApiController]
    [Authorize]

    public class ActivitiesController : ControllerBase
    {
        private readonly IBusClient busClient;
        private readonly IActivityRepository activityRepository;

        public ActivitiesController(IBusClient busClient, IActivityRepository activityRepository)
        {
            this.busClient = busClient;
            this.activityRepository = activityRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await activityRepository.BrowseAsync(User.Identity.GetUserIdIfExist()));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var activity = await activityRepository.GetAsync(id);
            if (activity == null)
            {
                return BadRequest();
            }

            if (activity.UserId != User.Identity.GetUserIdIfExist())
            {
                return Unauthorized();
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateActivityCommandModel model)
        {
            await busClient.PublishAsync(model
                .SetId(Guid.NewGuid())
                .SetCreatedAt(DateTime.Now)
                .SetUserId(User.Identity.GetUserIdIfExist()));
            return Accepted();
        }
    }
}
