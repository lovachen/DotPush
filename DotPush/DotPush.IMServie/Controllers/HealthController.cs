using Microsoft.AspNetCore.Mvc;

namespace DotPush.IMServie.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "ok";
        }
    }
}
