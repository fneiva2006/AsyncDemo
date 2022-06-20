using AsyncDemo.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Diagnostics;
using System.Threading.Tasks;

namespace AsyncDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly ILogger<DemoController> _logger;

        private const int LONG_OPERATION_DELAY_MS = 5000;

        public DemoController(ILogger<DemoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            Task.Delay(LONG_OPERATION_DELAY_MS).Wait();
            return Ok(GetProcessInfo());
        }

        [HttpGet("async")]
        public async Task GetAsync()
        {
            await Task.Delay(LONG_OPERATION_DELAY_MS);
        }

        private async Task<int> TestFunctionAsync()
        {
            await Task.Delay(LONG_OPERATION_DELAY_MS);
            return 5;
        }
        
        private static ProcessInfo GetProcessInfo()
        {
            var process = Process.GetCurrentProcess();

            var processInfo = new ProcessInfo
            {
                NumberOfThreads = process.Threads.Count,
                PrivateMemory = process.PrivateMemorySize64,
            };

            return processInfo;
        }
    }
}
