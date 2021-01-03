using Newtonsoft.Json;

namespace Conflux.KeyStore.Model
{
    public class KdfParams
    {
        [JsonProperty("dklen")]
        public int Dklen { get; set; }

        [JsonProperty("salt")]
        public string Salt { get; set; }
    }
}