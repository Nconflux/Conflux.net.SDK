using System.Numerics;
using System.Threading.Tasks;

namespace Conflux.BlockchainProcessing.ProgressRepositories
{
    public interface IBlockProgressRepository
    {
        Task UpsertProgressAsync(BigInteger blockNumber);
        Task<BigInteger?> GetLastBlockNumberProcessedAsync();
    }
}