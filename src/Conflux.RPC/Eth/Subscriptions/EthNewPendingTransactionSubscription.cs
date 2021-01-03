using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Conflux.JsonRpc.Client.Streaming;
using Conflux.JsonRpc.WebSocketStreamingClient;

namespace Conflux.RPC.Eth.Subscriptions
{
    public class EthNewPendingTransactionSubscription : RpcStreamingSubscriptionEventResponseHandler<string>
    {
        private EthNewPendingTransactionSubscriptionRequestBuilder _builder;

        public EthNewPendingTransactionSubscription(IStreamingClient client) : base(client, new EthUnsubscribeRequestBuilder())
        {
            _builder = new EthNewPendingTransactionSubscriptionRequestBuilder();
        }

        public Task SubscribeAsync(object id = null)
        {
            return base.SubscribeAsync(BuildRequest(id));
        }

        public RpcRequest BuildRequest(object id)
        {
            return _builder.BuildRequest(id);
        }
    }
}
