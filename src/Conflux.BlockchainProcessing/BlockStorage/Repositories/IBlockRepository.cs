using System.Threading.Tasks;
using Conflux.BlockchainProcessing.BlockStorage.Entities;
using Conflux.Hex.HexTypes;

namespace Conflux.BlockchainProcessing.BlockStorage.Repositories
{
    public interface IBlockRepository
    {
        Task UpsertBlockAsync(Conflux.RPC.Eth.DTOs.Block source);
        Task<IBlockView> FindByBlockNumberAsync(HexBigInteger blockNumber);
    }
}