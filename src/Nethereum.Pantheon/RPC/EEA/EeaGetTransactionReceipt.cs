using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Conflux.Pantheon.RPC.EEA.DTOs;

namespace Conflux.Pantheon.RPC.EEA
{
    /// <Summary>
    ///     Returns information about the private transaction after the transaction was mined. Receipts for pending
    ///     transactions are not available.
    /// </Summary>
    public class EeaGetTransactionReceipt : RpcRequestResponseHandler<EeaTransactionReceipt>, IEeaGetTransactionReceipt
    {
        public EeaGetTransactionReceipt(IClient client) : base(client, ApiMethods.eea_getTransactionReceipt.ToString())
        {
        }

        public async Task<EeaTransactionReceipt> SendRequestAsync(string transactionHash, object id = null)
        {
            return await base.SendRequestAsync(id, transactionHash);
        }

        public RpcRequest BuildRequest(string transactionHash, object id = null)
        {
            return base.BuildRequest(id, transactionHash);
        }
    }
}