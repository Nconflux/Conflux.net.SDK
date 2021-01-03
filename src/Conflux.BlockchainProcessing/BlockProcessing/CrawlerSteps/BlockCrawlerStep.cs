using System.Numerics;
using System.Threading.Tasks;
using Conflux.Contracts.Services;
using Conflux.Hex.HexTypes;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.BlockchainProcessing.BlockProcessing.CrawlerSteps
{
    public class BlockCrawlerStep : CrawlerStep<BigInteger, BlockWithTransactions>
    {
        public BlockCrawlerStep(ICfxApiContractService ethApiContractService) : base(ethApiContractService)
        {

        }
        public override Task<BlockWithTransactions> GetStepDataAsync(BigInteger blockNumber)
        {
            return EthApi.Blocks.GetBlockWithTransactionsByNumber.SendRequestAsync(blockNumber.ToHexBigInteger());
        }
    }
}