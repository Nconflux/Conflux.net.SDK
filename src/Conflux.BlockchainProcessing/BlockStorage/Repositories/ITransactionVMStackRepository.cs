using System.Threading.Tasks;
using Conflux.BlockchainProcessing.BlockStorage.Entities;
using Newtonsoft.Json.Linq;

namespace Conflux.BlockchainProcessing.BlockStorage.Repositories
{
    public interface ITransactionVMStackRepository
    { 
        Task UpsertAsync(string transactionHash, string address, JObject stackTrace);
        Task<ITransactionVmStackView> FindByTransactionHashAync(string hash);
        Task<ITransactionVmStackView> FindByAddressAndTransactionHashAync(string address, string hash);
    }
}