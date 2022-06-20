using System;
using System.Threading.Tasks;

using RestEase;

namespace AsyncDemoClient
{
    public static class Program
    {
        static async Task Main(string[] args)
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

                Console.Write("Type the number of requetss: ");
                var numberOfRequests = int.Parse(Console.ReadLine());

                switch (pressedKey.Key)
                {
                    case ConsoleKey.A:
                        await demoRoutines.AsyncDemo(numberOfRequests);
                        break;

                    case ConsoleKey.S:
                        await demoRoutines.SyncDemo(numberOfRequests);
                        break;

                    default:
                        break;
                }

                Console.WriteLine("Press ENTER to continue...");
                Console.ReadLine();
                Console.Clear();
            } 
        }
    }
}
