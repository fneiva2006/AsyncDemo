using System.Diagnostics;
using System.Threading.Tasks;

using AsyncDemo.Models;

using Microsoft.AspNetCore.Mvc;

namespace AsyncDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private const int LONG_OPERATION_DELAY_MS = 5_000;

        /// <summary>
        /// [Sync Endpoint] Returns information of the process in which the server application is running.
        /// </summary>
        /// <returns>IActionResult interface that holds the process information payload.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            // Thread blocked
            Task.Delay(LONG_OPERATION_DELAY_MS).Wait();
            
            return Ok(GetProcessInfo());
        }

        /// <summary>
        /// [Async Endpoint] Returns information of the process in which the server application is running.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. Its result contains an IActionResult interface that holds
        /// the process information payload.</returns>
        [HttpGet("async")]
        public async Task<IActionResult> GetAsync()
        {
            // Thread not blocked since delay is awaited in an async Task
            await Task.Delay(LONG_OPERATION_DELAY_MS);

            return Ok(GetProcessInfo());
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
