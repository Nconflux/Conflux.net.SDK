using System;
using Conflux.JsonRpc.Client;

namespace Conflux.RPC.Eth.Subscriptions
{
    public class EthNewBlockHeadersSubscriptionRequestBuilder : RpcRequestBuilder
    {
        public EthNewBlockHeadersSubscriptionRequestBuilder() : base(ApiMethods.cfx_subscribe.ToString())
        {
        }

        public override RpcRequest BuildRequest(object id = null)
        {
            if (id == null) id = Guid.NewGuid().ToString();
            return base.BuildRequest(id, "newHeads");
        }
    }
}