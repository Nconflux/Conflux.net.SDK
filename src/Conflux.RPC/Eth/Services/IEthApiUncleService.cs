using Conflux.RPC.Eth.Uncles;

namespace Conflux.RPC.Eth.Services
{
    public interface IEthApiUncleService
    {
        IEthGetUncleByBlockHashAndIndex GetUncleByBlockHashAndIndex { get; }
        IEthGetUncleByBlockNumberAndIndex GetUncleByBlockNumberAndIndex { get; }
        IEthGetUncleCountByBlockHash GetUncleCountByBlockHash { get; }
        IEthGetUncleCountByBlockNumber GetUncleCountByBlockNumber { get; }
    }
}