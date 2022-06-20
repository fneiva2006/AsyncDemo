using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AsyncDemoClient
{
    public class DemoRoutines
    {
        private readonly IAsyncDemoClient _client;

        private const int DEFAULT_NUMBER_OF_CONCURRENT_REQUESTS = 10;
        private const int MAX_NUMBER_OF_CONCURRENT_REQUESTS = 100;

        public DemoRoutines(IAsyncDemoClient client)
        {
            _client = client;
        }

        public async Task CallAsyncEndpointAsync(int numberOfRequests = DEFAULT_NUMBER_OF_CONCURRENT_REQUESTS)
        {
            if (!ValidateNumberOfRequests(numberOfRequests))
            {
                return;
            }

            Console.WriteLine($"[ASYNC] Running {numberOfRequests} requests in parallel.");

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

        public async Task CallSyncEndpointAsync(int numberOfRequests = DEFAULT_NUMBER_OF_CONCURRENT_REQUESTS)
        {
            if (!ValidateNumberOfRequests(numberOfRequests))
            {
                return;
            }

            Console.WriteLine($"[SYNC] Running {numberOfRequests} requests in parallel.");

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

        private static bool ValidateNumberOfRequests(int numberOfRequests)
        {
            if (numberOfRequests > MAX_NUMBER_OF_CONCURRENT_REQUESTS)
            {
                Console.WriteLine("Too many requests to run in parallel. Max number is 1000. Please try again.");
                return false;
            }

            if (numberOfRequests < 1)
            {
                Console.WriteLine("Number of requests should be greater than 0. Please try again.");
                return false;
            }

            return true;
        }

        #endregion
    }
}
