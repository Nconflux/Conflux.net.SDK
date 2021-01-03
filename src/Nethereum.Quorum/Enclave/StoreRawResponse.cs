using Newtonsoft.Json;

namespace Conflux.Quorum.Enclave
{
    public class StoreRawResponse
    {
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }
    }
}