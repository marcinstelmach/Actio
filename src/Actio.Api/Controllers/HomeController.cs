using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Actio.Api.Controllers
{
    [Route("api/Home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public async Task<IActionResult> Get()
            => Content("Hello From API GATEWAY !");
    }
}