using Conflux.Web3;

namespace Conflux.Geth
{
    public interface IWeb3Geth: IWeb3
    {
        IAdminApiService Admin { get; }
        IDebugApiService Debug { get; }
        IGethEthApiService GethEth { get; }
        IMinerApiService Miner { get; }
    }
}