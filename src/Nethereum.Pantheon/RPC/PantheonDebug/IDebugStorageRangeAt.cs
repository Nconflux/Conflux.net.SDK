using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Newtonsoft.Json.Linq;


namespace Conflux.Pantheon.RPC.Debug
{
    public interface IDebugStorageRangeAt
    {
        Task<JObject> SendRequestAsync(string blockHash, int txIndex, string contractAddress, string startKeyHash,
            int limitStorageEntries, object id = null);

        RpcRequest BuildRequest(string blockHash, int txIndex, string contractAddress, string startKeyHash,
            int limitStorageEntries, object id = null);
    }
}