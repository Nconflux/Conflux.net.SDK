using Conflux.RPC.Eth.DTOs;

namespace Conflux.Contracts
{
    public interface IEventLog
    {
        FilterLog Log { get; }
    }
}