using System;
using Conflux.JsonRpc.Client.RpcMessages;

namespace Conflux.JsonRpc.Client.Streaming
{
    public interface IRpcStreamingResponseHandler
    {
        void HandleResponse(RpcStreamingResponseMessage rpcStreamingResponse);
        void HandleClientError(Exception ex);
        void HandleClientDisconnection();
    }
}