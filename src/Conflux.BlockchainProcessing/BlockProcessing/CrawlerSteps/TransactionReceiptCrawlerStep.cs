using System.Threading.Tasks;
using Conflux.Contracts.Services;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.BlockchainProcessing.BlockProcessing.CrawlerSteps
{
    public class TransactionReceiptCrawlerStep : CrawlerStep<TransactionVO, TransactionReceiptVO>
    {
        public TransactionReceiptCrawlerStep(ICfxApiContractService ethApiContractService) : base(ethApiContractService)
        {
        }

        public override async Task<TransactionReceiptVO> GetStepDataAsync(TransactionVO transactionVO)
        {
            var receipt = await EthApi.Transactions
                .GetTransactionReceipt.SendRequestAsync(transactionVO.Transaction.TransactionHash)
                .ConfigureAwait(false);
            return new TransactionReceiptVO(transactionVO.Block, transactionVO.Transaction, receipt, receipt.HasErrors()?? false);
        }
    }
}