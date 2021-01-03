using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Conflux.JsonRpc.Client.Streaming;
using Conflux.RPC.Eth.Subscriptions;
using Conflux.RPC.Reactive.RpcStreaming;

namespace Conflux.RPC.Reactive.Eth.Subscriptions
{

    public class EthNewPendingTransactionObservableSubscription : RpcStreamingSubscriptionObservableHandler<string>
    {
        private EthNewPendingTransactionSubscriptionRequestBuilder _builder;

        public EthNewPendingTransactionObservableSubscription(IStreamingClient client) : base(client, new EthUnsubscribeRequestBuilder())
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
