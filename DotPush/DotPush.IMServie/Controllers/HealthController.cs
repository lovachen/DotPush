using Microsoft.AspNetCore.Mvc;

namespace DotPush.IMServie.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;



        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into HealthController");
        }


        [HttpGet]
        public string Get()
        {
            return "ok";
        }

        [HttpGet("log")]
        public string Index(string? q )
        {
            _logger.LogInformation($"提交 错误日志到 Elastic ! QueryString = {q}");
            return "log";
        }



    }
}
