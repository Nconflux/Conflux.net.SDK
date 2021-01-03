using System;
using System.Threading.Tasks;

namespace Conflux.JsonRpc.Client
{
    public class RpcRequestResponseHandler<TResponse> : IRpcRequestHandler
    {
        protected RpcRequestBuilder RpcRequestBuilder { get; }

        public RpcRequestResponseHandler(IClient client, string methodName)
        {
            RpcRequestBuilder = new RpcRequestBuilder(methodName);
            Client = client;
        }

        public string MethodName => RpcRequestBuilder.MethodName;

        public IClient Client { get; }

        protected Task<TResponse> SendRequestAsync(object id, params object[] paramList)
        {
            //refer https://conflux-chain.github.io/conflux-doc/json-rpc/#cfx_sendrawtransaction
            var request = BuildRequest(id, paramList);
            if(Client == null) throw new NullReferenceException("RpcRequestHandler Client is null");
            return Client.SendRequestAsync<TResponse>(request);
        }

        public RpcRequest BuildRequest(object id, params object[] paramList)
        {
            return RpcRequestBuilder.BuildRequest(id, paramList);
        }
    }
}