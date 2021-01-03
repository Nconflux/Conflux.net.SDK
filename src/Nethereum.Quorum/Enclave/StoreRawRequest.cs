using Newtonsoft.Json;

namespace Conflux.Quorum.Enclave
{
    public class StoreRawRequest
    {
        [JsonProperty(PropertyName = "payload")]
        public string Payload { get; set; }
        [JsonProperty(PropertyName = "from")]
        public string From { get; set; }
    }
}