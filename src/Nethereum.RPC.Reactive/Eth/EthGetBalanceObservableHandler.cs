using System;
using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client.Streaming;
using Conflux.RPC.Eth;
using Conflux.RPC.Eth.DTOs;
using Conflux.RPC.Reactive.RpcStreaming;

namespace Conflux.RPC.Reactive.Eth
{
    public class EthGetBalanceObservableHandler : RpcStreamingResponseParamsObservableHandler<HexBigInteger, EthGetBalance>
    {
        public EthGetBalanceObservableHandler(IStreamingClient streamingClient) : base(streamingClient, new EthGetBalance(null))
        {

        }

        public Task SendRequestAsync(string address, BlockParameter block, object id = null)
        {
            if (id == null) id = Guid.NewGuid().ToString();
            var request = RpcRequestResponseHandler.BuildRequest(address, block, id);
            return SendRequestAsync(request);
        }
    }
}
