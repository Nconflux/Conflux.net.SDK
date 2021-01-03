using Conflux.JsonRpc.Client;
using Conflux.Pantheon.RPC.EEA;
using Conflux.RPC;

namespace Conflux.Pantheon
{
    public class EeaApiService : RpcClientWrapper, IEeaApiService
    {
        public EeaApiService(IClient client) : base(client)
        {
            GetTransactionReceipt = new EeaGetTransactionReceipt(client);
            SendRawTransaction = new EeaSendRawTransaction(client);
        }

        public IEeaGetTransactionReceipt GetTransactionReceipt { get; }
        public IEeaSendRawTransaction SendRawTransaction { get; }
    }
}