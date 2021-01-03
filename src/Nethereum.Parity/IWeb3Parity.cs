using Conflux.Web3;

namespace Conflux.Parity
{
    public interface IWeb3Parity: IWeb3
    {
        IAccountsApiService Accounts { get; }
        IAdminApiService Admin { get; }
        IBlockAuthoringApiService BlockAuthoring { get; }
        INetworkApiService Network { get; }
        ITraceApiService Trace { get; }
    }
}