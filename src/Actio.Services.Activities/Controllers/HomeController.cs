using Microsoft.AspNetCore.Mvc;

namespace Actio.Services.Activities.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() 
            => Content("Hello from Actio.Services.Activites API!");
    }
}