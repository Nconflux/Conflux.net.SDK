using Conflux.BlockchainProcessing.Services;
using Conflux.Contracts.Services;
using Conflux.JsonRpc.Client;
using Conflux.RPC;
using Conflux.RPC.TransactionManagers;

namespace Conflux.Web3
{
    public interface IWeb3
    {
        IClient Client { get; }
        ICfxApiContractService Cfx { get; }
        IBlockchainProcessingService Processing { get; }
        INetApiService Net { get; }
        IPersonalApiService Personal { get; }
        IShhApiService Shh { get; }
        ITransactionManager TransactionManager { get; set; }
    }
}