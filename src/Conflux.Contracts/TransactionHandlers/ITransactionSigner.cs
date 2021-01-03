using System.Threading.Tasks;

namespace Conflux.Contracts.TransactionHandlers
{
    public interface ITransactionSigner<TFunctionMessage> where TFunctionMessage : FunctionMessage, new()
    {
        Task<string> SignTransactionAsync(string contractAddress, TFunctionMessage functionMessage = null);
    }
}