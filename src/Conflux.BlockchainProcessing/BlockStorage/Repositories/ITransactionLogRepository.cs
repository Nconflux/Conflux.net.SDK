using System.Numerics;
using System.Threading.Tasks;
using Conflux.BlockchainProcessing.BlockStorage.Entities;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.BlockchainProcessing.BlockStorage.Repositories
{
    public interface ITransactionLogRepository
    {
        Task UpsertAsync(FilterLogVO log);
        Task<ITransactionLogView> FindByTransactionHashAndLogIndexAsync(string hash, BigInteger logIndex);
    }
}