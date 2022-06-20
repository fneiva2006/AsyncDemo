using System.Threading.Tasks;

using AsyncDemo.Models;

using RestEase;

namespace AsyncDemoClient
{
    public interface IAsyncDemoClient
    {
        [Get("demo")]
        Task<ProcessInfo> SyncApiCallAsync();

        [Get("demo/async")]
        Task<ProcessInfo> AsyncApiCallAsync();
    }
}
