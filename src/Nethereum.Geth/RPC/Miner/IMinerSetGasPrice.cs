using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;

namespace Conflux.Geth.RPC.Miner
{
    public interface IMinerSetGasPrice
    {
        RpcRequest BuildRequest(HexBigInteger price, object id = null);
        Task<bool> SendRequestAsync(HexBigInteger price, object id = null);
    }
}