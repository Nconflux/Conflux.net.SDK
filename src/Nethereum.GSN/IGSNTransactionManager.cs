using System.Threading.Tasks;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.GSN
{
    public interface IGSNTransactionManager
    {
        Task<string> SendTransactionAsync(TransactionInput transactionInput);
    }
}