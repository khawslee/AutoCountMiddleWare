using AutoCountMiddleWare.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AutoCountMiddleWare.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AutoCountSettings _appSettings;

        public HomeController(
            ILogger<HomeController> logger,
            IOptions<AutoCountSettings> appSettings)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        [HttpGet(Name = "GetHome")]
        public int Get()
        {
            Console.Write(">>>" + _appSettings.UseSaLogin);
            return 0;
        }
    }
}
