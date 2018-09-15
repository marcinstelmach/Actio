using Actio.Api.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Actio.Api.Tests.Unit.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void HomeController_Get_ShouldReturnStringContent()
        {
            // arrange
            var sut = new HomeController();

            //act
            var result = sut.Get() as ContentResult;

            // assert
            result.Should().NotBeNull();
            result.Content.Should().Be("Hello From API GATEWAY !");
        }
    }
}
