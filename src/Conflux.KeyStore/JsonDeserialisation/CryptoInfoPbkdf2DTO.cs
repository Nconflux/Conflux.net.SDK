using Conflux.Hex.HexConvertors.Extensions;
using Conflux.KeyStore.Model;
using Newtonsoft.Json;

namespace Conflux.KeyStore.JsonDeserialisation
{
    public class CryptoInfoPbkdf2DTO : CryptoInfoDTOBase
    {
        public CryptoInfoPbkdf2DTO()
        {
            kdfparams = new Pbkdf2ParamsDTO();
        }

        public Pbkdf2ParamsDTO kdfparams { get; set; }
    }
}