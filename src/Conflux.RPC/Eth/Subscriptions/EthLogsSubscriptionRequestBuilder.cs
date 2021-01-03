using System;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.RPC.Eth.Subscriptions
{
    public class EthLogsSubscriptionRequestBuilder:RpcRequestBuilder
    {
        public EthLogsSubscriptionRequestBuilder() : base(ApiMethods.cfx_subscribe.ToString())
        {
        }

        public RpcRequest BuildRequest(NewFilterInput filterInput, object id)
        {
            if (id == null) id = Guid.NewGuid().ToString();
            return base.BuildRequest(id, "logs", filterInput);
        }
    }
}