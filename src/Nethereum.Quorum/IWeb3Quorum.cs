using System.Collections.Generic;
using Conflux.Quorum.RPC.Services;
using Conflux.Web3;

namespace Conflux.Quorum
{
    public interface IWeb3Quorum: IWeb3
    {
        List<string> PrivateFor { get; }
        string PrivateFrom { get; }
        IQuorumChainService Quorum { get; }

        void ClearPrivateForRequestParameters();
        void SetPrivateRequestParameters(IEnumerable<string> privateFor, string privateFrom = null);
    }
}