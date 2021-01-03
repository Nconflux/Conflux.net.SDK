using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Conflux.JsonRpc.Client.Streaming;
using Conflux.RPC.Eth.DTOs;
using Conflux.RPC.Eth.Subscriptions;
using Conflux.RPC.Reactive.RpcStreaming;

namespace Conflux.RPC.Reactive.Eth.Subscriptions
{

    public class EthNewBlockHeadersObservableSubscription : RpcStreamingSubscriptionObservableHandler<Block>
    {
        private EthNewBlockHeadersSubscriptionRequestBuilder _ethNewBlockHeadersSubscriptionRequestBuilder;

        public EthNewBlockHeadersObservableSubscription(IStreamingClient client) : base(client, new EthUnsubscribeRequestBuilder())
        {
            _ethNewBlockHeadersSubscriptionRequestBuilder = new EthNewBlockHeadersSubscriptionRequestBuilder();
        }

        public Task SubscribeAsync(object id = null)
        {
            return base.SubscribeAsync(BuildRequest(id));
        }

        public RpcRequest BuildRequest(object id)
        {
            return _ethNewBlockHeadersSubscriptionRequestBuilder.BuildRequest(id);
        }
    }
}
