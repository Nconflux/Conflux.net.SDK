using System.Threading.Tasks;
using Conflux.BlockchainProcessing.BlockStorage.Entities;
using Conflux.Hex.HexTypes;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.BlockchainProcessing.BlockStorage.Repositories
{
    public interface IAddressTransactionRepository
    {
        Task UpsertAsync(
            TransactionReceiptVO transactionReceiptVO, string address, string error = null, 
            string newContractAddress = null);

        Task<IAddressTransactionView> FindAsync(
            string address, HexBigInteger blockNumber, string transactionHash);
    }
}