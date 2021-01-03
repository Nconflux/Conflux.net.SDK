using Conflux.Parity.RPC.Accounts;

namespace Conflux.Parity
{
    public interface IAccountsApiService
    {
        IParityAccountsInfo AccountsInfo { get; }
        IParityDefaultAccount DefaultAccount { get; }
        IParityGenerateSecretPhrase GenerateSecretPhrase { get; }
        IParityHardwareAccountsInfo HardwareAccountsInfo { get; }
    }
}