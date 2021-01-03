using System.Numerics;

namespace Conflux.GSN.Models
{
    public class GetAddrResponse
    {
        public string RelayServerAddress { get; set; }

        public BigInteger MinGasPrice { get; set; }

        public bool Ready { get; set; }

        public string Version { get; set; }
    }
}
