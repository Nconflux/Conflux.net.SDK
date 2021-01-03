using Conflux.RPC.Eth;
using Conflux.RPC.Eth.DTOs;
using Conflux.RPC.Eth.Services;
using Conflux.RPC.TransactionManagers;

namespace Conflux.RPC
{
    public interface IEthApiService
    {
        IEthChainId ChainId { get; }
        IEthAccounts Accounts { get; }
        IEthApiBlockService Blocks { get; }
        IEthCoinBase CoinBase { get; }
        IEthApiCompilerService Compile { get; }
        BlockParameter DefaultBlock { get; set; }
        IEthApiFilterService Filters { get; }
        IEthGasPrice GasPrice { get; }
        IEthGetBalance GetBalance { get; }
        IEthGetEpochNumber GetEpochNumber { get; }
        IEthGetNextNonce GetNextNonce { get; }
        IEthGetCode GetCode { get; }
        IEthGetStorageAt GetStorageAt { get; }
        IEthApiMiningService Mining { get; }
        IEthProtocolVersion ProtocolVersion { get; }
        IEthSign Sign { get; }
        IEthSyncing Syncing { get; }
        ITransactionManager TransactionManager { get; set; }
        IEthApiTransactionsService Transactions { get; }
        IEthApiUncleService Uncles { get; }
#if !DOTNET35
        IEtherTransferService GetEtherTransferService();
#endif
    }
}