using System;
using Conflux.Hex.HexConvertors.Extensions;
using Conflux.JsonRpc.Client;
using Conflux.JsonRpc.Client.Streaming;

namespace Conflux.RPC.Eth.Subscriptions
{
    public class EthUnsubscribeRequestBuilder : RpcRequestBuilder, IUnsubscribeSubscriptionRpcRequestBuilder
    {
        public EthUnsubscribeRequestBuilder() : base(ApiMethods.cfx_unsubscribe.ToString())
        {
        }

        public RpcRequest BuildRequest(string subscriptionHash, object id = null)
        {
            if (id == null) id = Guid.NewGuid().ToString();
            return base.BuildRequest(id, subscriptionHash.EnsureHexPrefix());
        }
    }
}