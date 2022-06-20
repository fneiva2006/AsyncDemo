using System;
using System.Threading.Tasks;

using RestEase;

namespace AsyncDemoClient
{
    public static class Program
    {
        public static async Task Main()
        {
            var client = RestClient.For<IAsyncDemoClient>("https://localhost:8081");

            var demoRoutines = new DemoRoutines(client);

            ConsoleKeyInfo pressedKey;

            while(true)
            {
                Console.WriteLine("Press A to run the async demo, S to run the sync demo, ESC to exit");
                pressedKey = Console.ReadKey(true);

                if (pressedKey.Key == ConsoleKey.Escape)
                {
                    return;
                }

                if (pressedKey.Key != ConsoleKey.A && pressedKey.Key != ConsoleKey.S)
                {
                    Console.Clear();
                    continue;
                }

                Console.Write("Type the number of requests: ");

                if (int.TryParse(Console.ReadLine(), out var numberOfRequests))
                {
                    switch (pressedKey.Key)
                    {
                        case ConsoleKey.A:
                            await demoRoutines.CallAsyncEndpointAsync(numberOfRequests);
                            break;

                        case ConsoleKey.S:
                            await demoRoutines.CallSyncEndpointAsync(numberOfRequests);
                            break;

                        default:
                            break;
                    }

                    Console.WriteLine("Press ENTER to continue...");
                    Console.ReadLine();
                }
                
                Console.Clear();
            } 
        }
    }
}
