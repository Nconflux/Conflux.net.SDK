using System;
using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Conflux.JsonRpc.Client.Streaming;
using Conflux.RPC.Eth.DTOs;
using Conflux.RPC.Eth.Subscriptions;
using Conflux.RPC.Reactive.RpcStreaming;
using Newtonsoft.Json.Linq;
using Conflux.Parity.RPC.PubSub;

namespace Conflux.Parity.Reactive
{
    public class ParityPubSubObservableSubscription<TResponse> : RpcStreamingSubscriptionObservableHandler<TResponse>
    {
        private ParitySubscribeRequestBuilder _paritySubscribeRequestBuilder;

        public ParityPubSubObservableSubscription(IStreamingClient client) : base(client, new EthUnsubscribeRequestBuilder())
        {
            _paritySubscribeRequestBuilder = new ParitySubscribeRequestBuilder();
        }

        public Task SubscribeAsync(RpcRequest originalRequestToSubscribe, object id = null)
        {
            return base.SubscribeAsync(BuildRequest(originalRequestToSubscribe, id));
        }

        public RpcRequest BuildRequest(RpcRequest originalRequestToSubscribe, object id = null)
        {
            return _paritySubscribeRequestBuilder.BuildRequest(originalRequestToSubscribe, id);
        }
    }
}
