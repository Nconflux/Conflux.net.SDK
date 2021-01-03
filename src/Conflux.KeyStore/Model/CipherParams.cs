using Conflux.Hex.HexConvertors.Extensions;
using Newtonsoft.Json;

namespace Conflux.KeyStore.Model
{
    public class CipherParams
    {
        public CipherParams()
        {
        }

        public CipherParams(byte[] iv)
        {
            Iv = iv.ToHex();
        }

        [JsonProperty("iv")]
        public string Iv { get; set; }
    }
}