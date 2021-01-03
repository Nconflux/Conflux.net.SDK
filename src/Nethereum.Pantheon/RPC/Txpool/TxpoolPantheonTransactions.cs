using Conflux.JsonRpc.Client;
using Conflux.RPC.Infrastructure;
using Newtonsoft.Json.Linq;

namespace Conflux.Pantheon.RPC.Txpool
{
    /// <Summary>
    ///     Lists transactions in the node transaction pool.
    /// </Summary>
    public class TxpoolPantheonTransactions : GenericRpcRequestResponseHandlerNoParam<JArray>,
        ITxpoolPantheonTransactions
    {
        public TxpoolPantheonTransactions(IClient client) : base(client,
            ApiMethods.txpool_pantheonTransactions.ToString())
        {
        }
    }
}