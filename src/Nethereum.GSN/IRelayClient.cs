using System;
using System.Threading.Tasks;
using Conflux.GSN.Models;

namespace Conflux.GSN
{
    public interface IRelayClient
    {
        Task<GetAddrResponse> GetAddrAsync(Uri relayUrl);
        Task<RelayResponse> RelayAsync(Uri relayUrl, RelayRequest request);
    }
}