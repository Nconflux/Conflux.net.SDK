using System.Threading.Tasks;

namespace Conflux.Contracts.Services
{
    public interface IEthGetContractTransactionErrorReason
    {
#if !DOTNET35
        Task<string> SendRequestAsync(string transactionHash);
#endif
    }
}