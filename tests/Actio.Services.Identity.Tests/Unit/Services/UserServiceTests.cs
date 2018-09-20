using System;
using System.Threading.Tasks;
using Actio.Common.Auth;
using Actio.Services.Identity.Domain.Models;
using Actio.Services.Identity.Domain.Repositories;
using Actio.Services.Identity.Domain.Services;
using Actio.Services.Identity.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Actio.Services.Identity.Tests.Unit.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task UserLogin_ShouldReturnJwtToken()
        {
            //arrange
            var userRepository = new Mock<IUserRepository>();
            var encrypter = new Mock<IEncrypter>();
            var jwtManager = new Mock<IJwtManager>();
            var email = "test@test.com";
            var password = "1qaz@WSX";
            var name = "name";
            var salt = "salt";
            var token = "token";
            encrypter.Setup(s => s.GetSalt()).Returns(salt);
            encrypter.Setup(s => s.GetHash(password, salt)).Returns(password);
            jwtManager.Setup(s => s.GenerateToken(It.IsAny<Guid>())).Returns(new JsonWebToken(token, DateTime.Now.AddMinutes(5)));
            var user = new User(email, name);
            user.SetPassword(password, encrypter.Object);
            userRepository.Setup(s => s.GetAsync(email, false)).ReturnsAsync(user);

            var userService = new UserService(userRepository.Object, encrypter.Object, jwtManager.Object);

            //act
            var result = await userService.Login(email, password);

            //assert
            userRepository.Verify(s => s.GetAsync(email, false), Times.Once);
            jwtManager.Verify(s => s.GenerateToken(It.IsAny<Guid>()), Times.Once);
            result.Should().NotBeNull();
            result.Should().BeOfType<JsonWebToken>();
            result.Token.Should().BeEquivalentTo(token);
        }
    }
}
