using Newtonsoft.Json;

namespace AsyncDemo.Models
{
    public class ProcessInfo
    {
        [JsonProperty("privateMemory")]
        public long PrivateMemory { get; set; }
    
        [JsonProperty("numberOfThreads")]
        public int NumberOfThreads { get; set; }
    }
}
