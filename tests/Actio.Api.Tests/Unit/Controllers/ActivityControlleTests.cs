using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Actio.Api.Controllers;
using Actio.Api.Repositories;
using Actio.Common.Commands;
using Actio.Common.Commands.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RawRabbit;
using Xunit;

namespace Actio.Api.Tests.Unit.Controllers
{
    public class ActivityControlleTests
    {
        [Fact]
        public async Task ActivitiesController_Post_Should_Return_Accepted_StatusCode()
        {
            //arrange
            var busClient = new Mock<IBusClient>();
            var activityRepository = new Mock<IActivityRepository>();
            var userId = Guid.NewGuid();
            var command = new CreateActivityCommandModel
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Category = "TestCategory",
                Description = "Description Category"
            };

            var sut = new ActivitiesController(busClient.Object, activityRepository.Object);
            sut.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(
                        new ClaimsIdentity
                        (
                            new Claim[]
                            {
                                new Claim(ClaimTypes.Name, userId.ToString())
                            },
                            "test"
                        )
                    )
                }
            };

            var routeValues = new Dictionary<string, object>
            {
                {"id", command.Id}
            };
            //act

            var result = await sut.Post(command) as AcceptedAtRouteResult;

            //assert
            result.Should().NotBeNull();
            result.StatusCode?.Should().Be(StatusCodes.Status202Accepted);
            result.RouteName.Should().Be("Get");
            result.RouteValues.Should().BeEquivalentTo(routeValues);
        }
    }
}
