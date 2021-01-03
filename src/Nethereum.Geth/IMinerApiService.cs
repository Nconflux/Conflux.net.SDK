using Conflux.Geth.RPC.Miner;

namespace Conflux.Geth
{
    public interface IMinerApiService
    {
        IMinerSetGasPrice SetGasPrice { get; }
        IMinerStart Start { get; }
        IMinerStop Stop { get; }
    }
}