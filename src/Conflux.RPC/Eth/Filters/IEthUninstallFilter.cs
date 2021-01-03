using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;

namespace Conflux.RPC.Eth.Filters
{
    public interface IEthUninstallFilter
    {
        RpcRequest BuildRequest(HexBigInteger filterId, object id = null);
        Task<bool> SendRequestAsync(HexBigInteger filterId, object id = null);
    }
}