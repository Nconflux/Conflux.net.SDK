using Conflux.Hex.HexConvertors.Extensions;
using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Conflux.JsonRpc.WebSocketStreamingClient;

namespace Conflux.RPC.Eth.Subscriptions
{
    public class EthSyncingSubscriptionRequestBuilder : RpcRequestBuilder
    {
        public EthSyncingSubscriptionRequestBuilder() : base(ApiMethods.cfx_subscribe.ToString())
        {
        }

        public override RpcRequest BuildRequest(object id = null)
        {
            if (id == null) id = Guid.NewGuid().ToString();
            return base.BuildRequest(id, "syncing");
        }
    }
}
