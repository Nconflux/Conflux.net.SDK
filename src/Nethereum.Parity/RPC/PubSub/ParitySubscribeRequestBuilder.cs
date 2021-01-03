using System;
using Conflux.JsonRpc.Client;

namespace Conflux.Parity.RPC.PubSub
{
    public class ParitySubscribeRequestBuilder : RpcRequestBuilder
    {
        public ParitySubscribeRequestBuilder() : base(ApiMethods.parity_subscribe.ToString())
        {
        }

        public RpcRequest BuildRequest(RpcRequest originalRequestToSubscribe, object id = null)
        {
            if (id == null) id = Guid.NewGuid().ToString();
            return base.BuildRequest(id, originalRequestToSubscribe.Method, originalRequestToSubscribe.RawParameters);
        }
    }
}