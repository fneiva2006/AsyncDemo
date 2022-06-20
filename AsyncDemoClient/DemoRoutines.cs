using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AsyncDemoClient
{
    public class DemoRoutines
    {
        private readonly IAsyncDemoClient _client;

        private const int NUMBER_OF_CONCURRENT_REQUESTS = 10;

        public DemoRoutines(IAsyncDemoClient client)
        {
            _client = client;
        }

        public async Task AsyncDemo(int numberOfRequests = NUMBER_OF_CONCURRENT_REQUESTS)
        {
            Console.WriteLine("Running async calls demo");

            var sw = Stopwatch.StartNew();

            var asyncDemoTasks = new List<Task>();

            for (var i = 0; i < numberOfRequests; i++)
            {
                asyncDemoTasks.Add(AsyncCallAsync());
            }

            await Task.WhenAll(asyncDemoTasks);

            sw.Stop();

            Console.WriteLine("\n=====================================================\n");
            Console.WriteLine($@"Client spent {sw.Elapsed:mm\:ss\.ff} to complete {numberOfRequests} ASYNC requests");
        }

        public async Task SyncDemo(int numberOfRequests = NUMBER_OF_CONCURRENT_REQUESTS)
        {
            Console.WriteLine("Running sync calls demo");

            var sw = Stopwatch.StartNew();

            var syncDemoTasks = new List<Task>();

            try
            {
                for (var i = 0; i < numberOfRequests; i++)
                {
                    syncDemoTasks.Add(SyncCallAsync());
                }

                await Task.WhenAll(syncDemoTasks);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            sw.Stop();

            Console.WriteLine("\n=====================================================\n");
            Console.WriteLine($@"Client spent {sw.Elapsed:mm\:ss\.ff} to complete {numberOfRequests} SYNC requests");
        }

        #region Private auxiliary methods

        private async Task SyncCallAsync()
        {
            var sw = Stopwatch.StartNew();

            var result = await _client.SyncApiCallAsync();

            sw.Stop();

            Console.WriteLine($@"Sync api call took {sw.Elapsed:ss\.ff} to complete and used {result.PrivateMemory / 1000000} megabytes of RAM ({result.NumberOfThreads} threads).");
        }

        private async Task AsyncCallAsync()
        {
            var sw = Stopwatch.StartNew();

            var result = await _client.AsyncApiCallAsync();

            sw.Stop();

            Console.WriteLine($@"ASYNC api call took {sw.Elapsed:ss\.ff} to complete and used {result.PrivateMemory / 1000000} megabytes of RAM ({result.NumberOfThreads} threads).");
        }

        #endregion
    }
}
