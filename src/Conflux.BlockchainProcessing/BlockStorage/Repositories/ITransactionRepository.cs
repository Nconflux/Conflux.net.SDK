using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.BlockchainProcessing.BlockStorage.Repositories
{
    public interface ITransactionRepository
    {
        Task UpsertAsync(TransactionReceiptVO transactionReceiptVO, string code, bool failedCreatingContract);

        Task UpsertAsync(TransactionReceiptVO transactionReceiptVO);

        Task<Entities.ITransactionView> FindByBlockNumberAndHashAsync(HexBigInteger blockNumber, string hash);
    }
}