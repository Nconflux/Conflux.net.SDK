using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client.Streaming;
using Conflux.RPC.Eth.Blocks;
using Conflux.RPC.Reactive.RpcStreaming;

namespace Conflux.RPC.Reactive.Eth
{
    public class EthBlockNumberObservableHandler : RpcStreamingResponseNoParamsObservableHandler<HexBigInteger, EthBlockNumber>
    {
        public EthBlockNumberObservableHandler(IStreamingClient streamingClient) : base(streamingClient, new EthBlockNumber(null))
        {

        }
    }
}
