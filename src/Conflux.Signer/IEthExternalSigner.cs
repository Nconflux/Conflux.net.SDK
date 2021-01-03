using System.Threading.Tasks;
using Conflux.Signer.Crypto;

namespace Conflux.Signer
{
#if !DOTNET35

    public enum ExternalSignerTransactionFormat
    {
        RLP,
        Hash,
        Transaction
    }
    
#endif
}