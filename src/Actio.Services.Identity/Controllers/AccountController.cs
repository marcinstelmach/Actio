using System.Threading.Tasks;
using Actio.Common.Commands.Models;
using Actio.Services.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace Actio.Services.Identity.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthenticateUserCommandModel command)
            => Ok(await userService.Login(command.Email, command.Password));
    }
}