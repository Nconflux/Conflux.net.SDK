using Conflux.JsonRpc.Client;
using Conflux.JsonRpc.Client.Streaming;

namespace Conflux.RPC.Reactive.RpcStreaming
{
    public abstract class RpcStreamingResponseParamsObservableHandler<TResponse, TRpcRequestResponseHandler> : RpcStreamingResponseObservableHandler<TResponse>
        where TRpcRequestResponseHandler : RpcRequestResponseHandler<TResponse>
    {
        protected TRpcRequestResponseHandler RpcRequestResponseHandler { get; }

        protected RpcStreamingResponseParamsObservableHandler(IStreamingClient streamingClient, TRpcRequestResponseHandler rpcRequestResponseHandler):base(streamingClient)
        {
            RpcRequestResponseHandler = rpcRequestResponseHandler;
        }
    }
}
