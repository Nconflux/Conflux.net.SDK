using Conflux.RPC.Eth.Mining;

namespace Conflux.RPC.Eth.Services
{
    public interface IEthApiMiningService
    {
        IEthGetWork GetWork { get; }
        IEthHashrate Hashrate { get; }
        IEthMining IsMining { get; }
        IEthSubmitHashrate SubmitHashrate { get; }
        IEthSubmitWork SubmitWork { get; }
    }
}