using System;
using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Conflux.JsonRpc.Client.Streaming;

namespace Conflux.RPC.Reactive.RpcStreaming
{
    public abstract class RpcStreamingResponseNoParamsObservableHandler<TResponse, TRpcRequestResponseHandler> : RpcStreamingResponseObservableHandler<TResponse>
       where TRpcRequestResponseHandler : RpcRequestResponseHandlerNoParam<TResponse>
    {
        protected TRpcRequestResponseHandler RpcRequestResponseHandler { get; }

        protected RpcStreamingResponseNoParamsObservableHandler(IStreamingClient streamingClient, TRpcRequestResponseHandler rpcRequestResponseHandler) : base(streamingClient)
        {
            RpcRequestResponseHandler = rpcRequestResponseHandler;
        }

        public Task SendRequestAsync(object id = null)
        {
            if (id == null) id = Guid.NewGuid().ToString();
            var request = RpcRequestResponseHandler.BuildRequest(id);
            return base.SendRequestAsync(request);
        }
    }
}
